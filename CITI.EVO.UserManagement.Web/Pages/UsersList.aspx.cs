using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Utils;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Svc.Contracts;
using CITI.EVO.UserManagement.Web.Bases;
using CITI.EVO.UserManagement.Web.Enums;
using CITI.EVO.UserManagement.Web.Extensions;
using CITI.EVO.UserManagement.Web.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Web.UI.Controls;
using Button = System.Web.UI.WebControls.Button;
using ASPxComboBox = DevExpress.Web.ASPxComboBox;

public partial class Pages_UsersList : BasePage
{

	#region LocalEntities

	public class LocalUserEntity
	{
		public Guid ID { get; set; }

		public String LoginName { get; set; }

		public String Password { get; set; }

		public String FirstName { get; set; }

		public String LastName { get; set; }

		public String Email { get; set; }

		public String Address { get; set; }

		public bool IsSuperAdmin { get; set; }

		public bool IsActive { get; set; }

		public Guid? UserCategoryID { get; set; }

		public String UserCategoryName { get; set; }

		public DateTime? PasswordExpirationDate { get; set; }

		public ISet<Guid> BranchesID { get; set; }
		public String BranchesName { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime? DateChanged { get; set; }

		public DateTime? DateDeleted { get; set; }

		public string PersonPersonalID { get; set; }
		public string PersonFirstName { get; set; }
		public string PersonLastName { get; set; }
	}

	#endregion

	#region properties

	private const String userIdKey = "$_userId_$";

	public Guid? UserId
	{
		get { return ViewState[userIdKey] as Guid?; }
		set { ViewState[userIdKey] = value; }
	}

	#endregion

	#region Events

	protected void Page_Load(object sender, EventArgs e)
	{
		ApplyPermissions();

		FillUserCategories();
		FillUserGrid();
	}

	protected void chkChangePassword_CheckChanged(object sender, EventArgs e)
	{
		if (chkChangePassword.Checked)
		{
			var user = DataContext.UM_Users.FirstOrDefault(u => u.ID == UserId);
			if (user == null)
			{
				return;
			}

			tbPassword.Enabled = true;
			tbPassword.Text = user.Password;
		}
		else
		{
			tbPassword.Enabled = false;
			tbPassword.Text = String.Empty;
		}

		tlGroups.Visible = false;
		UpdatePanelUsers.Update();
		mpeUserForm.Show();

	}

	protected void btnAddUser_Click(object sender, EventArgs e)
	{
		ResetFields();

		tbPassword.Enabled = true;
		tbPassword.TextMode = TextBoxMode.Password;
		chkChangePassword.Visible = false;
		tbPassword.Attributes.Add("value", "resetpass");
		chkActivate.Checked = true;
		lblUserContext.Text = "მომხმარებლის დამატება";
		tbPasswordExpirationDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
		UpdatePanelUsers.Update();
		mpeUserForm.Show();
	}

	protected void btnSave_Click(object sender, EventArgs e)
	{
		//droebit chavakomenatre velebis shesavsebi validacia
		//if (!checkFields())
		//{
		//    return;
		//}

		if (UserId == null)
		{
			var existUser = ValidateUser(tbLoginName.Text);
			if (!existUser)
			{
				AddUser();
			}
			else
			{
				lblError.Text = "მომხმარებელი უკვე არსებობს";
				lblError.ForeColor = System.Drawing.Color.Red;
			}
		}
		else
		{
			EditUser(UserId.Value);
		}

		FillUserGrid();

	}

	protected void lnkEdit_Click(object sender, EventArgs e)
	{

		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
		{
			return;
		}

		Guid userID;
		if (!Guid.TryParse(lnkBtn.CommandArgument, out userID))
		{
			return;
		}

		if (FillUsersForm(userID))
		{

			UpdatePanelUsers.Update();
			mpeUserForm.Show();
		}


	}

	protected void lnkDelete_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
		{
			return;
		}

		Guid userID;
		if (!Guid.TryParse(lnkBtn.CommandArgument, out userID))
		{
			return;
		}

		var user = DataContext.UM_Users.FirstOrDefault(n => n.ID == userID);
		if (user == null)
		{
			return;
		}

