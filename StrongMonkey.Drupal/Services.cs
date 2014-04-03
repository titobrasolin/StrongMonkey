using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading;
using StrongMonkey.Core;
using CookComputing.XmlRpc;

using Mono.Unix;
using Mono.Addins;

namespace StrongMonkey.Drupal
{
	[Extension]
	public sealed class Services : DrupalConnectionBase, IDrupalConnection
	{
		#region Private Callbacks

		private AsyncCallback CommentCreateOperationCompleted;
		private AsyncCallback CommentRetrieveOperationCompleted;
		private AsyncCallback CommentUpdateOperationCompleted;
		private AsyncCallback CommentDeleteOperationCompleted;
		private AsyncCallback CommentIndexOperationCompleted;
		private AsyncCallback CommentCountAllOperationCompleted;
		private AsyncCallback CommentCountNewOperationCompleted;
		private AsyncCallback FileCreateOperationCompleted;
		private AsyncCallback FileRetrieveOperationCompleted;
		private AsyncCallback FileDeleteOperationCompleted;
		private AsyncCallback FileIndexOperationCompleted;
		private AsyncCallback FileCreateRawOperationCompleted;
		private AsyncCallback NodeRetrieveOperationCompleted;
		private AsyncCallback NodeCreateOperationCompleted;
		private AsyncCallback NodeUpdateOperationCompleted;
		private AsyncCallback NodeDeleteOperationCompleted;
		private AsyncCallback NodeIndexOperationCompleted;
		private AsyncCallback NodeFilesOperationCompleted;
		private AsyncCallback SystemConnectOperationCompleted;
		private AsyncCallback SystemGetVariableOperationCompleted;
		private AsyncCallback SystemSetVariableOperationCompleted;
		private AsyncCallback SystemDelVariableOperationCompleted;
		private AsyncCallback TaxonomyTermRetrieveOperationCompleted;
		private AsyncCallback TaxonomyTermCreateOperationCompleted;
		private AsyncCallback TaxonomyTermUpdateOperationCompleted;
		private AsyncCallback TaxonomyTermDeleteOperationCompleted;
		private AsyncCallback TaxonomyTermIndexOperationCompleted;
		private AsyncCallback TaxonomyTermSelectNodesOperationCompleted;
		private AsyncCallback TaxonomyVocabularyRetrieveOperationCompleted;
		private AsyncCallback TaxonomyVocabularyCreateOperationCompleted;
		private AsyncCallback TaxonomyVocabularyUpdateOperationCompleted;
		private AsyncCallback TaxonomyVocabularyDeleteOperationCompleted;
		private AsyncCallback TaxonomyVocabularyIndexOperationCompleted;
		private AsyncCallback TaxonomyVocabularyGetTreeOperationCompleted;
		private AsyncCallback UserRetrieveOperationCompleted;
		private AsyncCallback UserCreateOperationCompleted;
		private AsyncCallback UserUpdateOperationCompleted;
		private AsyncCallback UserDeleteOperationCompleted;
		private AsyncCallback UserIndexOperationCompleted;
		private AsyncCallback UserLoginOperationCompleted;
		private AsyncCallback UserLogoutOperationCompleted;
		private AsyncCallback UserRegisterOperationCompleted;
		private AsyncCallback MenuRetrieveOperationCompleted;
		private AsyncCallback ViewsRetrieveOperationCompleted;
		private AsyncCallback DefinitionIndexOperationCompleted;
		private AsyncCallback GeocoderRetrieveOperationCompleted;
		private AsyncCallback GeocoderIndexOperationCompleted;

		#endregion
		
		#region Public Events

		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> CommentCreateCompleted;
		public event DrupalAsyncCompletedEventHandler<object> CommentRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<object> CommentUpdateCompleted;
		public event DrupalAsyncCompletedEventHandler<bool> CommentDeleteCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> CommentIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<int> CommentCountAllCompleted;
		public event DrupalAsyncCompletedEventHandler<int> CommentCountNewCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalFile> FileCreateCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalFile> FileRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<bool> FileDeleteCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalFile[]> FileIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalFile[]> FileCreateRawCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> NodeRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> NodeCreateCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> NodeUpdateCompleted;
		public event DrupalAsyncCompletedEventHandler<bool> NodeDeleteCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> NodeIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalFile[]> NodeFilesCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalSessionObject> SystemConnectCompleted;
		public event DrupalAsyncCompletedEventHandler<object> SystemGetVariableCompleted;
		// public event DrupalAsyncCompletedEventHandler<...> SystemSetVariableCompleted;
		// public event DrupalAsyncCompletedEventHandler<...> SystemDelVariableCompleted;
		

		public event DrupalAsyncCompletedEventHandler<object> TaxonomyTermRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<int> TaxonomyTermCreateCompleted;
		public event DrupalAsyncCompletedEventHandler<int> TaxonomyTermUpdateCompleted;
		public event DrupalAsyncCompletedEventHandler<int> TaxonomyTermDeleteCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyTermIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyTermSelectNodesCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> TaxonomyVocabularyRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<int> TaxonomyVocabularyCreateCompleted;
		public event DrupalAsyncCompletedEventHandler<int> TaxonomyVocabularyUpdateCompleted;
		public event DrupalAsyncCompletedEventHandler<int> TaxonomyVocabularyDeleteCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyVocabularyIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyVocabularyGetTreeCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalUser> UserRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalUser> UserCreateCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalUser> UserUpdateCompleted;
		public event DrupalAsyncCompletedEventHandler<bool> UserDeleteCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalUser[]> UserIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalSessionObject> UserLoginCompleted;
		public event DrupalAsyncCompletedEventHandler<bool> UserLogoutCompleted;
		public event DrupalAsyncCompletedEventHandler<DrupalUser> UserRegisterCompleted;
		public event DrupalAsyncCompletedEventHandler<object> MenuRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> ViewsRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> DefinitionIndexCompleted;
		public event DrupalAsyncCompletedEventHandler<string> GeocoderRetrieveCompleted;
		public event DrupalAsyncCompletedEventHandler<XmlRpcStruct> GeocoderIndexCompleted;

		#endregion

		#region Constructor(s)

		public Services ()
			: this (ServiceSettings.Default.DrupalURL, ServiceSettings.Default.EndPoint, ServiceSettings.Default.CleanURL)
		{
		}
				
		public Services (string drupalUrl, string endPoint, bool cleanUrl)
		{
			_cleanUrl = cleanUrl;
			_drupalURL = drupalUrl;
			_endPoint = endPoint;
		}

		#endregion

		readonly string _loggerName = MethodBase.GetCurrentMethod ().DeclaringType.Name;
		bool _cleanUrl;

		public bool CleanUrl {
			get { return _cleanUrl; }
			set { _cleanUrl = value;
				SetServiceSystemUrl (); }
		}

