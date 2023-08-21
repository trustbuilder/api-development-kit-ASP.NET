<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Sample Functions</h2>

    <div id="msgOutput" runat="server"></div>

    <div runat="server" id="userInfo">
        <p><span class="bold"><asp:Label ID="Userfullname" runat="server" Text="Label"></asp:Label></span></p>
    </div>

    <div runat="server" id="userSuggestCreate">
        <p>For advanced testing it is recommended to add an inWebo service user to the demo site. You may:</p>
        <ul>
            <li>Create a new inWebo service user.
                <ul>
                    <li>You may define its login name with your current domain username (as shown at the top right of this page).<br />The site will automatically make the link between the two users</li>
                    <li>Or simply enter the login name of the newly created inWebo user in section "appSettings" of file "Web.config"</li>
                </ul>
            </li>
            <li>Enter the login name of an already existing inWebo service user in section "appSettings" of file "Web.config"</li>
        </ul>
    </div>

    <ul>
        <li id="ActivateLinkView" runat="server"><asp:HyperLink id="ActivateLink" runat="server">Activate a new Device</asp:HyperLink></li>
        <li id="UnlockLinkView" runat="server"><asp:HyperLink id="UnlockLink" runat="server">Unlock a Device</asp:HyperLink></li>
        <li><asp:HyperLink id="SoapAuthenticateLink" runat="server">Authenticate with SOAP</asp:HyperLink></li>
        <li><asp:HyperLink id="RestAuthenticateLink" runat="server">Authenticate with REST</asp:HyperLink></li>
        <li><asp:HyperLink id="PushAuthenticateLink" runat="server">Authenticate with Mobile Notifications</asp:HyperLink> (Push)</li>
        <li><asp:HyperLink id="ManageUsersLink" runat="server">Manage Users</asp:HyperLink></li>
        <li><asp:HyperLink id="ManageUserGroupsLink" runat="server">Manage Users in Groups</asp:HyperLink></li>
        <li><asp:HyperLink id="VerifyDataSealingLink" runat="server">Verify Data Sealing</asp:HyperLink> (requires mAccess)</li>
    </ul>
</asp:Content>
