<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tweakers.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Custom styles for this page -->
    <link href="Login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-signin">
            <h2 class="form-signin-heading">Please sign in</h2>
            <asp:TextBox CssClass="form-control" ID="tb_user" runat="server" placeholder="RFID" type="text" required="required" />
            <asp:TextBox CssClass="form-control" ID="tb_pw" runat="server" type="password" placeholder="Password" required="required" />
            <label class="checkbox">
                <asp:CheckBox ID="cb_remember" runat="server" />
                Remember me
            </label>
            <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="Submit" Text="Sign in" runat="server" OnClick="Submit_Click" />
        </div>
        <div class="form-signin">
            <asp:Panel CssClass="form-control alert alert-danger" ID="InvalidLogin" runat="server" Visible="false">
                <asp:Label ID="InvalidLoginText" runat="server" Text="Error" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>