		string _drupalURL;

		public string DrupalURL {
			get { return _drupalURL; }
			set { _drupalURL = value;
				SetServiceSystemUrl (); }
		}

		string _endPoint;

		public string EndPoint {
			get { return _endPoint; }
			set { _endPoint = value;
				SetServiceSystemUrl (); }
		}

		DrupalSessionObject _sessionData;

		public DrupalSessionObject SessionData {
			get { return _sessionData; }
		}
		
		bool _isLoggedIn;

		public bool IsLoggedIn {
			get { return _isLoggedIn; }
		}

		int _errorCode = 0;

		public int ErrorCode { 
			get { return _errorCode; }
		}

		string _errorMessage = "";

		public string ErrorMessage { 
			get { return _errorMessage; }
		}
		
		private readonly object _serviceSystemLock = new object ();
		private volatile IServiceSystem _serviceSystem;

		private IServiceSystem ServiceSystem {
			get {
				if (_serviceSystem == null) {
					lock (_serviceSystemLock) {
						if (_serviceSystem == null) {
							_serviceSystem = XmlRpcProxyGen.Create<IServiceSystem> ();
						}
					}
				}
				SetServiceSystemUrl ();
				return _serviceSystem;
			}
		}
		
		void SetServiceSystemUrl ()
		{
			if (_cleanUrl) {
				_serviceSystem.Url = _drupalURL + "/" + _endPoint;
			} else {
				_serviceSystem.Url = _drupalURL + "?q=" + _endPoint;
			}
		}
		
//		private void HandleException (Exception ex)
//		{
//			// get calling method name
//			HandleException (ex, new StackTrace ().GetFrame (1).GetMethod ().Name);
//		}

		private void HandleException (Exception ex, string functionName)
		{
			if (ex is XmlRpcFaultException) {
				_errorCode = (ex as XmlRpcFaultException).FaultCode;
				_errorMessage = (ex as XmlRpcFaultException).Message;
			} else {
				_errorCode = 0;
				_errorMessage = ex.Message;
			}
			SendErrorLogEvent (functionName + " - " + ex.Message, _loggerName);
		}
		
		private void ClearErrors ()
		{
			_errorCode = 0;
			_errorMessage = "";
		}

		private string _password = "";
		private string _username = "";

		public bool ReLogin ()
		{
			return Login (_username, _password);
		}

		public bool Login (string username, string password)
		{
			ClearErrors ();
			try {
				_password = password;
				_username = username;

				_sessionData = ServiceSystem.UserLogin (username, password);
				if (_sessionData.user.name == username) {
					_isLoggedIn = true;
				} else {
					_isLoggedIn = false;
					HandleException (new Exception (Catalog.GetString ("Unable to login")), "Login");
				}
			} catch (Exception ex) {
				HandleException (ex, "Login");
				_isLoggedIn = false;
			}
			return _isLoggedIn;
		}

		#region Flag
		
		public bool FlagIsFlagged (string flag_name, int content_id, int uid)
		{
			ClearErrors ();
			bool result = false;
			try {
				result = ServiceSystem.FlagIsFlagged (flag_name, content_id, uid);
			} catch (Exception ex) {
				HandleException (ex, "FlagIsFlagged");
			}
			return result;
		}
		
		public void FlagIsFlaggedAsync (string flag_name, int content_id, int uid, object asyncState)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Comment

