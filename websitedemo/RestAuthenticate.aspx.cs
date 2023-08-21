using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));
    
    protected string loginInputValue { get; set; } 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        success.Visible = false;

        //Trying to get a user in session to pre-fill the authentication form, otherwise form is left empty
        IWUser u = new IWUser();
        u = (IWUser)Session["curr_user"];

        if (u != null && !Page.IsPostBack)
        {
            this.loginInputValue = u.Login;
        }

        if (!string.IsNullOrEmpty(Request.Form["login"]) && !string.IsNullOrEmpty(Request.Form["code"]))
        {
            string login = Request.Form["login"];
            string code = Request.Form["code"];

            IWRestFunctions restservices = new IWRestFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

            //Authenticating the user with the posted login and OTP via REST API call
            string actionURL = "action=authenticateExtended" + "&serviceId=" + IWSettings.serviceId + "&userId=" + login + "&token=" + code + "&format=json";
            String queryResult = restservices.execRestCall(actionURL, "err");

            if (queryResult.StartsWith("NOK"))
            {
                string msg = "REST Authentication failed for user " + login;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }

            success.InnerText = "User " + login + " successfully authenticated";
            success.Visible = true;

            if (u != null && u.Login.Equals(login))
            {
                this.loginInputValue = u.Login;
            }
        }

        BackButton.NavigateUrl = "Default.aspx";
    }
}