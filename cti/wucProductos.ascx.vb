Imports RHLogica

Partial Class cti_wucProductos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProductos.DataSource = lista.wucProductos
            lista = Nothing
            ddlProductos.DataTextField = "key"
            ddlProductos.DataValueField = "value"
            ddlProductos.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProductos.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idProducto As Integer
        Get
            If ddlProductos.SelectedIndex >= 0 Then Return CInt(ddlProductos.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProductos.SelectedValue = value
            Catch ex As Exception
                ddlProductos.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProductos.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProductos.AutoPostBack = value
        End Set
    End Property

    Public Event productoSeleccionado As EventHandler
    Protected Sub ddlProductos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProductos.SelectedIndexChanged
        RaiseEvent productoSeleccionado(ddlProductos, e)
    End Sub
End Class
