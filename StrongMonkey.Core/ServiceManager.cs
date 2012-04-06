using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Mono.Addins;

namespace StrongMonkey.Core
{
	public sealed class ServiceManager
	{
		/* static ctor */
		static ServiceManager ()
		{
		}

		/* public properties */
		public static IService[] Services {
			get { return services.ToArray (); }
		}

		/* public methods */
		public static IService GetService (Type type)
		{
			return ServiceForType (type, true);
		}

		public static void RegisterService (IService service)
		{
			services.Add (service);
//			if (log_svc == null) {
//				ILogService new_log_svc = service as ILogService;
//				if (new_log_svc != null) {
//					log_svc = new_log_svc;
//				}
//			}
		}

		/*
		 * ** WARNING WARNING WARNING ***
		 *
		 * This isn't intended for general purpose use. This should
		 * only be used for *testing*
		 *
		 * ** WARNING WARNING WARNING ***
		 */
		public static void UnregisterService (IService service)
		{
			services.Remove (service);
			// XXX: Don't invalidate this whole thing?
			service_map.Clear ();
		}

		public static void BindFields (object obj, Type type)
		{
			if (obj == null || type == null) {
				throw new ArgumentNullException ();
			}

			if (!type.IsInstanceOfType (obj)) {
				throw new ArgumentException ();
			}
			
			ServiceDependencyAttribute[] dep_attrs
			    = (ServiceDependencyAttribute[])
			         type.GetCustomAttributes (typeof (ServiceDependencyAttribute), false);

			foreach (FieldInfo fi in
			         type.GetFields (BindingFlags.Public |
				                 BindingFlags.NonPublic |
				                 BindingFlags.DeclaredOnly |
			                         BindingFlags.Instance))
			{
				object[] ref_attrs
				    = fi.GetCustomAttributes (
				             typeof (ServiceReferenceAttribute),
					     false);

				if (ref_attrs == null ||
				    ref_attrs.Length == 0) {
					continue;
				}

				Type t = fi.FieldType;

				ServiceReferenceAttribute ref_attr
				    = (ServiceReferenceAttribute)ref_attrs[0];
				if (ref_attr.Type != null) {
					t = ref_attr.Type;
				}
				
				if (typeof (IService).IsAssignableFrom (type)) {
					bool found_dep = false;
					foreach (ServiceDependencyAttribute dep_attr in dep_attrs)
					{
						// As long as we find a dependency for the type we want,
						// and either
						//
						// 1) The reference *is* optional, or
						// 2) Our dependency *isn't*
						// 
						// then we're ok.
						if (t.IsAssignableFrom (dep_attr.Type)) {
							if (ref_attr.Optional || !dep_attr.Optional) {
								found_dep = true;
								break;
							}
						}
					}
					
					if (!found_dep) {
						throw new MissingServiceDependencyAttributeException (t.FullName, type.FullName, fi.Name);
					}
				}

				if (!typeof (IService).IsAssignableFrom (t)) {
					Log ("Can't bind non-service field '{0}' of type '{1}' in '{2}'.", fi.Name, t, obj);
					throw new ArgumentException ();
				}

				IService svc_field_val = GetService (t);

				if (svc_field_val == null &&
				    !ref_attr.Optional) {
					string msg = String.Format ("Service '{0}' not available to bind to '{1}' in '{2}'.", t, fi.Name, obj);
					Log (msg);
					throw new DependencyResolutionException (msg);
				}

				fi.SetValue (obj, svc_field_val);
			}
		}

		public static void RegisterDynamicServices ()
		{
			ExtensionNode node = AddinManager.GetExtensionNode ("/Core/Services");
			if (node == null) {
				return;
			}

			// First, make a hash of services, and the interfaces
			// they provide so we can do type lookup

			// At the same time, build a list of all the services
			// for iteration purposes
			int count = node.ChildNodes.Count;
			List<Type> services_list = new List<Type> (count);
			for (int i = 0; i < count; i++)
			{
				TypeExtensionNode child = node.ChildNodes[i] as TypeExtensionNode;
				if (child == null) {
					continue;
				}

				Type t = child.Type;
				services_list.Add (t);

				foreach (Type intrface in t.GetInterfaces ())
				{
					string i_name = intrface.ToString ();
					if (intrface.GetInterface (typeof (IService).ToString ()) == null) {
						continue;
					}

					if (!interface_provs.ContainsKey (i_name)) {
						interface_provs.Add (i_name, t);
						continue;
					}

					object o = interface_provs[i_name];
					ArrayList collisions = null;
					if (o is ArrayList) {
						collisions = (ArrayList)o;
					} else if (o is Type) {
						collisions = new ArrayList ();
					}

					collisions.Add ((Type)o);
					collisions.Add (t);
					interface_provs[i_name] = collisions;
					continue;
				}
			}


			// Iterate through all the services, chaining up it's
			// dependency tree until we hit a node with no
			// dependencies, load it, and chain back up
			foreach (Type t in services_list)
			{
				LazyServiceAttribute[] lazy_attrs = (LazyServiceAttribute[])t.GetCustomAttributes (typeof (LazyServiceAttribute), false);
				if (lazy_attrs != null && lazy_attrs.Length > 0) {
					lazy_service_types.Add (t);
					continue;
				}
				LoadServiceAndDependencies (t);
			}
		}
		
