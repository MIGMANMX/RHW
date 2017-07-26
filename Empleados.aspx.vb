Imports RHLogica

Partial Class _Default
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Login.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = "" : gvPos = 0
        If Request.Form("btnSi") <> "" Then
            Dim ep As New ctiCatalogos
            Dim err As String = ep.eliminarEmpleado(CInt(Session("idz_e").ToString))
            GridView1.DataSource = ep.gvEmpleados(wucSucursales.idSucursal, chkActivo.Checked)
            ep = Nothing
            GridView1.DataBind()
            If err.StartsWith("Error") Then
                Lmsg.CssClass = "error"
                If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) <= GridView1.Rows.Count Then
                    GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
                End If
            Else
                Lmsg.CssClass = "correcto"
                grdSR.Text = ""
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            End If
            Lmsg.Text = err
        End If
        Session("idz_e") = ""

        If Not IsPostBack Then
            FIngreso.Visible = False
            CFNacimiento.Visible = False
            CFBaja.Visible = False

        End If
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
            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosEmpleado(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                empleado.Text = datos(0)
                wucSucursal.idSucursal = CInt(datos(1))
                WucPuestos.idPuesto = datos(2)
                activo.Checked = datos(3)


                nss.Text = datos(4)
                fecha_ingreso.Text = datos(5)
                rfc.Text = datos(6)
                fecha_nacimiento.Text = datos(7)
                calle.Text = datos(8)
                numero.Text = datos(9)
                colonia.Text = datos(10)
                cp.Text = datos(11)
                telefono.Text = datos(12)
                correo.Text = datos(13)
                fecha_baja.Text = datos(14)

                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
        End If
        Dim gp As New ctiCatalogos
        Dim r() As String = gp.agregarEmpleado(empleado.Text, wucSucursales.idSucursal, WucPuestos.idPuesto, activo.Checked, nss.Text, fecha_ingreso.Text, rfc.Text, fecha_nacimiento.Text, calle.Text, numero.Text, colonia.Text, cp.Text, telefono.Text, correo.Text, fecha_baja.Text)
        GridView1.DataSource = gp.gvEmpleados(wucSucursales.idSucursal, chkActivo.Checked)
        gp = Nothing
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
        End If
        Lmsg.Text = r(0)
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim ap As New ctiCatalogos
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)
        Dim r As String = ap.actualizarEmpleado(idA, empleado.Text, wucSucursal.idSucursal, WucPuestos.idPuesto, activo.Checked, nss.Text, fecha_ingreso.Text, rfc.Text, fecha_nacimiento.Text, calle.Text, numero.Text, colonia.Text, cp.Text, telefono.Text, correo.Text, fecha_baja.Text)
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
            empleado.Text = "" : WucPuestos.idPuesto = 0 : wucSucursal.idSucursal = 0
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
            empleado.Text = "" : WucPuestos.idPuesto = 0 : wucSucursal.idSucursal = 0
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

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub

    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        fecha_ingreso.Text = FIngreso.SelectedDate.ToString("dd/MM/yyyy")
        FIngreso.Visible = False
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        If CFBaja.Visible = True Then
            CFBaja.Visible = False
        ElseIf CFBaja.Visible = False Then
            CFBaja.Visible = True
        End If
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If CFNacimiento.Visible = True Then
            CFNacimiento.Visible = False
        ElseIf CFNacimiento.Visible = False Then
            CFNacimiento.Visible = True
        End If
    End Sub

    Protected Sub CFNacimiento_SelectionChanged(sender As Object, e As EventArgs) Handles CFNacimiento.SelectionChanged
        fecha_nacimiento.Text = CFNacimiento.SelectedDate.ToString("dd/MM/yyyy")
        CFNacimiento.Visible = False
    End Sub

    Protected Sub CFBaja_SelectionChanged(sender As Object, e As EventArgs) Handles CFBaja.SelectionChanged
        fecha_baja.Text = CFBaja.SelectedDate.ToString("dd/MM/yyyy")
        CFBaja.Visible = False
    End Sub
End Class
