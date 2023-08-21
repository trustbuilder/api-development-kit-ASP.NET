using InWebo.ApiDemo;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);
        
        msgOutput.Visible = false;

        if (!string.IsNullOrEmpty(Request.QueryString["msg"]))
        {
            string msg = HttpUtility.UrlDecode(Request.QueryString["msg"]);
            msgOutput.InnerHtml = "<b>" + msg + "</b>";
            msgOutput.Visible = true;
        }

        //Getting current user connected to the machine
        string winLoginName = "";
        string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        if (!userName.Equals(""))
        {
            ;
            winLoginName = Regex.Replace(userName, ".*\\\\(.*)", "$1", RegexOptions.None);
            log.Info("Domain user name is: " + winLoginName);
        }
        else
        {
            log.Info("Failed to retrieve domain user");
            return;
        }


        //Checking if an inWebo service user can be used
        IWUser u = new IWUser();
        string loginName = "";

        //if a user login name has been defined in the config file, using it
        if (IWSettings.inWeboLogin != null && !IWSettings.inWeboLogin.Equals(""))
        {
            loginName = IWSettings.inWeboLogin;
        }
        // else using current connected domainUser name
        else
        {
            loginName = winLoginName;
        }

        log.Info("loginName is " + loginName);

        //Trying to retrieve user login properties with API function loginSearch
        IWUserList queryResult = webservices.loginSearch(loginName, 1, 0, 0, 0);

        if (webservices.errcode == "NOK:connectivity")
        {
            string msg = "Failed to connect to inWebo Web Services. Error message: " + webservices.exception;
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("error.aspx?error=" + urlMsg);
        }
        
        //User found
        else if (queryResult.Count > 0)
        {
            userSuggestCreate.Visible = false;
            u = queryResult[0];
            if (u.Firstname.Equals("") && u.Name.Equals(""))
            {
                Userfullname.Text = "Welcome!";
            }
            else
            {
                Userfullname.Text = "Welcome " + u.Firstname + " " + u.Name + "!";
            }
            
            Session.Add("curr_user", u);

            ActivateLink.NavigateUrl = "AddDevice.aspx";
            UnlockLink.NavigateUrl = "Unlock.aspx";
            SoapAuthenticateLink.NavigateUrl = "SoapAuthenticate.aspx";
            RestAuthenticateLink.NavigateUrl = "RestAuthenticate.aspx";
            PushAuthenticateLink.NavigateUrl = "PushAuthenticate.aspx";
            ManageUsersLink.NavigateUrl = "UserHome.aspx";
            ManageUserGroupsLink.NavigateUrl = "ServiceGroupHome.aspx";
            VerifyDataSealingLink.NavigateUrl = "SealVerify.aspx";
        }

        //No user
        else
        {
            userInfo.Visible = false;
            userSuggestCreate.Visible = true;
            
            ActivateLinkView.Visible = false;
            UnlockLinkView.Visible = false;
        }
    }
}