Imports RHLogica

Partial Class cti_wucProductosFam
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lista As New ctiWUC
            ddlProductosFam.DataSource = lista.wucProductosFam
            lista = Nothing
            ddlProductosFam.DataTextField = "key"
            ddlProductosFam.DataValueField = "value"
            ddlProductosFam.DataBind()
            Dim selItm As New ListItem
            selItm.Text = "Seleccionar..." : selItm.Value = 0
            ddlProductosFam.Items.Insert(0, selItm)
        End If
    End Sub

    Public Property idfam As Integer
        Get
            If ddlProductosFam.SelectedIndex >= 0 Then Return CInt(ddlProductosFam.SelectedValue) Else Return 0
        End Get
        Set(value As Integer)
            Try
                ddlProductosFam.SelectedValue = value
            Catch ex As Exception
                ddlProductosFam.SelectedIndex = -1
            End Try
        End Set
    End Property
    Public Property ddlAutoPostBack() As Boolean
        Get
            Return ddlProductosFam.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlProductosFam.AutoPostBack = value
        End Set
    End Property

    Public Event ProdFamSeleccionado As EventHandler
    Protected Sub ddlProdFam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProductosFam.SelectedIndexChanged
        RaiseEvent ProdFamSeleccionado(ddlProductosFam, e)
    End Sub

End Class
