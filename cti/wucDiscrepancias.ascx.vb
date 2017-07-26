Imports RHLogica

Partial Class cti_wucDiscrepancias
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlDiscrepancias.DataSource = lista.wucDiscrepancias
            lista = Nothing
            ddlDiscrepancias.DataTextField = "key"
            ddlDiscrepancias.DataValueField = "value"
            ddlDiscrepancias.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlDiscrepancias.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idDiscrepancia As Integer
        Get
            If ddlDiscrepancias.SelectedIndex >= 0 Then Return CInt(ddlDiscrepancias.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlDiscrepancias.SelectedValue = value
            Catch ex As Exception
                ddlDiscrepancias.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlDiscrepancias.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlDiscrepancias.AutoPostBack = value
        End Set
    End Property

    Public Event discrepanciaSeleccionada As EventHandler
    Protected Sub ddlDiscrepancias_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDiscrepancias.SelectedIndexChanged
        RaiseEvent discrepanciaSeleccionada(ddlDiscrepancias, e)
    End Sub

End Class
