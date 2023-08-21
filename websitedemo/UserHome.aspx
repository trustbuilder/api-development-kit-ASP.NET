<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserHome.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>User Management</h2>

    <p>We recommend you do not manipulate your own inWebo service user here. Use one of several dummy service users instead.</p>

    <div id="msgOutput" runat="server"></div>

    <p><asp:HyperLink id="UserCreateLink" runat="server">Create a new user</asp:HyperLink></p>

    <p>Search user</p>
    <form id="loginSearch" runat="server">
            <input class="textEntry" id="loginInput" name="login" type="text" placeholder="Enter a user login..." value="<%= this.loginInputValue %>" /><br/ >
            <input type="submit" id="sendPush" value="Search" />
    </form>

    <div id="UserInfo" runat="server">
        <p>User <b><asp:Label ID="UserName" runat="server" Text="Label"></asp:Label></b>&nbsp;
            <asp:HyperLink id="LoginUpdate" runat="server">Update user</asp:HyperLink>&nbsp;-&nbsp;
            <asp:HyperLink id="LoginDelete" runat="server">Delete user</asp:HyperLink></p>
    </div>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
