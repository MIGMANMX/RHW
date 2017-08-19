Imports RHLogica

Partial Class _Jornada
    Inherits System.Web.UI.Page
    Public gvPos As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        Session("menu") = "C"
        Lmsg.Text = "" : gvPos = 0
        If Not Page.IsPostBack Then
            Dim gvds As New ctiCalendario
            GridView1.DataSource = gvds.gvJornada
            gvds = Nothing
            GridView1.DataBind()
        End If
        If Request("btnSi") <> "" Then
            Dim ec As New ctiCalendario
            Dim err As String = ec.eliminarJornada(CInt(Session("idz_e")))
            GridView1.DataSource = ec.gvJornada
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
                jornada.Text = ""
                inicio.Text = ""
                fin.Text = ""
                color.Text = ""

            End If
            Lmsg.Text = err
        End If
        Session("idz_e") = ""
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Eliminar" Then
            Session("idz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text
            Session("dz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(2).Text
        ElseIf e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If
            Dim dsP As New ctiCalendario
            Dim datos() As String = dsP.datosJornada(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                jornada.Text = datos(1)
                inicio.Text = datos(2)
                fin.Text = datos(3)
                color.Text = datos(4)
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim ap As New ctiCalendario
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)

        Dim r As String = ap.actualizarJornada(idA, jornada.Text, inicio.Text, fin.Text, color.Text)

        GridView1.DataSource = ap.gvJornada
        ap = Nothing
        GridView1.DataBind()
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            jornada.Text = ""
            inicio.Text = ""
            fin.Text = ""
            color.Text = ""
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
        Dim gc As New ctiCalendario
        Dim j() As String = gc.agregarJornada(jornada.Text, inicio.Text, fin.Text, color.Text)
        GridView1.DataSource = gc.gvJornada
        gc = Nothing
        GridView1.DataBind()
        If j(0).StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            Dim sgr As New clsCTI
            grdSR.Text = sgr.seleccionarGridRow(GridView1, CInt(j(1))).ToString
            gvPos = sgr.gridViewScrollPos(CInt(grdSR.Text))
            sgr = Nothing
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
            jornada.Text = ""
            inicio.Text = ""
            fin.Text = ""
            color.Text = ""
        End If
        Lmsg.Text = j(0)
    End Sub


End Class
