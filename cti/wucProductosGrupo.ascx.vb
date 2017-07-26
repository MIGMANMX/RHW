Imports RHLogica

Partial Class cti_wucProductosGrupo
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProductosGrupo.DataSource = lista.wucProductosGrupo
            lista = Nothing
            ddlProductosGrupo.DataTextField = "key"
            ddlProductosGrupo.DataValueField = "value"
            ddlProductosGrupo.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProductosGrupo.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idgrupo As Integer
        Get
            If ddlProductosGrupo.SelectedIndex >= 0 Then Return CInt(ddlProductosGrupo.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProductosGrupo.SelectedValue = value
            Catch ex As Exception
                ddlProductosGrupo.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProductosGrupo.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProductosGrupo.AutoPostBack = value
        End Set
    End Property
    Public Function ProductoGrupo() As String
        If ddlProductosGrupo.SelectedIndex >= 0 Then Return ddlProductosGrupo.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlProductosGrupo.SelectedIndex >= 0
    End Function

    Public Event GrupoProdSeleccionado As EventHandler
    Protected Sub ddlGrupoProd_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProductosGrupo.SelectedIndexChanged
        RaiseEvent GrupoProdSeleccionado(ddlProductosGrupo, e)
    End Sub

End Class