		user.DateDeleted = DateTime.Now;
		foreach (var gu in user.GroupUsers)
		{
			gu.DateDeleted = DateTime.Now;
		}

		DataContext.SubmitChanges();

		FillUserGrid();

		ResetFields();
	}

	protected void lnkAttributes_Click(object sender, EventArgs e)
	{

		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
		{
			return;
		}

		Guid userID;
		if (!Guid.TryParse(lnkBtn.CommandArgument, out userID))
		{
			return;
		}

		var projects = DataContext.UM_Projects.Where(n => n.DateDeleted == null).ToList();
		projects.Insert(0, new UM_Project { Name = "Global" });

		if (lnkBtn.CommandName == "AddAttributes")
		{
			tbUserAttributesValue.Text = String.Empty;

			cmbProject.SelectedItem = null;
			cmbProject.Enabled = true;

			cmbProject.DataSource = projects;
			cmbProject.DataBind();

			cmbAttributeSchemas.SelectedItem = null;
			cmbAttributeSchemas.Enabled = false;

			cmbAttributeSchemaNodes.SelectedItem = null;
			cmbAttributeSchemaNodes.Enabled = false;

			UserId = userID;

			upnlUserAttributes.Update();
			mpeUserAttributes.Show();
		}
		else if (lnkBtn.CommandName == "ShowAttributes")
		{
			dwAttributeSchemaNodes.DataSource = null;
			dwAttributeSchemaNodes.DataBind();

			cmbShowProjects.SelectedItem = null;

			cmbShowProjects.DataSource = projects;
			cmbShowProjects.DataBind();

			cmbShowAttributeSchemas.SelectedItem = null;
			cmbShowAttributeSchemas.Enabled = false;

			UserId = userID;

			upnlShowUserAttributes.Update();
			mpeShowUserAttributes.Show();
		}
	}

	protected void btUserAttributesOK_Click(object sender, EventArgs eventArgs)
	{

		Guid nodeID;
		Guid.TryParse(cmbAttributeSchemaNodes.SelectedItem.Value.ToString(), out nodeID);

		var userAttribute = GetRelatedAttributes();
		if (userAttribute != null)
		{
			userAttribute.DateDeleted = DateTime.Now;
		}

		var newUserAttribute = new UM_UserAttribute
		{
			ID = Guid.NewGuid(),
			DateCreated = DateTime.Now,
			UserID = UserId.Value,
			AttributesSchemaNodeID = nodeID,
			Value = tbUserAttributesValue.Text
		};

		DataContext.UM_UserAttributes.InsertOnSubmit(newUserAttribute);

		DataContext.SubmitChanges();
	}


	protected void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		var drList = sender as ASPxComboBox;
		if (drList == null)
		{
			return;
		}

		if (drList.ID == "cmbProject")
		{
			var projectID = DataConverter.ToNullableGuid(cmbProject.SelectedItem.Value.ToString());

			var query = from n in DataContext.UM_AttributesSchemas
						where n.DateDeleted == null
						select n;

			if (projectID == Guid.Empty)
			{
				query = from n in query
						where n.ProjectID == null
						select n;
			}
			else
			{
				query = from n in query
						where n.ProjectID == projectID
						select n;
			}

			var attributeSchemas = query.ToList();

			cmbAttributeSchemas.SelectedItem = null;
			cmbAttributeSchemas.DataSource = attributeSchemas;
			cmbAttributeSchemas.DataBind();

			if (attributeSchemas.Count > 0)
			{
				cmbAttributeSchemas.Enabled = true;
			}
			else
			{
				cmbAttributeSchemas.Enabled = false;
				cmbAttributeSchemaNodes.Enabled = false;
			}


			upnlUserAttributes.Update();
			mpeUserAttributes.Show();
		}

