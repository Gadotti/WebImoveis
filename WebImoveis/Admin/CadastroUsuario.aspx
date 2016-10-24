<%@ Page Title="Cadastro Usuários" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="CadastroUsuario.aspx.vb" Inherits="WebImoveis.CadastroUsuario" Theme="TemaPrincipal" %>

<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Cadastro de usuários com acesso a área restrita do site.</h2>
            <div class="story">
                <asp:MultiView runat="server" ID="mtPrincipal" ActiveViewIndex="0">
                    <asp:View runat="server" ID="vwConsulta">
                        <div runat="server" id="divTotalRegistro" visible="true">
                            <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
                            <br />
                            <br />
                        </div>
                        <asp:GridView ID="gvUsuario" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                            Width="600px" DataKeyNames="Id" CssClass="ui-widget ui-widget-content" CellPadding="3"
                            AllowPaging="true" PageSize="30">
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="ui-widget-header" />
                            <AlternatingRowStyle BackColor="#e6f2ff" />
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="22px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/edit.png" Height="20px"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id")%>' CommandName="editar"
                                            CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Id" DataField="Id" ItemStyle-Width="20px" />
                                <asp:BoundField HeaderText="Login" DataField="Login" />
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:BoundField HeaderText="Email" DataField="Email" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Button runat="server" ID="btnNovo" Text="Novo" />
                    </asp:View>
                    <asp:View runat="server" ID="vwEdicao">
                        <table>
                            <tr>
                                <td>
                                    Id:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtId" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Login:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtLogin" MaxLength="45"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtLogin" runat="server" ErrorMessage="*Campo Obrigatório"
                                        ControlToValidate="txtLogin" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    Nome:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNome" MaxLength="45"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Campo Obrigatório"
                                        ControlToValidate="txtNome" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Senha:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSenha" MaxLength="45" TextMode="Password"></asp:TextBox>
                                </td>
                                <td>
                                    Repetir Senha:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSenhaRepetirSenha" MaxLength="45" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    E-mail:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtEmail" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button runat="server" ID="btnSalvar" Text="Salvar" ValidationGroup="Cadastro"
                                        CausesValidation="true" />
                                    <asp:Button runat="server" ID="btnExcluir" Text="Excluir" CausesValidation="false" />
                                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</asp:Content>
