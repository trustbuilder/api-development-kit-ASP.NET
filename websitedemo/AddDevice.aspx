<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddDevice.aspx.cs" Inherits="Default2" Debug="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Activate a New Device</h2>

    <p>You may activate a new device using the following Activation Code: <b><asp:Label ID="Activation_Code" runat="server" Text="Label"></asp:Label></b></p>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
