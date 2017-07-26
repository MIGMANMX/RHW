Imports RHLogica

Partial Class cti_wucProductosClase
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProductosClase.DataSource = lista.wucProductosClase
            lista = Nothing
            ddlProductosClase.DataTextField = "key"
            ddlProductosClase.DataValueField = "value"
            ddlProductosClase.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProductosClase.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idClasei As Integer
        Get
            If ddlProductosClase.SelectedIndex >= 0 Then Return CInt(ddlProductosClase.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProductosClase.SelectedValue = value
            Catch ex As Exception
                ddlProductosClase.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProductosClase.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProductosClase.AutoPostBack = value
        End Set
    End Property
    Public Property ProdClase() As String
        Get
            If ddlProductosClase.SelectedIndex >= 0 Then Return ddlProductosClase.SelectedItem.Text Else Return ""
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Event ProdClaseSeleccionada As EventHandler
    Protected Sub ddlProductosClase_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProductosClase.SelectedIndexChanged
        RaiseEvent ProdClaseSeleccionada(ddlProductosClase, e)
    End Sub
End Class
