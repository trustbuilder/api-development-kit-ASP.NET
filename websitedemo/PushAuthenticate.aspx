<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PushAuthenticate.aspx.cs" Inherits="Default2" Debug="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%= this.initJavascript %>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>inWebo API PHP - Authentication with Mobile Notifications (Push)</h2>
    
    <p>Requires inWebo Authenticator installed and activated on your mobile phone</p>

    <div class="success" id="success" style="display:none">User <%= this.loginSent %> successfully authenticated</div>
    <div class="success" id="authenticationError" style="display:none">Could not authenticate user <%= this.loginSent %></div>
    
    <div class="success" id="sendToMobile" runat="server">Sending confirmation notification to your mobile</div>

    <form id="authentication" runat="server">
        <div class="authForm">
            <input class="textEntry" id="loginInput" name="login" type="text" placeholder="Enter login..." value="<%= this.loginInputValue %>" /><br/ >
            <input type="submit" id="sendPush" value="Send confirmation to mobile" />
        </div>
    </form>

   <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>