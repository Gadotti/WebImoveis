<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VisualizarImovel.aspx.vb"
    Inherits="WebImoveis.VisualizarImovel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <link href="Styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/themes/redmond/jquery-ui-1.8.12.custom.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="Styles/lightbox.css" type="text/css" media="screen" />
    <script src="http://connect.facebook.net/pt_BR/all.js#xfbml=1"></script>
    <script src="Scripts/prototype.js" type="text/javascript"></script>
    <script src="Scripts/scriptaculous.js?load=effects" type="text/javascript"></script>
    <script src="Scripts/lightbox.js" type="text/javascript"></script>
    <style type="text/css">
        .heading
        {
            margin-top: 0;
            padding: 8px 15px;
            background: #5c9ccc url(Styles/themes/redmond/images/ui-bg_gloss-wave_55_5c9ccc_500x100.png) 50% 50% repeat-x;
            letter-spacing: 2px;
            color: #FFFFFF;
        }
    </style>
    <script type="text/javascript">
        //document.write('<div id="testexyz"><br><br>Please wait...</div>');

        function addLoadEvent(func) {
            var oldonload = window.onload;
            if (typeof window.onload != 'function') {
                window.onload = func;
            } else {
                window.onload = function () {
                    if (oldonload) {
                        oldonload();
                    }
                    func();
                }
            }
        }

        addLoadEvent(function () {
            document.getElementById("divGaleriaFotos").style.display = "block";
            document.getElementById("divCarregandoFotos").style.display = "none";
        });

        function enviarEmail(enviar) {
            if (enviar) {
                document.getElementById("divGaleriaFotos").style.display = "none";
                document.getElementById("divEnvioEmail").style.display = "inline";
            } else {
                document.getElementById("divGaleriaFotos").style.display = "block";
                document.getElementById("divEnvioEmail").style.display = "none";
            }

        };
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="wrapper" style="width: 550px">
        <div id="header">
        </div>
        <div style="background-color: #e6f2ff">
            <table style="padding-left: 20px; font-size: 1.5em;">
                <tr>
                    <td>
                        Referência:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblReferencia" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo Imóvel:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTipoImovel" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Valor:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblValor" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cidade:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblCidade" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bairro:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblBairro" Text=""></asp:Label>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblAreaConstruidaDescricao" Text="Área Cons.:"></asp:Label> 
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblAreaConstruida" Text=""></asp:Label>
                    </td>
                </tr>                
            </table>
        </div>
        <div id="content">
            <h1 class="heading">
                Características</h1>
            <table align="right">
                <tr>
                    <td>
                        <img alt"Enviar e-mail" src="Images/mail_icon.png" width="12px" />
                        <a href="#" onclick="enviarEmail(true); return false;">Enviar um e-mail</a> |
                    </td>
                    <td>
                        <img alt="Favoritar" src="Images/icon_destaques.png" width="12px" />
                        <asp:LinkButton runat="server" ID="lnkMarcarImovel" Text="Marcar este Imóvel"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkDermarcarImovel" Text="Desmarcar este Imóvel"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <br />
            <div style="float: left; width: 300px">
                <table>
                    <tr>
                        <td style="width:100px">
                            Tipo Negócio:
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblTipoNegocio" Text=""></asp:Label>
                        </td>
                    </tr>
                    <div runat="server" id="divTipoMaterial" visible="true">
                    <tr>
                        <td>
                            Tipo Material:
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblTipoMaterial" Text=""></asp:Label>
                        </td>
                    </tr>
                    </div>
                    <div runat="server" id="divValorCondominio">
                    <tr>
                        <td>
                            Valor Condomínio:
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblValorCondominio" Text=""></asp:Label>
                        </td>
                    </tr>
                    </div>
                    <tr>
                        <td>
                            Área Terreno (m²):
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblAreaTerreno" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Endereço:
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblEndereco" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <div runat="server" id="divDormSalaBanh">                    
                    <tr>
                        <td>
                            Dormitórios:
                        </td>
                        <td style="width: 60px">
                            <asp:Label runat="server" ID="lblQtdeDormitorio" Text=""></asp:Label>
                        </td>
                        <td>
                            Sendo, Suítes:
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblQtdeSuite" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Salas:
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblQtdeSala" Text=""></asp:Label>
                        </td>
                        <td>
                            Banheiros:
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblQtdeBanheiro" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vagas Garagem:
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblQtdeGaragem" Text=""></asp:Label>
                        </td>
                    </tr>
                    </div>
                    <tr>
                        <td valign="top">
                            Possui:
                        </td>
                        <td colspan="3" valign="top">
                            <asp:Literal runat="server" ID="ltrListaPossui"></asp:Literal>
                        </td>
                    </tr>
                    <div runat="server" id="divDescricao">
                        <tr>
                            <td>
                                Descrição Adicional:
                            </td>
                            <td colspan="3">
                                <asp:Label runat="server" ID="lblDescricao" Text=""></asp:Label>
                            </td>
                        </tr>
                    </div>
                    <div runat="server" id="divCondicaoVenda">
                        <tr>
                            <td>
                                Condições de Venda:
                            </td>
                            <td colspan="3">
                                <asp:Label runat="server" ID="lblCondicaoVenda" Text=""></asp:Label>
                            </td>
                        </tr>
                    </div>
                </table>
            </div>
            <div id="divCarregandoFotos">
                <strong>Carregando fotos, aguarde... </strong>
            </div>
            <div id="divGaleriaFotos" style="display: none">
            <br />
                <table>
                    <asp:Literal runat="server" ID="ltrGaleriaFotos"></asp:Literal>
                </table>
            </div>
            <div id="divEnvioEmail" style="display: none; float: right">
                <table width="200px" style="padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px">
                    <tr>
                        <td>
                            Nome:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtNome"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ErrorMessage="*"
                                ControlToValidate="txtNome" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Seu e-mail:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="txtEmail" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*E-mail inválido"
                                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contato para retorno:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtContato"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="txtContato" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Assunto:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtAssunto"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="txtAssunto" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Mensagem:
                        </td>
                    </tr>
                    <td>
                        <asp:TextBox runat="server" ID="txtMensagem" TextMode="MultiLine" Height="50px" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            ControlToValidate="txtMensagem" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnEnviar" Text="Enviar" CausesValidation="true" ValidationGroup="Email" />
                        </td>
                    </tr>
                </table>
                <br />
                <a href="#" onclick="enviarEmail(false); return false;">Voltar</a>
            </div>
        </div>
        <div>
        <br />
            Compartilhe este imóvel em sua rede social:<br />
            <asp:Literal runat="server" ID="ltrFacebook" Text=""></asp:Literal><br />
            <a href="http://twitter.com/share" class="twitter-share-button" data-text="Achei um imóvel interessante!" data-count="horizontal" data-lang="pt">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
            <br />
            <asp:Literal runat="server" ID="ltrOrkut" Text=""></asp:Literal><br />            

        </div>
        <div id="footer">
            <p>
                Copyright &copy; 2011 WebImóveis. Designed by <a href="mailto:gadotti.eduardo@gmail.com.br">
                    <strong>Eduardo Gadotti</strong></a></p>
        </div>
    </div>
    </form>
</body>
</html>
