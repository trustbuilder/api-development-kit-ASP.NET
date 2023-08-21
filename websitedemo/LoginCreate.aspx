<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LoginCreate.aspx.cs" Inherits="Default2" Debug="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2>Create a new user</h2>

    <div class="success" id="success" runat="server"></div>

    <form name="loginCreate" runat="server">
        <div class="authForm">
            Login:  <input class="textEntry" id="loginInput" name="login" type="text" placeholder="Enter user login..." value="<%= this.loginInputValue %>" /><br/>
            First name: <input class="textEntry" id="loginFirstnameInput" name="loginFirstname" type="text" placeholder="Enter user firstname..." value="<%= this.loginFirstnameInputValue %>" /><br/>
            Last name: <input class="textEntry" id="loginNameInput" name="loginName" type="text" placeholder="Enter user name..." value="<%= this.loginNameInputValue %>" /><br/>
            Email address: <input class="textEntry" id="loginEmailInput" name="loginEmail" type="text" placeholder="Enter user email address..." value="<%= this.loginEmailInputValue %>" /><br/>
            Activation code: <select id="loginCodetypeSelect" name="loginCodetype">
                    <option value="0" <% if (loginCodetypeOptionValue == "0") { %>selected<% } %>>Immediate code (15 minutes)</option>
                    <option value="2" <% if (loginCodetypeOptionValue == "2") { %>selected<% } %>>3 week code</option>
                 </select><br/>
            <input type="submit" value="Create" />
        </div>
    </form>

    <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
