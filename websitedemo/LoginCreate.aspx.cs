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
    public string loginCodetypeOptionValue = "0";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        success.Visible = false;

        //If form is posted
        if (Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.Form["login"]))
            {
                string login = Request.Form["login"];
                string loginFirstname = Request.Form["loginFirstname"];
                string loginName = Request.Form["loginName"];
                string loginEmail = Request.Form["loginEmail"];
                string loginCodetype = Request.Form["loginCodetype"];

                int _codetype = int.Parse(loginCodetype);

                //Creating a new user properties with API function LoginCreate
                IWUser queryResult = webservices.loginCreate(login, loginFirstname, loginName, loginEmail, 0, 0, _codetype, "en");

                if (webservices.errcode == "NOK:connectivity")
                {
                    string msg = "Failed to connect to inWebo Web Services. Error message: " + webservices.exception;
                    log.Error(msg);
                    string urlMsg = HttpUtility.UrlEncode(msg);
                    Response.Redirect("error.aspx?error=" + urlMsg);
                }

                else if (webservices.errcode.StartsWith("NOK")) {
                    string msg = "Failed to create new user " + login + ". Error message: " + webservices.errcode;
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
                    this.loginCodetypeOptionValue = loginCodetype;

                    if (loginCodetype == "0")
                    {
                        success.InnerHtml = "User <b>" + queryResult.Login + "</b> successfully created. He may activated a new device with code: <b>" + queryResult.Code + "</b>";
                    }
                    else if (loginCodetype == "2")
                    {
                        String longCode = queryResult.Code;
                        success.InnerHtml = "User <b>" + queryResult.Login + "</b> successfully created. 3 week long code generated is: <b>" + longCode + "</b><br/><br/>";

                        string activationCode = webservices.loginGetCodeFromLink(longCode);
                        string[] activationInfo = webservices.loginGetInfoFromLink(longCode);

                        success.InnerHtml += "Use function <b>loginGetCodeFromLink</b> to retrieve the final activation code of the login that was created. Activation code is: <b>" + activationCode + "</b><br/>";
                        success.InnerHtml += "Use function <b>loginGetInfoFromLink</b> to retrieve the final activation code and the ID of the login that was created:";
                        success.InnerHtml += "<ul><li>Activation code is: <b>" + activationInfo[1] + "</b></li>";
                        success.InnerHtml += "<li>Login ID is: <b>" + activationInfo[2] + "</b></li></ul>";
                    }
                    success.Visible = true;
                }
            }
        }
        
        BackButton.NavigateUrl = "UserHome.aspx";
    }
}