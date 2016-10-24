<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="CadastroEstado.aspx.vb" Inherits="WebImoveis.CadastroEstado"  Theme="TemaPrincipal" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Cadastro Estados</h2>
            <div class="story">
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
                            Estado:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDescricao" MaxLength="45" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtNome" runat="server" ErrorMessage="*Campo Obrigatório"
                                ControlToValidate="txtDescricao" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Sigla:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSigla" MaxLength="2" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Campo Obrigatório"
                                ControlToValidate="txtSigla" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button runat="server" ID="btnSalvar" Text="Salvar" ValidationGroup="Cadastro"
                                CausesValidation="true" Visible="false" />
                            <asp:Button runat="server" ID="btnExcluir" Text="Excluir" CausesValidation="false"
                                Visible="false" />
                            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CausesValidation="false"
                                Visible="false" />
                            <asp:Button runat="server" ID="btnNovo" Text="Novo" CausesValidation="false" Visible="true" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvEstado" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                    Width="600px" DataKeyNames="Id" CssClass="ui-widget ui-widget-content" CellPadding="3"
                    AllowPaging="false">
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
                        <asp:BoundField HeaderText="Descricao" DataField="Descricao" />
                        <asp:BoundField HeaderText="Sigla" DataField="Sigla" />
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
