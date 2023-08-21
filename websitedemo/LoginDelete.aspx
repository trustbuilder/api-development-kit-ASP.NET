<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LoginDelete.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2>Delete a user</h2>

    <p><b>Warning:</b> this action cannot be cancelled.</p>

    <form name="loginDelete" runat="server">
        <div class="authForm">
            <input name="loginId" type="hidden" value="<%= this.loginIdValue %>" />
            <input name="login" type="hidden" value="<%= this.loginInputValue %>" />
            <input type="submit" value="<%= this.inputSubmitValue %>" />
            <input type="button" value="Cancel" onclick='javascript:document.location.href="UserHome.aspx"' />
        </div>
    </form>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
