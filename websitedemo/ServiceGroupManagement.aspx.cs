using InWebo.ApiDemo;
using System;
using System.Web;
using log4net;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    public string groupUserList;
    public string serviceUserList;
    string receivedGroupId;

    protected void Page_Load(object sender, EventArgs e)
    {
        IWSoapFunctions webservices = new IWSoapFunctions(IWSettings.p12file, IWSettings.p12password, IWSettings.serviceId);

        msgOutput.Visible = false;

        if (!string.IsNullOrEmpty(Request.QueryString["groupId"]))
        {
            receivedGroupId = Request.QueryString["groupId"];
            int groupId = int.Parse(receivedGroupId);
            string groupName = HttpUtility.UrlDecode(Request.QueryString["groupname"]);
            UserGroupName.Text = groupName;

            //Get group users - Fectching the 10 first users
            IWUserList gresult = webservices.loginsQueryByGroup(groupId, 0, 10, 0);
            if (webservices.errcode == "NOK:connectivity")
            {
                string msg = "Failed to connect to inWebo Web Services. Error message: " + webservices.exception;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }
            else if (webservices.errcode.StartsWith("NOK"))
            {
                string msg = "Failed to retrieve service group users. Error message: " + webservices.errcode;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }
            else
            {
                if (gresult.Count > 0)
                {
                    //looping over the group users for display
                    for (int i = 0; i < gresult.Count; i++)
                    {
                        IWUser ug = gresult[i];
                        groupUserList += "<li><b>" + ug.Login + "</b> - <a href=\"ServiceGroupMembershipManagement.aspx?action=delete&loginId=" + ug.Id
                            + "&groupId=" + groupId + "&groupName=" + HttpUtility.UrlEncode(groupName) + "\">Remove user from group</a></li>";
                    }
                }
                else
                {
                    groupUserList += "<li>No users in the group</li>";
                }
            }

            //Get service users - Fectching the 10 first users
            IWUserList sresult = webservices.loginsQuery(0, 10, 0);
            if (webservices.errcode == "NOK:connectivity")
            {
                string msg = "Failed to connect to inWebo Web Services. Error message: " + webservices.exception;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }
            else if (webservices.errcode.StartsWith("NOK"))
            {
                string msg = "Failed to retrieve service users. Error message: " + webservices.errcode;
                log.Error(msg);
                string urlMsg = HttpUtility.UrlEncode(msg);
                Response.Redirect("error.aspx?error=" + urlMsg);
            }
            else
            {
                if (sresult.Count > 0)
                {
                    //looping over the service users for display
                    for (int i = 0; i < sresult.Count; i++)
                    {
                        IWUser us = sresult[i];
                        bool inGroup = false;

                        //Test if service user member of the group
                        for (int j = 0; j < gresult.Count; j++)
                        {
                            IWUser ug = gresult[j];
                            if (us.Id.Equals(ug.Id))
                            {
                                inGroup = true;
                            }
                        }

                        serviceUserList += "<li><b>" + us.Login + "</b>";

                        //Not in group - displaying link to allow adding user to the group
                        if (false == inGroup)
                        {
                            serviceUserList += " - <a href=\"ServiceGroupMembershipManagement.aspx?action=add&loginId=" + us.Id
                                + "&groupId=" + groupId + "&groupName=" + HttpUtility.UrlEncode(groupName) + "\">Add user to group</a>";
                        }
                            
                        serviceUserList += "</li>";
                    }
                }
                else
                {
                    serviceUserList += "<li>No users in the service</li>";
                }
            }
        }
        else
        {
            string msg = "Cannot init group management. Missing groupId";
            log.Error(msg);
            string urlMsg = HttpUtility.UrlEncode(msg);
            Response.Redirect("error.aspx?error=" + urlMsg);
        }
        
        BackButton.NavigateUrl = "ServiceGroupHome.aspx";
    }
}