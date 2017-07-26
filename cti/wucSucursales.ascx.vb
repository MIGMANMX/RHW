Imports RHLogica

Partial Class cti_wucSucursales
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlSucursales.DataSource = lista.wucSucursales
            lista = Nothing
            ddlSucursales.DataTextField = "key"
            ddlSucursales.DataValueField = "value"
            ddlSucursales.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlSucursales.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idSucursal As Integer
        Get
            If ddlSucursales.SelectedIndex >= 0 Then Return CInt(ddlSucursales.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlSucursales.SelectedValue = value
            Catch ex As Exception
                ddlSucursales.SelectedIndex = 0
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlSucursales.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlSucursales.AutoPostBack = value
        End Set
    End Property
    Public Function sucursal() As String
        If ddlSucursales.SelectedIndex >= 0 Then Return ddlSucursales.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlSucursales.SelectedIndex >= 0
    End Function

    Public Event sucursalSeleccionada As EventHandler
    Protected Sub ddlSucursales_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSucursales.SelectedIndexChanged
        RaiseEvent sucursalSeleccionada(ddlSucursales, e)
    End Sub
End Class
