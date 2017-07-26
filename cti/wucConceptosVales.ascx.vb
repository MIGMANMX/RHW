Imports RHLogica

Partial Class cti_wucConceptosVales
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlConceptosVales.DataSource = lista.wucConceptosVales
            lista = Nothing
            ddlConceptosVales.DataTextField = "key"
            ddlConceptosVales.DataValueField = "value"
            ddlConceptosVales.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlConceptosVales.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idConceptoVale As Integer
        Get
            If ddlConceptosVales.SelectedIndex >= 0 Then Return CInt(ddlConceptosVales.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlConceptosVales.SelectedValue = value
            Catch ex As Exception
                ddlConceptosVales.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlConceptosVales.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlConceptosVales.AutoPostBack = value
        End Set
    End Property

    Public Event conceptoValeSeleccionado As EventHandler
    Protected Sub ddlClaseCC_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlConceptosVales.SelectedIndexChanged
        RaiseEvent conceptoValeSeleccionado(ddlConceptosVales, e)
    End Sub
End Class
