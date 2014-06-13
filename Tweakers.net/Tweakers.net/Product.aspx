<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Tweakers.Product1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Custom styles for this page -->
    <link href="Product.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="page-header">
            <asp:Label ID="TitleProd" runat="server" CssClass="h1" Text="Product_naam"></asp:Label>
        </div>
        <div class="info">
            <asp:GridView ID="ItemTable" runat="server" Width="100%" AllowSorting="False" RowStyle-CssClass="lead" GridLines="Horizontal">
            </asp:GridView>
        </div>
        <div class="prices">
            <asp:GridView ID="PricesTable" runat="server" Width="100%" AllowSorting="False" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="PrijsDouble" HeaderText="Prijs" />
                    <asp:HyperLinkField DataTextField="Winkel" DataNavigateUrlFields="Winkel" DataNavigateUrlFormatString="Winkel.aspx?w={0}" DataTextFormatString="{0}" HeaderText="Winkel" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="container reviews">
        <asp:GridView ID="ReviewTable" runat="server" Width="100%" AllowSorting="False">
        </asp:GridView>
    </div>
    <div class="container">
        <div>
        <asp:TextBox ID="tb_onderwerp" runat="server" type="text" required="required" />
        <asp:TextBox ID="tb_tekst" runat="server" type="text" required="required" Style="width: 600px;" />
        <asp:TextBox ID="tb_beoordeling" runat="server" type="number" required="required" Text="0" />
            </div>
        <div>
            <asp:Panel CssClass="form-control alert alert-danger" ID="InvalidEntry" runat="server" Visible="false">
                <asp:Label ID="InvalidEntryText" runat="server" Text="Error" />
            </asp:Panel>
            <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="Submit" Text="Place review" runat="server" OnClick="Submit_Click" />
        </div>
    </div>
</asp:Content>
