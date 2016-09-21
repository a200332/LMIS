using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Comparers;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Web.UI.Controls;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Svc.Contracts;
using CITI.EVO.UserManagement.Web.Bases;
using CITI.EVO.UserManagement.Web.Enums;
using CITI.EVO.UserManagement.Web.Extensions;
using CITI.EVO.UserManagement.Web.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using Button = System.Web.UI.WebControls.Button;
using DropDownList = System.Web.UI.WebControls.DropDownList;
using CITI.EVO.Tools.Extensions;

public partial class Pages_GroupsList : BasePage
{
	#region Properties

	public List<UM_GroupUser> CurrentAdminGroupUsers
	{
		get
		{
			return LoginUtil.CurrentUser.GroupUsers.Where(u => u.AccessLevel > 0).ToList();
		}

	}

	#endregion

	#region Events

	protected void Page_Load(object sender, EventArgs e)
	{
		ApplyPermissions();
		tlGroups.DataBind();
	}

	protected void tlGroups_VirtualModeCreateChildren(object sender, TreeListVirtualModeCreateChildrenEventArgs e)
	{

		var nodeKeyObject = e.NodeObject as NodeKeyObject;
		if (nodeKeyObject == null)
		{
			var query = from n in CacheEntitiesUtil.UmProjects.Values
						select NodeKeyObject.CreateForProject(n);



			var lookup = query.ToLookup(n => n.ToString());
			var projects = lookup.Select(n => n.First()).ToList();

			projects = projects.OrderBy(n => n.Project.Name, new StringLogicalComparer()).ToList();

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

			groups = groups.OrderBy(n => n.Group.Name, new StringLogicalComparer()).ToList();

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

			var queryUsers = from groupUser in CacheEntitiesUtil.UmGroupUsers.Values
							 where groupUser != null
							 let @group = CacheEntitiesUtil.UmGroups.GetValueOrDefault(groupUser.GroupID)
							 where @group != null
							 let project = CacheEntitiesUtil.UmProjects.GetValueOrDefault(@group.ProjectID)
							 where project != null
							 let user = CacheEntitiesUtil.UmUsers.GetValueOrDefault(groupUser.UserID)
							 where user != null &&
								   groupUser.DateDeleted == null &&
								   groupUser.GroupID == nodeKeyObject.GroupID
							 select NodeKeyObject.CreateForUser(project, @group, user);

			var lookupUsers = queryUsers.ToLookup(n => n.ToString());

			var users = lookupUsers.Select(n => n.First()).ToList();
			users = users.OrderBy(n => n.User.LoginName, new StringLogicalComparer()).ToList();

			foreach (var user in users)
			{
				e.Children.Add(user);
			}
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

			bool visible = UmUtil.Instance.CurrentUser.IsSuperAdmin;

			e.IsLeaf = !CacheEntitiesUtil.UmGroupsByProject.ContainsKey(item.ID);
			e.NodeKeyValue = nodeKeyObject.ToString();
			e.SetNodeValue("imgUrl", "");
			e.SetNodeValue("ID", e.NodeKeyValue);
			e.SetNodeValue("Name", item.Name);
			e.SetNodeValue("DateCreated", item.DateCreated);
			e.SetNodeValue("DateChanged", item.DateChanged);
			e.SetNodeValue("DateDeleted", item.DateDeleted);
			e.SetNodeValue("NewVisible", visible);
			e.SetNodeValue("ToolTip", "ჯგუფის დამატება");
			e.SetNodeValue("NewSubGroupVisible", false);
			e.SetNodeValue("EditVisible", false);
			e.SetNodeValue("DeleteVisible", false);
			e.SetNodeValue("AttrVisible", false);
			e.SetNodeValue("AttrShowVisible", false);
			e.SetNodeValue("MessageVisible", false);
			e.SetNodeValue("ConfirmationDialog", String.Empty);
		}
		else if (nodeKeyObject.IsGroupType() || nodeKeyObject.IsChildType())
		{
			var item = nodeKeyObject.Group;

			bool visible = UmUtil.Instance.CurrentUser.IsSuperAdmin;

			e.IsLeaf = !CacheEntitiesUtil.UmGroupUsersByGroup.ContainsKey(item.ID) && !CacheEntitiesUtil.UmGroupsByParent.ContainsKey(item.ID);
			e.NodeKeyValue = nodeKeyObject.ToString();
			e.SetNodeValue("imgUrl", "~/App_Themes/default/images/group_grid.png");
			e.SetNodeValue("ID", e.NodeKeyValue);
			e.SetNodeValue("Name", item.Name);
			e.SetNodeValue("DateCreated", item.DateCreated);
			e.SetNodeValue("DateChanged", item.DateChanged);
			e.SetNodeValue("DateDeleted", item.DateDeleted);
			e.SetNodeValue("NewVisible", visible);
			e.SetNodeValue("ToolTip", "მომხმარებლის დამატება");
			e.SetNodeValue("NewSubGroupVisible", visible);
			e.SetNodeValue("EditVisible", visible);
			e.SetNodeValue("DeleteVisible", visible);
			e.SetNodeValue("AttrVisible", visible);
			e.SetNodeValue("AttrShowVisible", visible);
			e.SetNodeValue("MessageVisible", true);
			e.SetNodeValue("ConfirmationDialog", "return confirm('გსურთ ჯგუფის გაუქმება?');");
		}
		else if (nodeKeyObject.IsUserType())
		{
			var item = nodeKeyObject.User;

			bool visible = UmUtil.Instance.CurrentUser.IsSuperAdmin;

			e.IsLeaf = true;
			e.NodeKeyValue = nodeKeyObject.ToString();
			e.SetNodeValue("imgUrl", "~/App_Themes/default/images/user_grid.png");
			e.SetNodeValue("ID", e.NodeKeyValue);
			e.SetNodeValue("Name", item.LoginName);
			e.SetNodeValue("DateCreated", item.DateCreated);
			e.SetNodeValue("DateChanged", item.DateChanged);
			e.SetNodeValue("DateDeleted", item.DateDeleted);
			e.SetNodeValue("NewVisible", false);
			e.SetNodeValue("ToolTip", "");
			e.SetNodeValue("NewSubGroupVisible", false);
			e.SetNodeValue("EditVisible", false);
			e.SetNodeValue("DeleteVisible", visible);
			e.SetNodeValue("AttrVisible", false);
			e.SetNodeValue("AttrShowVisible", false);
			e.SetNodeValue("MessageVisible", false);
			e.SetNodeValue("ConfirmationDialog", "return confirm('გსურთ მომხმარებლის ჯგუფიდან ამოღება?');");
		}

	}

