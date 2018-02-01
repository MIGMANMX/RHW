Imports RHLogica

Partial Class cti_wucTipoJornada
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlTipoJornada.DataSource = lista.wucTipoJornada
            lista = Nothing
            ddlTipoJornada.DataTextField = "key"
            ddlTipoJornada.DataValueField = "value"
            ddlTipoJornada.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlTipoJornada.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idTipoJornada As Integer
        Get
            If ddlTipoJornada.SelectedIndex >= 0 Then Return CInt(ddlTipoJornada.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlTipoJornada.SelectedValue = value
            Catch ex As Exception
                ddlTipoJornada.SelectedIndex = 0
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlTipoJornada.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlTipoJornada.AutoPostBack = value
        End Set
    End Property
    Public Function TipoJornada() As String
        If ddlTipoJornada.SelectedIndex >= 0 Then Return ddlTipoJornada.SelectedItem.Text Else Return ""
    End Function
    Public Function idSeleccionada() As Boolean
        Return ddlTipoJornada.SelectedIndex >= 0
    End Function

    Public Event TipoJornadaSeleccionada As EventHandler
    Protected Sub ddlTipoJornada_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoJornada.SelectedIndexChanged
        RaiseEvent TipoJornadaSeleccionada(ddlTipoJornada, e)
    End Sub
End Class