		public XmlRpcStruct CommentCreate (XmlRpcStruct comment)
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.CommentCreate (comment);
			} catch (Exception ex) {
				HandleException (ex, "CommentCreate");
			}
			return res;
		}

		public void CommentCreateAsync (XmlRpcStruct comment, object asyncState)
		{
			if (this.CommentCreateOperationCompleted == null) {
				this.CommentCreateOperationCompleted = new AsyncCallback (this.OnCommentCreateCompleted);
			}
			ServiceSystem.BeginCommentCreate (comment, this.CommentCreateOperationCompleted, asyncState);
		}

		void OnCommentCreateCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentCreateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentCreate (asyncResult);
					this.CommentCreateCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentCreateCompleted");
					this.CommentCreateCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public object CommentRetrieve (int cid)
		{
			ClearErrors ();
			object res = null;
			try {
				res = ServiceSystem.CommentRetrieve (cid);
			} catch (Exception ex) {
				HandleException (ex, "CommentRetrieve");
			}
			return res;
		}

		public void CommentRetrieveAsync (int cid, object asyncState)
		{
			if (this.CommentRetrieveOperationCompleted == null) {
				this.CommentRetrieveOperationCompleted = new AsyncCallback (this.OnCommentRetrieveCompleted);
			}
			ServiceSystem.BeginCommentRetrieve (cid, this.CommentRetrieveOperationCompleted, asyncState);
		}
		
		void OnCommentRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				object result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentRetrieve (asyncResult);
					this.CommentRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentRetrieveCompleted");
					this.CommentRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public object CommentUpdate (int cid, XmlRpcStruct comment)
		{
			ClearErrors ();
			object res = null;
			try {
				res = ServiceSystem.CommentUpdate (cid, comment);
			} catch (Exception ex) {
				HandleException (ex, "CommentUpdate");
			}
			return res;
		}

		public void CommentUpdateAsync (int cid, XmlRpcStruct comment, object asyncState)
		{
			if (this.CommentUpdateOperationCompleted == null) {
				this.CommentUpdateOperationCompleted = new AsyncCallback (this.OnCommentUpdateCompleted);
			}
			ServiceSystem.BeginCommentUpdate (cid, comment, this.CommentUpdateOperationCompleted, asyncState);
		}

		void OnCommentUpdateCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentUpdateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				object result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentUpdate (asyncResult);
					this.CommentUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentUpdateCompleted");
					this.CommentUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public bool CommentDelete (int cid)
		{
			ClearErrors ();
			bool res = false;
			try {
				res = ServiceSystem.CommentDelete (cid);
			} catch (Exception ex) {
				HandleException (ex, "CommentDelete");
			}
			return res;
		}

		public void CommentDeleteAsync (int cid, object asyncState)
		{
			if (this.CommentDeleteOperationCompleted == null) {
				this.CommentDeleteOperationCompleted = new AsyncCallback (this.OnCommentDeleteCompleted);
			}
			ServiceSystem.BeginCommentDelete (cid, this.CommentDeleteOperationCompleted, asyncState);
		}

		void OnCommentDeleteCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentDeleteCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				bool result = false;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentDelete (asyncResult);
					this.CommentDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentDeleteCompleted");
					this.CommentDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct[] CommentIndex (int page, string fields, XmlRpcStruct[] parameters, int page_size)
		{
			ClearErrors ();
			XmlRpcStruct[] res = null;
			try {
				res = ServiceSystem.CommentIndex (page, fields, parameters, page_size);
			} catch (Exception ex) {
				HandleException (ex, "CommentIndex");
			}
			return res;
		}

		public void CommentIndexAsync (int page, string fields, XmlRpcStruct[] parameters, int page_size, object asyncState)
		{
			if (this.CommentIndexOperationCompleted == null) {
				this.CommentIndexOperationCompleted = new AsyncCallback (this.OnCommentIndexCompleted);
			}
			ServiceSystem.BeginCommentIndex (page, fields, parameters, page_size, this.CommentIndexOperationCompleted, asyncState);
		}

		void OnCommentIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentIndex (asyncResult);
					this.CommentIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentIndexCompleted");
					this.CommentIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int CommentCountAll (int nid)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.CommentCountAll (nid);
			} catch (Exception ex) {
				HandleException (ex, "CommentCountAll");
			}
			return res;
		}

		public void CommentCountAllAsync (int nid, object asyncState)
		{
			if (this.CommentCountAllOperationCompleted == null) {
				this.CommentCountAllOperationCompleted = new AsyncCallback (this.OnCommentCountAllCompleted);
			}
			ServiceSystem.BeginCommentCountAll (nid, this.CommentCountAllOperationCompleted, asyncState);
		}

		void OnCommentCountAllCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentCountAllCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentCountAll (asyncResult);
					this.CommentCountAllCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentCountAllCompleted");
					this.CommentCountAllCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int CommentCountNew (int nid, int timestamp)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.CommentCountNew (nid, timestamp);
			} catch (Exception ex) {
				HandleException (ex, "CommentCountNew");
			}
			return res;
		}

		public void CommentCountNewAsync (int nid, int timestamp, object asyncState)
		{
			if (this.CommentCountNewOperationCompleted == null) {
				this.CommentCountNewOperationCompleted = new AsyncCallback (this.OnCommentCountNewCompleted);
			}
			ServiceSystem.BeginCommentCountNew (nid, timestamp, this.CommentCountNewOperationCompleted, asyncState);
		}

		void OnCommentCountNewCompleted (IAsyncResult asyncResult)
		{
			if (this.CommentCountNewCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndCommentCountNew (asyncResult);
					this.CommentCountNewCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnCommentCountNewCompleted");
					this.CommentCountNewCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion

		#region File

		public DrupalFile FileCreate (DrupalFile file)
		{
			ClearErrors ();
			DrupalFile res = default(DrupalFile);
			try {
				res = ServiceSystem.FileCreate (file);
			} catch (Exception ex) {
				HandleException (ex, "FileCreate");
			}
			return res;
		}

		public void FileCreateAsync (DrupalFile file, object asyncState)
		{
			if (this.FileCreateOperationCompleted == null) {
				this.FileCreateOperationCompleted = new AsyncCallback (this.OnFileCreateCompleted);
			}
			ServiceSystem.BeginFileCreate (file, this.FileCreateOperationCompleted, asyncState);
		}

		void OnFileCreateCompleted (IAsyncResult asyncResult)
		{
			if (this.FileCreateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalFile result = default(DrupalFile);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndFileCreate (asyncResult);
					this.FileCreateCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnFileCreateCompleted");
					this.FileCreateCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalFile FileRetrieve (int fid, bool include_file_contents, bool get_image_style)
		{
			ClearErrors ();
			DrupalFile res = default(DrupalFile);
			try {
				res = ServiceSystem.FileRetrieve (fid, include_file_contents, get_image_style);
			} catch (Exception ex) {
				HandleException (ex, "FileRetrieve");
			}
			return res;
		}

		public void FileRetrieveAsync (int fid, bool include_file_contents, bool get_image_style, object asyncState)
		{
			if (this.FileRetrieveOperationCompleted == null) {
				this.FileRetrieveOperationCompleted = new AsyncCallback (this.OnFileRetrieveCompleted);
			}
			ServiceSystem.BeginFileRetrieve (fid, include_file_contents, get_image_style, this.FileRetrieveOperationCompleted, asyncState);
		}

		private void OnFileRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.FileRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalFile result = default(DrupalFile);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndFileRetrieve (asyncResult);
					this.FileRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnFileRetrieveCompleted");
					this.FileRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public bool FileDelete (int fid)
		{
			ClearErrors ();
			bool res = false;
			try {
				res = ServiceSystem.FileDelete (fid);
			} catch (Exception ex) {
				HandleException (ex, "FileDelete");
			}
			return res;
		}

		public void FileDeleteAsync (int fid, object asyncState)
		{
			if (this.FileDeleteOperationCompleted == null) {
				this.FileDeleteOperationCompleted = new AsyncCallback (this.OnFileDeleteCompleted);
			}
			ServiceSystem.BeginFileDelete (fid, this.FileDeleteOperationCompleted, asyncState);
		}

		void OnFileDeleteCompleted (IAsyncResult asyncResult)
		{
			if (this.FileDeleteCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				bool result = false;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndFileDelete (asyncResult);
					this.FileDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnFileDeleteCompleted");
					this.FileDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalFile[] FileIndex (int page, string fields, XmlRpcStruct parameters, int page_size)
		{
			ClearErrors ();
			DrupalFile[] res = null;
			try {
				res = ServiceSystem.FileIndex (page, fields, parameters, page_size);
			} catch (Exception ex) {
				HandleException (ex, "FileIndex");
			}
			return res;
		}

		public void FileIndexAsync (int page, string fields, XmlRpcStruct parameters, int page_size, object asyncState)
		{
			if (this.FileIndexOperationCompleted == null) {
				this.FileIndexOperationCompleted = new AsyncCallback (this.OnFileIndexCompleted);
			}
			ServiceSystem.BeginFileIndex (page, fields, parameters, page_size, this.FileIndexOperationCompleted, asyncState);
		}

		void OnFileIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.FileIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalFile[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndFileIndex (asyncResult);
					this.FileIndexCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnFileIndexCompleted");
					this.FileIndexCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalFile[] FileCreateRaw ()
		{
			ClearErrors ();
			DrupalFile[] res = null;
			try {
				res = ServiceSystem.FileCreateRaw ();
			} catch (Exception ex) {
				HandleException (ex, "FileCreateRaw");
			}
			return res;
		}

		public void FileCreateRawAsync (object asyncState)
		{
			if (this.FileCreateRawOperationCompleted == null) {
				this.FileCreateRawOperationCompleted = new AsyncCallback (this.OnFileCreateRawCompleted);
			}
			ServiceSystem.BeginFileCreateRaw (this.FileCreateRawOperationCompleted, asyncState);
		}

		void OnFileCreateRawCompleted (IAsyncResult asyncResult)
		{
			if (this.FileCreateRawCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalFile[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndFileCreateRaw (asyncResult);
					this.FileCreateRawCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnFileCreateRawCompleted");
					this.FileCreateRawCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion
		
		#region Node
		
		public XmlRpcStruct NodeRetrieve (int nid)
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.NodeRetrieve (nid);
			} catch (Exception ex) {
				HandleException (ex, "NodeRetrieve");
			}
			return res;
		}
		
		public void NodeRetrieveAsync (int nid, object asyncState)
		{
			if (this.NodeRetrieveOperationCompleted == null) {
				this.NodeRetrieveOperationCompleted = new AsyncCallback (this.OnNodeRetrieveCompleted);
			}
			ServiceSystem.BeginNodeRetrieve (nid, this.NodeRetrieveOperationCompleted, asyncState);
		}

		void OnNodeRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.NodeRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndNodeRetrieve (asyncResult);
					this.NodeRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnNodeRetrieveCompleted");
					this.NodeRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct NodeCreate (XmlRpcStruct node)
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.NodeCreate (node);
			} catch (Exception ex) {
				HandleException (ex, "NodeCreate");
			}
			return res;
		}

		public void NodeCreateAsync (XmlRpcStruct node, object asyncState)
		{
			if (this.NodeCreateOperationCompleted == null) {
				this.NodeCreateOperationCompleted = new AsyncCallback (this.OnNodeCreateCompleted);
			}
			ServiceSystem.BeginNodeCreate (node, this.NodeCreateOperationCompleted, asyncState);
		}

		void OnNodeCreateCompleted (IAsyncResult asyncResult)
		{
			if (this.NodeCreateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndNodeCreate (asyncResult);
					this.NodeCreateCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnNodeCreateCompleted");
					this.NodeCreateCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct NodeUpdate (int nid, XmlRpcStruct node)
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.NodeUpdate (nid, node);
			} catch (Exception ex) {
				HandleException (ex, "NodeUpdate");
			}
			return res;
		}

		public void NodeUpdateAsync (int nid, XmlRpcStruct node, object asyncState)
		{
			if (this.NodeUpdateOperationCompleted == null) {
				this.NodeUpdateOperationCompleted = new AsyncCallback (this.OnNodeUpdateCompleted);
			}
			ServiceSystem.BeginNodeUpdate (nid, node, this.NodeUpdateOperationCompleted, asyncState);
		}

		void OnNodeUpdateCompleted (IAsyncResult asyncResult)
		{
			if (this.NodeUpdateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndNodeUpdate (asyncResult);
					this.NodeUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnNodeUpdateCompleted");
					this.NodeUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public bool NodeDelete (int nid)
		{
			ClearErrors ();
			bool res = false;
			try {
				res = ServiceSystem.NodeDelete (nid);
			} catch (Exception ex) {
				HandleException (ex, "NodeDelete");
			}
			return res;
		}

		public void NodeDeleteAsync (int nid, object asyncState)
		{
			if (this.NodeDeleteOperationCompleted == null) {
				this.NodeDeleteOperationCompleted = new AsyncCallback (this.OnNodeDeleteCompleted);
			}
			ServiceSystem.BeginNodeDelete (nid, this.NodeUpdateOperationCompleted, asyncState);
		}

		void OnNodeDeleteCompleted (IAsyncResult asyncResult)
		{
			if (this.NodeDeleteCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				bool result = false;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndNodeDelete (asyncResult);
					this.NodeDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnNodeDeleteCompleted");
					this.NodeDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct[] NodeIndex (int page, string fields, XmlRpcStruct parameters, int page_size)
		{
			ClearErrors ();
			XmlRpcStruct[] res = null;
			try {
				res = ServiceSystem.NodeIndex (page, fields, parameters, page_size);
			} catch (Exception ex) {
				HandleException (ex, "NodeIndex");
			}
			return res;
		}

		public void NodeIndexAsync (int page, string fields, XmlRpcStruct parameters, int page_size, object asyncState)
		{
			if (this.NodeIndexOperationCompleted == null) {
				this.NodeIndexOperationCompleted = new AsyncCallback (this.OnNodeIndexCompleted);
			}
			ServiceSystem.BeginNodeIndex (page, fields, parameters, page_size, this.NodeIndexOperationCompleted, asyncState);
		}

		void OnNodeIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.NodeIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct[] res = null;
				try {
					res = ((IServiceSystem)clientResult.ClientProtocol).EndNodeIndex (asyncResult);
					this.NodeIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (res, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnNodeIndexCompleted");
					this.NodeIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (res, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalFile[] NodeFiles (int nid, bool include_file_contents, bool get_image_style)
		{
			ClearErrors ();
			DrupalFile[] res = null;
			try {
				res = ServiceSystem.NodeFiles (nid, include_file_contents, get_image_style);
			} catch (Exception ex) {
				HandleException (ex, "NodeFiles");
			}
			return res;
		}
		
		public void NodeFilesAsync (int nid, bool include_file_contents, bool get_image_style, object asyncState)
		{
			if (this.NodeFilesOperationCompleted == null) {
				this.NodeFilesOperationCompleted = new AsyncCallback (this.OnNodeFilesCompleted);
			}
			ServiceSystem.BeginNodeFiles (nid, include_file_contents, get_image_style, this.NodeFilesOperationCompleted, asyncState);
		}

		void OnNodeFilesCompleted (IAsyncResult asyncResult)
		{
			if (this.NodeFilesCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalFile[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndNodeFiles (asyncResult);
					this.NodeFilesCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnNodeFilesCompleted");
					this.NodeFilesCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalFile[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion
		
		#region System

		public DrupalSessionObject SystemConnect ()
		{
			ClearErrors ();
			DrupalSessionObject res = default(DrupalSessionObject);
			try {
				res = ServiceSystem.SystemConnect ();
			} catch (Exception ex) {
				HandleException (ex, "SystemConnect");
			}
			return res;
		}

		public void SystemConnectAsync (object asyncState)
		{
			if (this.SystemConnectOperationCompleted == null) {
				this.SystemConnectOperationCompleted = new AsyncCallback (this.OnSystemConnectCompleted);
			}
			ServiceSystem.BeginSystemConnect (this.SystemConnectOperationCompleted, asyncState);
		}

		void OnSystemConnectCompleted (IAsyncResult asyncResult)
		{
			if (this.SystemConnectCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalSessionObject result = default(DrupalSessionObject);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndSystemConnect (asyncResult);
					this.SystemConnectCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalSessionObject> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnSystemConnectCompleted");
					this.SystemConnectCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalSessionObject> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public object SystemGetVariable (string name, object @default)
		{
			ClearErrors ();
			object res = null;
			try {
				res = ServiceSystem.SystemGetVariable (name, @default);
			} catch (Exception ex) {
				HandleException (ex, "SystemGetVariable");
			}
			return res;
		}

		public void SystemGetVariableAsync (string name, object @default, object asyncState)
		{
			if (this.SystemGetVariableOperationCompleted == null) {
				this.SystemGetVariableOperationCompleted = new AsyncCallback (this.OnSystemGetVariableCompleted);
			}
			ServiceSystem.BeginSystemGetVariable (name, @default, this.SystemGetVariableOperationCompleted, asyncState);
		}

		void OnSystemGetVariableCompleted (IAsyncResult asyncResult)
		{
			if (this.SystemGetVariableCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				object result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndSystemGetVariable (asyncResult);
					this.SystemGetVariableCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnSystemGetVariableCompleted");
					this.SystemGetVariableCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public void SystemSetVariable (string name, object @value)
		{
			ClearErrors ();
			try {
				ServiceSystem.SystemSetVariable (name, @value);
			} catch (Exception ex) {
				HandleException (ex, "SystemSetVariable");
			}
		}

		public void SystemSetVariableAsync (string name, object @value, object asyncState)
		{
			if (this.SystemSetVariableOperationCompleted == null) {
				this.SystemSetVariableOperationCompleted = new AsyncCallback (this.OnSystemSetVariableCompleted);
			}
			ServiceSystem.BeginSystemSetVariable (name, @value, this.SystemSetVariableOperationCompleted, asyncState);
		}

		void OnSystemSetVariableCompleted (IAsyncResult asyncResult)
		{
			// TODO OnSystemSetVariableCompleted
		}

		public void SystemDelVariable (string name)
		{
			ClearErrors ();
			try {
				ServiceSystem.SystemDelVariable (name);
			} catch (Exception ex) {
				HandleException (ex, "SystemDelVariable");
			}
		}

		public void SystemDelVariableAsync (string name, object asyncState)
		{
			if (this.SystemDelVariableOperationCompleted == null) {
				this.SystemDelVariableOperationCompleted = new AsyncCallback (this.OnSystemDelVariableCompleted);
			}
			ServiceSystem.BeginSystemDelVariable (name, this.SystemDelVariableOperationCompleted, asyncState);
		}

		void OnSystemDelVariableCompleted (IAsyncResult asr)
		{
			// TODO OnSystemDelVariableCompleted
		}

		#endregion
		
		#region TaxonomyTree

		public object TaxonomyTermRetrieve (int tid)
		{
			ClearErrors ();
			object res = null;
			try {
				res = ServiceSystem.TaxonomyTermRetrieve (tid);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyTermRetrieve");
			}
			return res;
		}

		public void TaxonomyTermRetrieveAsync (int tid, object asyncState)
		{
			if (this.TaxonomyTermRetrieveOperationCompleted == null) {
				this.TaxonomyTermRetrieveOperationCompleted = new AsyncCallback (this.OnTaxonomyTermRetrieveCompleted);
			}
			ServiceSystem.BeginTaxonomyTermRetrieve (tid, this.TaxonomyTermRetrieveOperationCompleted, asyncState);
		}

		void OnTaxonomyTermRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyTermRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				object result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyTermRetrieve (asyncResult);
					this.TaxonomyTermRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyTermRetrieveCompleted");
					this.TaxonomyTermRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int TaxonomyTermCreate (XmlRpcStruct term)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.TaxonomyTermCreate (term);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyTermCreate");
			}
			return res;
		}

		public void TaxonomyTermCreateAsync (XmlRpcStruct term, object asyncState)
		{
			if (this.TaxonomyTermCreateOperationCompleted == null) {
				this.TaxonomyTermCreateOperationCompleted = new AsyncCallback (this.OnTaxonomyTermCreateCompleted);
			}
			ServiceSystem.BeginTaxonomyTermCreate (term, this.TaxonomyTermCreateOperationCompleted, asyncState);
		}

		void OnTaxonomyTermCreateCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyTermCreateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyTermCreate (asyncResult);
					this.TaxonomyTermCreateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyTermCreateCompleted");
					this.TaxonomyTermCreateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int TaxonomyTermUpdate (int tid, XmlRpcStruct term)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.TaxonomyTermUpdate (tid, term);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyTermUpdate");
			}
			return res;
		}

		public void TaxonomyTermUpdateAsync (int tid, XmlRpcStruct term, object asyncState)
		{
			if (this.TaxonomyTermUpdateOperationCompleted == null) {
				this.TaxonomyTermUpdateOperationCompleted = new AsyncCallback (this.OnTaxonomyTermUpdateCompleted);
			}
			ServiceSystem.BeginTaxonomyTermUpdate (tid, term, this.TaxonomyTermUpdateOperationCompleted, asyncState);
		}

		void OnTaxonomyTermUpdateCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyTermUpdateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyTermUpdate (asyncResult);
					this.TaxonomyTermUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyTermUpdateCompleted");
					this.TaxonomyTermUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int TaxonomyTermDelete (int tid)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.TaxonomyTermDelete (tid);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyTermDelete");
			}
			return res;
		}

		public void TaxonomyTermDeleteAsync (int tid, object asyncState)
		{
			if (this.TaxonomyTermDeleteOperationCompleted == null) {
				this.TaxonomyTermDeleteOperationCompleted = new AsyncCallback (this.OnTaxonomyTermDeleteCompleted);
			}
			ServiceSystem.BeginTaxonomyTermDelete (tid, this.TaxonomyTermDeleteOperationCompleted, asyncState);
		}

		void OnTaxonomyTermDeleteCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyTermDeleteCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyTermDelete (asyncResult);
					this.TaxonomyTermDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyTermDeleteCompleted");
					this.TaxonomyTermDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct[] TaxonomyTermIndex (int page, string fields, XmlRpcStruct parameters, int page_size)
		{
			ClearErrors ();
			XmlRpcStruct[] res = null;
			try {
				res = ServiceSystem.TaxonomyTermIndex (page, fields, parameters, page_size);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyTermIndex");
			}
			return res;
		}

		public void TaxonomyTermIndexAsync (int page, string fields, XmlRpcStruct parameters, int page_size, object asyncState)
		{
			if (this.TaxonomyTermIndexOperationCompleted == null) {
				this.TaxonomyTermIndexOperationCompleted = new AsyncCallback (this.OnTaxonomyTermIndexCompleted);
			}
			ServiceSystem.BeginTaxonomyTermIndex (page, fields, parameters, page_size, this.TaxonomyTermIndexOperationCompleted, asyncState);
		}

		void OnTaxonomyTermIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyTermIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyTermIndex (asyncResult);
					this.TaxonomyTermIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyTermIndexCompleted");
					this.TaxonomyTermIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct[] TaxonomyTermSelectNodes (int tid, bool pager, bool limit, XmlRpcStruct order)
		{
			ClearErrors ();
			XmlRpcStruct[] res = null;
			try {
				res = ServiceSystem.TaxonomyTermSelectNodes (tid, pager, limit, order);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyTermSelectNodes");
			}
			return res;
		}

		public void TaxonomyTermSelectNodesAsync (int tid, bool pager, bool limit, XmlRpcStruct order, object asyncState)
		{
			if (this.TaxonomyTermSelectNodesOperationCompleted == null) {
				this.TaxonomyTermSelectNodesOperationCompleted = new AsyncCallback (this.OnTaxonomyTermSelectNodesCompleted);
			}
			ServiceSystem.BeginTaxonomyTermSelectNodes (tid, pager, limit, order, this.TaxonomyTermSelectNodesOperationCompleted, asyncState);
		}

		void OnTaxonomyTermSelectNodesCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyTermSelectNodesCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyTermSelectNodes (asyncResult);
					this.TaxonomyTermSelectNodesCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyTermSelectNodesCompleted");
					this.TaxonomyTermSelectNodesCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion
		
		#region TaxonomyVocabulary

		public XmlRpcStruct TaxonomyVocabularyRetrieve (int vid)
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.TaxonomyVocabularyRetrieve (vid);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyVocabularyRetrieve");
			}
			return res;
		}

		public void TaxonomyVocabularyRetrieveAsync (int vid, object asyncState)
		{
			if (this.TaxonomyVocabularyRetrieveOperationCompleted == null) {
				this.TaxonomyVocabularyRetrieveOperationCompleted = new AsyncCallback (this.OnTaxonomyVocabularyRetrieveCompleted);
			}
			ServiceSystem.BeginTaxonomyVocabularyRetrieve (vid, this.TaxonomyVocabularyRetrieveOperationCompleted, asyncState);
		}

		void OnTaxonomyVocabularyRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyVocabularyRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyVocabularyRetrieve (asyncResult);
					this.TaxonomyVocabularyRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyVocabularyRetrieveCompleted");
					this.TaxonomyVocabularyRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int TaxonomyVocabularyCreate (XmlRpcStruct vocabulary)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.TaxonomyVocabularyCreate (vocabulary);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyVocabularyCreate");
			}
			return res;
		}

		public void TaxonomyVocabularyCreateAsync (XmlRpcStruct vocabulary, object asyncState)
		{
			if (this.TaxonomyVocabularyCreateOperationCompleted == null) {
				this.TaxonomyVocabularyCreateOperationCompleted = new AsyncCallback (this.OnTaxonomyVocabularyCreateCompleted);
			}
			ServiceSystem.BeginTaxonomyVocabularyCreate (vocabulary, this.TaxonomyVocabularyRetrieveOperationCompleted, asyncState);
		}

		void OnTaxonomyVocabularyCreateCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyVocabularyCreateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyVocabularyCreate (asyncResult);
					this.TaxonomyVocabularyCreateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyVocabularyCreateCompleted");
					this.TaxonomyVocabularyCreateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int TaxonomyVocabularyUpdate (int vid, XmlRpcStruct vocabulary)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.TaxonomyVocabularyUpdate (vid, vocabulary);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyVocabularyUpdate");
			}
			return res;
		}

		public void TaxonomyVocabularyUpdateAsync (int vid, XmlRpcStruct vocabulary, object asyncState)
		{
			if (this.TaxonomyVocabularyUpdateOperationCompleted == null) {
				this.TaxonomyVocabularyUpdateOperationCompleted = new AsyncCallback (this.OnTaxonomyVocabularyUpdateCompleted);
			}
			ServiceSystem.BeginTaxonomyVocabularyUpdate (vid, vocabulary, this.TaxonomyVocabularyUpdateOperationCompleted, asyncState);
		}

		void OnTaxonomyVocabularyUpdateCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyVocabularyUpdateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyVocabularyUpdate (asyncResult);
					this.TaxonomyVocabularyUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyVocabularyUpdateCompleted");
					this.TaxonomyVocabularyUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public int TaxonomyVocabularyDelete (int vid)
		{
			ClearErrors ();
			int res = -1;
			try {
				res = ServiceSystem.TaxonomyVocabularyDelete (vid);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyVocabularyDelete");
			}
			return res;
		}

		public void TaxonomyVocabularyDeleteAsync (int vid, object asyncState)
		{
			if (this.TaxonomyVocabularyDeleteOperationCompleted == null) {
				this.TaxonomyVocabularyDeleteOperationCompleted = new AsyncCallback (this.OnTaxonomyVocabularyDeleteCompleted);
			}
			ServiceSystem.BeginTaxonomyVocabularyDelete (vid, this.TaxonomyVocabularyDeleteOperationCompleted, asyncState);
		}

		void OnTaxonomyVocabularyDeleteCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyVocabularyDeleteCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				int result = -1;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyVocabularyDelete (asyncResult);
					this.TaxonomyVocabularyDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyVocabularyDeleteCompleted");
					this.TaxonomyVocabularyDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<int> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct[] TaxonomyVocabularyIndex (int page, string fields, XmlRpcStruct parameters, int page_size)
		{
			ClearErrors ();
			XmlRpcStruct[] res = null;
			try {
				res = ServiceSystem.TaxonomyVocabularyIndex (page, fields, parameters, page_size);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyVocabularyIndex");
			}
			return res;
		}

		public void TaxonomyVocabularyIndexAsync (int page, string fields, XmlRpcStruct parameters, int page_size, object asyncState)
		{
			if (this.TaxonomyVocabularyIndexOperationCompleted == null) {
				this.TaxonomyVocabularyIndexOperationCompleted = new AsyncCallback (this.OnTaxonomyVocabularyIndexCompleted);
			}
			ServiceSystem.BeginTaxonomyVocabularyIndex (page, fields, parameters, page_size, this.TaxonomyVocabularyIndexOperationCompleted, asyncState);
		}

		void OnTaxonomyVocabularyIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyVocabularyIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyVocabularyIndex (asyncResult);
					this.TaxonomyVocabularyIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyVocabularyIndexCompleted");
					this.TaxonomyVocabularyIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct[] TaxonomyVocabularyGetTree (int vid, int parent, int max_depth)
		{
			ClearErrors ();
			XmlRpcStruct[] res = null;
			try {
				res = ServiceSystem.TaxonomyVocabularyGetTree (vid, parent, max_depth);
			} catch (Exception ex) {
				HandleException (ex, "TaxonomyVocabularyGetTree");
			}
			return res;
		}

		public void TaxonomyVocabularyGetTreeAsync (int vid, int parent, int max_depth, object asyncState)
		{
			if (this.TaxonomyVocabularyGetTreeOperationCompleted == null) {
				this.TaxonomyVocabularyGetTreeOperationCompleted = new AsyncCallback (this.OnTaxonomyVocabularyGetTreeCompleted);
			}
			ServiceSystem.BeginTaxonomyVocabularyGetTree (vid, parent, max_depth, this.TaxonomyVocabularyGetTreeOperationCompleted, asyncState);
		}

		void OnTaxonomyVocabularyGetTreeCompleted (IAsyncResult asyncResult)
		{
			if (this.TaxonomyVocabularyGetTreeCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndTaxonomyVocabularyGetTree (asyncResult);
					this.TaxonomyVocabularyGetTreeCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnTaxonomyVocabularyGetTreeCompleted");
					this.TaxonomyVocabularyGetTreeCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion

		#region User

		public DrupalUser UserRetrieve (int uid)
		{
			ClearErrors ();
			DrupalUser res = default(DrupalUser);
			try {
				res = ServiceSystem.UserRetrieve (uid);
			} catch (Exception ex) {
				HandleException (ex, "UserRetrieve");
			}
			return res;
		}
		
		public void UserRetrieveAsync (int uid, object asyncState)
		{
			if (this.UserRetrieveOperationCompleted == null) {
				this.UserRetrieveOperationCompleted = new AsyncCallback (this.OnUserRetrieveCompleted);
			}
			ServiceSystem.BeginUserRetrieve (uid, this.UserRetrieveOperationCompleted, asyncState);
		}

		void OnUserRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.UserRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalUser result = default(DrupalUser);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserRetrieve (asyncResult);
					this.UserRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserRetrieveCompleted");
					this.UserRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalUser UserCreate (XmlRpcStruct account)
		{
			ClearErrors ();
			DrupalUser res = default(DrupalUser);
			try {
				res = ServiceSystem.UserCreate (account);
			} catch (Exception ex) {
				HandleException (ex, "UserCreate");
			}
			return res;
		}
		
		public void UserCreateAsync (XmlRpcStruct account, object asyncState)
		{
			if (this.UserCreateOperationCompleted == null) {
				this.UserCreateOperationCompleted = new AsyncCallback (this.OnUserCreateCompleted);
			}
			ServiceSystem.BeginUserCreate (account, this.UserCreateOperationCompleted, asyncState);
		}

		void OnUserCreateCompleted (IAsyncResult asyncResult)
		{
			if (this.UserCreateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalUser result = default(DrupalUser);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserCreate (asyncResult);
					this.UserCreateCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserCreateCompleted");
					this.UserCreateCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalUser UserUpdate (int uid, XmlRpcStruct account)
		{
			ClearErrors ();
			DrupalUser res = default(DrupalUser);
			try {
				res = ServiceSystem.UserUpdate (uid, account);
			} catch (Exception ex) {
				HandleException (ex, "UserUpdate");
			}
			return res;
		}
		
		public void UserUpdateAsync (int uid, XmlRpcStruct account, object asyncState)
		{
			if (this.UserUpdateOperationCompleted == null) {
				this.UserUpdateOperationCompleted = new AsyncCallback (this.OnUserUpdateCompleted);
			}
			ServiceSystem.BeginUserUpdate (uid, account, this.UserUpdateOperationCompleted, asyncState);
		}

		void OnUserUpdateCompleted (IAsyncResult asyncResult)
		{
			if (this.UserUpdateCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalUser result = default(DrupalUser);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserUpdate (asyncResult);
					this.UserUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserUpdateCompleted");
					this.UserUpdateCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public bool UserDelete (int uid)
		{
			ClearErrors ();
			bool res = false;
			try {
				res = ServiceSystem.UserDelete (uid);
			} catch (Exception ex) {
				HandleException (ex, "UserDelete");
			}
			return res;
		}
		
		public void UserDeleteAsync (int uid, object asyncState)
		{
			if (this.UserDeleteOperationCompleted == null) {
				this.UserDeleteOperationCompleted = new AsyncCallback (this.OnUserDeleteCompleted);
			}
			ServiceSystem.BeginUserDelete (uid, this.UserDeleteOperationCompleted, asyncState);
		}

		void OnUserDeleteCompleted (IAsyncResult asyncResult)
		{
			if (this.UserDeleteCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				bool result = false;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserDelete (asyncResult);
					this.UserDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserDeleteCompleted");
					this.UserDeleteCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalUser[] UserIndex (int page, string fields, XmlRpcStruct parameters, int page_size)
		{
			ClearErrors ();
			DrupalUser[] res = null;
			try {
				res = ServiceSystem.UserIndex (page, fields, parameters, page_size);
			} catch (Exception ex) {
				HandleException (ex, "UserIndex");
			}
			return res;
		}

		public void UserIndexAsync (int page, string fields, XmlRpcStruct parameters, int page_size, object asyncState)
		{
			if (this.UserIndexOperationCompleted == null) {
				this.UserIndexOperationCompleted = new AsyncCallback (this.OnUserIndexCompleted);
			}
			ServiceSystem.BeginUserIndex (page, fields, parameters, page_size, this.UserIndexOperationCompleted, asyncState);
		}

		void OnUserIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.UserIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalUser[] result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserIndex (asyncResult);
					this.UserIndexCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser[]> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserIndexCompleted");
					this.UserIndexCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser[]> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalSessionObject UserLogin (string username, string password)
		{
			ClearErrors ();
			DrupalSessionObject res = default(DrupalSessionObject);
			try {
				res = ServiceSystem.UserLogin (username, password);
			} catch (Exception ex) {
				HandleException (ex, "UserLogin");
			}
			return res;
		}

		public void UserLoginAsync (string username, string password, object asyncState)
		{
			if (this.UserLoginOperationCompleted == null) {
				this.UserLoginOperationCompleted = new AsyncCallback (this.OnUserLoginCompleted);
			}
			ServiceSystem.BeginUserLogin (username, password, this.UserLoginOperationCompleted, asyncState);
		}

		void OnUserLoginCompleted (IAsyncResult asyncResult)
		{
			if (this.UserLoginCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalSessionObject result = default(DrupalSessionObject);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserLogin (asyncResult);
					this.UserLoginCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalSessionObject> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserLoginCompleted");
					this.UserLoginCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalSessionObject> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public bool UserLogout ()
		{
			ClearErrors ();
			bool res = false;
			try {
				res = ServiceSystem.UserLogout ();
			} catch (Exception ex) {
				HandleException (ex, "UserLogout");
			}
			return res;
		}
		
		public void UserLogoutAsync (object asyncState)
		{
			if (this.UserLogoutOperationCompleted == null) {
				this.UserLogoutOperationCompleted = new AsyncCallback (this.OnUserLogoutCompleted);
			}
			ServiceSystem.BeginUserLogout (this.UserLogoutOperationCompleted, asyncState);
		}

		void OnUserLogoutCompleted (IAsyncResult asyncResult)
		{
			if (this.UserLogoutCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				bool result = false;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserLogout (asyncResult);
					this.UserLogoutCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserLogoutCompleted");
					this.UserLogoutCompleted (this, new DrupalAsyncCompletedEventArgs<bool> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public DrupalUser UserRegister (XmlRpcStruct account)
		{
			ClearErrors ();
			DrupalUser res = default(DrupalUser);
			try {
				res = ServiceSystem.UserRegister (account);
			} catch (Exception ex) {
				HandleException (ex, "UserRegister");
			}
			return res;
		}

		public void UserRegisterAsync (XmlRpcStruct account, object asyncState)
		{
			if (this.UserRegisterOperationCompleted == null) {
				this.UserRegisterOperationCompleted = new AsyncCallback (this.OnUserRegisterCompleted);
			}
			ServiceSystem.BeginUserRegister (account, this.UserRegisterOperationCompleted, asyncState);
		}

		void OnUserRegisterCompleted (IAsyncResult asyncResult)
		{
			if (this.UserRegisterCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				DrupalUser result = default(DrupalUser);
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndUserRegister (asyncResult);
					this.UserRegisterCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnUserRegisterCompleted");
					this.UserRegisterCompleted (this, new DrupalAsyncCompletedEventArgs<DrupalUser> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion
		
		#region Menu

		public object MenuRetrieve (string menu_name)
		{
			ClearErrors ();
			object res = null;
			try {
				res = ServiceSystem.MenuRetrieve (menu_name);
			} catch (Exception ex) {
				HandleException (ex, "MenuRetrieve");
			}
			return res;
		}
		
		public void MenuRetrieveAsync (string menu_name, object asyncState)
		{
			if (this.MenuRetrieveOperationCompleted == null) {
				this.MenuRetrieveOperationCompleted = new AsyncCallback (this.OnMenuRetrieveCompleted);
			}
			ServiceSystem.BeginMenuRetrieve (menu_name, this.MenuRetrieveOperationCompleted, asyncState);
		}

		void OnMenuRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.MenuRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				object result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndMenuRetrieve (asyncResult);
					this.MenuRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnMenuRetrieveCompleted");
					this.MenuRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<object> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion
		
		#region Views

		public XmlRpcStruct ViewsRetrieve (string view_name, string display_id, XmlRpcStruct args, int offset, int limit, object return_type, XmlRpcStruct filters)
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.ViewsRetrieve (view_name, display_id, args, offset, limit, return_type, filters);
			} catch (Exception ex) {
				HandleException (ex, "ViewsRetrieve");
			}
			return res;
		}

		public void ViewsRetrieveAsync (string view_name, string display_id, XmlRpcStruct args, int offset, int limit, object return_type, XmlRpcStruct filters, object asyncState)
		{
			if (this.ViewsRetrieveOperationCompleted == null) {
				this.ViewsRetrieveOperationCompleted = new AsyncCallback (this.OnViewsRetrieveCompleted);
			}
			ServiceSystem.BeginViewsRetrieve (view_name, display_id, args, offset, limit, return_type, filters, this.ViewsRetrieveOperationCompleted, asyncState);
		}

		void OnViewsRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.ViewsRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndViewsRetrieve (asyncResult);
					this.ViewsRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnViewsRetrieveCompleted");
					this.ViewsRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion

		#region Definition

		public XmlRpcStruct DefinitionIndex ()
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.DefinitionIndex ();
			} catch (Exception ex) {
				HandleException (ex, "DefinitionIndex");
			}
			return res;
		}
		
		public void DefinitionIndexAsync (object asyncState)
		{
			if (this.DefinitionIndexOperationCompleted == null) {
				this.DefinitionIndexOperationCompleted = new AsyncCallback (this.OnDefinitionIndexCompleted);
			}
			ServiceSystem.BeginDefinitionIndex (this.DefinitionIndexOperationCompleted, asyncState);
		}

		void OnDefinitionIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.DefinitionIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndDefinitionIndex (asyncResult);
					this.DefinitionIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnDefinitionIndexCompleted");
					this.DefinitionIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion

		#region Geocoder

		public string GeocoderRetrieve(string handler, string data, string output)
		{
			ClearErrors ();
			string res = "";
			try {
				res = ServiceSystem.GeocoderRetrieve (handler, data, output);
			} catch (Exception ex) {
				HandleException (ex, "GeocoderRetrieve");
			}
			return res;
		}
		
		public void GeocoderRetrieveAsync (string handler, string data, string output, object asyncState)
		{
			if (this.GeocoderRetrieveOperationCompleted == null) {
				this.GeocoderRetrieveOperationCompleted = new AsyncCallback (this.OnGeocoderRetrieveCompleted);
			}
			ServiceSystem.BeginGeocoderRetrieve (handler, data, output, this.GeocoderRetrieveOperationCompleted, asyncState);
		}

		void OnGeocoderRetrieveCompleted (IAsyncResult asyncResult)
		{
			if (this.GeocoderRetrieveCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				string result = "";
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndGeocoderRetrieve (asyncResult);
					this.GeocoderRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<string> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnGeocoderRetrieveCompleted");
					this.GeocoderRetrieveCompleted (this, new DrupalAsyncCompletedEventArgs<string> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		public XmlRpcStruct GeocoderIndex ()
		{
			ClearErrors ();
			XmlRpcStruct res = null;
			try {
				res = ServiceSystem.GeocoderIndex ();
			} catch (Exception ex) {
				HandleException (ex, "GeocoderIndex");
			}
			return res;
		}

		public void GeocoderIndexAsync (object asyncState)
		{
			if (this.GeocoderIndexOperationCompleted == null) {
				this.GeocoderIndexOperationCompleted = new AsyncCallback (this.OnGeocoderIndexCompleted);
			}
			ServiceSystem.BeginGeocoderIndex (this.GeocoderIndexOperationCompleted, asyncState);
		}

		void OnGeocoderIndexCompleted (IAsyncResult asyncResult)
		{
			if (this.GeocoderIndexCompleted != null) {
				XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asyncResult;
				XmlRpcStruct result = null;
				try {
					result = ((IServiceSystem)clientResult.ClientProtocol).EndGeocoderIndex (asyncResult);
					this.GeocoderIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, null, asyncResult.AsyncState));
				} catch (Exception ex) {
					HandleException (ex, "OnGeocoderIndexCompleted");
					this.GeocoderIndexCompleted (this, new DrupalAsyncCompletedEventArgs<XmlRpcStruct> (result, ex, asyncResult.AsyncState));
				}
			}
		}

		#endregion
	}
}
