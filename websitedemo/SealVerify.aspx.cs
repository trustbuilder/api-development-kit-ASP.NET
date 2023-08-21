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

        IWUser u = new IWUser();
        u = (IWUser)Session["curr_user"];

        if (u != null && !Page.IsPostBack)
        {
            this.loginInputValue = u.Login;
        }

        if (!string.IsNullOrEmpty(Request.Form["login"]) && !string.IsNullOrEmpty(Request.Form["code"]) && !string.IsNullOrEmpty(Request.Form["data"]))
        {
            string login = Request.Form["login"];
            string code = Request.Form["code"];
            string data = Request.Form["data"];

            //Verify the sealed otp with API REST call
            IWRestFunctions restservices = new IWRestFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);
            string actionURL = "action=sealVerify&serviceId=" + IWSettings.serviceId+ "&userId=" + login + "&token=" + code + "&data=" + data + "&format=json";

            String queryResult = restservices.execRestCall(actionURL, "err");

            if (queryResult.StartsWith("NOK"))
            {
                string msg = "REST Seal Verify failed for user " + login + " and data " + data;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }

            success.InnerText = "Data sealing successfully verified";
            success.Visible = true;

            if (u != null && u.Login.Equals(login))
            {
                this.loginInputValue = u.Login;
            }
        }

        BackButton.NavigateUrl = "Default.aspx";
    }
}