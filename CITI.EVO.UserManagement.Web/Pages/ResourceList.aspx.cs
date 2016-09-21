using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Security;
using CITI.EVO.UserManagement.Web.Bases;
using DevExpress.Utils;
using DevExpress.Web.ASPxTreeList;
using CITI.EVO.Tools.Utils;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Svc.Enums;
using CITI.EVO.Tools.Extensions;

public partial class Pages_ResourceList : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		ApplyPermissions();

		FillProjects();
		FillResourceType();
		FillResources();
	}

	protected void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		FillResources();
	}

	protected void btSearch_OnClick(object sender, EventArgs e)
	{

	}

	protected void btNew_OnClick(object sender, EventArgs e)
	{
		hdResourceID.Value = null;
		btResourceOK.CommandArgument = null;

		cmbResourceType.SelectedIndex = 0;

		tbResourceName.Text = null;
		tbResourceValue.Text = null;

		mpeResource.Show();
	}

	protected void lnkEdit_OnCommand(object sender, CommandEventArgs e)
	{
		var resourceID = DataConverter.ToNullableGuid(e.CommandArgument);
		if (resourceID == null)
		{
			return;
		}

		var item = DataContext.UM_Resources.FirstOrDefault(n => n.ID == resourceID);
		if (item == null)
		{
			return;
		}

		hdResourceID.Value = item.ID.ToString();
		btResourceOK.CommandArgument = Convert.ToString(item.ParentID);

		cmbResourceType.SelectedItem = cmbResourceType.Items.FindByValue(item.Type.ToString());
		tbResourceName.Text = item.Name;
		tbResourceValue.Text = item.Value;

		upnlResource.Update();
		mpeResource.Show();
	}

	protected void lnkDelete_OnCommand(object sender, CommandEventArgs e)
	{
		var resourceID = DataConverter.ToNullableGuid(e.CommandArgument);
		if (resourceID == null)
		{
			return;
		}

		var resource = (from n in DataContext.UM_Resources
						where n.ID == resourceID
						select n).FirstOrDefault();

		if (resource != null)
		{
			resource.DateDeleted = DateTime.Now;
		}

		DataContext.SubmitChanges();

		FillResources();
	}

	protected void lnkNew_OnCommand(object sender, EventArgs e)
	{
		var lnkBtn = sender as LinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		Guid currID;
		if (!Guid.TryParse(lnkBtn.CommandArgument, out currID))
		{
			return;
		}

		var item = DataContext.UM_Resources.FirstOrDefault(n => n.ID == currID);
		if (item == null)
		{
			return;
		}

		hdResourceID.Value = null;
		btResourceOK.CommandArgument = Convert.ToString(item.ID);
		cmbResourceType.SelectedIndex = 0;

		tbResourceName.Text = null;
		tbResourceValue.Text = null;

		mpeResource.Show();
	}

	protected void btResourceOK_Click(object sender, CommandEventArgs e)
	{
		UM_Resource item;

		var resourceID = DataConverter.ToNullableGuid(hdResourceID.Value);
		if (resourceID != null)
		{
			item = DataContext.UM_Resources.First(n => n.ID == resourceID);
		}
		else
		{
			item = new UM_Resource
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now
			};

			DataContext.UM_Resources.InsertOnSubmit(item);
		}

		item.ProjectID = DataConverter.ToGuid(cmbProject.SelectedItem.Value);
		item.ParentID = DataConverter.ToNullableGuid(e.CommandArgument);

		item.Name = tbResourceName.Text;
		item.Type = Int32.Parse(cmbResourceType.SelectedItem.Value.ToString());
		item.Value = tbResourceValue.Text;

		DataContext.SubmitChanges();

		FillResources();
	}

	#region methods
	protected void FillResources()
	{
		var query = from n in DataContext.UM_Resources
					where n.DateDeleted == null
					select n;

		var projectID = DataConverter.ToNullableGuid(cmbProject.SelectedItem.Value);
		if (projectID == null)
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

		var resources = query.ToList();

		var keyword = (tbxKeyword.Text ?? String.Empty).Trim();
		if (!String.IsNullOrWhiteSpace(keyword))
		{
			var list = (from n in resources
						where n.Name.Contains(keyword) ||
							  n.Value.Contains(keyword)
						select n).ToList();


			var @set = FullHierarchyTraversal(list, resources).ToHashSet();
			resources = @set.ToList();
		}

		tlResources.DataSource = resources;
		tlResources.DataBind();
	}

	protected void FillProjects()
	{
		var projects = (from n in DataContext.UM_Projects
						where n.DateDeleted == null
						select n).ToList();

		var list = projects.Select(n => new KeyValuePair<Guid?, String>(n.ID, n.Name)).ToList();
		list.Insert(0, new KeyValuePair<Guid?, String>(null, "Global"));

		cmbProject.DataSource = list;
		cmbProject.DataBind();

		if (!IsPostBack)
		{
			cmbProject.SelectedIndex = 0;
		}
	}

	protected void FillResourceType()
	{

		var names = Enum.GetNames(typeof(RuleTypesEnum)).ToList();
		var values = Enum.GetValues(typeof(RuleTypesEnum)).Cast<int>().ToList();

		var ruleTypeCollection = (from name in names
								  let index = names.IndexOf(name)
								  let value = values[index]
								  select new KeyValuePair<String, int>(name, value)).ToList();

		cmbResourceType.DataSource = ruleTypeCollection;
		cmbResourceType.DataBind();

	}

	protected void ApplyPermissions()
	{
		if (!UmUtil.Instance.HasAccess("ResourceList"))
		{
			Response.Redirect("~/Pages/UsersList.aspx");
		}
	}

	protected bool NewEnabled(Object value)
	{
		return true;
	}
	protected bool EditEnabled(Object value)
	{
		return true;
	}
	protected bool DeleteEnabled(Object value)
	{
		return true;
	}

	protected IEnumerable<UM_Resource> FullHierarchyTraversal(IList<UM_Resource> resources, IList<UM_Resource> allResources)
	{
		var resourcesDict = allResources.ToDictionary(n => n.ID);
		var resourcesLp = allResources.ToLookup(n => n.ParentID.GetValueOrDefault());

		foreach (var item in resources)
		{
			yield return item;

			foreach (var parent in GetAllParents(item, resourcesDict))
				yield return parent;

			foreach (var child in GetAllChildren(item, resourcesLp))
				yield return child;
		}
	}

	protected IEnumerable<UM_Resource> GetAllParents(UM_Resource resource, IDictionary<Guid, UM_Resource> allResourceses)
	{
		while (resource != null)
		{
			resource = allResourceses.GetValueOrDefault(resource.ParentID.GetValueOrDefault());
			if (resource != null)
				yield return resource;
		}
	}

	protected IEnumerable<UM_Resource> GetAllChildren(UM_Resource resource, ILookup<Guid, UM_Resource> allResourceses)
	{
		var children = allResourceses[resource.ID];

		var stack = new Stack<UM_Resource>(children);
		while (stack.Count > 0)
		{
			var item = stack.Pop();
			yield return item;
		}
	}

	#endregion


}