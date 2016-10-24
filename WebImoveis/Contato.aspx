<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" Theme="TemaPrincipal"
    CodeBehind="Contato.aspx.vb" Inherits="WebImoveis.Contato" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Contato</h2>
            <h3 class="posted">
            </h3>
            <div class="story">
                <asp:Literal runat="server" ID="ltrContato" Text=""></asp:Literal>                
                <p style="font-size:1.3em">
                    Envie uma mensagem:
                </p>
                <table>
                    <tr>
                        <td>
                            Nome:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNome"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ErrorMessage="*Campo Obrigatório" 
                            ControlToValidate="txtNome" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Seu e-mail:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Campo Obrigatório" 
                            ControlToValidate="txtEmail" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*E-mail inválido" ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contato para retorno:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtContato"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Campo Obrigatório" 
                            ControlToValidate="txtContato" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Assunto:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAssunto" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Campo Obrigatório" 
                            ControlToValidate="txtAssunto" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Mensagem:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMensagem" TextMode="MultiLine" Height="100px" Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Campo Obrigatório" 
                            ControlToValidate="txtMensagem" ValidationGroup="Email" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="btnEnviar" Text="Enviar" CausesValidation="true" ValidationGroup="Email" />
                            <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CausesValidation="false" />
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
</asp:Content>
