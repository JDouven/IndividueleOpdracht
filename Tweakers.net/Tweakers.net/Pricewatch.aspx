<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pricewatch.aspx.cs" Inherits="Tweakers.Pricewatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Custom styles for this page -->
    <link href="Pricewatch.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="page-header">
            <h1>Categoriën</h1>
        </div>
        <asp:GridView AutoGenerateColumns="false" ID="ItemTable" runat="server" Width="100%" GridLines="None" AllowSorting="False">
            <Columns>
                <asp:HyperLinkField DataTextField="Naam" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Categorie.aspx?c={0}" DataTextFormatString="{0}" HeaderText="Naam" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
