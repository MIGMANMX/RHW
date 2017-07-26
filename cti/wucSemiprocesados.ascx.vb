Imports RHLogica

Partial Class cti_wucSemiprocesados
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlSemiprocesados.DataSource = lista.wucSemiprocesados
            lista = Nothing
            ddlSemiprocesados.DataTextField = "key"
            ddlSemiprocesados.DataValueField = "value"
            ddlSemiprocesados.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlSemiprocesados.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idSemiprocesado As Integer
        Get
            If ddlSemiprocesados.SelectedIndex >= 0 Then Return CInt(ddlSemiprocesados.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlSemiprocesados.SelectedValue = value
            Catch ex As Exception
                ddlSemiprocesados.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlSemiprocesados.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlSemiprocesados.AutoPostBack = value
        End Set
    End Property

    Public Event semiprocesadoSeleccionado As EventHandler
    Protected Sub ddlSemiprocesados_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSemiprocesados.SelectedIndexChanged
        RaiseEvent semiprocesadoSeleccionado(ddlSemiprocesados, e)
    End Sub
End Class