	protected void tlGroups_VirtualModeNodeCreated(object sender, TreeListVirtualNodeEventArgs e)
	{

		if (Request.QueryString["ExpandAll"] != null)
		{
			tlGroups.ExpandToLevel(5);
		}

	}

	protected void btSearchUsers_Click(object sender, EventArgs e)
	{
		var users = (from n in DataContext.UM_Users
					 where n.DateDeleted == null
					 select n).ToList();

		if (!String.IsNullOrWhiteSpace(tbUsersKeyword.Text))
		{
			users = (from n in users
					 where n.LoginName.Trim().Contains(tbUsersKeyword.Text.Trim())
					 select n).ToList();
		}

		lstUsers.DataSource = users;
		lstUsers.DataBind();

		upnlUsers.Update();
		mpeUsers.Show();
	}

	protected void lnkNew_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as LinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var nodeKeyObject = NodeKeyObject.Parse(lnkBtn.CommandArgument);
		if (nodeKeyObject == null)
		{
			return;
		}

		if (nodeKeyObject.IsProjectType())
		{
			var project = DataContext.UM_Projects.FirstOrDefault(n => n.ID == nodeKeyObject.ProjectID);
			if (project == null)
			{
				return;
			}

			hdMainGroupParentID.Value = project.ID.ToString();
			hdParentID.Value = null;

			context.Text = "ჯგუფის დამატება";
			upnlGroup.Update();
			mpeGroup.Show();
		}

