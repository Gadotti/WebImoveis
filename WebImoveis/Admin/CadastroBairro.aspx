<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="CadastroBairro.aspx.vb" Inherits="WebImoveis.CadastroBairro" Theme="TemaPrincipal" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Casdastro de Bairros</h2>
            <div class="story">
                <asp:MultiView runat="server" ID="mtPrincipal" ActiveViewIndex="0">
                    <asp:View runat="server" ID="vwConsulta">
                        <div>
                            Selecione Estado:
                            <asp:DropDownList runat="server" ID="ddlEstadoConsulta" AutoPostBack="true">
                            </asp:DropDownList>
                            <br />
                            Selecione Cidade:
                            <asp:DropDownList runat="server" ID="ddlCidadeConsulta" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <asp:GridView ID="gvBairro" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                            Width="400px" DataKeyNames="Id" CssClass="ui-widget ui-widget-content" CellPadding="3">
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
                                <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                <asp:BoundField HeaderText="Cidade" DataField="CidadeDescricao" />
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
                                    Estado:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlEstado" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Cidade:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCidade">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Descrição:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtDescricao" MaxLength="45"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtDescricao" runat="server" ErrorMessage="*Campo Obrigatório"
                                        ControlToValidate="txtDescricao" ValidationGroup="Cadastro" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
