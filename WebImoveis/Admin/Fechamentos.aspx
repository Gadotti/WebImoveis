<%@ Page Title="Vendas por Período" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="Fechamentos.aspx.vb" Inherits="WebImoveis.Fechamentos" Theme="TemaPrincipal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Fechamentos por período</h2>
            <div class="story">
                <table>
                    <tr>
                        <td>
                            Data de:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDataFechamentoDe"></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDataFechamentoDe" Format="dd/MM/yyyy" runat="server">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            Até:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDataFechamentoAte"></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDataFechamentoAte" Format="dd/MM/yyyy" runat="server">
                            </ajax:CalendarExtender>
                        </td>
                    </tr>                   
                    <tr>
                        <td colspan="4">
                            <asp:Button runat="server" ID="btnConsultar" Text="Consultar" />
                        </td>
                    </tr>
                </table>
                <br />
                <div runat="server" id="divTotalRegistro" visible="false">
                    <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
                    <br />
                    <br />
                </div>
                <asp:GridView ID="dtgImovel" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                    DataKeyNames="Id" Width="600px" CssClass="ui-widget ui-widget-content" CellPadding="3"
                    PageSize="50" AllowPaging="true">
                    <PagerStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="ui-widget-header" />
                    <AlternatingRowStyle BackColor="#e6f2ff" />
                    <Columns>
                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/edit.png" Height="20px"
                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id")%>' CommandName="editar" />
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
                        <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
