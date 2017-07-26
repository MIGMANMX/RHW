Imports RHLogica

Partial Class cti_wucIncidencias
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlIncidencias.DataSource = lista.wucIncidencias
            lista = Nothing
            ddlIncidencias.DataTextField = "key"
            ddlIncidencias.DataValueField = "value"
            ddlIncidencias.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlIncidencias.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idIncidencia As Integer
        Get
            If ddlIncidencias.SelectedIndex >= 0 Then Return CInt(ddlIncidencias.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlIncidencias.SelectedValue = value
            Catch ex As Exception
                ddlIncidencias.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlIncidencias.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlIncidencias.AutoPostBack = value
        End Set
    End Property
    Public Function incidencia() As String
        If ddlIncidencias.SelectedIndex >= 0 Then Return ddlIncidencias.SelectedItem.Text Else Return ""
    End Function

    Public Event incidenciaSeleccionada As EventHandler
    Protected Sub ddlIncidencias_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIncidencias.SelectedIndexChanged
        RaiseEvent incidenciaSeleccionada(ddlIncidencias, e)
    End Sub
End Class
