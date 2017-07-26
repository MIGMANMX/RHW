Imports RHLogica

Partial Class cti_wucProductosActivos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProductosA.DataSource = lista.wucProductosActivos
            lista = Nothing
            ddlProductosA.DataTextField = "key"
            ddlProductosA.DataValueField = "value"
            ddlProductosA.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProductosA.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idProducto As Integer
        Get
            If ddlProductosA.SelectedIndex >= 0 Then Return CInt(ddlProductosA.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProductosA.SelectedValue = value
            Catch ex As Exception
                ddlProductosA.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProductosA.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProductosA.AutoPostBack = value
        End Set
    End Property
    Public Function producto() As String
        If ddlProductosA.SelectedIndex >= 0 Then Return ddlProductosA.SelectedItem.Text Else Return ""
    End Function

    Public Event productoSeleccionado As EventHandler
    Protected Sub ddlProductos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProductosA.SelectedIndexChanged
        RaiseEvent productoSeleccionado(ddlProductosA, e)
    End Sub
End Class
