<%@ Page Title="Edição de Imóvel" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master" CodeBehind="EdicaoImovel.aspx.vb" Inherits="WebImoveis.EdicaoImovel" Theme="TemaPrincipal" %>
<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="posts">
                <div class="post">
                    <h2 class="title">
                        Edição Imóvel</h2>
                    <div class="story">
                        <table>
                            <tr>
                                <td>
                                    Identificador Imóvel (Id):
                                </td>
                                <td>
                                    <cc1:CampoNumerico runat="server" ID="txtImovelId" TipoCampoNumerico="Inteiro" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnEditar" Text="Editar" />
                                </td>
                            </tr>                            
                        </table>
                    </div>
                </div>
                <div class="post">
                    <h2 class="title">
                        Consulta Imóvel</h2>
                    <div class="story">
                        <table>
                            <tr>
                                <td>
                                    Referência:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtReferencia" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Estado:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlEstado" Width="175px" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Cidade:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCidade" AutoPostBack="true" Width="175px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr> 
                                <td>
                                    Bairro:
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlBairro" Width="175px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tipo Imóvel:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTipoImovel" Width="175px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Tipo Material:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTipoMaterial" Width="175px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Valor Imóvel
                                </td>
                                <td colspan="3">
                                    <cc1:CampoNumerico ID="txtValor" runat="server" TipoCampoNumerico="Valor" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button runat="server" ID="btnConsultar" Text="Consultar" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="dtgImovel" runat="server" ShowFooter="False" AutoGenerateColumns="False" DataKeyNames="Id" Width="800px" 
                        CssClass="ui-widget ui-widget-content" CellPadding="3"  >
                            <PagerStyle HorizontalAlign="Center"  />
                            <HeaderStyle CssClass="ui-widget-header" />
                            <AlternatingRowStyle BackColor="#e6f2ff" />
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/edit.png" Height="20px" 
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id")%>'
                                        CommandName="editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Id" DataField="Id" />       
                                <asp:BoundField HeaderText="Referência" DataField="Referencia" />
                                <asp:BoundField HeaderText="Cidade" DataField="CidadeDescricao" />
                                <asp:BoundField HeaderText="Bairro" DataField="BairroDescricao" />
                                <asp:BoundField HeaderText="Tipo Imóvel" DataField="TipoImovelDescricao" />
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <%# Utilitarios.Utils.FormataValor(DataBinder.Eval(Container, "DataItem.Valor"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publicado">
                                    <ItemTemplate>
                                        <%# IIf(CBool(DataBinder.Eval(Container, "DataItem.InPublicar")), "Sim", "Não").ToString %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Visualizações" DataField="QtdeVisitas" />                                
                                <asp:TemplateField HeaderText="Exportar Anúncio">                                    
                                    <ItemTemplate>
                                        <a href="ExportarImovel.ashx?imovelId=<%# DataBinder.Eval(Container, "DataItem.Id")%>">Imóveis-SC</a>                                       
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
