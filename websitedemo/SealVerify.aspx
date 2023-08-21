<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SealVerify.aspx.cs" Inherits="Default2" Debug="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>inWebo API PHP - Data Sealing Verification (REST)</h2>
    <p>Data sealing requires a valid and activated mAccess application</p>

    <div class="success" id="success" runat="server"></div>

    <form id="verify" runat="server">
        <div class="authForm">
            <input class="textEntry" id="loginInput" name="login" type="text" placeholder="Enter login..." value="<%= this.loginInputValue %>" /><br/ >
            <input class="passwordEntry" id="codeInput" name="code" type="text" placeholder="Enter OTP..." /><br/ >
            <input class="textEntry" id="dataInput" name="data" type="text" placeholder="Enter Data to verify..." /><br/>
            <input type="submit" value="Verify Data" />
        </div>
    </form>

   <p><asp:HyperLink ID="BackButton" runat="server">Back</asp:HyperLink></p>

</asp:Content>
