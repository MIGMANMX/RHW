Imports RHLogica

Partial Class cti_wucFormasPagoSub
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlFormasPagoSub.DataSource = lista.wucFormasPagoSub
            lista = Nothing
            ddlFormasPagoSub.DataTextField = "key"
            ddlFormasPagoSub.DataValueField = "value"
            ddlFormasPagoSub.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlFormasPagoSub.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idFormaPagoSub As Integer
        Get
            If ddlFormasPagoSub.SelectedIndex >= 0 Then Return CInt(ddlFormasPagoSub.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlFormasPagoSub.SelectedValue = value
            Catch ex As Exception
                ddlFormasPagoSub.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlFormasPagoSub.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlFormasPagoSub.AutoPostBack = value
        End Set
    End Property

    Public Event formaPagoSubSeleccionada As EventHandler
    Protected Sub ddlFormasPagoSub_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFormasPagoSub.SelectedIndexChanged
        RaiseEvent formaPagoSubSeleccionada(ddlFormasPagoSub, e)
    End Sub
End Class
