using System;
using System.Linq;
using CITI.EVO.Tools.Security;
using CITI.EVO.Tools.Web.UI.Controls;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Web.Bases;

public partial class Pages_ProjectsList : BasePage
{
    private const String projectIdKey = "$_projectId_$";

    public Guid? ProjectId
    {
        get
        {
            return ViewState[projectIdKey] as Guid?;
        }
        set
        {
            ViewState[projectIdKey] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ApplyPermissions();
        FillProjectsGrid();
    }

    protected void btNewProject_Click(object sender, EventArgs e)
    {
        ResetProjectForm();
        ModuleContext.Text = "მოდულის დამატება";
        upnlProject.Update();
        mpeProject.Show();
    }

    protected void btProjectOK_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(tbProjectName.Text))
        {
            lblProjectError.Text = "შეიყვანეთ სახელი";

            upnlProject.Update();
            mpeProject.Show();

            return;
        }

        var project = new UM_Project
        {
            ID = Guid.NewGuid(),
            DateCreated = DateTime.Now
        };

        if (ProjectId == null)
        {
            DataContext.UM_Projects.InsertOnSubmit(project);
        }
        else
        {
            project = DataContext.UM_Projects.FirstOrDefault(n => n.ID == ProjectId);

            if (project == null)
            {
                lblProjectError.Text = "პროექტი არ არსებობს";

                upnlProject.Update();
                mpeProject.Show();

                return;
            }
        }

        var exists = DataContext.UM_Projects.Count(n => n.Name == tbProjectName.Text && n.ID != project.ID && n.DateDeleted == null) > 0;
        if (exists)
        {
            lblProjectError.Text = "პროექტი ამ სახელით უკვე არსებობს";

            upnlProject.Update();
            mpeProject.Show();

            return;
        }

        project.Name = tbProjectName.Text;
        project.IsActive = chkIsActive.Checked;

        DataContext.SubmitChanges();

        FillProjectsGrid();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        ResetProjectForm();

        var lnkBtn = sender as ImageLinkButton;
        if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
        {
            return;
        }

        Guid projectID;
        if (!Guid.TryParse(lnkBtn.CommandArgument, out  projectID))
        {
            return;
        }

        if (FillProjectsForm(projectID))
        {
            upnlProject.Update();
            mpeProject.Show();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        var lnkBtn = sender as ImageLinkButton;
        if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
        {
            return;
        }

        Guid projectID;
        if (!Guid.TryParse(lnkBtn.CommandArgument, out  projectID))
        {
            return;
        }

        var project = DataContext.UM_Projects.FirstOrDefault(n => n.ID == projectID);
        if (project == null)
        {
            return;
        }

        project.DateDeleted = DateTime.Now;

        DataContext.SubmitChanges();

        FillProjectsGrid();
    }

    protected bool FillProjectsGrid()
    {
        var projects = DataContext.UM_Projects.Where(n => n.DateDeleted == null).ToList();

        gvProjects.DataSource = projects;
        gvProjects.DataBind();

        return true;
    }

    protected bool FillProjectsForm(Guid projectID)
    {
        var project = DataContext.UM_Projects.FirstOrDefault(n => n.ID == projectID);
        if (project == null)
        {
            return false;
        }

        ModuleContext.Text = "მოდულის რედაქტირება";
        ProjectId = project.ID;
        tbProjectName.Text = project.Name;
        chkIsActive.Checked = project.IsActive;

        return true;
    }

    protected void ResetProjectForm()
    {
        lblProjectError.Text =
        tbProjectName.Text =
        String.Empty;
        ProjectId = null;
        chkIsActive.Checked = true;

    }

    protected void lnkAddMessage_Click(object sender, EventArgs e)
    {
        var lnkBtn = sender as ImageLinkButton;
        if (lnkBtn == null || String.IsNullOrWhiteSpace(lnkBtn.CommandArgument))
        {
            return;
        }

        Guid projectID;
        if (!Guid.TryParse(lnkBtn.CommandArgument, out  projectID))
        {
            return;
        }

        ucMessage.ObjectId = projectID;
        ucMessage.Show();
        ucMessage.Update();
    }

    #region Methods
    private void ApplyPermissions()
    {
        if (!UmUtil.Instance.HasAccess("ProjectsList"))
        {
            Response.Redirect("~/Pages/UsersList.aspx");
        }

        btNewProject.Visible = UmUtil.Instance.HasAccess("NewProjectButton");

        gvProjects.Columns["Edit"].Visible =
        gvProjects.Columns["Delete"].Visible =
        UmUtil.Instance.HasAccess("ProjectsGrid");
    }
    #endregion

}