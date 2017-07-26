Imports RHLogica

Partial Class cti_wucUnidadMedida
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlUnidadMedida.DataSource = lista.wucUnidadMedida
            lista = Nothing
            ddlUnidadMedida.DataTextField = "key"
            ddlUnidadMedida.DataValueField = "value"
            ddlUnidadMedida.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlUnidadMedida.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idUnidadMedida As Integer
        Get
            If ddlUnidadMedida.SelectedIndex >= 0 Then Return CInt(ddlUnidadMedida.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlUnidadMedida.SelectedValue = value
            Catch ex As Exception
                ddlUnidadMedida.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlUnidadMedida.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlUnidadMedida.AutoPostBack = value
        End Set
    End Property
    Public Function ClaseGasto() As String
        If ddlUnidadMedida.SelectedIndex >= 0 Then Return ddlUnidadMedida.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlUnidadMedida.SelectedIndex >= 0
    End Function

    Public Event unidadMedidaSeleccionada As EventHandler
    Protected Sub ddlUnidadMedida_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlUnidadMedida.SelectedIndexChanged
        RaiseEvent unidadMedidaSeleccionada(ddlUnidadMedida, e)
    End Sub
End Class
