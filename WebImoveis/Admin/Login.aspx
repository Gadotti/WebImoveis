<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebImoveis.Login"
    Theme="TemaPrincipal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/themes/redmond/jquery-ui-1.8.12.custom.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" href="../Styles/themes/redmond/jquery.ui.all.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="wrapper">
        <div id="header">
            <%--<h1>
                <a href="http://www.gadottiimoveis.com/">Gadotti</a></h1>
            <h2>
                <a href="http://www.gadottiimoveis.com/">Corretor de imóveis</a></h2>--%>
        </div>
        <div id="pages">
            <img runat="server" id="pagesimg" src="~/Images/img2.jpg" alt="img" />
        </div>
        <div id="content">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="overlay">
                        <div id="divAguarde" class="ui-widget-content ui-corner-all" style="width: 240px;
                            padding: 0.4em; position: absolute; left: 50%; top: 50%; margin-left: -170px;
                            margin-top: -100px;">
                            <h3 class="ui-widget-header ui-corner-all" style="margin-top: 0px;">
                                Carregando</h3>
                            <p style="text-align: center">
                                <img src="../Images/loading.gif" alt="Carregando" />
                                Aguarde...
                            </p>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="posts">
                        <div class="post" style="margin-left: 220px;">
                            <h2 class="title">
                                Login Sistema</h2>
                            <h3 class="posted">
                            </h3>
                            <div class="story">
                                <table>
                                    <tr>
                                        <td colspan="2" style="height: 50px">
                                            <div class="ui-widget">
                                                <div id="divMensagemErro" runat="server" class="ui-state-error ui-corner-all" style="padding: 0 .7em;">
                                                    <p>
                                                        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                                        <strong>Erro:</strong> Login e/ou Senha inválidos.</p>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Login:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Senha:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSenha" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="btnEntrar" Text="Entrar" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="meta">
                                <p>
                                </p>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="search" class="boxed">
            </div>
            <div id="archives" class="boxed">
            </div>
        </div>
        <div id="footer">
            <p>
                Copyright &copy; 2011 WebImóveis. Designed by <a href="http://www.freecsstemplates.org/">
                    <strong>Eduardo Gadotti</strong></a></p>
        </div>
    </div>
    </form>
</body>
</html>
