<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tweakers.Login" %>

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
    <link href="Login.css" rel="stylesheet" />
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
                        <asp:Button CssClass="btn btn-lg btn-success logout" ID="LoginOrOut" runat="server" Text="Login" OnClick="LoginOrOut_Click" Enabled="false"/>
                    </div>
                    <!--/.nav-collapse -->
                </div>
            </div>
            <div class="container">
                <div class="form-signin">
                    <h2 class="form-signin-heading">Please sign in</h2>
                    <asp:TextBox CssClass="form-control" ID="tb_rfid" runat="server" placeholder="RFID" type="text" required="required" />
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
        </div>
    </form>
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
