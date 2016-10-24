<%@ Page Title="Novo Imóvel" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="CadastroImovel.aspx.vb" Inherits="WebImoveis.CadastroImovel" Theme="TemaPrincipal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Assembly="Utilitarios" Namespace="Utilitarios.WebComponents" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<script type="text/javascript">

    function verificaExclusao() {
        var answer = confirm("Deseja realmente excluir o imóvel e todas suas fotos?")
        if (answer)
            return true;
        else
            return false;
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="posts">
        <div class="post">
            <h2 class="title">
                Cadastro Imóvel</h2>
            <div class="story">
                <asp:MultiView runat="server" ID="mtwImovel" ActiveViewIndex="0">
                    <asp:View runat="server" ID="vwCadastro">
                        <table>
                            <tr>
                                <td>
                                    Id:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtId" Enabled="false"></asp:TextBox>
                                    (Gerado automaticamente)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Referência:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReferencia" runat="server" MaxLength="45"></asp:TextBox>
                                </td>
                                <td>
                                    Tipo Negocio:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTipoNegocio">
                                    </asp:DropDownList>
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
                                    <asp:DropDownList runat="server" ID="ddlCidade" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bairro:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlBairro">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Endereço:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEnderenco" MaxLength="45"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tipo Imóvel:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTipoImovel">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Tipo Material:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTipoMaterial">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Valor Imóvel (R$):
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtValor" runat="server" TipoCampoNumerico="Valor" />
                                </td>
                                <td>
                                    Valor Condomínio (R$):
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtValorCondominio" runat="server" TipoCampoNumerico="Valor" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Área Construida (m²):
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtAreaConstruida" runat="server" TipoCampoNumerico="Valor" />
                                </td>
                                <td>
                                    Área Terreno (m²):
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtAreaTerreno" runat="server" TipoCampoNumerico="Valor" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Qtde Dormitórios:
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtQtdeDormitorio" runat="server" TipoCampoNumerico="Inteiro" />
                                </td>
                                <td>
                                    Sendo, Suítes:
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtQtdeSuite" runat="server" TipoCampoNumerico="Inteiro" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Qtde Banheiros:
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtQtdeBanheiro" runat="server" TipoCampoNumerico="Inteiro" />
                                </td>
                                <td>
                                    Qtde Sala:
                                </td>
                                <td>
                                    <cc1:CampoNumerico ID="txtQtdeSala" runat="server" TipoCampoNumerico="Inteiro" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Qtde Garagens:
                                </td>
                                <td colspan="3">
                                    <cc1:CampoNumerico ID="txtQtdeGaragem" runat="server" TipoCampoNumerico="Inteiro" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InCozinha" Text="Cozinha" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InCopa" Text="Copa" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InPiscina" Text="Piscina" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InAreaServico" Text="Área de Serviço" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InDependenciaEmpregada" Text="Dep. de Empregada" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InChurrasqueira" Text="Churrasqueira" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InCloset" Text="Closet" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InAdega" Text="Adega" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InAreaFesta" Text="Área de Festas" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InAreaJogos" Text="Área de Jogos" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InLareira" Text="Lareira" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InSacada" Text="Sacada" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InEscritorio" Text="Escritório" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InGasCentral" Text="Gás Central" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InPortaoEletronico" Text="Portão Eletrônico" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InMobiliado" Text="Mobiliado" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InLancamento" Text="Lançamento" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InCobertura" Text="Cobertura" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InPronto" Text="Pronto" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InPorteiroEletronico" Text="Porteiro Eletrônico" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InLavabo" Text="Lavabo" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InTerraco" Text="Terraço" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Descrição Adicional:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtDescricao" TextMode="MultiLine" Width="300px" MaxLength="500"
                                        Height="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Condições de Venda:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtCondicaoVenda" TextMode="MultiLine" Width="300px" MaxLength="255"
                                        Height="60px"></asp:TextBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="InDestaque" Text="Destacar imóvel na página inicial" />                                    
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox runat="server" ID="InPublicar" Text="Publicar Imóvel" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Data Fechamento (Venda):
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtDataFechamento"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDataFechamento" Format="dd/MM/yyyy" runat="server">
                                    </ajax:CalendarExtender>
                                    (Utilizado caso o imóvel seja vendido)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    Obs: Para adicionar as fotos é necessário primeiramente clicar no botão gravar.
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button runat="server" ID="btnGravar" Text="Gravar" />                                    
                                    <asp:Button runat="server" ID="btnLimpar" Text="Limpar" />
                                    <asp:button runat="server" ID="btnExcluir" Text="Excluir Imóvel e fotos" OnClientClick="javascript:if (verificaExclusao()==false) return false;" Visible="false"  />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View runat="server" ID="vwFotos">
                        Selecione uma foto e clique no botão 'Enviar':
                        <asp:FileUpload ID="uplFoto" runat="server" /><asp:Button runat="server" ID="btnEnviarFoto"
                            Text="Enviar" />
                        <br />
                        <table>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button runat="server" ID="btnExcluirFotos" Text="Excluir Selecionadas" Visible="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="dtgFotos" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                                        DataKeyNames="Id" CssClass="ui-widget ui-widget-content" >
                                        <PagerStyle HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="ui-widget-header" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Foto">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgNmArqvFoto" runat="server" Width="100px" Height="100px" ImageUrl='<%# DataBinder.Eval(Container, "DataItem.VisualizacaoImagemMiniaturaAdmin") %>'
                                                        ToolTip="Foto" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sequência" ControlStyle-Width="30px">
                                                <ItemTemplate>
                                                    <cc1:CampoNumerico runat="server" Width="20px" ID="txtNrSequ" Text='<%# DataBinder.Eval(Container, "DataItem.Sequencia") %>'
                                                        MaxLength="5" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descrição" ControlStyle-Width="450px">
                                                <ItemTemplate>
                                                    <asp:TextBox Height="100px" Width="100%" ID="txtDescricaoFoto" Text='<%# DataBinder.Eval(Container, "DataItem.Descricao") %>'
                                                        runat="server" TextMode="MultiLine" TabIndex='<%#Container.DataItemIndex+1 %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ControlStyle-Width="50px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkExcluirTodos" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkExcluir" runat="server" Checked="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button runat="server" ID="btnFinalizarFotos" Text="Finalizar Cadastro" />
                    </asp:View>
                    <asp:View runat="server" ID="vwFinalizacao">
                        <div>
                            <br />
                            Imóvel gravado com sucesso!
                            <br />
                            Anote o código interno para futuras consultas:
                            <asp:Label runat="server" ID="lblImovelId" Text="ImovelId"></asp:Label><br />
                            <br />
                            <br />
                            Caso deseja realizar um novo cadastro, clique no botão abaixo.<br />
                            <br />
                            <asp:Button runat="server" ID="btnNovoCadastro" Text="Novo Cadastro" />
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</asp:Content>
