using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));
    
    protected string loginInputValue { get; set; }
    protected string loginSent { get; set; }
    protected string initJavascript { get; set; }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        sendToMobile.Visible = false;
        this.initJavascript = "";

        //Trying to get a user in session to pre-fill the authentication form, otherwise form is left empty
        IWUser u = new IWUser();
        u = (IWUser)Session["curr_user"];

        if (u != null && !Page.IsPostBack)
        {
            this.loginInputValue = u.Login;
        }

        if (!string.IsNullOrEmpty(Request.Form["login"]))
        {
            string login = Request.Form["login"];

            IWRestFunctions restservices = new IWRestFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

            //Sending push notification via REST API call
            string actionURL = "action=pushAuthenticate" + "&serviceId=" + IWSettings .serviceId + "&userId=" + login + "&format=json";
            String queryResult = restservices.execRestCall(actionURL, "sessionId");

            if (queryResult.StartsWith("NOK"))
            {
                string msg = "Push Authentication failed for user " + login + ". Error message: " + queryResult;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }

            //Push notification sent successfully
            this.loginSent = login;
            sendToMobile.Visible = true;

            if (u != null && u.Login.Equals(login))
            {
                this.loginInputValue = u.Login;
            }

            //Inserting javascript to trigger check push listener (looping over ajax call in the page)
            string checkURL = "CheckPushResult.aspx?sessionId=" + queryResult + "&userId=" + login;
            this.initJavascript = "<script type='text/javascript'>checkPushAjax('" + checkURL + "');</script>";
        }

        BackButton.NavigateUrl = "Default.aspx";
    }
}