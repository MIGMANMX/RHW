Imports RHLogica

Partial Class cti_wucClasesG
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlClaseG.DataSource = lista.wucClasesG
            lista = Nothing
            ddlClaseG.DataTextField = "key"
            ddlClaseG.DataValueField = "value"
            ddlClaseG.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlClaseG.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idClase As Integer
        Get
            If ddlClaseG.SelectedIndex >= 0 Then Return CInt(ddlClaseG.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlClaseG.SelectedValue = value
            Catch ex As Exception
                ddlClaseG.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlClaseG.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlClaseG.AutoPostBack = value
        End Set
    End Property
    Public Function ClaseGasto() As String
        If ddlClaseG.SelectedIndex >= 0 Then Return ddlClaseG.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlClaseG.SelectedIndex >= 0
    End Function

    Public Event claseSeleccionada As EventHandler
    Protected Sub ddlClaseG_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlClaseG.SelectedIndexChanged
        RaiseEvent claseSeleccionada(ddlClaseG, e)
    End Sub
End Class
