using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Utils;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Web.Bases;
using CITI.EVO.UserManagement.Web.Units;
using CITI.EVO.Tools.Extensions;
using CITI.EVO.Tools.Web.UI.Controls;

public partial class Pages_AttributesCategoriesTypesList : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		ApplyPermissions();
		FillAttributesTree();
	}

	protected void lnkEdit_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var treeNode = TreeNodeUnit.Parse(lnkBtn.CommandArgument);
		if (treeNode == null)
		{
			return;
		}

		if (treeNode.Type == "Category")
		{
			FillAttributeCategoryForm(treeNode);

			lblCategoryContext.Text = "კატეგორიების რედაქტირება";
			upnlAttributeCategory.Update();
			mpeAttributeCategory.Show();
		}
		else if (treeNode.Type == "Type")
		{
			FillAttributeTypeForm(treeNode);

			lblTypeContext.Text = "ტიპების რედაქტირება";
			upnlAttributeTypes.Update();
			mpeAttributeType.Show();
		}

	}

	protected void lnkDelete_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var treeNode = TreeNodeUnit.Parse(lnkBtn.CommandArgument);
		if (treeNode == null)
		{
			return;
		}

		if (treeNode.Type == "Category")
		{
			var attributeCategory = (from n in DataContext.UM_AttributeCategories
									 where n.ID == treeNode.ID
									 select n).FirstOrDefault();

			if (attributeCategory != null)
			{
				attributeCategory.DateDeleted = DateTime.Now;
			}
		}
		else if (treeNode.Type == "Type")
		{
			var attributeType = (from n in DataContext.UM_AttributeTypes
								 where n.ID == treeNode.ID
								 select n).FirstOrDefault();

			if (attributeType != null)
			{
				attributeType.DateDeleted = DateTime.Now;
			}
		}

		DataContext.SubmitChanges();

		FillAttributesTree();
	}

	protected void lnkNew_Click(object sender, EventArgs e)
	{
		var lnkBtn = sender as ImageLinkButton;
		if (lnkBtn == null)
		{
			return;
		}

		var treeNode = TreeNodeUnit.Parse(lnkBtn.CommandArgument);
		if (treeNode == null)
		{
			return;
		}

		if (treeNode.Type == "Categories")
		{
			ResetAttributeCategoryForm();
			hdAttributeCategoryProjectID.Value = Convert.ToString(treeNode.ParentID);

			lblCategoryContext.Text = "კატეგორიების დამატება";
			upnlAttributeCategory.Update();
			mpeAttributeCategory.Show();
		}
		else if (treeNode.Type == "Types")
		{
			ResetAttributeTypeForm();
			hdAttributeTypeProjectID.Value = Convert.ToString(treeNode.ParentID);

			lblTypeContext.Text = "ტიპების დამატება";
			upnlAttributeTypes.Update();
			mpeAttributeType.Show();
		}
	}

	protected void btAttributeCategoryOK_Click(object sender, EventArgs e)
	{
		var attributeCategoryID = DataConverter.ToNullableGuid(hdAttributeCategoryID.Value);
		if (attributeCategoryID.IsNullOrEmpty())
		{
			var item = new UM_AttributeCategory
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
				Name = tbAttributeCategoryName.Text,
				ProjectID = null
			};

			DataContext.UM_AttributeCategories.InsertOnSubmit(item);
		}
		else
		{
			var item = (from n in DataContext.UM_AttributeCategories
						where n.ID == attributeCategoryID
						select n).First();

			item.Name = tbAttributeCategoryName.Text;

		}

		DataContext.SubmitChanges();

		FillAttributesTree();
	}

	protected void btAttributeTypeOK_Click(object sender, EventArgs e)
	{
		var attributeTypeID = DataConverter.ToNullableGuid(hdAttributeTypeID.Value);
		if (attributeTypeID.IsNullOrEmpty())
		{
			var item = new UM_AttributeType
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
				Name = tbAttributeType.Text,
				ProjectID = null
			};

			DataContext.UM_AttributeTypes.InsertOnSubmit(item);
		}
		else
		{
			var item = (from n in DataContext.UM_AttributeTypes
						where n.ID == attributeTypeID
						select n).First();

			item.Name = tbAttributeType.Text;
		}

		DataContext.SubmitChanges();

		FillAttributesTree();
	}

	#region Methods

	protected void ResetAttributeCategoryForm()
	{
		hdAttributeCategoryID.Value = null;
		tbAttributeCategoryName.Text = String.Empty;
	}

	protected void ResetAttributeTypeForm()
	{
		hdAttributeTypeID.Value = null;
		tbAttributeType.Text = String.Empty;
	}

	protected void FillAttributesTree()
	{
		var list = CreateListOfTree().ToList();

		tlAttributes.DataSource = list;
		tlAttributes.DataBind();
	}

	private IEnumerable<TreeNodeUnit> CreateListOfTree()
	{
		var projects = (from n in DataContext.UM_Projects
						where n.DateDeleted == null
						orderby n.Name
						select n).ToList();

		var attributeTypesLp = (from n in DataContext.UM_AttributeTypes
								where n.DateDeleted == null
								select n).ToLookup(n => n.ProjectID.GetValueOrDefault());

		var attributeCategoriesLp = (from n in DataContext.UM_AttributeCategories
									 where n.DateDeleted == null
									 select n).ToLookup(n => n.ProjectID.GetValueOrDefault());

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

			var typesUnit = new TreeNodeUnit
			{
				ID = CryptographyUtil.ComputeGuidMD5(project.ID, "Types"),
				ParentID = project.ID,
				Name = "ტიპები",
				Type = "Types"
			};

			yield return typesUnit;

			var categoriesUnit = new TreeNodeUnit
			{
				ID = CryptographyUtil.ComputeGuidMD5(project.ID, "Categories"),
				ParentID = project.ID,
				Name = "კატეგორიები",
				Type = "Categories"
			};

			yield return categoriesUnit;

			var attributesTypes = attributeTypesLp[project.ID];
			foreach (var attributesType in attributesTypes)
			{
				var typeUnit = new TreeNodeUnit
				{
					ID = attributesType.ID,
					Name = attributesType.Name,
					ParentID = typesUnit.ID,
					Type = "Type"
				};

				yield return typeUnit;
			}

			var attributesCategories = attributeCategoriesLp[project.ID];
			foreach (var attributesCategory in attributesCategories)
			{
				var categoryUnit = new TreeNodeUnit
				{
					ID = attributesCategory.ID,
					Name = attributesCategory.Name,
					ParentID = categoriesUnit.ID,
					Type = "Category"
				};

				yield return categoryUnit;
			}
		}
	}

	protected void FillAttributeCategoryForm(TreeNodeUnit unit)
	{
		var item = (from n in DataContext.UM_AttributeCategories
					where n.ID == unit.ID
					select n).FirstOrDefault();

		if (item == null)
		{
			return;
		}

		var list = (IList<TreeNodeUnit>)tlAttributes.DataSource;

		var projectUnit = GetParentByType(list, unit, "Project");

		hdAttributeCategoryProjectID.Value = Convert.ToString(projectUnit.ID);
		hdAttributeCategoryID.Value = Convert.ToString(unit.ID);
		tbAttributeCategoryName.Text = item.Name;

	}

	protected void FillAttributeTypeForm(TreeNodeUnit unit)
	{
		var item = (from n in DataContext.UM_AttributeTypes
					where n.ID == unit.ID
					select n).FirstOrDefault();

		if (item == null)
		{
			return;
		}

		var list = (IList<TreeNodeUnit>)tlAttributes.DataSource;

		var projectUnit = GetParentByType(list, unit, "Project");

		hdAttributeTypeProjectID.Value = Convert.ToString(projectUnit.ID);
		hdAttributeTypeID.Value = Convert.ToString(unit.ID);
		tbAttributeType.Text = item.Name;
	}

	private TreeNodeUnit GetParentByType(IList<TreeNodeUnit> list, TreeNodeUnit unit, String type)
	{
		while (unit != null)
		{
			if (unit.Type == type)
				return unit;

			unit = list.FirstOrDefault(n => n.ID == unit.ParentID);
		}

		return null;
	}

	protected bool GetEditVisible(object eval)
	{
		var type = DataConverter.ToString(eval);
		if (type == "Category" || type == "Type")
			return true;

		return false;
	}

	protected bool GetDeleteVisible(object eval)
	{
		var type = DataConverter.ToString(eval);
		if (type == "Category" || type == "Type")
			return true;

		return false;
	}

	protected bool GetNewVisible(object eval)
	{
		var type = DataConverter.ToString(eval);
		if (type == "Categories" || type == "Types")
			return true;

		return false;
	}

	private void ApplyPermissions()
	{
		if (!UmUtil.Instance.HasAccess("AttributesCategoriesTypesList"))
		{
			Response.Redirect("~/Pages/UsersList.aspx");
		}
	}
	#endregion
}