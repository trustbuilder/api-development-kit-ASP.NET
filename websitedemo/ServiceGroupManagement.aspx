<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ServiceGroupManagement.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Manage users of the group <b><asp:Label ID="UserGroupName" runat="server" Text="Label"></asp:Label></b></h2>

     <div id="msgOutput" runat="server"></div>

    <h3>Users of the group are:</h3>

    <ul id="Ul2" runat="server">
        <%= this.groupUserList %>
    </ul>
    

    <h3>Users of the service are:</h3>

    <ul id="Ul1" runat="server">

        <%= this.serviceUserList %>
    </ul>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>

