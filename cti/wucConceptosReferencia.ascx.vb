Imports RHLogica

Partial Class cti_wucConceptosReferencia
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlConceptosRef.DataSource = lista.wucConceptosReferencia
            lista = Nothing
            ddlConceptosRef.DataTextField = "key"
            ddlConceptosRef.DataValueField = "value"
            ddlConceptosRef.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlConceptosRef.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idConceptoRef As Integer
        Get
            If ddlConceptosRef.SelectedIndex >= 0 Then Return CInt(ddlConceptosRef.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlConceptosRef.SelectedValue = value
            Catch ex As Exception
                ddlConceptosRef.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlConceptosRef.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlConceptosRef.AutoPostBack = value
        End Set
    End Property
    Public Property ddlEnabled As Boolean
        Get
            Return ddlConceptosRef.Enabled
        End Get
        Set(value As Boolean)
            ddlConceptosRef.Enabled = value
        End Set
    End Property

    Public Event conceptoRefSeleccionado As EventHandler
    Protected Sub ddlConceptosRef_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlConceptosRef.SelectedIndexChanged
        RaiseEvent conceptoRefSeleccionado(ddlConceptosRef, e)
    End Sub
End Class
