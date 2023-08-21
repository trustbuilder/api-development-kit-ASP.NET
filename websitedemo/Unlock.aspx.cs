using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    protected void Page_Load(object sender, EventArgs e)
    {
        //Retrieving current user in session
        IWUser u = new IWUser();
        u = (IWUser)Session["curr_user"];

        if (u == null)
        {
            string msg = "No inWebo user found";
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("default.aspx?msg=" + urlMsg);
        }

        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);
        int loginid = (int)u.Id;

        //Generating device unlock code with API function loginResetPwd
        String queryResult = webservices.loginResetPwd(loginid, 0);

        if (queryResult.StartsWith("NOK"))
        {
            string msg = "inWebo Unlock Device Failed. Error message: " + queryResult;
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("error.aspx?error=" + urlMsg);
        }

        Unlock_Code.Text = queryResult;

        BackButton.NavigateUrl = "Default.aspx";
    }
 
}
