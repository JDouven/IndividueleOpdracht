﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Tweakers.Product1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="shortcut icon" href="/assets/ico/favicon.ico" />

    <title>Tweakers</title>

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap theme -->
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" />
    <!-- Custom styles for this page -->
    <link href="Product.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Fixed navbar -->
            <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="Default.aspx">Tweakers</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li><a href="Pricewatch.aspx">Pricewatch</a></li>
                            <li><a href="User.aspx">User</a></li>
                        </ul>
                        <asp:Button CssClass="btn btn-lg btn-success logout" ID="LoginOrOut" runat="server" Text="Login" OnClick="LoginOrOut_Click" />
                    </div>
                    <!--/.nav-collapse -->
                </div>
            </div>
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
        </div>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>