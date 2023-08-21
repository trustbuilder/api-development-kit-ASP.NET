<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LoginUpdate.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2>Create a new user</h2>

    <div class="success" id="success" runat="server"></div>

    <form name="loginUpdate" runat="server">
        <div class="authForm">
            <input class="textEntry" id="loginInput" name="login" type="text" placeholder="Enter user login..." value="<%= this.loginInputValue %>" /><br/>
            <input class="textEntry" id="loginFirstnameInput" name="loginFirstname" type="text" placeholder="Enter user firstname..." value="<%= this.loginFirstnameInputValue %>" /><br/>
            <input class="textEntry" id="loginNameInput" name="loginName" type="text" placeholder="Enter user name..." value="<%= this.loginNameInputValue %>" /><br/>
            <input class="textEntry" id="loginEmailInput" name="loginEmail" type="text" placeholder="Enter user email address..." value="<%= this.loginEmailInputValue %>" /><br/>
            <input name="loginId" type="hidden" value="<%= this.loginIdValue %>" />
            <input type="submit" value="Update" />
        </div>
    </form>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
