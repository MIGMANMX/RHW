
Imports RHLogica

Partial Class _Incidencias
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        Session("menu") = "C"
        Lmsg.Text = "" : gvPos = 0
        If Not Page.IsPostBack Then
            Dim gvds As New ctiCatalogos
            GridView1.DataSource = gvds.gvIncidencia
            gvds = Nothing
            GridView1.DataBind()
        End If
        If Request("btnSi") <> "" Then
            Dim ec As New ctiCatalogos
            Dim err As String = ec.eliminarIncidencia(CInt(Session("idz_e")))
            GridView1.DataSource = ec.gvIncidencia
            ec = Nothing
            GridView1.DataBind()
            If err.StartsWith("Error") Then
                Lmsg.CssClass = "error"
                grdSR.Text = ""
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            Else
                Lmsg.CssClass = "correcto"
                grdSR.Text = ""
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
                incidencia.Text = ""
                descripcion.Text = ""
            End If
            Lmsg.Text = err
        End If
        Session("idz_e") = ""
    End Sub
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim ap As New ctiCatalogos
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)
        Dim r As String = ap.actualizarIncidencia(idA, incidencia.Text, descripcion.Text)
        GridView1.DataSource = ap.gvIncidencia
        ap = Nothing
        GridView1.DataBind()
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            incidencia.Text = ""
            descripcion.Text = ""
        End If
        Dim gvp As New clsCTI
        grdSR.Text = gvp.seleccionarGridRow(GridView1, idA)
        GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
        gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
        gvp = Nothing
        Lmsg.Text = r
    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
        End If
        Dim gc As New ctiCatalogos
        Dim r() As String = gc.agregarIncidencia(incidencia.Text, descripcion.Text)
        GridView1.DataSource = gc.gvIncidencia
        gc = Nothing
        GridView1.DataBind()
        If r(0).StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            Dim sgr As New clsCTI
            grdSR.Text = sgr.seleccionarGridRow(GridView1, CInt(r(1))).ToString
            gvPos = sgr.gridViewScrollPos(CInt(grdSR.Text))
            sgr = Nothing
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
            incidencia.Text = ""
            descripcion.Text = ""
        End If
        Lmsg.Text = r(0)
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Eliminar" Then
            Session("idz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text
            Session("dz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(3).Text
        ElseIf e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If
            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosIncidencias(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                incidencia.Text = datos(0)
                descripcion.Text = datos(1)
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
            End If
        End If
    End Sub
End Class
