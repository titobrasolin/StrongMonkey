using System;
using CookComputing.XmlRpc;
using Mono.Addins;
using StrongMonkey.Core;
namespace StrongMonkey.Core
{
	[TypeExtensionPoint]
	public interface IDrupalConnection
	{
		#region Events
		
		event DrupalConnectionBase.UpdateLog OnUpdateLog;

		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> CommentCreateCompleted;
		event DrupalAsyncCompletedEventHandler<object> CommentRetrieveCompleted;
		event DrupalAsyncCompletedEventHandler<object> CommentUpdateCompleted;
		event DrupalAsyncCompletedEventHandler<bool> CommentDeleteCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> CommentIndexCompleted;
		event DrupalAsyncCompletedEventHandler<int> CommentCountAllCompleted;
		event DrupalAsyncCompletedEventHandler<int> CommentCountNewCompleted;

		event DrupalAsyncCompletedEventHandler<DrupalFile> FileCreateCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalFile> FileRetrieveCompleted;
		event DrupalAsyncCompletedEventHandler<bool> FileDeleteCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalFile[]> FileIndexCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalFile[]> FileCreateRawCompleted;

		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> NodeRetrieveCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> NodeCreateCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> NodeUpdateCompleted;
		event DrupalAsyncCompletedEventHandler<bool> NodeDeleteCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> NodeIndexCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalFile[]> NodeFilesCompleted;

		event DrupalAsyncCompletedEventHandler<DrupalSessionObject> SystemConnectCompleted;
		event DrupalAsyncCompletedEventHandler<object> SystemGetVariableCompleted;
		// event DrupalAsyncCompletedEventHandler<...> SystemSetVariableCompleted;
		// event DrupalAsyncCompletedEventHandler<...> SystemDelVariableCompleted;

		event DrupalAsyncCompletedEventHandler<object> TaxonomyTermRetrieveCompleted;
		event DrupalAsyncCompletedEventHandler<int> TaxonomyTermCreateCompleted;
		event DrupalAsyncCompletedEventHandler<int> TaxonomyTermUpdateCompleted;
		event DrupalAsyncCompletedEventHandler<int> TaxonomyTermDeleteCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyTermIndexCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyTermSelectNodesCompleted;

		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> TaxonomyVocabularyRetrieveCompleted;
		event DrupalAsyncCompletedEventHandler<int> TaxonomyVocabularyCreateCompleted;
		event DrupalAsyncCompletedEventHandler<int> TaxonomyVocabularyUpdateCompleted;
		event DrupalAsyncCompletedEventHandler<int> TaxonomyVocabularyDeleteCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyVocabularyIndexCompleted;
		event DrupalAsyncCompletedEventHandler<XmlRpcStruct[]> TaxonomyVocabularyGetTreeCompleted;

		event DrupalAsyncCompletedEventHandler<DrupalUser> UserRetrieveCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalUser> UserCreateCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalUser> UserUpdateCompleted;
		event DrupalAsyncCompletedEventHandler<bool> UserDeleteCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalUser[]> UserIndexCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalSessionObject> UserLoginCompleted;
		event DrupalAsyncCompletedEventHandler<bool> UserLogoutCompleted;
		event DrupalAsyncCompletedEventHandler<DrupalUser> UserRegisterCompleted;

		event DrupalAsyncCompletedEventHandler<object> MenuRetrieveCompleted;

		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> ViewsRetrieveCompleted;

		event DrupalAsyncCompletedEventHandler<XmlRpcStruct> DefinitionIndexCompleted;

		#endregion

		bool Login (string username, string password);
		bool ReLogin ();

		int ErrorCode { get; }
		string ErrorMessage { get; }
	
		DrupalSessionObject SessionData { get; }

		#region Flag
		
		bool FlagIsFlagged (string flag_name, int content_id, int uid);
		void FlagIsFlaggedAsync (string flag_name, int content_id, int uid, object userState);
		
		#endregion

		#region Comment
		
		XmlRpcStruct CommentCreate (XmlRpcStruct comment);
		void CommentCreateAsync (XmlRpcStruct comment, object userState);
		
		object CommentRetrieve (int cid);
		void CommentRetrieveAsync (int cid, object userState);

		object CommentUpdate (int cid, XmlRpcStruct comment);
		void CommentUpdateAsync (int cid, XmlRpcStruct comment, object userState);

		bool CommentDelete (int cid);
		void CommentDeleteAsync (int cid, object userState);

		XmlRpcStruct[] CommentIndex (int page, string fields, XmlRpcStruct[] parameters, int page_size);
		void CommentIndexAsync (int page, string fields, XmlRpcStruct[] parameters, int page_size, object userState);

		int CommentCountAll (int nid);
		void CommentCountAllAsync (int nid, object userState);

		int CommentCountNew (int nid, int timestamp);
		void CommentCountNewAsync (int nid, int timestamp, object userState);

		#endregion

		#region File
		
		DrupalFile FileCreate(DrupalFile file);
		void FileCreateAsync(DrupalFile file, object userState);

		DrupalFile FileRetrieve(int fid, bool include_file_contents, bool get_image_style);
		void FileRetrieveAsync(int fid, bool include_file_contents, bool get_image_style, object userState);

		bool FileDelete(int fid);
		void FileDeleteAsync(int fid, object userState);

