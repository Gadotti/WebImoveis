<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MeusImoveis.aspx.vb" Inherits="WebImoveis.MeusImoveis" %>
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
                Meus de imóveis</h2>
            <h3 class="posted">
            </h3>
            <div class="story">               
                <div runat="server" id="divTotalRegistro" visible="true">
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
