<%@ Page Title="Configurações do Sistema" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="Configuracao.aspx.vb" Inherits="WebImoveis.Configuracao" Theme="TemaPrincipal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Configurações de E-mail</h2>
            <div class="story">
                <table>
                    <tr>
                        <td>
                            E-mail:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmail" MaxLength="45"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Senha E-mail:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmailSenha" MaxLength="45" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Smtp Host:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSmtpHost" MaxLength="45"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Smtp Pota:
                        </td>
                        <td>
                            <cc1:CampoNumerico runat="server" ID="txtSmtpPorta" MaxLength="11" TipoCampoNumerico="Inteiro" />
                        </td>
                    </tr>                    
                     <tr>
                        <td>
                            Título Site:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitulo" MaxLength="45"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="btnSalvar" Text="Salvar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
