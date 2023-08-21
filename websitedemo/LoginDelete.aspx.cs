using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    public string inputSubmitValue = "";
    public string loginIdValue = "";
    public string loginInputValue = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        //Before posting form
        if (false == Page.IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["loginId"]))
            {
                this.loginIdValue = Request.QueryString["loginId"];
                int loginId = int.Parse(this.loginIdValue);

                ///Retrieving current user properties with API function loginQuery
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
                    this.inputSubmitValue = "Confirm delete user " + queryResult.Login;
                }
            }
            else
            {
                string msg = "Cannot init delete user. Missing loginId";
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }

        }
        else
        {
            this.loginIdValue = Request.Form["loginId"];
            int loginId = int.Parse(this.loginIdValue);

            this.loginInputValue = Request.Form["login"];

            //deleting user with API function loginDelete
            string queryResult = webservices.loginDelete(loginId);

            if (queryResult.StartsWith("NOK"))
            {
                string msg = "Failed to delete user " + this.loginInputValue + ". Error message: " + queryResult;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }

            else
            {
                string msg = "Successfully deleted user " + this.loginInputValue;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("UserHome.aspx?msg=" + urlMsg);
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