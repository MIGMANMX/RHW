Imports RHLogica

Partial Class cti_wucInsumos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlInsumos.DataSource = lista.wucInsumos
            lista = Nothing
            ddlInsumos.DataTextField = "key"
            ddlInsumos.DataValueField = "value"
            ddlInsumos.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlInsumos.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idInsumo As Integer
        Get
            If ddlInsumos.SelectedIndex >= 0 Then Return CInt(ddlInsumos.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlInsumos.SelectedValue = value
            Catch ex As Exception
                ddlInsumos.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlInsumos.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlInsumos.AutoPostBack = value
        End Set
    End Property
    Public Function insumo() As String
        If ddlInsumos.SelectedIndex >= 0 Then Return ddlInsumos.SelectedItem.Text Else Return ""
    End Function

    Public Event insumoSeleccionado As EventHandler
    Protected Sub ddlInsumos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlInsumos.SelectedIndexChanged
        RaiseEvent insumoSeleccionado(ddlInsumos, e)
    End Sub
End Class
