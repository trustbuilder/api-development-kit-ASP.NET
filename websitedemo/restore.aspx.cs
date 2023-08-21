using InWebo.ApiDemo;
using System;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);
        IWUser u = new IWUser();

        String id = Request.QueryString["id"];
        int loginid = int.Parse(id);

        u = webservices.loginQuery(loginid);
        LoginName.Text = u.Login;
        
        String queryResult = webservices.loginRestore(loginid);

        RestoreCode.Text = queryResult;

        BackButton.NavigateUrl = "Default.aspx";
    }
}
