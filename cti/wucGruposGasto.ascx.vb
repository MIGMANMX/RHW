Imports RHLogica

Partial Class cti_wucGruposGasto
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlGruposGasto.DataSource = lista.wucGruposGasto
            lista = Nothing
            ddlGruposGasto.DataTextField = "key"
            ddlGruposGasto.DataValueField = "value"
            ddlGruposGasto.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlGruposGasto.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idgrupo As Integer
        Get
            If ddlGruposGasto.SelectedIndex >= 0 Then Return CInt(ddlGruposGasto.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlGruposGasto.SelectedValue = value
            Catch ex As Exception
                ddlGruposGasto.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlGruposGasto.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlGruposGasto.AutoPostBack = value
        End Set
    End Property
    Public Function GrupoGasto() As String
        If ddlGruposGasto.SelectedIndex >= 0 Then Return ddlGruposGasto.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlGruposGasto.SelectedIndex >= 0
    End Function

    Public Event GrupoGSeleccionado As EventHandler
    Protected Sub ddlGrupoG_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGruposGasto.SelectedIndexChanged
        RaiseEvent GrupoGSeleccionado(ddlGruposGasto, e)
    End Sub

End Class
