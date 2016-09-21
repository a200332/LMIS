using System;
using System.Configuration;
using System.Text;
using System.Web;
using CITI.EVO.Tools.Helpers;
using CITI.EVO.Tools.Security;


public partial class Pages_Login : System.Web.UI.Page
{
    private UmUtil instance;
    public UmUtil Instance
    {
        get
        {
            instance = (instance ?? UmUtil.Instance);
            return instance;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
    }

    protected void btnGoToLicPage_OnClick(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lstErrorMessages.Text = String.Empty;

        if (Instance.IsLogged)
        {
            return;
        }

        var redirectUrl = GetReturnUrl();
        if (String.IsNullOrEmpty(redirectUrl))
        {
            redirectUrl = ConfigurationManager.AppSettings["DefaultPage"];
        }

        if (Instance.Login())
        {
            var uriHelper = new UrlHelper(redirectUrl);
            uriHelper["loginToken"] = Convert.ToString(Instance.CurrentToken);

            redirectUrl = uriHelper.ToString();
            Response.Redirect(redirectUrl);
        }

        if (Instance.IsPasswordExpired)
        {
            Instance.GoToChangePassword(redirectUrl);
        }
    }

    protected void btOK_Click(object sender, EventArgs e)
    {
        bool login = Instance.Login(tbLoginName.Text, tbPassword.Text);

        if (!login)
        {
            lstErrorMessages.Text = "თქვენ არასწორად შეიყვანეთ მომხმარებლის სახელი ან პაროლი";
            return;
        }

        if (chkRememberMe.Checked)
        {
            Instance.SaveCurrentLoginCookies();
        }
        else
        {
            Instance.ClearCurrentLoginCookies();
        }

        var redirectUrl = GetReturnUrl();
        if (String.IsNullOrEmpty(redirectUrl))
        {
            redirectUrl = "~/Default.aspx";
        }

        var uriHelper = new UrlHelper(redirectUrl);
        uriHelper["loginToken"] = Convert.ToString(Instance.CurrentToken);

        redirectUrl = uriHelper.ToString();
        Response.Redirect(redirectUrl);
    }

    protected String GetReturnUrl()
    {
        if (String.IsNullOrEmpty(Request["ReturnUrl"]))
        {
            return null;
        }

        try
        {
            var bytes = HttpServerUtility.UrlTokenDecode(Request["ReturnUrl"]);
            return Encoding.UTF8.GetString(bytes);
        }
        catch (Exception)
        {
        }

        try
        {
            var bytes = Convert.FromBase64String(Request["ReturnUrl"]);
            return Encoding.ASCII.GetString(bytes);
        }
        catch (Exception)
        {
        }

        return Request["ReturnUrl"];
    }
}