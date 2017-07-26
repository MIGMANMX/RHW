Imports RHLogica

Partial Class cti_wucPuestos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlPuestos.DataSource = lista.wucPuestos
            lista = Nothing
            ddlPuestos.DataTextField = "key"
            ddlPuestos.DataValueField = "value"
            ddlPuestos.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlPuestos.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idPuesto As Integer
        Get
            If ddlPuestos.SelectedIndex >= 0 Then Return CInt(ddlPuestos.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlPuestos.SelectedValue = value
            Catch ex As Exception
                ddlPuestos.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlPuestos.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlPuestos.AutoPostBack = value
        End Set
    End Property
    Public Function puesto() As String
        If ddlPuestos.SelectedIndex >= 0 Then Return ddlPuestos.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlPuestos.SelectedIndex >= 0
    End Function

    Public Event puestoSeleccionado As EventHandler
    Protected Sub ddlPuestos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPuestos.SelectedIndexChanged
        RaiseEvent puestoSeleccionado(ddlPuestos, e)
    End Sub
End Class