Imports RHLogica

Partial Class cti_wucProveedoresG
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProveedoresG.DataSource = lista.wucProveedoresG
            lista = Nothing
            ddlProveedoresG.DataTextField = "key"
            ddlProveedoresG.DataValueField = "value"
            ddlProveedoresG.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProveedoresG.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idProveedorG As Integer
        Get
            If ddlProveedoresG.SelectedIndex >= 0 Then Return CInt(ddlProveedoresG.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProveedoresG.SelectedValue = value
            Catch ex As Exception
                ddlProveedoresG.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProveedoresG.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProveedoresG.AutoPostBack = value
        End Set
    End Property

    Public Event proveedorSeleccionado As EventHandler
    Protected Sub ddlProveedoresG_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProveedoresG.SelectedIndexChanged
        RaiseEvent proveedorSeleccionado(ddlProveedoresG, e)
    End Sub

End Class
