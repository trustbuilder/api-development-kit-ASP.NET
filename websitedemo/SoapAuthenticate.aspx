<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SoapAuthenticate.aspx.cs" Inherits="Default2" Debug="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>inWebo API PHP - Authentication (SOAP)</h2>

    <div class="success" id="success" runat="server"></div>

    <form id="authentication" runat="server">
        <div class="authForm">
            <input class="textEntry" id="loginInput" name="login" type="text" placeholder="Enter login..." value="<%= this.loginInputValue %>" /><br/ >
            <input class="passwordEntry" id="codeInput" name="code" type="text" placeholder="Enter OTP..." /><br/ >
            <input type="submit" value="Log on" />
        </div>
    </form>

   <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
