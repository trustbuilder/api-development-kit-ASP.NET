using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    public string serviceGroupList = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        msgOutput.Visible = false;
        userGroupSuggestCreate.Visible = false;

        //Getting the first 25 groups in the service
        IWServiceGroupList queryResult = webservices.serviceGroupsQuery(0,25);

        if (webservices.errcode == "NOK:connectivity")
        {
            string msg = "Failed to connect to inWebo Web Services. Error message: " + webservices.exception;
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("error.aspx?error=" + urlMsg);
        }

        //Groups found
        else if (queryResult.Count > 0)
        {
            //looping of groups for display
            for (int i = 0; i < queryResult.Count; i++)
            {
                IWServiceGroup ug = queryResult[i];
                serviceGroupList += "<li><a href=\"serviceGroupManagement.aspx?groupId=" + ug.Id + "&groupName=" + HttpUtility.UrlEncode(ug.Name) + "\">" + ug.Name + "</a></li>";
            }
        }

        //No Group found
        else
        {
            userGroupSuggestCreate.Visible = true;
        }

        BackButton.NavigateUrl = "Default.aspx";
    }
}