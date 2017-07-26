Imports RHLogica

Partial Class cti_wucLstIncidencias
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            lstIncidencia.DataSource = lista.wucIncidencias
            lista = Nothing
            lstIncidencia.DataTextField = "key"
            lstIncidencia.DataValueField = "value"
            lstIncidencia.DataBind()
            'Dim selItm As New ListItem
            'selItm.Text = "Seleccionar..." : selItm.Value = 0
            'ddlInsumos.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idIncidencia As Integer
        Get
            If lstIncidencia.SelectedIndex >= 0 Then Return CInt(lstIncidencia.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                lstIncidencia.SelectedValue = value
            Catch ex As Exception
                lstIncidencia.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property incidencia As String
        Get
            If lstIncidencia.SelectedIndex >= 0 Then Return lstIncidencia.SelectedItem.Text Else Return ""
        End Get
        Set(value As String)
            lstIncidencia.Text = value
        End Set
    End Property
    Public Property lstAutoPostBack() As Boolean
        Get
            Return lstIncidencia.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            lstIncidencia.AutoPostBack = value
        End Set
    End Property
    Public Property lstRows() As Integer
        Get
            Return lstIncidencia.Rows
        End Get
        Set(value As Integer)
            If lstIncidencia.Items.Count > 0 And lstIncidencia.Items.Count < value Then
                lstIncidencia.Rows = lstIncidencia.Items.Count
            Else
                lstIncidencia.Rows = value
            End If
        End Set
    End Property

    Public Event incidenciaSeleccionada As EventHandler

    Protected Sub lstIncidencia_DataBound(sender As Object, e As EventArgs) Handles lstIncidencia.DataBound
        If lstIncidencia.Items.Count > 0 And lstIncidencia.Items.Count < lstRows Then
            lstIncidencia.Rows = lstIncidencia.Items.Count
        End If
    End Sub
    Protected Sub lstIncidencia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstIncidencia.SelectedIndexChanged
        RaiseEvent incidenciaSeleccionada(lstIncidencia, e)
    End Sub
End Class
