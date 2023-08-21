<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="restore.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2>Restore a user</h2>
    
    <p>User <asp:Label ID="LoginName" runat="server" Text="Label"></asp:Label> can be restored with code: <asp:Label ID="RestoreCode" runat="server" Text="Label"></asp:Label></p>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
