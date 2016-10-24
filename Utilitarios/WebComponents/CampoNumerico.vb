Imports System
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports AjaxControlToolkit

<Assembly: System.Web.UI.WebResource("Utilitarios.CampoNumerico.js", "text/javascript")> 
Namespace WebComponents
    <ValidationProperty("Text")>
    <Themeable(True)>
    Public Class CampoNumerico
        Inherits WebControl

        Public Enum enumTipoCampoNumerico
            '        Custom,
            Inteiro
            Valor
            '        Conta,
            '        MesAno,
            '        CPF,
            '        CPFCNPJ,
            '        Porcentagem,
            '        CEP,
            '        RENAVAM
        End Enum

        Public Property TipoCampoNumerico As enumTipoCampoNumerico
        Public Txt As New TextBox()
        Protected TxtFtb As New FilteredTextBoxExtender()
        Protected TxtRfv As New RequiredFieldValidator()
        Protected TxtCv As New CustomValidator()

        Public Overrides Property ID As String
            Get
                Return Txt.ID
            End Get
            Set(ByVal value As String)
                Txt.ID = value
            End Set
        End Property

        Public Overrides ReadOnly Property ClientID As String
            Get
                Return Txt.ClientID
            End Get
        End Property

        Public Property CaracteresValidos As String
            Get
                Return TxtFtb.ValidChars
            End Get
            Set(ByVal value As String)
                TxtFtb.ValidChars = value
            End Set
        End Property

        Public Property TipoFiltro As FilterTypes
            Get
                Return TxtFtb.FilterType
            End Get
            Set(ByVal value As FilterTypes)
                TxtFtb.FilterType = value
            End Set
        End Property

        Public Property ModoFiltro As FilterModes
            Get
                Return TxtFtb.FilterMode
            End Get
            Set(ByVal value As FilterModes)
                TxtFtb.FilterMode = value
            End Set
        End Property

        Private wrkCustomValidatorEnabled As Boolean = False
        Private wrkCampoObrigatorio As Boolean = False
        '        string textAlign = "right";
        '        string mensagemMesAnoInvalido = "Campo MM/AAAA Inválido!";
        '        bool validaAteDataHoje = true;

        '        protected WebControl proximo;

        Public Property CampoObrigatorio As Boolean
            Get
                Return wrkCampoObrigatorio
            End Get
            Set(ByVal value As Boolean)
                wrkCampoObrigatorio = value
            End Set
        End Property

        Public Property SetFocusOnError As Boolean

        Public Property MensagemErro As String

        '        public string MensagemMesAnoInvalido
        '        {
        '            get { return mensagemMesAnoInvalido; }
        '            set { mensagemMesAnoInvalido = value; }
        '        }

        Public Property GrupoValidacao As String

        '        public string CaracteresInvalidos
        '        {
        '            get { return TxtFtb.InvalidChars; }
        '            set { TxtFtb.InvalidChars = value; }
        '        }

        '        public string TextAlign
        '        {
        '            get { return textAlign; }
        '            set { textAlign = value; }
        '        }        

        '        public bool ReadOnly
        '        {
        '            get { return Txt.ReadOnly; }
        '            set { Txt.ReadOnly = value; }
        '        }

        '        public string ProximoCampo { get; set; }

        '        protected WebControl Proximo
        '        {
        '            get
        '            {
        '                if (ProximoCampo != null && proximo == null)
        '                {
        '                    Control p = this.Parent.FindControl(ProximoCampo, true, true);
        '                    if (p != null)
        '                        proximo = (WebControl)p;
        '                }
        '                return proximo;
        '            }
        '        }

        '        public string ValidatorClientID { get { return TxtRfv.ClientID; } }
        '        public string CustomValidatorClientID { get { return TxtCv.ClientID; } }

        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)

            'Page.ClientScript.RegisterClientScriptInclude(Me.GetType(), "CampoNumerico", Page.ClientScript.GetWebResourceUrl(Me.GetType, "Utilitarios.WebComponents.CampoNumerico.js"))
            'ToolkitScriptManager.RegisterClientScriptResource(Me.Page, Me.GetType, "Utilitarios.WebComponents.CampoNumerico.js")

            ScriptManager.RegisterClientScriptResource(Me.Page, Me.GetType, "Utilitarios.CampoNumerico.js")

            TxtFtb.ID = (Txt.ID + "Ftb")
            TxtFtb.TargetControlID = Txt.ID
            TxtRfv.ID = (Txt.ID + "Rfv")
            TxtRfv.ControlToValidate = Txt.ID
            TxtRfv.Enabled = ValidatorEnabled
            TxtRfv.ErrorMessage = MensagemErro
            TxtRfv.Display = ValidatorDisplay.None
            TxtRfv.ValidationGroup = GrupoValidacao
            TxtRfv.SetFocusOnError = SetFocusOnError
            Controls.Add(TxtFtb)
            If (CampoObrigatorio) Then
                Controls.Add(TxtRfv)
            End If
            Controls.Add(Txt)


            '            //Se for do tipo MesAno, adiciona validação para que o usuário digite um valor correto
            '            if (TipoCampoNumerico == TipoCampoNumerico.MesAno)
            '            {
            '                TxtCv = new CustomValidator();
            '                TxtCv.ID = Txt.ID + "Cv";
            '                TxtCv.ControlToValidate = Txt.ID;
            '        If (ValidaAteDataHoje) Then
            '                    TxtCv.ClientValidationFunction = "validaMesAnoAteDataHoje";
            '        Else
            '                    TxtCv.ClientValidationFunction = "validaMesAnoUltimo";

            '                TxtCv.ErrorMessage = MensagemMesAnoInvalido;
            '                TxtCv.Display = ValidatorDisplay.None;
            '                TxtCv.Enabled = true;
            '                Controls.Add(TxtCv);
            '            }

            '            if (this.TipoCampoNumerico == TipoCampoNumerico.CPF)
            '            {
            '                TxtCv = new CustomValidator();
            '                TxtCv.ID = Txt.ID + "Cv";
            '                TxtCv.ControlToValidate = Txt.ID;
            '                TxtCv.ClientValidationFunction = "validaCPF";
            '                TxtCv.ErrorMessage = "CPF Inválido!";
            '                TxtCv.Display = ValidatorDisplay.None;
            '                TxtCv.Enabled = true;
            '                Controls.Add(TxtCv);
            '            }

            '            if (this.TipoCampoNumerico == TipoCampoNumerico.CPFCNPJ)
            '            {
            '                TxtCv = new CustomValidator();
            '                TxtCv.ID = Txt.ID + "Cv";
            '                TxtCv.ControlToValidate = Txt.ID;
            '                TxtCv.ClientValidationFunction = "validaCNPJCPF";
            '                TxtCv.ErrorMessage = "CPF/CNPJ Inválido!";
            '                TxtCv.Display = ValidatorDisplay.None;
            '                TxtCv.Enabled = true;
            '                Controls.Add(TxtCv);
            '            }

            If ((Me.TipoCampoNumerico = enumTipoCampoNumerico.Valor) AndAlso Me.CustomValidatorEnabled) Then
                TxtCv = New CustomValidator
                TxtCv.ID = (Txt.ID + "Cv")
                TxtCv.ControlToValidate = Txt.ID
                TxtCv.ClientValidationFunction = "validaValorVazioZerado"
                TxtCv.ErrorMessage = MensagemErroCustomValidator
                TxtCv.Display = ValidatorDisplay.None
                TxtCv.Enabled = CustomValidatorEnabled
                Controls.Add(TxtCv)
            End If

            '            Txt.Style.Add(HtmlTextWriterStyle.TextAlign, TextAlign);
            '            Txt.MaxLength = Txt.MaxLength > 0 ? Txt.MaxLength : 14;
            MyBase.OnInit(e)
        End Sub

        Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
            MyBase.OnPreRender(e)

            Txt.Attributes("onfocus") = Me.Attributes("onfocus") & "this.select();"

            Select Case TipoCampoNumerico
                Case enumTipoCampoNumerico.Valor
                    Txt.Attributes("onblur") = ("formataValor(this);" + Me.Attributes("onblur"))
                    Txt.Attributes("onkeyup") = ("formataValorDigitando(this, event);" + Me.Attributes("onkeyup"))
                    CaracteresValidos = "0123456789,."
                    TipoFiltro = FilterTypes.Custom
                    ModoFiltro = FilterModes.ValidChars

                Case enumTipoCampoNumerico.Inteiro
                    TipoFiltro = FilterTypes.Numbers
                    ModoFiltro = FilterModes.ValidChars
            End Select

            '            if (Proximo != null && (this.MaxLength > 0 || this.QtdeCaracteresPula > 0))
            '                Funcoes.PulaCampoAutomatico(Txt, this.Proximo, this.QtdeCaracteresPula == 0 ? this.MaxLength : this.QtdeCaracteresPula, this.QtdeCaracteresPula != 0);

            'For Each atributo As String In Me.Attributes.Keys
            '    If ((Not (Txt.Attributes(atributo)) Is Nothing) AndAlso (Not (Me.Attributes(atributo)) Is Nothing)) Then
            '        If (Txt.Attributes(atributo).IndexOf(Me.Attributes(atributo)) < 0) Then
            '            Txt.Attributes(atributo) = (Txt.Attributes(atributo) + Me.Attributes(atributo))
            '        End If
            '    ElseIf (Not (Me.Attributes(atributo)) Is Nothing) Then
            '        Txt.Attributes.Add(atributo, Me.Attributes(atributo))
            '    End If
            'Next

        End Sub
        

        Public Property Text As String
            Get
                Return Txt.Text
            End Get
            Set(ByVal value As String)
                Txt.Text = value
            End Set
        End Property

        Public Overrides Property Width As Unit
            Get
                Return Txt.Width
            End Get
            Set(ByVal value As Unit)
                Txt.Width = value
            End Set
        End Property

        Public Property MaxLength As Integer
            Get
                Return Txt.MaxLength
            End Get
            Set(ByVal value As Integer)
                Txt.MaxLength = value
            End Set
        End Property

        Public Property QtdeCaracteresPula As Integer

        Public Property ValidatorEnabled As Boolean
            Get
                Return TxtRfv.Enabled
            End Get
            Set(ByVal value As Boolean)
                TxtRfv.Enabled = value
            End Set
        End Property

        Public Property CustomValidatorEnabled As Boolean
            Get
                Return wrkCustomValidatorEnabled
            End Get
            Set(ByVal value As Boolean)
                wrkCustomValidatorEnabled = value
            End Set
        End Property

        Public Property MensagemErroCustomValidator As String

        Public Overrides Sub RenderBeginTag(ByVal writer As HtmlTextWriter)
        End Sub
        Public Overrides Sub RenderEndTag(ByVal writer As System.Web.UI.HtmlTextWriter)
        End Sub
    End Class
End Namespace