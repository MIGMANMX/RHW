Imports RHLogica

Partial Class cti_wucTiposProducto
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlTiposProducto.DataSource = lista.wucTiposProducto
            lista = Nothing
            ddlTiposProducto.DataTextField = "key"
            ddlTiposProducto.DataValueField = "value"
            ddlTiposProducto.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlTiposProducto.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idTipoProducto As Integer
        Get
            If ddlTiposProducto.SelectedIndex >= 0 Then Return CInt(ddlTiposProducto.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlTiposProducto.SelectedValue = value
            Catch ex As Exception
                ddlTiposProducto.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlTiposProducto.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlTiposProducto.AutoPostBack = value
        End Set
    End Property
    Public Property tipoProducto() As String
        Get
            If ddlTiposProducto.SelectedIndex >= 0 Then Return ddlTiposProducto.SelectedItem.Text Else Return ""
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Event tipoProductoSeleccionado As EventHandler
    Protected Sub ddlTiposProducto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTiposProducto.SelectedIndexChanged
        RaiseEvent tipoProductoSeleccionado(ddlTiposProducto, e)
    End Sub
End Class
