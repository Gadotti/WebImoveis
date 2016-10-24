<%@ Page Title="Cadastro Clientes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master" CodeBehind="CadastroCliente.aspx.vb" 
Inherits="WebImoveis.CadastroCliente" Theme="TemaPrincipal" %>
<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<div id="posts">
        <div class="post">
            <h2 class="title">
                Cadastro Clientes</h2>
            <div class="story">
                <asp:MultiView runat="server" ID="mtPrincipal" ActiveViewIndex="0">
                    <asp:View runat="server" ID="vwConsulta">
                         <div runat="server" id="divTotalRegistro" visible="true">
                            <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
                            <br />
                            <br />
                        </div>
                        <asp:GridView ID="gvCliente" runat="server" ShowFooter="False" AutoGenerateColumns="False" Width="600px"
                            DataKeyNames="Id" CssClass="ui-widget ui-widget-content" CellPadding="3" AllowPaging="true" PageSize="30">
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="ui-widget-header" />
                            <AlternatingRowStyle BackColor="#e6f2ff" />
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="22px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/edit.png" Height="20px"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id")%>' CommandName="editar" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Id" DataField="Id" ItemStyle-Width="20px"/>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:BoundField HeaderText="Sobrenome" DataField="Sobrenome" />
                                <asp:BoundField HeaderText="Telefones" DataField="Telefones" />
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
                                    Nome:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNome" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtNome" runat="server" ErrorMessage="*Campo Obrigatório" 
                                        ControlToValidate="txtNome" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    Sobrenome:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSobrenome" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Campo Obrigatório" 
                                        ControlToValidate="txtSobrenome" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CPF/CNPJ:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCpfCnpj" MaxLength="14"></asp:TextBox>
                                </td>
                                <td>
                                    Telefone Residencial:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTelefoneResidencial" MaxLength="14"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Telefone Comercial:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTelefoneComercial" MaxLength="14"></asp:TextBox>
                                </td>
                                <td>
                                    Telefone Celular:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTelefoneCelular" MaxLength="14"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    E-mail:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEmail" MaxLength="255"></asp:TextBox>
                                </td>
                                <td>
                                    Observações:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtObservacoes" MaxLength="255"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button runat="server" ID="btnSalvar" Text="Salvar" ValidationGroup="Cadastro" CausesValidation="true" />
                                    <asp:Button runat="server" ID="btnExcluir" Text="Excluir" CausesValidation="false"  />
                                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CausesValidation="false"  />
                                </td>
                            </tr>
                        </table>                        
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</asp:Content>
