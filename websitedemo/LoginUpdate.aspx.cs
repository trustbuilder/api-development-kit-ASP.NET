using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    public string loginInputValue = "";
    public string loginFirstnameInputValue = "";
    public string loginNameInputValue = "";
    public string loginEmailInputValue = "";
    public string loginIdValue;
    private int loginId;

    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        success.Visible = false;

        //Before posting form
        if (false == Page.IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["loginId"]))
            {
                this.loginIdValue = Request.QueryString["loginId"];
                int loginId = int.Parse(this.loginIdValue);

                //Retrieving current user properties with API function loginQuery
                IWUser queryResult = webservices.loginQuery(loginId);

                if (webservices.errcode.StartsWith("NOK"))
                {
                    string msg = "Cannot retrieve user properties. Error message: " + webservices.errcode;
                    log.Error(msg);
                    string urlMsg = HttpUtility.UrlEncode(msg);
                    Response.Redirect("error.aspx?error=" + urlMsg);
                }
                else
                {
                    this.loginInputValue = queryResult.Login;
                    this.loginFirstnameInputValue = queryResult.Firstname;
                    this.loginNameInputValue = queryResult.Name;
                    this.loginEmailInputValue = queryResult.Mail;
                }
            }
            else
            {
                string msg = "Cannot init update user. Missing loginId";
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }

        }

        //After posting form
        else if (Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.Form["login"]))
            {
                string login = Request.Form["login"];
                string loginFirstname = Request.Form["loginFirstname"];
                string loginName = Request.Form["loginName"];
                string loginEmail = Request.Form["loginEmail"];

                loginId = int.Parse(Request.Form["loginId"]);

                //Updating user properties with API function loginUpdate
                string queryResult = webservices.loginUpdate(loginId, login, loginFirstname, loginName, loginEmail, 0, 0);

                if (queryResult.StartsWith("NOK"))
                {
                    string msg = "Failed to connect to inWebo Web Services. Error message: " + queryResult;
                    log.Error(msg);
                    string urlMsg = HttpUtility.UrlEncode(msg);
                    Response.Redirect("error.aspx?error=" + urlMsg);
                }

                else
                {
                    this.loginInputValue = login;
                    this.loginFirstnameInputValue = loginFirstname;
                    this.loginNameInputValue = loginName;
                    this.loginEmailInputValue = loginEmail;
                    this.loginIdValue = loginId.ToString();

                    success.InnerHtml = "User <b>" + login + "</b> successfully updated";
                    success.Visible = true;
                }
            }
        }

        string redir = "UserHome.aspx";

        if (this.loginInputValue != "")
        {
            redir += "?login=" + this.loginInputValue;
        }

        BackButton.NavigateUrl = redir;
    }
}