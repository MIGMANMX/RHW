Imports RHLogica

Partial Class cti_wucCasesInsumos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlClaseInsumo.DataSource = lista.wucClasesInsumos
            lista = Nothing
            ddlClaseInsumo.DataTextField = "key"
            ddlClaseInsumo.DataValueField = "value"
            ddlClaseInsumo.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlClaseInsumo.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idClase As Integer
        Get
            If ddlClaseInsumo.SelectedIndex >= 0 Then Return CInt(ddlClaseInsumo.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlClaseInsumo.SelectedValue = value
            Catch ex As Exception
                ddlClaseInsumo.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlClaseInsumo.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlClaseInsumo.AutoPostBack = value
        End Set
    End Property

    Public Event insumoSeleccionado As EventHandler
    Protected Sub ddlClaseInsumo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlClaseInsumo.SelectedIndexChanged
        RaiseEvent insumoSeleccionado(ddlClaseInsumo, e)
    End Sub
End Class
