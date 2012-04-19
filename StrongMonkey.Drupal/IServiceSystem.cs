using System;
using CookComputing.XmlRpc;
using StrongMonkey.Core;
namespace StrongMonkey.Drupal
{
	public interface IServiceSystem : IXmlRpcProxy
	{
		#region Flag

		[XmlRpcMethod("flag.is_flagged")]
		bool FlagIsFlagged (string flag_name, int content_id, int uid);

		[XmlRpcBegin("flag.is_flagged")]
		IAsyncResult BeginFlagIsFlagged (string flag_name, int content_id, int uid, AsyncCallback callback, object asyncState);

		[XmlRpcMethod("flag.is_flagged")]
		bool FlagIsFlagged (string flag_name, int content_id);

		[XmlRpcBegin("flag.is_flagged")]
		IAsyncResult BeginFlagIsFlagged (string flag_name, int content_id, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("flag.is_flagged")]
		bool EndFlagIsFlagged (IAsyncResult asyncResult);
		
		[XmlRpcMethod("flag.flag")]
		bool FlagFlag (string flag_name, int content_id, string action, int uid, bool skip_permission_check);

		[XmlRpcBegin("flag.flag")]
		IAsyncResult BeginFlagFlag (string flag_name, int content_id, string action, int uid, bool skip_permission_check, AsyncCallback callback, object asyncState);

		[XmlRpcMethod("flag.flag")]
		bool FlagFlag (string flag_name, int content_id, string action, bool skip_permission_check);

		[XmlRpcBegin("flag.flag")]
		IAsyncResult BeginFlagFlag (string flag_name, int content_id, string action, bool skip_permission_check, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("flag.flag")]
		bool EndFlagFlag (IAsyncResult asyncResult);

		[XmlRpcMethod("flag.countall")]
		XmlRpcStruct FlagCountAll (string flag_name, int content_id);

		[XmlRpcBegin("flag.countall")]
		XmlRpcStruct BeginFlagCountAll (string flag_name, int content_id, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("flag.countall")]
		XmlRpcStruct EndFlagCountAll (IAsyncResult asyncResult);

		#endregion

		#region Comment
		
		[XmlRpcMethod("comment.create")]
		XmlRpcStruct CommentCreate (XmlRpcStruct comment);

