Imports RHLogica

Partial Class cti_wucFamiliasTiposProducto
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlFamiliasTiposProducto.DataSource = lista.wucFamiliasTiposProducto
            lista = Nothing
            ddlFamiliasTiposProducto.DataTextField = "key"
            ddlFamiliasTiposProducto.DataValueField = "value"
            ddlFamiliasTiposProducto.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlFamiliasTiposProducto.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idFamiliaTipoProducto As Integer
        Get
            If ddlFamiliasTiposProducto.SelectedIndex >= 0 Then Return CInt(ddlFamiliasTiposProducto.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlFamiliasTiposProducto.SelectedValue = value
            Catch ex As Exception
                ddlFamiliasTiposProducto.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlFamiliasTiposProducto.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlFamiliasTiposProducto.AutoPostBack = value
        End Set
    End Property
    Public Property familiaTipoProducto() As String
        Get
            If ddlFamiliasTiposProducto.SelectedIndex >= 0 Then Return ddlFamiliasTiposProducto.SelectedItem.Text Else Return ""
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Event familiaTipoProductoSeleccionado As EventHandler
    Protected Sub ddlFamiliasTiposProducto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFamiliasTiposProducto.SelectedIndexChanged
        RaiseEvent familiaTipoProductoSeleccionado(ddlFamiliasTiposProducto, e)
    End Sub
End Class