		else if (drList.ID == "cmbShowProjects")
		{
			var projectID = DataConverter.ToNullableGuid(cmbShowProjects.SelectedItem.Value);

			var query = from n in DataContext.UM_AttributesSchemas
						where n.DateDeleted == null
						select n;

			if (projectID == Guid.Empty)
			{
				query = from n in query
						where n.ProjectID == null
						select n;
			}
			else
			{
				query = from n in query
						where n.ProjectID == projectID
						select n;
			}

			var attributeSchemas = query.ToList();

			cmbShowAttributeSchemas.SelectedItem = null;
			cmbShowAttributeSchemas.DataSource = attributeSchemas;
			cmbShowAttributeSchemas.DataBind();

			if (attributeSchemas.Count > 0)
			{
				cmbShowAttributeSchemas.Enabled = true;
			}
			else
			{
				cmbShowAttributeSchemas.Enabled = false;

			}

			upnlShowUserAttributes.Update();
			mpeShowUserAttributes.Show();
		}
	}

	protected void cmbAttributeSchemas_SelectedIndexChanged(object sender, EventArgs e)
	{
		var drList = sender as ASPxComboBox;
		if (drList == null)
		{
			return;
		}

		if (drList.ID == "cmbAttributeSchemas")
		{
			var attributesSchemaID = DataConverter.ToNullableGuid(cmbAttributeSchemas.SelectedItem.Value.ToString());

			var attributeSchemaNodes = (from n in DataContext.UM_AttributesSchemaNodes
										where n.DateDeleted == null &&
											  n.AttributesSchemaID == attributesSchemaID
										select n).ToList();

			cmbAttributeSchemaNodes.SelectedItem = null;
			cmbAttributeSchemaNodes.DataSource = attributeSchemaNodes;
			cmbAttributeSchemaNodes.DataBind();

			if (attributeSchemaNodes.Count > 0)
			{
				cmbAttributeSchemaNodes.Enabled = true;
			}
			else
			{
				cmbAttributeSchemaNodes.Enabled = false;
			}

			upnlUserAttributes.Update();
			mpeUserAttributes.Show();
		}
		else if (drList.ID == "cmbShowAttributeSchemas")
		{
			var attrSchemaID = Guid.Parse(cmbShowAttributeSchemas.SelectedItem.Value.ToString());

			var attrSchemaNodeValues = (from an in DataContext.UM_AttributesSchemaNodes
										join ua in DataContext.UM_UserAttributes on an.ID equals ua.AttributesSchemaNodeID
										where an.DateDeleted == null &&
											  ua.UserID == UserId &&
											  ua.DateDeleted == null &&
											  an.AttributesSchemaID == attrSchemaID
										select new { an.Name, ua.Value }).ToList();

			dwAttributeSchemaNodes.DataSource = attrSchemaNodeValues;
			dwAttributeSchemaNodes.DataBind();

			upnlShowUserAttributes.Update();
			mpeShowUserAttributes.Show();
		}
	}

	protected void cmbAttributeSchemaNodes_SelectedIndexChanged(object sender, EventArgs e)
	{
		var relatedAttributes = GetRelatedAttributes();
		if (relatedAttributes != null)
		{
			tbUserAttributesValue.Text = relatedAttributes.Value;
		}
		else
		{
			tbUserAttributesValue.Text = String.Empty;
		}


		upnlUserAttributes.Update();
		mpeUserAttributes.Show();

	}

	//protected void ddlRecordCount_SelectedIndexChanged(object sender, EventArgs e)
	//{
	//    var pageSize = ddlRecordCount.SelectedItem.Value.ToString();
	//    gvUsers.SettingsPager.PageSize = Convert.ToInt32(pageSize);

	//    FillUserGrid();
	//}

	protected void btnBindData_Click(object sender, EventArgs e)
	{
		FillUserGrid();
	}

	protected void lnkView_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
		{
			return;
		}

		Guid userID;
		if (!Guid.TryParse(lnkBtn.CommandArgument, out userID))
		{
			return;
		}

		if (FillUserViewForm(userID))
		{

			UpdatePanelUserView.Update();
			mpeUserView.Show();
		}


	}

	protected void tlGroups_VirtualModeCreateChildren(object sender, TreeListVirtualModeCreateChildrenEventArgs e)
	{
		var nodeKeyObject = e.NodeObject as NodeKeyObject;
		if (nodeKeyObject == null)
		{
			var query = (from n in CacheEntitiesUtil.UmProjects.Values
						 where n.DateDeleted == null
						 select NodeKeyObject.CreateForProject(n));


			var lookup = query.ToLookup(n => n.ToString());
			var projects = lookup.Select(n => n.First()).ToList();

			e.Children = projects;
			return;
		}

		if (nodeKeyObject.IsProjectType())
		{
			var query = from n in CacheEntitiesUtil.UmGroups.Values
						let project = CacheEntitiesUtil.UmProjects.GetValueOrDefault(n.ProjectID)
						where n.DateDeleted == null && n.ProjectID == nodeKeyObject.ProjectID
						let parentGroup = GetParent(n)
						select NodeKeyObject.CreateForGroup(project, parentGroup);

			var lookup = query.ToLookup(n => n.ToString());
			var groups = lookup.Select(n => n.First()).ToList();

			e.Children = groups;
		}
		else if (nodeKeyObject.IsGroupType() || nodeKeyObject.IsChildType())
		{


			var queryChildren = from n in CacheEntitiesUtil.UmGroups.Values
								let project = CacheEntitiesUtil.UmProjects.GetValueOrDefault(n.ProjectID)
								where n.DateDeleted == null &&
									  n.ProjectID == nodeKeyObject.ProjectID &&
									  n.ParentID == nodeKeyObject.GroupID
								select NodeKeyObject.CreateForGroup(project, n, n.ParentID.GetValueOrDefault());

			var lookupChildren = queryChildren.ToLookup(n => n.ToString());
			var children = lookupChildren.Select(n => n.First()).ToList();

			e.Children = children;

		}

		else if (nodeKeyObject.IsUserType())
		{
			return;
		}
	}

	protected void tlGroups_VirtualModeNodeCreating(object sender, TreeListVirtualModeNodeCreatingEventArgs e)
	{
		var nodeKeyObject = e.NodeObject as NodeKeyObject;
		if (nodeKeyObject == null)
		{
			return;
		}
		if (nodeKeyObject.IsProjectType())
		{
			var item = nodeKeyObject.Project;

			if (item == null)
			{
				return;
			}


			e.IsLeaf = !CacheEntitiesUtil.UmGroupsByProject.ContainsKey(item.ID);
			e.NodeKeyValue = nodeKeyObject.ToString();
			e.SetNodeValue("ID", e.NodeKeyValue);
			e.SetNodeValue("Name", item.Name);
			e.SetNodeValue("DateCreated", item.DateCreated);
			e.SetNodeValue("DateChanged", item.DateChanged);
			e.SetNodeValue("DateDeleted", item.DateDeleted);

		}
		else if (nodeKeyObject.IsGroupType() || nodeKeyObject.IsChildType())
		{
			var item = nodeKeyObject.Group;

			if (item == null)
			{
				return;
			}

			e.IsLeaf = !CacheEntitiesUtil.UmGroupUsersByGroup.ContainsKey(item.ID) &&
					   !CacheEntitiesUtil.UmGroupsByParent.ContainsKey(item.ID);
			e.NodeKeyValue = nodeKeyObject.ToString();
			e.SetNodeValue("ID", e.NodeKeyValue);
			e.SetNodeValue("Name", item.Name);
			e.SetNodeValue("DateCreated", item.DateCreated);
			e.SetNodeValue("DateChanged", item.DateChanged);
			e.SetNodeValue("DateDeleted", item.DateDeleted);

		}
	}

	protected void tlGroups_VirtualModeNodeCreated(object sender, TreeListVirtualNodeEventArgs e)
	{
		var nodeKeyObject = NodeKeyObject.Parse(e.NodeObject);
		if (nodeKeyObject == null)
		{
			return;
		}

		if (nodeKeyObject.IsProjectType())
		{
			e.Node.AllowSelect = false;
		}
	}

	protected void Page_LoadComplete(object sender, EventArgs e)
	{

	}

	protected void lnkAddMessage_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as Button;
		if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
		{
			return;
		}

		Guid userId;
		if (!Guid.TryParse(lnkBtn.CommandArgument, out userId))
		{
			return;
		}

		ucMessage.ObjectId = userId;
		ucMessage.Show();
		ucMessage.Update();
	}

	#endregion

	#region Methods

	protected UM_UserAttribute GetRelatedAttributes()
	{
		var attrNodeID = Guid.Parse(cmbAttributeSchemaNodes.SelectedItem.Value.ToString());

		var attributeValue = (from v in DataContext.UM_UserAttributes
							  where v.UserID == UserId.Value &&
									v.AttributesSchemaNodeID == attrNodeID &&
									v.DateDeleted == null
							  select v).FirstOrDefault();

		if (attributeValue == null)
		{
			return null;
		}

		return attributeValue;

	}

	protected UM_User InitializeUser
		(
		String loginName,
		String password,
		String firstName,
		String lastName,
		String email,
		String address,
		DateTime passwordExpirationDate,
		bool isActive
		)
	{
		return new UM_User
		{
			ID = Guid.NewGuid(),
			DateCreated = DateTime.Now,
			LoginName = loginName,
			Password = password,
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			Address = address,
			PasswordExpirationDate = passwordExpirationDate,
			IsActive = isActive

		};
	}

	protected void AddUser()
	{
		var loginName = tbLoginName.Text.Trim();
		var password = tbPassword.Text.Trim();
		var firstName = tbFirstName.Text.Trim();
		var lastName = tbLastName.Text.Trim();
		var email = tbEmail.Text.Trim();
		var address = tbAddress.Text.Trim();
		var passwordExpirationDate = DataConverter.ToDateTime(tbPasswordExpirationDate.Text);
		var activate = chkActivate.Checked;

		var user = InitializeUser
			(
				loginName,
				password,
				firstName,
				lastName,
				email,
				address,
				passwordExpirationDate,
				activate
			);

		DataContext.UM_Users.InsertOnSubmit(user);

		var nodes = tlGroups.GetSelectedNodes();

		foreach (var treeListNode in nodes)
		{
			var nodeObject = treeListNode.Key;
			var group = NodeKeyObject.Parse(nodeObject);

			var groupUser = new UM_GroupUser
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
				GroupID = @group.GroupID,
				UserID = user.ID,
				AccessLevel = (int)Enum.Parse(typeof(AccessLevelEnum), ddlAccessLevels.SelectedItem.Value.ToString())
			};

			user.GroupUsers.Add(groupUser);

		}

		try
		{
			DataContext.SubmitChanges();
		}
		catch (Exception e)
		{
			Response.Write(e.Message);
		}

		ResetFields();
	}

	protected void EditUser(Guid userID)
	{
		var user = (from n in DataContext.UM_Users
					where n.ID == userID
					select n).First();

		user.LoginName = tbLoginName.Text;
		user.FirstName = tbFirstName.Text;
		user.LastName = tbLastName.Text;
		user.Email = tbEmail.Text;
		user.Address = tbAddress.Text;
		user.IsActive = chkActivate.Checked;
		user.PasswordExpirationDate = Convert.ToDateTime(tbPasswordExpirationDate.Text);

		if (chkChangePassword.Checked)
		{
			user.Password = tbPassword.Text;
		}

		try
		{
			DataContext.SubmitChanges();
		}
		catch (Exception e)
		{
			Response.Write(e.Message);
		}

		ResetFields();
	}

	protected bool FillUsersForm(Guid userID)
	{
		var user = DataContext.UM_Users.FirstOrDefault(n => n.ID == userID);
		if (user == null)
		{
			return false;
		}

		UserId = user.ID;

		lblUserContext.Text = "მომხმარებლის რედაქტირება";
		//hdUserID.Value = Convert.ToString(user.ID);
		tbLoginName.Text = user.LoginName;
		tbFirstName.Text = user.FirstName;
		tbLastName.Text = user.LastName;
		tbEmail.Text = user.Email;
		tbAddress.Text = user.Address;
		chkActivate.Checked = user.IsActive;

		tbPassword.Enabled = false;
		tbPassword.TextMode = TextBoxMode.SingleLine;
		tbPassword.Text = String.Empty;
		chkChangePassword.Visible = true;
		chkChangePassword.Checked = false;
		tbPasswordExpirationDate.Text = DataConverter.ToDateTime(user.PasswordExpirationDate).ToString("dd.MM.yyyy");
		tlGroups.Visible = false;

		pnlAccessLevel.Visible = false;

		return true;
	}

	protected bool FillUserViewForm(Guid userID)
	{
		var user = DataContext.UM_Users.FirstOrDefault(n => n.ID == userID);
		if (User == null)
		{
			return false;
		}

		var groupsLookup = (from g in DataContext.UM_Groups
							join ug in DataContext.UM_GroupUsers on g.ID equals ug.GroupID
							where ug.DateDeleted == null
							join p in DataContext.UM_Projects on g.ProjectID equals p.ID
							where ug.User == user
							group g by g.Project
			into grouping
							select grouping);


		lstUserGroups.Items.Clear();

		foreach (var @group in groupsLookup)
		{
			var projectListItem = new ListEditItem(@group.Key.Name, @group.Key.ID);
			lstUserGroups.Items.Add(projectListItem);

			foreach (var umGroup in @group)
			{
				var text = String.Format("{0,15}", umGroup.Name);
				var groupListItem = new ListEditItem(text, umGroup.ID);

				lstUserGroups.Items.Add(groupListItem);
			}

			lstUserGroups.Items.Add(String.Empty);
		}

		lblUserViewContext.Text = "მომხმარებლის ნახვა";
		lbLoginName.Text = user.LoginName;
		lbFirstName.Text = user.FirstName;
		lbLastName.Text = user.LastName;
		lbEmail.Text = user.Email;
		lbAddress.Text = user.Address;
		lbPasswordExpirationDate.Text = String.Format("{0:dd.MM.yyyy}", user.PasswordExpirationDate);
		chkStatus.Checked = user.IsActive;

		return true;
	}

	protected bool ValidateUser(string loginName)
	{
		var item = (from n in DataContext.UM_Users
					where n.LoginName.ToLower() == loginName.ToLower() &&
						  n.DateDeleted == null
					select n.ID).FirstOrDefault();

		return item != Guid.Empty;
	}

	protected void ResetFields()
	{
		tbLoginName.Text = String.Empty;
		tbPassword.Text = String.Empty;
		tbFirstName.Text = String.Empty;
		tbLastName.Text = String.Empty;
		tbEmail.Text = String.Empty;
		tbAddress.Text = String.Empty;
		chkActivate.Checked = false;
		pnlAccessLevel.Visible = true;
		lblError.Text = String.Empty;

		UserId = null;
	}

	protected bool CheckFields()
	{
		var errText = String.Empty;

		if (String.IsNullOrWhiteSpace(tbLoginName.Text))
		{
			errText = "მომხმარებლის სახელი";
		}

		if (String.IsNullOrWhiteSpace(tbFirstName.Text))
		{
			errText += ", სახელი";
		}
		if (String.IsNullOrWhiteSpace(tbLastName.Text))
		{
			errText += ", გვარი";
		}

		if (String.IsNullOrWhiteSpace(tbEmail.Text))
		{
			errText += ", ელ-ფოსტა";
		}

		if (String.IsNullOrWhiteSpace(tbPasswordExpirationDate.Text))
		{
			errText += ", პაროლის ვალიდურობის თარიღი";
		}

		if (String.IsNullOrWhiteSpace(tbAddress.Text))
		{
			errText += ", მისამართი";
		}

		if (String.IsNullOrWhiteSpace(errText))
		{
			return true;
		}

		lblError.Text = String.Format("შეავსეთ შემდეგი ველები: {0} !", errText);
		lblError.ForeColor = System.Drawing.Color.Red;

		return false;
	}

	protected void FillUserGrid()
	{
		var keyword = txtVariousFilter.Text.Trim();

		var status = ddlStatues.SelectedItem.Value.ToString();
		var categoryID = DataConverter.ToNullableGuid(ddlUserCategories.SelectedItem.Value.ToString());

		var result = (from ent in LoadUsers(status, categoryID)
					  where FillAndFilterEntity(ent, keyword)
					  orderby ent.DateCreated descending,
						  ent.LastName descending,
						  ent.FirstName descending
					  select ent).ToList();

		gvUsers.DataSource = result;
		gvUsers.DataBind();
	}

	protected IEnumerable<LocalUserEntity> LoadUsers(String status, Guid? categoryID)
	{
		var usersQuery = (from n in DataContext.UM_Users
						  where n.DateDeleted == null
						  select n);

		if (status != "-1" && categoryID == Guid.Empty)
		{
			usersQuery = (from n in usersQuery
						  where n.IsActive == bool.Parse(status)
						  select n);
		}
		else if (status != "-1" && categoryID != Guid.Empty)
		{
			usersQuery = (from n in usersQuery
						  where n.IsActive == bool.Parse(status) &&
								n.UserCategoryID == categoryID
						  select n);
		}
		else if (categoryID != Guid.Empty && status == "-1")
		{
			usersQuery = (from n in usersQuery
						  where n.UserCategoryID == categoryID
						  select n);
		}

		var userCategoriesQuery = (from n in usersQuery
								   where n.UserCategory != null
								   select n.UserCategory).Distinct();

		var userCategoriesDict = userCategoriesQuery.ToDictionary(n => n.ID);

		var userList = usersQuery.ToList();
		foreach (var user in userList)
		{
			var entity = new LocalUserEntity
			{
				ID = user.ID,
				DateChanged = user.DateChanged,
				DateCreated = user.DateCreated,
				DateDeleted = user.DateDeleted,
				LoginName = user.LoginName,
				Password = user.Password,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Address = user.Address,
				IsActive = user.IsActive,
				UserCategoryID = user.UserCategoryID,
				PasswordExpirationDate = user.PasswordExpirationDate,
			};

			var userCat = userCategoriesDict.GetValueOrDefault(user.UserCategoryID.GetValueOrDefault());
			if (userCat != null)
				entity.UserCategoryName = userCat.Name;

			yield return entity;
		}
	}

	protected ILookup<String, String> GetArrtibuteLookup(IEnumerable<UM_UserAttribute> userAttributes, IDictionary<Guid, UM_AttributesSchemaNode> attributeNodesDict)
	{
		var query = from n in userAttributes
					let m = attributeNodesDict.GetValueOrDefault(n.AttributesSchemaNodeID)
					where !String.IsNullOrWhiteSpace(n.Value)
					select new
					{
						m.Name,
						n.Value
					};

		var lookup = query.ToLookup(n => n.Name, n => n.Value, StringComparer.OrdinalIgnoreCase);
		return lookup;
	}

	protected Guid? GetUserPerson(IEnumerable<String> attributes)
	{
		var @set = new HashSet<String>(attributes);
		var value = @set.FirstOrDefault();

		return DataConverter.ToNullableGuid(value);
	}

	protected bool FillAndFilterEntity(LocalUserEntity ent, String keyword)
	{
		if (ent == null)
		{
			return true;
		}

		if (!String.IsNullOrWhiteSpace(keyword) &&
			(cbUserName.Checked ||
			 cbPassword.Checked ||
			 cbFirstNameFilter.Checked ||
			 cbLastNameFilter.Checked ||
			 cbEmail.Checked ||
			 cbAddress.Checked))
		{
			var flag = (cbUserName.Checked && !String.IsNullOrWhiteSpace(ent.LoginName) && ent.LoginName.Contains(keyword)) ||
					   (cbPassword.Checked && !String.IsNullOrWhiteSpace(ent.Password) && ent.Password.Contains(keyword)) ||
					   (cbFirstNameFilter.Checked && !String.IsNullOrWhiteSpace(ent.FirstName) && ent.FirstName.Contains(keyword)) ||
					   (cbLastNameFilter.Checked && !String.IsNullOrWhiteSpace(ent.LastName) && ent.LastName.Contains(keyword)) ||
					   (cbEmail.Checked && !String.IsNullOrWhiteSpace(ent.Email) && ent.Email == keyword) ||
					   (cbAddress.Checked && !String.IsNullOrWhiteSpace(ent.Address) && ent.Address.Contains(keyword));


			return flag;
		}

		return true;
	}


	protected GroupContract GetParent(GroupContract group)
	{
		var dbGroup = DataContext.UM_Groups.Single(n => n.ID == group.ID);

		while (dbGroup.ParentID != null)
		{
			dbGroup = dbGroup.Parent;
		}

		return dbGroup.ToContract();
	}

	protected UM_Group GetRootGroup(UM_Group upperLevelGroup, UM_Group group)
	{
		while (group.Parent != null && group.ID != upperLevelGroup.ID)
		{
			group = group.Parent;
		}

		return group;
	}

	protected void SetUserGlobalAttribute(Guid userID, String attributeName, String value)
	{
		var globalAttr = (from n in DataContext.UM_Users
						  where n.ID == userID
						  from m in n.UserAttributes
						  where m.DateDeleted == null
						  let node = m.AttributesSchemaNode
						  let cat = node.AttributeCategory
						  let type = node.AttributeType
						  where node != null &&
								cat != null &&
								type != null &&
								cat.ProjectID == null &&
								type.ProjectID == null &&
								node.Name == attributeName
						  select m).FirstOrDefault();

		if (globalAttr == null)
		{
			globalAttr = new UM_UserAttribute
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
				UserID = userID,
			};

			DataContext.UM_UserAttributes.InsertOnSubmit(globalAttr);

			var globalAttrNode = (from n in DataContext.UM_AttributesSchemaNodes
								  where n.Name == attributeName &&
										n.DateDeleted == null
								  let cat = n.AttributeCategory
								  let type = n.AttributeType
								  where cat != null &&
										type != null &&
										cat.ProjectID == null &&
										type.ProjectID == null
								  select n).SingleOrDefault();

			if (globalAttrNode == null)
			{
				var globalSchema = (from n in DataContext.UM_AttributesSchemas
									where n.DateDeleted == null &&
										  n.ProjectID == null
									select n).Single();

				var globalType = (from n in DataContext.UM_AttributeTypes
								  where n.DateDeleted == null &&
										n.ProjectID == null
								  select n).Single();

				var globalCategory = (from n in DataContext.UM_AttributeCategories
									  where n.DateDeleted == null &&
											n.ProjectID == null
									  select n).Single();

				globalAttrNode = new UM_AttributesSchemaNode
				{
					ID = Guid.NewGuid(),
					Name = attributeName,
					DateCreated = DateTime.Now,
					AttributeType = globalType,
					AttributeCategory = globalCategory,
					AttributesSchema = globalSchema,
				};
			}

			globalAttr.AttributesSchemaNode = globalAttrNode;
		}

		globalAttr.Value = value;

		DataContext.SubmitChanges();
	}

	protected String GetUserGlobalAttribute(Guid userID, String attributeName)
	{
		var globalAttr = (from n in DataContext.UM_Users
						  where n.ID == userID
						  from m in n.UserAttributes
						  where m.DateDeleted == null
						  let node = m.AttributesSchemaNode
						  let cat = node.AttributeCategory
						  let type = node.AttributeType
						  where node != null &&
								cat != null &&
								type != null &&
								cat.ProjectID == null &&
								type.ProjectID == null &&
								node.Name == attributeName
						  select m).FirstOrDefault();

		if (globalAttr != null)
			return globalAttr.Value;

		return null;
	}

	protected void FillUserCategories()
	{
		var userCategoris = DataContext.UM_UserCategories.Where(n => n.DateDeleted == null).ToList();

		var item = new UM_UserCategory();
		item.ID = Guid.Empty;
		item.DateCreated = DateTime.Now;
		item.Name = "ყველა";

		userCategoris.Insert(0, item);

		ddlUserCategories.DataSource = userCategoris;
		ddlUserCategories.DataBind();

		ddlUserCategories.SelectedItem = ddlUserCategories.Items.FindByValue(Guid.Empty.ToString());
	}

	private void ApplyPermissions()
	{
		if (!UmUtil.Instance.HasAccess("UsersList"))
		{
			Response.Redirect("~/Pages/Login.aspx");

		}

		btnAddUser.Visible = UmUtil.Instance.HasAccess("AddUserButton");

		gvUsers.Columns["Edit"].Visible =
			gvUsers.Columns["Delete"].Visible =
				gvUsers.Columns["AddAttributes"].Visible =
					UmUtil.Instance.HasAccess("UsersGrid");
	}
	#endregion
}