		[XmlRpcBegin("comment.create")]
		IAsyncResult BeginCommentCreate (XmlRpcStruct comment, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.create")]
		XmlRpcStruct EndCommentCreate (IAsyncResult asyncResult);

		[XmlRpcMethod("comment.retrieve")]
		object CommentRetrieve (int cid);

		[XmlRpcBegin("comment.retrieve")]
		IAsyncResult BeginCommentRetrieve (int cid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.retrieve")]
		object EndCommentRetrieve (IAsyncResult asyncResult);

		[XmlRpcMethod("comment.update")]
		object CommentUpdate (int cid, XmlRpcStruct comment);

		[XmlRpcBegin("comment.update")]
		IAsyncResult BeginCommentUpdate (int cid, XmlRpcStruct comment, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.update")]
		object EndCommentUpdate (IAsyncResult asyncResult);

		[XmlRpcMethod("comment.delete")]
		bool CommentDelete (int cid);

		[XmlRpcBegin("comment.delete")]
		IAsyncResult BeginCommentDelete (int cid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.delete")]
		bool EndCommentDelete (IAsyncResult asyncResult);

		[XmlRpcMethod("comment.index")]
		XmlRpcStruct[] CommentIndex (int page, string fields, XmlRpcStruct[] parameters, int page_size);

		[XmlRpcBegin("comment.index")]
		IAsyncResult BeginCommentIndex (int page, string fields, XmlRpcStruct[] parameters, int page_size, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.index")]
		XmlRpcStruct[] EndCommentIndex (IAsyncResult asyncResult);

		[XmlRpcMethod("comment.countAll")]
		int CommentCountAll (int nid);

		[XmlRpcBegin("comment.countAll")]
		IAsyncResult BeginCommentCountAll (int nid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.countAll")]
		int EndCommentCountAll (IAsyncResult asyncResult);

		[XmlRpcMethod("comment.countNew")]
		int CommentCountNew (int nid, int timestamp);

		[XmlRpcBegin("comment.countNew")]
		IAsyncResult BeginCommentCountNew (int nid, int timestamp, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("comment.countNew")]
		int EndCommentCountNew (IAsyncResult asyncResult);

		#endregion

		#region File

		[XmlRpcMethod("file.create")]
		DrupalFile FileCreate(DrupalFile file);

		[XmlRpcBegin("file.create")]
		IAsyncResult BeginFileCreate(DrupalFile file, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("file.create")]
		DrupalFile EndFileCreate(IAsyncResult asyncResult);

		[XmlRpcMethod("file.retrieve")]
		DrupalFile FileRetrieve(int fid, bool include_file_contents, bool get_image_style);
		
		[XmlRpcBegin("file.retrieve")]
		IAsyncResult BeginFileRetrieve(int fid, bool include_file_contents, bool get_image_style, AsyncCallback callback, object asyncState);
		
		[XmlRpcEnd("file.retrieve")]
		DrupalFile EndFileRetrieve(IAsyncResult asyncResult);

		[XmlRpcMethod("file.delete")]
		bool FileDelete(int fid);

		[XmlRpcBegin("file.delete")]
		IAsyncResult BeginFileDelete(int fid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("file.delete")]
		bool EndFileDelete(IAsyncResult asyncResult);

		[XmlRpcMethod("file.index")]
		DrupalFile[] FileIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
	
		[XmlRpcBegin("file.index")]
		IAsyncResult BeginFileIndex(int page, string fields, XmlRpcStruct parameters, int page_size, AsyncCallback callback, object asyncState);
	
		[XmlRpcEnd("file.index")]
		DrupalFile[] EndFileIndex(IAsyncResult asyncResult);
	
		[XmlRpcMethod("file.create_raw")]
		DrupalFile[] FileCreateRaw();

		[XmlRpcBegin("file.create_raw")]
		IAsyncResult BeginFileCreateRaw(AsyncCallback callback, object asyncState);

		[XmlRpcEnd("file.create_raw")]
		DrupalFile[] EndFileCreateRaw(IAsyncResult asyncResult);
	
		#endregion

		#region Node
		
		[XmlRpcMethod("node.retrieve")]
		XmlRpcStruct NodeRetrieve (int nid);

		[XmlRpcBegin("node.retrieve")]
		IAsyncResult BeginNodeRetrieve(int nid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("node.retrieve")]
		XmlRpcStruct EndNodeRetrieve(IAsyncResult asyncResult);
		
        [XmlRpcMethod("node.create")]
        XmlRpcStruct NodeCreate(XmlRpcStruct node);

        [XmlRpcBegin("node.create")]
        IAsyncResult BeginNodeCreate(XmlRpcStruct node, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("node.create")]
        XmlRpcStruct EndNodeCreate(IAsyncResult asyncResult);

        [XmlRpcMethod("node.update")]
        XmlRpcStruct NodeUpdate(int nid, XmlRpcStruct node);

        [XmlRpcBegin("node.update")]
        IAsyncResult BeginNodeUpdate(int nid, XmlRpcStruct node, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("node.update")]
        XmlRpcStruct EndNodeUpdate(IAsyncResult asyncResult);

		[XmlRpcMethod("node.delete")]
        bool NodeDelete(int nid);

		[XmlRpcBegin("node.delete")]
        IAsyncResult BeginNodeDelete(int nid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("node.delete")]
        bool EndNodeDelete(IAsyncResult asyncResult);

        [XmlRpcMethod("node.index")]
        XmlRpcStruct[] NodeIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
		
		[XmlRpcBegin("node.index")]
		IAsyncResult BeginNodeIndex(int page, string fields, XmlRpcStruct parameters, int page_size, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("node.index")]
		XmlRpcStruct[] EndNodeIndex(IAsyncResult asyncResult);

		[XmlRpcMethod("node.files")]
        DrupalFile[] NodeFiles(int nid, bool include_file_contents, bool get_image_style);

		[XmlRpcBegin("node.files")]
        IAsyncResult BeginNodeFiles(int nid, bool include_file_contents, bool get_image_style, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("node.files")]
        DrupalFile[] EndNodeFiles(IAsyncResult asyncResult);

		#endregion

		#region System

        [XmlRpcMethod("system.connect")]
        DrupalSessionObject SystemConnect();

        [XmlRpcBegin("system.connect")]
        IAsyncResult BeginSystemConnect(AsyncCallback callback, object asyncState);

        [XmlRpcEnd("system.connect")]
        DrupalSessionObject EndSystemConnect(IAsyncResult asyncResult);

        [XmlRpcMethod("system.get_variable")]
        object SystemGetVariable(string name, object @default);

        [XmlRpcBegin("system.get_variable")]
        IAsyncResult BeginSystemGetVariable(string name, object @default, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("system.get_variable")]
        object EndSystemGetVariable(IAsyncResult asyncResult);

        [XmlRpcMethod("system.set_variable")]
        void SystemSetVariable(string name, object @value);
		
        [XmlRpcBegin("system.set_variable")]
        IAsyncResult BeginSystemSetVariable(string name, object @value, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("system.set_variable")]
        void EndSystemSetVariable(IAsyncResult asyncResult);
		
        [XmlRpcMethod("system.del_variable")]
        void SystemDelVariable(string name);
		
        [XmlRpcBegin("system.del_variable")]
        IAsyncResult BeginSystemDelVariable(string name, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("system.del_variable")]
        void EndSystemDelVariable(IAsyncResult asyncResult);
		
		#endregion

		#region TaxonomyTerm

        [XmlRpcMethod("taxonomy_term.retrieve")]
        object TaxonomyTermRetrieve(int tid);
		
        [XmlRpcBegin("taxonomy_term.retrieve")]
        IAsyncResult BeginTaxonomyTermRetrieve(int tid, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("taxonomy_term.retrieve")]
        object EndTaxonomyTermRetrieve(IAsyncResult asyncResult);
		
        [XmlRpcMethod("taxonomy_term.create")]
        int TaxonomyTermCreate(XmlRpcStruct term);

        [XmlRpcBegin("taxonomy_term.create")]
        IAsyncResult BeginTaxonomyTermCreate(XmlRpcStruct term, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("taxonomy_term.create")]
        int EndTaxonomyTermCreate(IAsyncResult asyncResult);

        [XmlRpcMethod("taxonomy_term.update")]
        int TaxonomyTermUpdate(int tid, XmlRpcStruct term);

        [XmlRpcBegin("taxonomy_term.update")]
        IAsyncResult BeginTaxonomyTermUpdate(int tid, XmlRpcStruct term, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("taxonomy_term.update")]
        int EndTaxonomyTermUpdate(IAsyncResult asyncResult);

        [XmlRpcMethod("taxonomy_term.delete")]
        int TaxonomyTermDelete(int tid);

        [XmlRpcBegin("taxonomy_term.delete")]
        IAsyncResult BeginTaxonomyTermDelete(int tid, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("taxonomy_term.delete")]
        int EndTaxonomyTermDelete(IAsyncResult asyncResult);

        [XmlRpcMethod("taxonomy_term.index")]
        XmlRpcStruct[] TaxonomyTermIndex(int page, string fields, XmlRpcStruct parameters, int page_size);

        [XmlRpcBegin("taxonomy_term.index")]
        IAsyncResult BeginTaxonomyTermIndex(int page, string fields, XmlRpcStruct parameters, int page_size, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("taxonomy_term.index")]
        XmlRpcStruct[] EndTaxonomyTermIndex(IAsyncResult asyncResult);

        [XmlRpcMethod("taxonomy_term.selectNodes")]
        XmlRpcStruct[] TaxonomyTermSelectNodes(int tid, bool pager, bool limit, XmlRpcStruct order);
		
        [XmlRpcBegin("taxonomy_term.selectNodes")]
        IAsyncResult BeginTaxonomyTermSelectNodes(int tid, bool pager, bool limit, XmlRpcStruct order, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("taxonomy_term.selectNodes")]
        XmlRpcStruct[] EndTaxonomyTermSelectNodes(IAsyncResult asyncResult);
		
		#endregion

		#region TaxonomyVocabulary
		
        [XmlRpcMethod("taxonomy_vocabulary.retrieve")]
        XmlRpcStruct TaxonomyVocabularyRetrieve(int vid);

        [XmlRpcBegin("taxonomy_vocabulary.retrieve")]
        IAsyncResult BeginTaxonomyVocabularyRetrieve(int vid, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("taxonomy_vocabulary.retrieve")]
        XmlRpcStruct EndTaxonomyVocabularyRetrieve(IAsyncResult asyncResult);

        [XmlRpcMethod("taxonomy_vocabulary.create")]
        int TaxonomyVocabularyCreate(XmlRpcStruct vocabulary);
		
        [XmlRpcBegin("taxonomy_vocabulary.create")]
        IAsyncResult BeginTaxonomyVocabularyCreate(XmlRpcStruct vocabulary, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("taxonomy_vocabulary.create")]
        int EndTaxonomyVocabularyCreate(IAsyncResult asyncResult);

		[XmlRpcMethod("taxonomy_vocabulary.update")]
        int TaxonomyVocabularyUpdate(int vid, XmlRpcStruct vocabulary);

		[XmlRpcBegin("taxonomy_vocabulary.update")]
        IAsyncResult BeginTaxonomyVocabularyUpdate(int vid, XmlRpcStruct vocabulary, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("taxonomy_vocabulary.update")]
        int EndTaxonomyVocabularyUpdate(IAsyncResult asyncResult);

        [XmlRpcMethod("taxonomy_vocabulary.delete")]
        int TaxonomyVocabularyDelete(int vid);
		
        [XmlRpcBegin("taxonomy_vocabulary.delete")]
        IAsyncResult BeginTaxonomyVocabularyDelete(int vid, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("taxonomy_vocabulary.delete")]
        int EndTaxonomyVocabularyDelete(IAsyncResult asyncResult);
		
        [XmlRpcMethod("taxonomy_vocabulary.index")]
        XmlRpcStruct[] TaxonomyVocabularyIndex(int page, string fields, XmlRpcStruct parameters, int page_size);
		
        [XmlRpcBegin("taxonomy_vocabulary.index")]
        IAsyncResult BeginTaxonomyVocabularyIndex(int page, string fields, XmlRpcStruct parameters, int page_size, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("taxonomy_vocabulary.index")]
        XmlRpcStruct[] EndTaxonomyVocabularyIndex(IAsyncResult asyncResult);
		
        [XmlRpcMethod("taxonomy_vocabulary.getTree")]
        XmlRpcStruct[] TaxonomyVocabularyGetTree(int vid, int parent, int max_depth);

        [XmlRpcBegin("taxonomy_vocabulary.getTree")]
        IAsyncResult BeginTaxonomyVocabularyGetTree(int vid, int parent, int max_depth, AsyncCallback callback, object asyncState);

        [XmlRpcEnd("taxonomy_vocabulary.getTree")]
        XmlRpcStruct[] EndTaxonomyVocabularyGetTree(IAsyncResult asyncResult);

		#endregion
		
		#region User

		[XmlRpcMethod("user.retrieve")]
        DrupalUser UserRetrieve(int uid);
		
		[XmlRpcBegin("user.retrieve")]
        IAsyncResult BeginUserRetrieve(int uid, AsyncCallback callback, object asyncState);
		
		[XmlRpcEnd("user.retrieve")]
        DrupalUser EndUserRetrieve(IAsyncResult asyncResult);
		
		[XmlRpcMethod("user.create")]
        DrupalUser UserCreate(XmlRpcStruct account);
		
		[XmlRpcBegin("user.create")]
        IAsyncResult BeginUserCreate(XmlRpcStruct account, AsyncCallback callback, object asyncState);
		
		[XmlRpcEnd("user.create")]
        DrupalUser EndUserCreate(IAsyncResult asyncResult);
		
		[XmlRpcMethod("user.update")]
        DrupalUser UserUpdate(int uid, XmlRpcStruct account);
		
		[XmlRpcBegin("user.update")]
        IAsyncResult BeginUserUpdate(int uid, XmlRpcStruct account, AsyncCallback callback, object asyncState);
		
		[XmlRpcEnd("user.update")]
        DrupalUser EndUserUpdate(IAsyncResult asyncResult);
		
		[XmlRpcMethod("user.delete")]
        bool UserDelete(int uid);

		[XmlRpcBegin("user.delete")]
        IAsyncResult BeginUserDelete(int uid, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("user.delete")]
        bool EndUserDelete(IAsyncResult asyncResult);

		[XmlRpcMethod("user.index")]
        DrupalUser[] UserIndex(int page, string fields, XmlRpcStruct parameters, int page_size);

		[XmlRpcBegin("user.index")]
        IAsyncResult BeginUserIndex(int page, string fields, XmlRpcStruct parameters, int page_size, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("user.index")]
        DrupalUser[] EndUserIndex(IAsyncResult asyncResult);

        [XmlRpcMethod("user.login")]
        DrupalSessionObject UserLogin(string username, string password);
		
        [XmlRpcBegin("user.login")]
        IAsyncResult BeginUserLogin(string username, string password, AsyncCallback callback, object asyncState);
		
        [XmlRpcEnd("user.login")]
        DrupalSessionObject EndUserLogin(IAsyncResult asyncResult);
		
		[XmlRpcMethod("user.logout")]
        bool UserLogout();

		[XmlRpcBegin("user.logout")]
        IAsyncResult BeginUserLogout(AsyncCallback callback, object asyncState);

		[XmlRpcEnd("user.logout")]
        bool EndUserLogout(IAsyncResult asyncResult);

		[XmlRpcMethod("user.register")]
        DrupalUser UserRegister(XmlRpcStruct account);

		[XmlRpcBegin("user.register")]
        IAsyncResult BeginUserRegister(XmlRpcStruct account, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("user.register")]
        DrupalUser EndUserRegister(IAsyncResult asyncResult);

		#endregion

		#region Menu
		
		[XmlRpcMethod("menu.retrieve")]
        object MenuRetrieve(string menu_name);
		
		[XmlRpcBegin("menu.retrieve")]
        IAsyncResult BeginMenuRetrieve(string menu_name, AsyncCallback callback, object asyncState);
		
		[XmlRpcEnd("menu.retrieve")]
        object EndMenuRetrieve(IAsyncResult asyncResult);
		
		#endregion

		#region Views

		[XmlRpcMethod("views.retrieve")]
        XmlRpcStruct ViewsRetrieve(string view_name, string display_id, XmlRpcStruct args, int offset, int limit, object return_type, XmlRpcStruct filters);

		[XmlRpcBegin("views.retrieve")]
        IAsyncResult BeginViewsRetrieve(string view_name, string display_id, XmlRpcStruct args, int offset, int limit, object return_type, XmlRpcStruct filters, AsyncCallback callback, object asyncState);

		[XmlRpcEnd("views.retrieve")]
        XmlRpcStruct EndViewsRetrieve(IAsyncResult asyncResult);

		#endregion
	}
}
