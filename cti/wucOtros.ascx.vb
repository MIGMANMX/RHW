Imports RHLogica

Partial Class cti_wucOtros
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlOtros.DataSource = lista.wucOtros
            lista = Nothing
            ddlOtros.DataTextField = "key"
            ddlOtros.DataValueField = "value"
            ddlOtros.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlOtros.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idOtro As Integer
        Get
            If ddlOtros.SelectedIndex >= 0 Then Return CInt(ddlOtros.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlOtros.SelectedValue = value
            Catch ex As Exception
                ddlOtros.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlOtros.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlOtros.AutoPostBack = value
        End Set
    End Property

    Public Event otroSeleccionado As EventHandler
    Protected Sub ddlOtros_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOtros.SelectedIndexChanged
        RaiseEvent otroSeleccionado(ddlOtros, e)
    End Sub
End Class
