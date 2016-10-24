<%@ Page Title="Estatísticas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="Default.aspx.vb" Inherits="WebImoveis._Default1" Theme="TemaPrincipal" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
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
            <h1>
                Estatísticas</h1>
            <div class="post">
                <h3>
                    Imóveis publicados:
                    <asp:Label runat="server" ID="lblImoveisPublicados" Text=""></asp:Label>
                    <br />
                </h3>
                <h3>
                    Imóveis fechados:
                    <asp:Label runat="server" ID="lblImoveisFechados" Text=""></asp:Label><br />
                </h3>
            </div>
            <div class="post">
                <h2>
                    Imóveis publicados mais procurados - 5 primeiros</h2>
                <asp:GridView ID="dtgMaisVisitados" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                    DataKeyNames="Id" Width="600px" CssClass="ui-widget ui-widget-content" CellPadding="3">
                    <PagerStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="ui-widget-header" />
                    <AlternatingRowStyle BackColor="#e6f2ff" />
                    <Columns>
                        <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="../VisualizarImovel.aspx?imovelid=<%# DataBinder.Eval(Container, "DataItem.Id")%>"
                                    target="_blank" onclick="PopupCenter(this.href, this.target, 650, 660); return false;">
                                    <img src="../Images/camera_icon.gif" alt="" height="20px" />
                                </a>
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
                        <asp:BoundField HeaderText="Visitas" DataField="QtdeVisitas" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="post">
                <table>
                    <tr>
                        <td>
                            <h3>
                                Bairros mais citados nas pesquisas</h3>
                            <asp:GridView ID="dtgBairros" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                                Width="300px" CssClass="ui-widget ui-widget-content" CellPadding="3">
                                <PagerStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="ui-widget-header" />
                                <AlternatingRowStyle BackColor="#e6f2ff" />
                                <Columns>
                                    <asp:BoundField HeaderText="Bairro" DataField="Descricao" />
                                    <asp:BoundField HeaderText="Cidade" DataField="CidadeDescricao" />
                                    <asp:BoundField HeaderText="Pesquisas" DataField="QtdePesquisa" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>
                            <h3>
                                Tipos de Imóveis mais citados nas pesquisas</h3>
                            <asp:GridView ID="gvTipoImoveis" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                                Width="250px" CssClass="ui-widget ui-widget-content" CellPadding="3">
                                <PagerStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="ui-widget-header" />
                                <AlternatingRowStyle BackColor="#e6f2ff" />
                                <Columns>
                                    <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                    <asp:BoundField HeaderText="Pesquisas" DataField="QtdePesquisa" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="post">
                <h3>
                    Valores (R$) mais pesquisados</h3>
                <asp:GridView ID="gvEstatisticas" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                    Width="250px" CssClass="ui-widget ui-widget-content" CellPadding="3">
                    <PagerStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="ui-widget-header" />
                    <AlternatingRowStyle BackColor="#e6f2ff" />
                    <Columns>
                        <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                        <asp:BoundField HeaderText="Pesquisas" DataField="QtdePesquisa" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