		DrupalFile[] FileIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
		void FileIndexAsync(int page, string fields, XmlRpcStruct parameters, int page_size, object userState);

		DrupalFile[] FileCreateRaw();
		void FileCreateRawAsync(object userState);
		
		#endregion

		#region Node
		
		XmlRpcStruct NodeRetrieve (int nid);
		void NodeRetrieveAsync (int nid, object userState);

		XmlRpcStruct NodeCreate(XmlRpcStruct node);
		void NodeCreateAsync(XmlRpcStruct node, object userState);

		XmlRpcStruct NodeUpdate(int nid, XmlRpcStruct node);
		void NodeUpdateAsync(int nid, XmlRpcStruct node, object userState);

		bool NodeDelete(int nid);
		void NodeDeleteAsync(int nid, object userState);

		XmlRpcStruct[] NodeIndex (int page, string fields, XmlRpcStruct parameters, int page_size);
		void NodeIndexAsync (int page, string fields, XmlRpcStruct parameters, int page_size, object userState);

		DrupalFile[] NodeFiles (int nid, bool include_file_contents, bool get_image_style);
		void NodeFilesAsync (int nid, bool include_file_contents, bool get_image_style, object userState);

		#endregion
		
		#region System
		
		DrupalSessionObject SystemConnect();
		object SystemGetVariable(string name, object @default);
		void SystemSetVariable(string name, object @value);
		void SystemDelVariable(string name);		
		
		#endregion

		#region TaxonomyTerm
		
		object TaxonomyTermRetrieve(int tid);
		void TaxonomyTermRetrieveAsync(int tid, object userState);

		int TaxonomyTermCreate(XmlRpcStruct term);
		void TaxonomyTermCreateAsync(XmlRpcStruct term, object userState);

		int TaxonomyTermUpdate(int tid, XmlRpcStruct term);
		void TaxonomyTermUpdateAsync(int tid, XmlRpcStruct term, object userState);

		int TaxonomyTermDelete(int tid);
		void TaxonomyTermDeleteAsync(int tid, object userState);

		XmlRpcStruct[] TaxonomyTermIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
		void TaxonomyTermIndexAsync(int page, string fields, XmlRpcStruct parameters, int page_size, object userState);

		XmlRpcStruct[] TaxonomyTermSelectNodes(int tid, bool pager, bool limit, XmlRpcStruct order);
		void TaxonomyTermSelectNodesAsync(int tid, bool pager, bool limit, XmlRpcStruct order, object userState);
		
		#endregion

		#region TaxonomyVocabulary
		
		XmlRpcStruct TaxonomyVocabularyRetrieve(int vid);
		void TaxonomyVocabularyRetrieveAsync(int vid, object userState);

		int TaxonomyVocabularyCreate(XmlRpcStruct vocabulary);
		void TaxonomyVocabularyCreateAsync(XmlRpcStruct vocabulary, object userState);

		int TaxonomyVocabularyUpdate(int vid, XmlRpcStruct vocabulary);
		void TaxonomyVocabularyUpdateAsync(int vid, XmlRpcStruct vocabulary, object userState);

		int TaxonomyVocabularyDelete(int vid);
		void TaxonomyVocabularyDeleteAsync(int vid, object userState);

		XmlRpcStruct[] TaxonomyVocabularyIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
		void TaxonomyVocabularyIndexAsync(int page, string fields, XmlRpcStruct parameters, int page_size, object userState);

		XmlRpcStruct[] TaxonomyVocabularyGetTree(int vid, int parent, int max_depth);
		void TaxonomyVocabularyGetTreeAsync(int vid, int parent, int max_depth, object userState);

		#endregion

		#region User
		
		DrupalUser UserRetrieve(int uid);
		void UserRetrieveAsync(int uid, object userState);

		DrupalUser UserCreate(XmlRpcStruct account);
		void UserCreateAsync(XmlRpcStruct account, object userState);

		DrupalUser UserUpdate(int uid, XmlRpcStruct account);
		void UserUpdateAsync(int uid, XmlRpcStruct account, object userState);

		bool UserDelete(int uid);
		void UserDeleteAsync(int uid, object userState);

		DrupalUser[] UserIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
		void UserIndexAsync(int page, string fields, XmlRpcStruct parameters, int page_size, object userState);

		DrupalSessionObject UserLogin(string username, string password);
		void UserLoginAsync(string username, string password, object userState);

		bool UserLogout();
		void UserLogoutAsync(object userState);

		DrupalUser UserRegister(XmlRpcStruct account);
		void UserRegisterAsync(XmlRpcStruct account, object userState);	
		
		#endregion

		#region Menu
		
		object MenuRetrieve(string menu_name);
		void MenuRetrieveAsync(string menu_name, object userState);	
		
		#endregion

		#region Views
		
		XmlRpcStruct ViewsRetrieve(string view_name, string display_id, XmlRpcStruct args, int offset, int limit, object return_type, XmlRpcStruct filters);
		void ViewsRetrieveAsync(string view_name, string display_id, XmlRpcStruct args, int offset, int limit, object return_type, XmlRpcStruct filters, object userState);	

		#endregion

		#region Definition
		
		XmlRpcStruct DefinitionIndex();
		void DefinitionIndexAsync(object userState);

		#endregion
	}
}
