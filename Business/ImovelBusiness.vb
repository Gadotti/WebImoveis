Imports DataAccess.DataAccess
Imports Utilitarios.Utils

Public Class ImovelBusiness

    Const DestaqueTD As String =
                    "<div style=""width: 268px; float: left; background: url(@Foto) no-repeat top left;"">" &
                    " <a href=""@Pagina"" target=""_blank"" onclick=""PopupCenter(this.href, this.target, 650, 660); return false;"">" &
                    "   <div class=""rent_buy"">" &
                    "       <span class=""to_rent"">@TipoImovel</span>" &
                    "       <span class=""to_ref"">Ref : @Referencia</span>" &
                    "   </div>" &
                    "   <div class=""rent_buyP1"">" &
                    "     <table width=""228"" height=""166"" border=""0"" align=""center"" cellpadding=""5"" cellspacing=""0"">" &
                    "       <tr>" &
                    "         <td width=""72"" align=""left"" valign=""top"" class=""txt"">" &
                    "           <strong>Localização</strong>" &
                    "        </td>" &
                    "        <td align=""left"" valign=""top"" class=""txt"">" &
                    "          @Localizacao " &
                    "         </td>" &
                    "       </tr>" &
                    "       <tr>" &
                    "        <td align=""left"" valign=""top"" class=""txt"">" &
                    "           <strong>Descrição</strong>" &
                    "        </td>" &
                    "        <td align=""left"" class=""txt"">" &
                    "          @Descricao" &
                    "        </td>" &
                    "       </tr>" &
                    "      <tr>" &
                    "        <td align=""left"" valign=""top"" class=""txt"">" &
                    "           <strong>Área Cons.</strong>" &
                    "        </td>" &
                    "        <td align=""left"" valign=""top"" class=""txt"">" &
                    "          @AreaConstruida m² " &
                    "        </td>" &
                    "      </tr>" &
                    "      <tr>" &
                    "       <td align=""left"" valign=""top"" class=""txt"">" &
                    "          <strong>Valor</strong>" &
                    "        </td>" &
                    "        <td align=""left"" valign=""top"" class=""txt"">" &
                    "           R$ @Valor " &
                    "        </td>" &
                    "      </tr>" &
                    "    </table>" &
                    "   <img src=""images/fond_rentp2.gif"" width=""268"" height=""16"" alt="""" style=""border-width: 0px;border-style: none;""/> </div>" &
                    " </a>" &
                    "</div>"


    Public Shared Function CarregaDestaques() As String
        Dim objImovel As List(Of Imovel) = Imovel.BuscaDestaques

        'Verifica se encontrou destaques
        If objImovel Is Nothing OrElse objImovel.Count.Equals(0) Then
            Return String.Empty
        End If

        Dim retorno As New Text.StringBuilder

        'Trata cara imóvel
        'Dim abreTR As Boolean = True
        For Each item As Imovel In objImovel
            Dim linhatable As String = String.Empty

            ''Nova linha
            'If abreTR Then
            '    linhatable = "<tr>"
            'End If

            'Monta conteúdo
            linhatable = linhatable & DestaqueTD
            linhatable = linhatable.Replace("@Foto", Foto.Destaque(item.Id))
            linhatable = linhatable.Replace("@Pagina", "VisualizarImovel.aspx?imovelid=" & item.Id)
            linhatable = linhatable.Replace("@TipoImovel", item.TipoImovelDescricao)
            linhatable = linhatable.Replace("@Referencia", item.Referencia)
            linhatable = linhatable.Replace("@Localizacao", item.CidadeDescricao & " / " & item.BairroDescricao)
            linhatable = linhatable.Replace("@Descricao", MontaResumo(item))
            linhatable = linhatable.Replace("@AreaConstruida", FormataValor(item.AreaConstruida))
            linhatable = linhatable.Replace("@Valor", FormataValor(item.Valor))

            ''Fecha linha
            'If Not abreTR Then
            '    linhatable = linhatable & "</tr>"
            'End If

            ''Troca valor indicador
            'abreTR = Not abreTR

            'Armazena conteúdo
            retorno.Append(linhatable)
        Next

        ''Verifica se é qtde impar para fechar o tr
        'If objImovel.Count Mod 2 > 0 Then
        '    retorno.Append("<td></td></tr>")
        'End If

        'Retorna resultado
        Return retorno.ToString

    End Function

    Public Shared Function MontaResumo(ByVal objImovel As Imovel) As String
        Dim texto As New Text.StringBuilder
        texto.Append(String.Format("{0} dormitório(s), sendo {1} suíte(s), {2} banheiro(s)", objImovel.QtdeDormitorio, objImovel.QtdeSuite, objImovel.QtdeBanheiro))

        If objImovel.InPiscina Then
            texto.Append(", piscina, ")
        End If
        If objImovel.InAreaFesta Then
            texto.Append(", área de festas")
        End If
        If objImovel.InMobiliado Then
            texto.Append(", mobiliado")
        End If

        texto.Append(String.Format(" e {0} vaga(s) de garagem", objImovel.QtdeGaragem))

        Return texto.ToString

    End Function

    Public Shared Function MontaListaPossui(ByVal objImovel As Imovel) As String
        Dim retorno As New Text.StringBuilder
        retorno.Append("<ul>")

        With objImovel
            If .InCozinha Then retorno.Append("<li>Cozinha</li>")
            If .InCopa Then retorno.Append("<li>Copa</li>")
            If .InPiscina Then retorno.Append("<li>Piscina</li>")
            If .InAreaServico Then retorno.Append("<li>Área de serviço</li>")
            If .InDependenciaEmpregada Then retorno.Append("<li>Dep. empregada</li>")
            If .InChurrasqueira Then retorno.Append("<li>Churrasqueira</li>")
            If .InCloset Then retorno.Append("<li>Closet</li>")
            If .InAdega Then retorno.Append("<li>Adega</li>")
            If .InAreaFesta Then retorno.Append("<li>Área de festas</li>")
            If .InAreaJogos Then retorno.Append("<li>Área de jogos</li>")
            If .InLareira Then retorno.Append("<li>Lareira</li>")
            If .InSacada Then retorno.Append("<li>Sacada</li>")
            If .InEscritorio Then retorno.Append("<li>Escritório</li>")
            If .InGasCentral Then retorno.Append("<li>Gás central</li>")
            If .InPortaoEletronico Then retorno.Append("<li>Portão eletrônico</li>")
            If .InMobiliado Then retorno.Append("<li>Mobiliado</li>")
            If .InLancamento Then retorno.Append("<li>Lançamento</li>")
            If .InCobertura Then retorno.Append("<li>Cobertura</li>")
            If .InPronto Then retorno.Append("<li>Pronta entrega</li>")
            If .InPorteiroEletronico Then retorno.Append("<li>Porteiro eletrônico</li>")
            If .InLavabo Then retorno.Append("<li>Lavabo</li>")
            If .InTerraco Then retorno.Append("<li>Terraço</li>")
        End With
        retorno.Append("</ul>")

        Return retorno.ToString
    End Function

End Class
