Imports RHLogica

Partial Class cti_wucEmpresas
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlEmpresas.DataSource = lista.wucEmpresas
            lista = Nothing
            ddlEmpresas.DataTextField = "key"
            ddlEmpresas.DataValueField = "value"
            ddlEmpresas.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlEmpresas.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idEmpresa As Integer
        Get
            If ddlEmpresas.SelectedIndex >= 0 Then Return CInt(ddlEmpresas.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlEmpresas.SelectedValue = value
            Catch ex As Exception
                ddlEmpresas.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlEmpresas.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlEmpresas.AutoPostBack = value
        End Set
    End Property

    Public Event empresaSeleccionada As EventHandler
    Protected Sub ddlEmpresas_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEmpresas.SelectedIndexChanged
        RaiseEvent empresaSeleccionada(ddlEmpresas, e)
    End Sub

End Class
