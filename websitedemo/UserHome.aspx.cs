using InWebo.ApiDemo;
using System;
using System.Web;
using System.Web.UI;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    public string loginInputValue = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        msgOutput.Visible = false;

        UserCreateLink.NavigateUrl = "LoginCreate.aspx";

        if (!string.IsNullOrEmpty(Request.QueryString["msg"]))
        {
            string msg = HttpUtility.UrlDecode(Request.QueryString["msg"]);
            msgOutput.InnerHtml = "<b>" + msg + "</b>";
            msgOutput.Visible = true;
        }

        UserInfo.Visible = false;

        //A user has been searched or has been previously edited
        if (!string.IsNullOrEmpty(Request.Form["login"]))
        {
            this.loginInputValue = Request.Form["login"];
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["login"]))
        {
            this.loginInputValue = Request.QueryString["login"];
        }

        if (!string.IsNullOrEmpty(this.loginInputValue))
        {
               
            //Trying to retrieve user login properties with API function loginSearch
            IWUserList queryResult = webservices.loginSearch(this.loginInputValue, 1, 0, 0, 0);

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
                IWUser u = new IWUser();
                u = queryResult[0];

                UserName.Text = u.Login;
                if (u.Firstname != "" || u.Name != "")
                {
                    UserName.Text += " - " + u.Firstname + " " + u.Name;
                }

                LoginUpdate.NavigateUrl = "LoginUpdate.aspx?loginId=" + u.Id;
                LoginDelete.NavigateUrl = "LoginDelete.aspx?loginId=" + u.Id;

                UserInfo.Visible = true;
            }
        }

        BackButton.NavigateUrl = "Default.aspx";
    }
}