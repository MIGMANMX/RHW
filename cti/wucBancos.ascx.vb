Imports RHLogica

Partial Class cti_wucBancos
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlBancos.DataSource = lista.wucBancos
            lista = Nothing
            ddlBancos.DataTextField = "key"
            ddlBancos.DataValueField = "value"
            ddlBancos.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlBancos.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idBanco As Integer
        Get
            If ddlBancos.SelectedIndex >= 0 Then Return CInt(ddlBancos.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlBancos.SelectedValue = value
            Catch ex As Exception
                ddlBancos.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlBancos.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlBancos.AutoPostBack = value
        End Set
    End Property

    Public Event bancoSeleccionado As EventHandler
    Protected Sub ddlBancos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBancos.SelectedIndexChanged
        RaiseEvent bancoSeleccionado(ddlBancos, e)
    End Sub
End Class
