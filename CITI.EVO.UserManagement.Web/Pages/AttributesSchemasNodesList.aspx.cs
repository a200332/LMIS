using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Utils;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Web.Bases;
using CITI.EVO.UserManagement.Web.Units;
using CITI.EVO.Tools.Extensions;

public partial class Pages_AttributesSchemasNodesList : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		ApplyPermissions();
		FillAttributesTree();
	}

	protected void lnkEdit_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as LinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var treeNode = TreeNodeUnit.Parse(lnkBtn.CommandArgument);
		if (treeNode == null)
		{
			return;
		}

		if (treeNode.Type == "Schema")
		{
			FillAttributeSchemaForm(treeNode.ID);

			upnlAttributeSchema.Update();
			mpeAttributeSchema.Show();
		}
		else if (treeNode.Type == "Node")
		{
			var list = (IList<TreeNodeUnit>)tlAttributes.DataSource;

			var node = list.Single(n => n.ID == treeNode.ID);
			var schema = list.Single(n => n.ID == node.ParentID);
			var project = list.Single(n => n.ID == schema.ParentID);

			FillCategoriesAndTypes(project.ID);
			FillAttributeSchemaNodeForm(treeNode.ID);

			upnlAttributeSchemaNode.Update();
			mpeAttributeSchemaNode.Show();
		}
	}

	protected void lnkDelete_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as LinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var treeNode = TreeNodeUnit.Parse(lnkBtn.CommandArgument);
		if (treeNode == null)
		{
			return;
		}

		if (treeNode.Type == "Schema")
		{
			var attributeSchema = (from n in DataContext.UM_AttributesSchemas
								   where n.ID == treeNode.ID
								   select n).FirstOrDefault();

			if (attributeSchema != null)
			{
				foreach (var attributesSchemaNode in attributeSchema.AttributesSchemaNodes)
				{
					attributesSchemaNode.DateDeleted = DateTime.Now;
				}

				attributeSchema.DateDeleted = DateTime.Now;
			}
		}
		else if (treeNode.Type == "Node")
		{
			var attributeSchemaNode = (from n in DataContext.UM_AttributesSchemaNodes
									   where n.ID == treeNode.ID
									   select n).FirstOrDefault();

			if (attributeSchemaNode != null)
			{
				attributeSchemaNode.DateDeleted = DateTime.Now;
			}
		}

		DataContext.SubmitChanges();

		FillAttributesTree();
	}

	protected void lnkNew_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as LinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var treeNode = TreeNodeUnit.Parse(lnkBtn.CommandArgument);
		if (treeNode == null)
		{
			return;
		}

		if (treeNode.Type == "Project")
		{
			ResetAttributeSchemaForm();

			hdAttributeSchemaParentID.Value = treeNode.ID.ToString();

			upnlAttributeSchema.Update();
			mpeAttributeSchema.Show();
		}
		else if (treeNode.Type == "Schema")
		{
			ResetAttributeSchemaNodeForm();

			hdAttributeSchemaNodeParentID.Value = treeNode.ID.ToString();

			FillCategoriesAndTypes(treeNode.ParentID.Value);

			upnlAttributeSchemaNode.Update();
			mpeAttributeSchemaNode.Show();
		}
	}

	protected void btAttributeSchemaOK_Click(object sender, EventArgs e)
	{
		var itemID = DataConverter.ToNullableGuid(hdAttributeSchemaID.Value);

		var item = DataContext.UM_AttributesSchemas.FirstOrDefault(n => n.ID == itemID);
		if (item == null)
		{
			item = new UM_AttributesSchema
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now
			};

			DataContext.UM_AttributesSchemas.InsertOnSubmit(item);
		}

		item.Name = tbAttributeSchemaName.Text;
		item.ProjectID = Guid.Parse(hdAttributeSchemaParentID.Value);

		DataContext.SubmitChanges();

		FillAttributesTree();
	}

	protected void btAttributeSchemaNodeOK_Click(object sender, EventArgs e)
	{
		var attributeTypeID = cmbAttributeTypes.TryGetGuidValue();
		var attributeCategoryID = cmbAttributeCategories.TryGetGuidValue();

		if (attributeTypeID == null || attributeCategoryID == null)
			return;

		var itemID = DataConverter.ToNullableGuid(hdAttributeSchemaNodeID.Value);

		var item = DataContext.UM_AttributesSchemaNodes.FirstOrDefault(n => n.ID == itemID);
		if (item == null)
		{
			item = new UM_AttributesSchemaNode
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now
			};

			DataContext.UM_AttributesSchemaNodes.InsertOnSubmit(item);
		}

		item.Name = tbAttributeSchemaNodeName.Text;
		item.AttributesSchemaID = Guid.Parse(hdAttributeSchemaNodeParentID.Value);

		item.AttributeCategoryID = attributeCategoryID.Value;
		item.AttributeTypeID = attributeTypeID.Value;

		DataContext.SubmitChanges();

		FillAttributesTree();
	}

	protected void FillAttributesTree()
	{
		var list = CreateListOfTree().ToList();

		tlAttributes.DataSource = list;
		tlAttributes.DataBind();
	}

	protected void FillAttributeSchemaForm(Guid attributeSchemaID)
	{
		var item = (from n in DataContext.UM_AttributesSchemas
					where n.ID == attributeSchemaID
					select n).FirstOrDefault();

		if (item == null)
		{
			return;
		}

		hdAttributeSchemaID.Value = item.ID.ToString();
		hdAttributeSchemaParentID.Value = item.ProjectID.ToString();
		tbAttributeSchemaName.Text = item.Name;
	}

	protected void FillAttributeSchemaNodeForm(Guid attributeSchemaNodeID)
	{
		var item = (from n in DataContext.UM_AttributesSchemaNodes
					where n.ID == attributeSchemaNodeID
					select n).FirstOrDefault();

		if (item == null)
		{
			return;
		}

		hdAttributeSchemaNodeID.Value = item.ID.ToString();
		hdAttributeSchemaNodeParentID.Value = item.AttributesSchemaID.ToString();

		tbAttributeSchemaNodeName.Text = item.Name;

		cmbAttributeCategories.SelectedItem = cmbAttributeCategories.Items.FindByValue(item.AttributeCategoryID);
		cmbAttributeTypes.SelectedItem = cmbAttributeTypes.Items.FindByValue(item.AttributeTypeID);
	}

	protected void FillCategoriesAndTypes(Guid projectID)
	{
		var categoriesQuery = from n in DataContext.UM_AttributeCategories
							  where n.DateDeleted == null
							  select n;

		if (projectID == Guid.Empty)
			categoriesQuery = categoriesQuery.Where(n => n.ProjectID == null);
		else
			categoriesQuery = categoriesQuery.Where(n => n.ProjectID == projectID);

		cmbAttributeCategories.Items.Clear();

		var categories = categoriesQuery.ToList();
		if (categories.Count > 0)
		{
			cmbAttributeCategories.DataSource = categories;
			cmbAttributeCategories.SelectedItem = cmbAttributeCategories.Items.FindByValue(categories.First().ID);
			cmbAttributeCategories.DataBind();
		}

		var typesQuery = from n in DataContext.UM_AttributeTypes
						 where n.DateDeleted == null
						 select n;

		if (projectID == Guid.Empty)
			typesQuery = typesQuery.Where(n => n.ProjectID == null);
		else
			typesQuery = typesQuery.Where(n => n.ProjectID == projectID);

		cmbAttributeTypes.Items.Clear();

		var types = typesQuery.ToList();
		if (types.Count > 0)
		{
			cmbAttributeTypes.DataSource = types;
			cmbAttributeTypes.SelectedItem = cmbAttributeTypes.Items.FindByValue(types.First().ID);
			cmbAttributeTypes.DataBind();
		}
	}

	protected void ResetAttributeSchemaForm()
	{
		hdAttributeSchemaID.Value = null;
		hdAttributeSchemaParentID.Value = null;
		tbAttributeSchemaName.Text = null;
	}

	protected void ResetAttributeSchemaNodeForm()
	{
		hdAttributeSchemaNodeID.Value = null;
		hdAttributeSchemaNodeParentID.Value = null;
		tbAttributeSchemaNodeName.Text = null;

		//cmbAttributeCategories.SelectedValue = Guid.Empty.ToString();
		//cmbAttributeTypes.SelectedValue = Guid.Empty.ToString();
	}

	#region Methods

	protected IEnumerable<TreeNodeUnit> CreateListOfTree()
	{
		var projects = (from n in DataContext.UM_Projects
						where n.DateDeleted == null
						orderby n.Name
						select n).ToList();

		var attributesSchemasLp = (from n in DataContext.UM_AttributesSchemas
								   where n.DateDeleted == null
								   orderby n.Name
								   select n).ToLookup(n => n.ProjectID.GetValueOrDefault());

		var attributesSchemaNodesLp = (from n in DataContext.UM_AttributesSchemaNodes
									   where n.DateDeleted == null
									   orderby n.Name
									   select n).ToLookup(n => n.AttributesSchemaID);

		var globalProjectEntity = new UM_Project
		{
			ID = Guid.Empty,
			Name = "Global",
		};

		projects.Insert(0, globalProjectEntity);

		foreach (var project in projects)
		{
			var projectUnit = new TreeNodeUnit
			{
				ID = project.ID,
				Name = project.Name,
				Type = "Project"
			};

			yield return projectUnit;

			var attributesSchemas = attributesSchemasLp[project.ID];
			foreach (var attributesSchema in attributesSchemas)
			{
				var schemaUnit = new TreeNodeUnit
				{
					ID = attributesSchema.ID,
					Name = attributesSchema.Name,
					ParentID = project.ID,
					Type = "Schema"
				};

				yield return schemaUnit;

				var attributesSchemaNodes = attributesSchemaNodesLp[attributesSchema.ID];
				foreach (var attributesSchemaNode in attributesSchemaNodes)
				{
					var nodeUnit = new TreeNodeUnit
					{
						ID = attributesSchemaNode.ID,
						Name = attributesSchemaNode.Name,
						ParentID = attributesSchema.ID,
						Type = "Node"
					};

					yield return nodeUnit;
				}
			}
		}
	}

	protected void ApplyPermissions()
	{
		if (!UmUtil.Instance.HasAccess("AttributesSchemasNodesList"))
		{
			Response.Redirect("~/Pages/UsersList.aspx");
		}
	}

	protected bool GetEditVisible(object eval)
	{
		var type = DataConverter.ToString(eval);
		if (type == "Project")
			return false;

		return true;
	}

	protected bool GetDeleteVisible(object eval)
	{
		var type = DataConverter.ToString(eval);
		if (type == "Project")
			return false;

		return true;
	}

	protected bool GetNewVisible(object eval)
	{
		var type = DataConverter.ToString(eval);
		if (type == "Project" || type == "Schema")
			return true;

		return false;
	}

	#endregion
}