Imports RHLogica

Partial Class cti_wucEmpleados
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If ddlEmpleados.Items.Count = 0 Then
                Dim selItm As New ListItem
                selItm.Text = "Seleccionar..." : selItm.Value = 0
                ddlEmpleados.Items.Insert(0, selItm)
            End If
        End If
    End Sub

    Public Property idEmpleado As Integer
        Get
            If ddlEmpleados.SelectedIndex >= 0 Then Return CInt(ddlEmpleados.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlEmpleados.SelectedValue = value
            Catch ex As Exception
                ddlEmpleados.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlEmpleados.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlEmpleados.AutoPostBack = value
        End Set
    End Property
    Public Sub ddlDataSource(activo As Boolean, idSucursal As Integer)
        Dim lista As New ctiWUC
        ddlEmpleados.DataSource = lista.wucEmpleados(True, idSucursal)
        lista = Nothing
        ddlEmpleados.DataTextField = "key"
        ddlEmpleados.DataValueField = "value"
        ddlEmpleados.DataBind()
        If ddlEmpleados.Items(0).Text <> "Seleccionar..." Then
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlEmpleados.Items.Insert(0, selItm)
        End If
    End Sub

    Public Event empleadoSeleccionado As EventHandler
    Protected Sub ddlEmpleados_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEmpleados.SelectedIndexChanged
        RaiseEvent empleadoSeleccionado(ddlEmpleados, e)
    End Sub
End Class