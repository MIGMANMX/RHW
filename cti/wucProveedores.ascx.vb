Imports RHLogica

Partial Class cti_wucProveedores
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProveedores.DataSource = lista.wucProveedores
            lista = Nothing
            ddlProveedores.DataTextField = "key"
            ddlProveedores.DataValueField = "value"
            ddlProveedores.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProveedores.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idProveedor As Integer
        Get
            If ddlProveedores.SelectedIndex >= 0 Then Return CInt(ddlProveedores.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProveedores.SelectedValue = value
            Catch ex As Exception
                ddlProveedores.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProveedores.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProveedores.AutoPostBack = value
        End Set
    End Property
    Public Property proveedor() As String
        Get
            If ddlProveedores.SelectedIndex >= 0 Then Return ddlProveedores.SelectedItem.Text Else Return ""
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Event proveedorSeleccionado As EventHandler
    Protected Sub ddlProveedores_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProveedores.SelectedIndexChanged
        RaiseEvent proveedorSeleccionado(ddlProveedores, e)
    End Sub

End Class
