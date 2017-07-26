Imports RHLogica

Partial Class cti_wucBancosOtros
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlBancosOtros.DataSource = lista.wucBancosOtros
            lista = Nothing
            ddlBancosOtros.DataTextField = "key"
            ddlBancosOtros.DataValueField = "value"
            ddlBancosOtros.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlBancosOtros.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idBancoOtro As Integer
        Get
            If ddlBancosOtros.SelectedIndex >= 0 Then Return CInt(ddlBancosOtros.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlBancosOtros.SelectedValue = value
            Catch ex As Exception
                ddlBancosOtros.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlBancosOtros.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlBancosOtros.AutoPostBack = value
        End Set
    End Property

    Public Event bancoOtroSeleccionado As EventHandler
    Protected Sub ddlBancosOtros_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBancosOtros.SelectedIndexChanged
        RaiseEvent bancoOtroSeleccionado(ddlBancosOtros, e)
    End Sub
End Class
