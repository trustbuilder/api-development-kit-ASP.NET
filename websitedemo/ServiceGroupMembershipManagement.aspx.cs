using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(_Default));

        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        int groupId;
        int loginId;
        string groupName;
        string action;

        if (!string.IsNullOrEmpty(Request.QueryString["groupId"]) && !string.IsNullOrEmpty(Request.QueryString["loginId"]))
        {
            groupId = int.Parse(Request.QueryString["groupId"]);
            loginId = int.Parse(Request.QueryString["loginId"]);
            groupName = Request.QueryString["groupName"];
            action = Request.QueryString["action"];

            if (action == "add") {

                //creating new group membership with API function groupMembershipCreate
                string aresult = webservices.groupMembershipCreate(groupId, loginId, 0); //user created with roleId = 0 (simple service users)

                if (aresult.StartsWith("NOK"))
                {
                    string msg = "Failed to add user " + loginId + " to group " + groupName + ", groupId" + groupId;
                    log.Error(msg);
                    string urlMsg = HttpUtility.UrlEncode(msg);
                    Response.Redirect("error.aspx?error=" + urlMsg);
                }
                else 
                {
                    Response.Redirect("ServiceGroupManagement.aspx?groupId=" + groupId + "&groupName=" + HttpUtility.UrlEncode(groupName));
                }

            }
            else if (action == "delete") 
            {
                //deleting group membership with API function groupMembershipDelete
                string dresult = webservices.groupMembershipDelete(groupId, loginId);

                if (dresult.StartsWith("NOK"))
                {
                    string msg = "Failed to delete user " + loginId + " from group " + groupName + ", groupId" + groupId;
                    log.Error(msg);
                    string urlMsg = HttpUtility.UrlEncode(msg);
                    Response.Redirect("error.aspx?error=" + urlMsg);
                }
                else 
                {
                    Response.Redirect("ServiceGroupManagement.aspx?groupId=" + groupId + "&groupName=" + HttpUtility.UrlEncode(groupName));
                }
            }
        }
        else
        {
            string msg = "Cannot init groupMembership management. Missing groupId and/or loginId";
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("error.aspx?error=" + urlMsg);
        }
    }
}