		else if (nodeKeyObject.IsGroupType() || nodeKeyObject.IsChildType())
		{
			if (lnkBtn.CommandName == "AddSubGroup")
			{
				var project = DataContext.UM_Projects.FirstOrDefault(n => n.ID == nodeKeyObject.ProjectID);
				if (project == null)
				{
					return;
				}

				ResetGroupForm();

				hdMainGroupParentID.Value = project.ID.ToString();
				hdParentID.Value = nodeKeyObject.GroupID.ToString();

				context.Text = "ჯგუფის დამატება";
				upnlGroup.Update();
				mpeGroup.Show();
			}
			else if (lnkBtn.CommandName == "AddUser")
			{
				ResetUsersForm();

				hdUsersGroupID.Value = nodeKeyObject.GroupID.ToString();

				upnlUsers.Update();
				mpeUsers.Show();
			}
		}
	}

	protected void btGroupOK_Click(object sender, EventArgs e)
	{
		if (String.IsNullOrWhiteSpace(tbGroupName.Text))
		{
			lblGroupError.Text = "შეიყვანეთ სახელი";

			upnlGroup.Update();
			mpeGroup.Show();

			return;
		}

		if (String.IsNullOrWhiteSpace(hdMainGroupParentID.Value))
		{
			lblGroupError.Text = "მონიშნეთ პროექტი";

			upnlGroup.Update();
			mpeGroup.Show();

			return;
		}

		var projectID = Guid.Parse(hdMainGroupParentID.Value);

		var project = DataContext.UM_Projects.FirstOrDefault(n => n.ID == projectID);
		if (project == null)
		{
			return;
		}

		UM_Group group;

		Guid groupID;
		if (Guid.TryParse(hdMainGroupID.Value, out groupID))
		{
			group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == groupID);
			if (group == null)
			{
				return;
			}

		}
		else
		{
			group = new UM_Group();
			group.ID = Guid.NewGuid();
			group.DateCreated = DateTime.Now;
			DataContext.UM_Groups.InsertOnSubmit(group);
		}


		group.Name = tbGroupName.Text;
		group.ProjectID = Guid.Parse(hdMainGroupParentID.Value);
		var parentID = hdParentID.Value;

		if (parentID == String.Empty)
		{
			group.ParentID = null;
		}
		else
		{
			group.ParentID = Guid.Parse(parentID);
		}

		DataContext.SubmitChanges();

		CacheEntitiesUtil.ResetAll();
		tlGroups.RefreshVirtualTree();
	}

	protected void lnkEdit_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var nodeKeyObject = NodeKeyObject.Parse(lnkBtn.CommandArgument);
		if (nodeKeyObject == null)
		{
			return;
		}

		if (nodeKeyObject.IsProjectType())
		{
			return;
		}

		if (nodeKeyObject.IsGroupType() || nodeKeyObject.IsChildType())
		{
			ResetGroupForm();

			var group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == nodeKeyObject.GroupID);
			if (group == null)
			{
				return;
			}

			FillGroupForm(group);

			context.Text = "ჯგუფის რედაქტირება";
			upnlGroup.Update();
			mpeGroup.Show();
		}
		else if (nodeKeyObject.IsUserType())
		{
			return;
		}

		CacheEntitiesUtil.ResetAll();
		// tlGroups.RefreshVirtualTree();
	}

	protected void lnkDelete_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var nodeKeyObject = NodeKeyObject.Parse(lnkBtn.CommandArgument);
		if (nodeKeyObject == null)
		{
			return;
		}

		if (nodeKeyObject.IsProjectType())
		{
			return;
		}

		if (nodeKeyObject.IsGroupType() || nodeKeyObject.IsChildType())
		{
			var @group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == nodeKeyObject.GroupID);
			if (@group != null)
			{
				foreach (var groupUser in @group.GroupUsers)
				{
					groupUser.DateDeleted = DateTime.Now;
				}

				@group.DateDeleted = DateTime.Now;
			}

			DataContext.SubmitChanges();
		}
		else if (nodeKeyObject.IsUserType())
		{
			var groupUsers = (from n in DataContext.UM_GroupUsers
							  where n.DateDeleted == null &&
									n.GroupID == nodeKeyObject.GroupID &&
									n.UserID == nodeKeyObject.UserID
							  select n).ToList();

			foreach (var groupUser in groupUsers)
			{
				groupUser.DateDeleted = DateTime.Now;
			}

			DataContext.SubmitChanges();
		}

		CacheEntitiesUtil.ResetAll();
		tlGroups.RefreshVirtualTree();
	}

	protected void lnkAttributes_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var nodeKeyObject = NodeKeyObject.Parse(lnkBtn.CommandArgument);
		if (nodeKeyObject == null)
		{
			return;
		}

		ResetGroupAttributesForm();

		var group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == nodeKeyObject.GroupID);
		if (group == null)
		{
			return;
		}

		var project = group.Project;

		if (lnkBtn.CommandName == "lnkAttributes")
		{
			hdAttributeGroupID.Value = nodeKeyObject.GroupID.ToString();

			cmbAttributeSchemas.Items.Clear();
			cmbAttributeSchemas.Items.Add(new ListEditItem("-- select schema --", Guid.Empty.ToString()));
			cmbAttributeSchemas.DataSource = project.AttributesSchemas.Where(n => n.DateDeleted == null).ToList();
			cmbAttributeSchemas.DataBind();

			cmbAttributeSchemaNodes.Items.Clear();
			cmbAttributeSchemaNodes.Items.Add(new ListEditItem("-- select node --", Guid.Empty.ToString()));
			cmbAttributeSchemaNodes.Enabled = false;

			upnlGroupAttributes.Update();
			mpeGroupAttributes.Show();
		}
		else if (lnkBtn.CommandName == "ShowAttributes")
		{
			dwAttributeSchemaNodes.DataSource = String.Empty;
			dwAttributeSchemaNodes.DataBind();

			cmbShowAttributeSchemas.Items.Clear();
			cmbShowAttributeSchemas.Items.Add(new ListEditItem("-- select schema --", Guid.Empty.ToString()));
			cmbShowAttributeSchemas.DataSource = project.AttributesSchemas.Where(n => n.DateDeleted == null).ToList();
			cmbShowAttributeSchemas.DataBind();


			hdGroupIDShow.Value = nodeKeyObject.GroupID.ToString();

			upnlShowGroupAttributes.Update();
			mpeShowGroupAttributes.Show();
		}


	}

	protected void cmbAttributeSchemas_SelectedIndexChanged(object sender, EventArgs e)
	{
		var drList = sender as DropDownList;

		if (drList == null)
		{
			return;
		}

		if (drList.ID == "cmbAttributeSchemas")
		{
			Guid groupID;
			if (!Guid.TryParse(hdAttributeGroupID.Value, out groupID))
			{
				return;
			}

			Guid schemaID;
			if (!Guid.TryParse(cmbAttributeSchemas.SelectedItem.Value.ToString(), out schemaID))
			{
				return;
			}

			cmbAttributeSchemaNodes.Items.Clear();
			cmbAttributeSchemaNodes.Items.Add(new ListEditItem("-- select node --", Guid.Empty.ToString()));

			var group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == groupID);
			if (group == null)
			{
				return;
			}


			var attributeSchemaNodes = (from n in @group.Project.AttributesSchemas
										where n.DateDeleted == null &&
											  n.ID == schemaID
										from a in n.AttributesSchemaNodes
										where a.DateDeleted == null
										select a).ToList();

			cmbAttributeSchemaNodes.DataSource = attributeSchemaNodes;
			cmbAttributeSchemaNodes.DataBind();

			if (cmbAttributeSchemaNodes.Items.Count > 1)
			{
				cmbAttributeSchemaNodes.Enabled = true;
			}
			else
			{
				cmbAttributeSchemaNodes.Enabled = false;
			}

			upnlGroupAttributes.Update();
			mpeGroupAttributes.Show();
		}
		else if (drList.ID == "cmbShowAttributeSchemas")
		{
			var attrSchemaNodeValues = (from an in DataContext.UM_AttributesSchemaNodes
										join ua in DataContext.UM_GroupAttributes
											on an.ID equals ua.AttributesSchemaNodeID
										where an.DateDeleted == null &&
											  ua.GroupID == Guid.Parse(hdGroupIDShow.Value) &&
											  an.AttributesSchemaID ==
											  Guid.Parse(cmbShowAttributeSchemas.SelectedItem.Value.ToString())
										select new { an.Name, ua.Value }).ToList();

			dwAttributeSchemaNodes.DataSource = attrSchemaNodeValues;
			dwAttributeSchemaNodes.DataBind();

			upnlShowGroupAttributes.Update();
			mpeShowGroupAttributes.Show();
		}
	}

	protected void cmbAttributeSchemaNodes_SelectedIndexChanged(object sender, EventArgs e)
	{
		Guid groupID;
		if (!Guid.TryParse(hdAttributeGroupID.Value, out groupID))
		{
			return;
		}

		Guid schemaNodeID;
		if (!Guid.TryParse(cmbAttributeSchemaNodes.SelectedItem.Value.ToString(), out schemaNodeID))
		{
			return;
		}

		var group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == groupID);
		if (group == null)
		{
			return;
		}

		var groupAttribute = group.GroupAttributes.FirstOrDefault(n => n.AttributesSchemaNodeID == schemaNodeID);
		if (groupAttribute != null)
		{
			tbGroupAttributesValue.Text = groupAttribute.Value;
		}

		upnlGroupAttributes.Update();
		mpeGroupAttributes.Show();
	}

	protected void btGroupAttributesOK_Click(object sender, EventArgs e)
	{
		Guid groupID;
		if (!Guid.TryParse(hdAttributeGroupID.Value, out groupID))
		{
			return;
		}

		Guid schemaNodeID;
		if (!Guid.TryParse(cmbAttributeSchemaNodes.SelectedItem.Value.ToString(), out schemaNodeID))
		{
			return;
		}

		var group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == groupID);
		if (group == null)
		{
			return;
		}

		var groupAttribute = GetRelatedGroupAttributes(groupID, schemaNodeID);
		if (groupAttribute == null)
		{
			groupAttribute = new UM_GroupAttribute
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now
			};

			DataContext.UM_GroupAttributes.InsertOnSubmit(groupAttribute);
		}

		groupAttribute.Value = tbGroupAttributesValue.Text;
		groupAttribute.AttributesSchemaNodeID = schemaNodeID;
		groupAttribute.GroupID = groupID;

		DataContext.SubmitChanges();

		upnlGroupAttributes.Update();
		mpeGroupAttributes.Show();
	}

	protected void btUsersOK_Click(object sender, EventArgs e)
	{
		Guid groupID;
		if (!Guid.TryParse(hdUsersGroupID.Value, out groupID))
		{
			return;
		}

		Guid userID;
		if (!Guid.TryParse(lstUsers.SelectedValue, out userID))
		{
			return;
		}

		AccessLevelEnum accessLevel;
		if (!Enum.TryParse(ddlAccessLevels.SelectedItem.Value.ToString(), out accessLevel))
		{
			return;
		}

		var group = DataContext.UM_Groups.FirstOrDefault(n => n.ID == groupID);
		if (group == null)
		{
			return;
		}

		var user = DataContext.UM_Users.FirstOrDefault(n => n.ID == userID);
		if (user == null)
		{
			return;
		}

		var exists = DataContext.UM_GroupUsers.Count(n => n.DateDeleted == null && n.GroupID == groupID && n.UserID == userID) > 0;
		if (exists)
		{
			return;
		}

		var groupUser = new UM_GroupUser
		{
			ID = Guid.NewGuid(),
			DateCreated = DateTime.Now,
			GroupID = groupID,
			UserID = userID,
			AccessLevel = (int)accessLevel
		};

		DataContext.UM_GroupUsers.InsertOnSubmit(groupUser);
		DataContext.SubmitChanges();

		CacheEntitiesUtil.ResetAll();
		tlGroups.RefreshVirtualTree();
	}

	protected void btGroupAttributesCancel_Click(object sender, ImageClickEventArgs e)
	{
	}

	protected void lnkMessage_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as Button;
		if (lnkBtn == null)
		{
			return;
		}

		var nodeKeyObject = NodeKeyObject.Parse(lnkBtn.CommandArgument);
		if (nodeKeyObject == null)
		{
			return;
		}

		ucMessage.ObjectId = nodeKeyObject.GroupID;
		ucMessage.Show();
		ucMessage.Update();
	}

	#endregion

	#region Methods

	protected void ResetGroupForm()
	{
		tbGroupName.Text = String.Empty;
		hdMainGroupID.Value = null;
		hdMainGroupParentID.Value = null;
		hdParentID.Value = null;

		// cmbAttributeSchemas.Items.Clear();
		// cmbAttributeSchemaNodes.Items.Clear();
	}

	protected void ResetGroupAttributesForm()
	{
		hdAttributeGroupID.Value = null;
		hdAttributeGroupParentID.Value = null;

		//tbGroupAttributesValue.Text = null;

	}

	protected void ResetUsersForm()
	{
		tbUsersKeyword.Text = null;
		lstUsers.Items.Clear();
	}

	protected void FillGroupForm(UM_Group @group)
	{
		hdMainGroupID.Value = @group.ID.ToString();
		hdMainGroupParentID.Value = @group.ProjectID.ToString();
		hdParentID.Value = @group.ParentID.ToString();
		tbGroupName.Text = @group.Name;
	}

	protected UM_GroupAttribute GetRelatedGroupAttributes(Guid groupID, Guid SchemaNodeID)
	{
		var attributeValue = (from v in DataContext.UM_GroupAttributes
							  where v.GroupID == groupID &&
									v.AttributesSchemaNodeID == SchemaNodeID
							  select v).FirstOrDefault();

		if (attributeValue == null)
		{
			return null;
		}

		return attributeValue;
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

	private void ApplyPermissions()
	{
		if (!UmUtil.Instance.HasAccess("GroupList"))
		{
			Response.Redirect("~/Pages/UsersList.aspx");
		}
	}

	#endregion

}