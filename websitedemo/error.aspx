<%@ Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="error.aspx.cs" Inherits="Default2" Debug="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>An error occured</h2>

    <div id="errorOutput" runat="server" style="margin-top: 18px"></div>

    <p>Reasons might be:</p>
    <ul>
        <li>Wrong <b>inWebo API function</b> call</li>
        <li><b>inWebo service ID</b> is missing</li>
        <li><b>inWebo certificate file</b> is missing</li>
        <li><b>inWebo user login name</b> is misspelled</li>
        <li><b>inWebo certificate passphrase</b> is missing or erroneous</li>
    </ul>

    <p>Please check your configuration in section "appSettings" of file "Web.config"</p>
    
    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>