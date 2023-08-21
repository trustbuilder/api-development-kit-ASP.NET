<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ServiceGroupHome.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>User Group Management</h2>

    <div id="msgOutput" runat="server"></div>

    <div id="userGroupSuggestCreate" runat="server">No user group found for your service. You should create at least one user group in inWebo administration console.</div>

    <h3>Service available user groups are:</h3>

    <ul runat="server">
        <%= this.serviceGroupList %>
    </ul>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
