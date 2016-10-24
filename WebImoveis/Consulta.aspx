<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Consulta.aspx.vb" Inherits="WebImoveis.Consulta" Theme="TemaPrincipal" %>

<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<script type="text/javascript">
    function PopupCenter(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=0, left=' + left);
    } 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Consulta de imóvel</h2>
            <h3 class="posted">
            </h3>
            <div class="story">
                <p>
                </p>
                <table>
                    <tr>
                        <td style="width: 80px">
                            Referência:
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="txtReferencia"></asp:TextBox>
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
                    <asp:Panel runat="server" ID="pnlBairro">
                        <tr>
                            <td>
                                Bairro:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList runat="server" ID="ddlBairro" Width="175px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </asp:Panel>
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
                            Valor de (R$):
                        </td>
                        <td>
                            <cc1:CampoNumerico ID="txtValorDe" runat="server" TipoCampoNumerico="Valor" />
                        </td>
                        <td>
                            Valor Até (R$):
                        </td>
                        <td>
                            <cc1:CampoNumerico ID="txtValorAte" runat="server" TipoCampoNumerico="Valor" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:LinkButton runat="server" ID="btnAbrirBuscaAvancada" Text="Busca Avançada" CssClass="linkbutton"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <div runat="server" id="divBuscaAvancada" visible="false">
                    <table>
                        <tr>
                            <td style="width: 80px; vertical-align: top">
                                Bairros:
                            </td>
                            <td colspan="3">
                                <div style="height: 100px; width: 300px; overflow: auto; border: 1px solid black;">
                                    <asp:CheckBoxList ID="cblBairros" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mínimo Dorms.:
                            </td>
                            <td>
                                <cc1:CampoNumerico runat="server" ID="txtQtdeDomitorio" TipoCampoNumerico="Inteiro" Width="80px" />
                            </td>
                            <td>
                                Mínimo Suíte.:
                            </td>
                            <td>
                                <cc1:CampoNumerico runat="server" ID="txtQtdeSuite" TipoCampoNumerico="Inteiro" Width="80px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Área Cons. De:
                            </td>
                            <td>
                                <cc1:CampoNumerico runat="server" ID="txtAreaConstruideDe" TipoCampoNumerico="Valor" Width="80px" />m²
                            </td>
                            <td>
                                Até:
                            </td>
                            <td>
                                <cc1:CampoNumerico runat="server" ID="txtAreaConstruideAte" TipoCampoNumerico="Valor" Width="80px"/>m²
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Deve possuir:
                            </td>
                            <td colspan="3">
                                <div style="height: 100px; width: 300px; overflow: auto; border: 1px solid black;">
                                    <asp:CheckBoxList ID="cblItensImovel" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:LinkButton runat="server" ID="btnAbrirBuscaBasica" Text="Busca Básica" CssClass="linkbutton"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                Obs: Os campos deixados em branco ou zerados, não serão levados em conta na consulta.
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div style="text-align:center">
                    <asp:Button runat="server" ID="btnConsultar" Text="Consultar" />
                </div>
                <br />
                <br />
                <div runat="server" id="divTotalRegistro" visible="false">
                    <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
                    <br />
                    <br />
                </div>
                <asp:GridView ID="dtgImovel" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                    DataKeyNames="Id" Width="800px" CssClass="ui-widget ui-widget-content" CellPadding="3"
                    PageSize="50" AllowPaging="true">
                    <PagerStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="ui-widget-header" />
                    <AlternatingRowStyle BackColor="#e6f2ff" />
                    <Columns>
                        <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="VisualizarImovel.aspx?imovelid=<%# DataBinder.Eval(Container, "DataItem.Id")%>"
                                    target="_blank" onclick="PopupCenter(this.href, this.target, 650, 660); return false;">
                                    <img src="../Images/camera_icon.gif" alt="" height="20px" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Ref." DataField="Referencia" />
                        <asp:BoundField HeaderText="Tipo Imóvel" DataField="TipoImovelDescricao" />
                        <asp:BoundField HeaderText="Cidade" DataField="CidadeDescricao" />
                        <asp:BoundField HeaderText="Bairro" DataField="BairroDescricao" />
                        <asp:BoundField HeaderText="Dorms." DataField="QtdeDormitorio" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Suítes" DataField="QtdeSuite" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Garagem" DataField="QtdeGaragem" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Área (m²)" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# Utilitarios.Utils.FormataValor(DataBinder.Eval(Container, "DataItem.AreaConstruida"))%>
                                m²
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor (R$)" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                R$
                                <%# Utilitarios.Utils.FormataValor(DataBinder.Eval(Container, "DataItem.Valor"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
