Imports RHLogica

Partial Class cti_wucDescuentos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlDescuentos.DataSource = lista.wucDescuentos
            lista = Nothing
            ddlDescuentos.DataTextField = "key"
            ddlDescuentos.DataValueField = "value"
            ddlDescuentos.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlDescuentos.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idDescuento As Integer
        Get
            If ddlDescuentos.SelectedIndex >= 0 Then Return CInt(ddlDescuentos.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlDescuentos.SelectedValue = value
            Catch ex As Exception
                ddlDescuentos.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlDescuentos.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlDescuentos.AutoPostBack = value
        End Set
    End Property

    Public Event descuentoSeleccionado As EventHandler
    Protected Sub ddlDescuentos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDescuentos.SelectedIndexChanged
        RaiseEvent descuentoSeleccionado(ddlDescuentos, e)
    End Sub
End Class