		/* private fields */
		// private static ILogService log_svc;
		private static List<IService> services = new List<IService> ();
		private static List<Type> lazy_service_types = new List<Type> ();
		private static IDictionary<Type, IService> service_map = new Dictionary<Type, IService> ();

		private static Hashtable interface_provs = new Hashtable ();
		private static IDictionary<Type, bool> seen_list = new Dictionary<Type, bool> ();

		/* private methods */
		// Private constructor until we can make this a static class.
		private ServiceManager ()
		{
		}

		private static IService ServiceForType (Type type, bool load_lazy_if_needed)
		{
			if (service_map.ContainsKey (type)) {
				return service_map[type];
			}

			foreach (IService service in services)
			{
				if (type.IsInstanceOfType (service)) {
					service_map[type] = service;
					return service;
				}
			}

			if (load_lazy_if_needed) {
				foreach (Type candidate in lazy_service_types)
				{
					if (!type.IsAssignableFrom (candidate)) {
						continue;
					}

					if (!LoadServiceAndDependencies (candidate)) {
						return null;
					}

					return ServiceForType (candidate, false);
				}
			}

			return null;
		}

		private static bool LoadServiceAndDependencies (Type t)
		{
			// Check to see if t is already loaded
			if (ServiceForType (t, false) != null) {
				return true;
			}

			// ensure no cyclical dependencies
			if (seen_list.ContainsKey (t)) {
				throw new CyclicalDependencyException ();
			}

			seen_list[t] = true;

			// Find t's dependencies
			ServiceDependencyAttribute[] attrs;
			attrs = (ServiceDependencyAttribute[])t.GetCustomAttributes (
				typeof (ServiceDependencyAttribute), true);

			// Ensure that t's deps are loaded
			foreach (ServiceDependencyAttribute a in attrs)
			{
				Type dep = a.Type;
				if (!dep.IsAbstract) {
					LoadServiceAndDependencies (a.Type);
					continue;
				}

				// we can't initialize it, so see if a service provides this interface
				object o = interface_provs[dep.ToString ()];
				if (o == null && !a.Optional) {
					throw new DependencyResolutionException (String.Format ("Dependency load failed for {0}.  No service provides {1}.",
												t.ToString (), dep.ToString ()));

				}

				if (o is Type) {
					LoadServiceAndDependencies ((Type)o);
				} else if (o is ArrayList) {
					ArrayList providers = (ArrayList)o;
					
					// make sure that there is only
					// one *loadable* service which
					// provides this interface
					bool already_loaded_one = false;
					foreach (Type p in providers)
					{
						if (already_loaded_one) {
							throw new DependencyResolutionException (String.Format ("More than one service cannot provide {0}.", dep));
						}

						if (LoadServiceAndDependencies (p)) {
							already_loaded_one = true;
						}
					}

					if (!already_loaded_one) {
						throw new DependencyResolutionException (String.Format ("Dependency load failed for {0}.  No service provides {1}.",
													t.ToString (), dep.ToString ()));
					}
				}
			}

			// Load t
			IService service = (IService)Activator.CreateInstance (t);
			try {
				// We have to iterate the class hierarchy
				// to make sure the fields at each level
				// are assigned.
				Type service_type = service.GetType ();
				while (service_type != null &&
				       service_type != typeof (System.Object)) {
					BindFields (service, service_type);
					service_type = service_type.BaseType;
				}
			} catch (MissingServiceDependencyAttributeException e) {
				throw e;
			} catch (Exception) {
				Log ("Exception while binding fields for service: {0}", service);
				return false;
			}

			if (!service.Initialize ()) {
				Log ("Unable to initialize service: {0}", service);
				return false;
			}

			int idx = lazy_service_types.IndexOf (t);
			if (idx >= 0) {
				lazy_service_types.RemoveAt (idx);
			}

			RegisterService (service);
			return true;
		}
		
		private static void Log (string msg, params object[] args)
		{
//			if (log_svc != null) {
//				log_svc.Log ("Services: " + msg, args);
//			}
		}
	}

	public class CyclicalDependencyException : Exception
	{
		public CyclicalDependencyException () : base ("A cyclical service dependency was detected.")
		{
		}
	}

	public class DependencyResolutionException : Exception
	{
		public DependencyResolutionException (string msg) : base (msg)
		{
		}
	}
	
	class MissingServiceDependencyAttributeException : Exception
	{
		public MissingServiceDependencyAttributeException (string dep,
		                                                   string klass,
		                                                   string field)
		{
			this.dep = dep;
			this.klass = klass;
			this.field = field;
		}
		
		public override string ToString ()
		{
			return String.Format ("Missing dependency {0} on class {1} for field {2}", dep, klass, field);
		}

		private string dep = String.Empty;
		private string klass = String.Empty;
		private string field = String.Empty;
	}
}
