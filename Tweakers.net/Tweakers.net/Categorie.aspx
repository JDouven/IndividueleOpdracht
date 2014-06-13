<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorie.aspx.cs" Inherits="Tweakers.Categorie1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Custom styles for this page -->
    <link href="Categorie.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="page-header">
            <asp:Label ID="TitleCat" runat="server" CssClass="h1" Text="Categorie_naam"></asp:Label>
        </div>
        <asp:GridView AutoGenerateColumns="False" ID="ItemTable" runat="server" Width="100%" GridLines="None" AllowSorting="False">
            <Columns>
                <asp:HyperLinkField DataTextField="Naam" DataNavigateUrlFields="Naam" DataNavigateUrlFormatString="Product.aspx?p={0}" DataTextFormatString="{0}" HeaderText="Naam" />
                <asp:BoundField DataField="Prijs" HeaderText="Prijs" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

