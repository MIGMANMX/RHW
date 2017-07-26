Imports RHLogica

Partial Class cti_wucJornadas
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlJornadas.DataSource = lista.wucJornadas
            lista = Nothing
            ddlJornadas.DataTextField = "key"
            ddlJornadas.DataValueField = "value"
            ddlJornadas.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlJornadas.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idJornada As Integer
        Get
            If ddlJornadas.SelectedIndex >= 0 Then Return CInt(ddlJornadas.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlJornadas.SelectedValue = value
            Catch ex As Exception
                ddlJornadas.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlJornadas.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlJornadas.AutoPostBack = value
        End Set
    End Property
    Public Function puesto() As String
        If ddlJornadas.SelectedIndex >= 0 Then Return ddlJornadas.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlJornadas.SelectedIndex >= 0
    End Function

    Public Event jornadaSeleccionado As EventHandler
    Protected Sub ddlJornadas_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlJornadas.SelectedIndexChanged
        RaiseEvent jornadaSeleccionado(ddlJornadas, e)
    End Sub
End Class