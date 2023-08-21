using InWebo.ApiDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

public partial class _Default : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(_Default));
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["sessionId"]) && !string.IsNullOrEmpty(Request.QueryString["userId"]))
        {

            string sessionId = Request.QueryString["sessionId"];
            string userId = Request.QueryString["userId"];
            string json = "";

            IWRestFunctions restservices = new IWRestFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

            //Sending check push result to test if confirmation has been accepted or refused via REST API call
            string actionURL = "action=checkPushResult" + "&serviceId=" + IWSettings.serviceId + "&sessionId=" + sessionId + "&userId=" + userId + "&format=json";
            String queryResult = restservices.execRestCall(actionURL, "err");

            //Sending check push result as a JSON string

            if (queryResult.Equals("NOK:WAITING"))
            {
                json = "{\"result\":\"WAITING\"}";
            }
            else if (queryResult.Equals("OK"))
            {
                json = "{\"result\":\"OK\"}";
            }
            else
            {
                string msg = "checkPushResult failed for user " + userId + " - sessionId " + sessionId;
                log.Error(msg);
                json = "{\"result\":\"NOK\"}";
            }

            Response.Clear();
            Response.ContentType = "text/html; charset=utf-8";
            Response.Write(json);
            Response.End();
        }
        else
        {
            string msg = "Missing sessionId or userId while accessing CheckPushResult ";
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("error.aspx?error=" + urlMsg);
        }        
    }
}