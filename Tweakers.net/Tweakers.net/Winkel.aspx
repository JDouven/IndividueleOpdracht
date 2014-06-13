<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Winkel.aspx.cs" Inherits="Tweakers.Winkel1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Custom styles for this page -->
    <link href="Default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="page-header">
            <asp:Label ID="TitleWinkel" runat="server" CssClass="h1" Text="Winkel_naam"></asp:Label>
        </div>
        <asp:Label ID="LabelLocatie" runat="server" CssClass="lead" Text="Locatie"></asp:Label>
        <asp:Label ID="LabelAwards" runat="server" CssClass="lead" Text="Awards"></asp:Label>
        </div>
</asp:Content>
