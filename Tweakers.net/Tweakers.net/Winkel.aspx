<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Winkel.aspx.cs" Inherits="Tweakers.Winkel1" %>

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
    <link href="Default.css" rel="stylesheet" />
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
                    <asp:Label ID="TitleWinkel" runat="server" CssClass="h1" Text="Winkel_naam"></asp:Label>
                </div>
                <asp:Label ID="LabelLocatie" runat="server" CssClass="lead" Text="Locatie"></asp:Label>
                <asp:Label ID="LabelAwards" runat="server" CssClass="lead" Text="Awards"></asp:Label>
            </div>
        </div>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
