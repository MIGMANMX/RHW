
Imports RHLogica

Partial Class _Directorio
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Login.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = "" : gvPos = 0
        Session("idz_e") = ""

        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))


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
            baj.Visible = True

            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosEmpleado(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                empleado.Text = datos(0)

                calle.Text = datos(8)
                numero.Text = datos(9)
                colonia.Text = datos(10)
                cp.Text = datos(11)
                telefono.Text = datos(12)
                correo.Text = datos(13)
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
        Dim ap As New ctiCatalogos
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)
        Dim r As String = ap.actualizarDirectorio(idA, Session("idsucursal"), empleado.Text, calle.Text, numero.Text, colonia.Text, cp.Text, telefono.Text, correo.Text)
        GridView1.DataSource = ap.gvEmpleados(wucSucursales.idSucursal, chkActivo.Checked)
        ap = Nothing
        GridView1.DataBind()
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
        End If
        Dim gvp As New clsCTI
        grdSR.Text = gvp.seleccionarGridRow(GridView1, idA)
        If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) > 0 Then
            GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
            gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
        Else
            empleado.Text = ""
            calle.Text = "" : colonia.Text = "" : numero.Text = "" : cp.Text = "" : telefono.Text = "" : correo.Text = ""
        End If
        gvp = Nothing
        Lmsg.Text = r
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvEmpleados(wucSucursales.idSucursal, chkActivo.Checked)
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            empleado.Text = ""
        End If
    End Sub
    Protected Sub chkActivo_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivo.CheckedChanged
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvEmpleados(wucSucursales.idSucursal, chkActivo.Checked)
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
        End If
    End Sub

End Class
