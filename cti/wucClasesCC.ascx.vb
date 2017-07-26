Imports RHLogica

Partial Class cti_wucClasesCC
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlClaseCC.DataSource = lista.wucClasesCC
            lista = Nothing
            ddlClaseCC.DataTextField = "key"
            ddlClaseCC.DataValueField = "value"
            ddlClaseCC.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlClaseCC.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idClase As Integer
        Get
            If ddlClaseCC.SelectedIndex >= 0 Then Return CInt(ddlClaseCC.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlClaseCC.SelectedValue = value
            Catch ex As Exception
                ddlClaseCC.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlClaseCC.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlClaseCC.AutoPostBack = value
        End Set
    End Property

    Public Event claseSeleccionada As EventHandler
    Protected Sub ddlClaseCC_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlClaseCC.SelectedIndexChanged
        RaiseEvent claseSeleccionada(ddlClaseCC, e)
    End Sub
End Class
