Imports RHLogica

Partial Class _RegistroIncidencias
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        btnActualizarr.Enabled = False
        btnGuardarNuevo.Enabled = True
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC
        FechaC.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday

        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"

            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True

            If Request("btnSi") <> "" Then
                Dim ec As New ctiCatalogos
                Dim err As String = ec.eliminarAsigIncidencias(CInt(Session("idz_e")))
                GridView1.DataSource = ec.gvAsigIncidencias(wucEmpleados2.idEmpleado)
                ec = Nothing
                GridView1.DataBind()
                If err.StartsWith("Error") Then
                    Lmsg.CssClass = "error"
                    grdSR.Text = ""

                Else
                    Lmsg.CssClass = "correcto"
                    grdSR.Text = ""



                End If
                Lmsg.Text = err
            End If
            Session("idz_e") = ""

        End If
        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 2 Then
                chkVer.Checked = False
                VER.Visible = False
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                suc.Visible = False
                wucEmpleados2.ddlDataSource(datos(1))

                gvds = Nothing
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
            If datos(0) = 8 Then
                chkVer.Checked = False
                VER.Visible = False
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
        Else
            wucEmpleados2.ddlAutoPostBack = True

        End If
    End Sub
    Protected Sub FechaC_SelectionChanged(sender As Object, e As EventArgs) Handles FechaC.SelectionChanged
        fecha.Text = FechaC.SelectedDate.ToString("dd/MM/yyyy")
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        wucIncidencias.idIncidencia = 0
        fecha.Text = ""
        Lmsg.Text = ""
        TxObservaciones.Text = ""
        GridView1.Visible = False
        Dim gvds As New ctiWUC
        Dim acceso As New ctiCatalogos
        wucEmpleados2.ddlDataSource(wucSucursales.idSucursal)
        gvds = Nothing
        wucEmpleados2.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If

    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado
        wucIncidencias.idIncidencia = 0
        fecha.Text = ""
        Lmsg.Text = ""
        TxObservaciones.Text = ""
        GridView1.Visible = False
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvAsigIncidencias(wucEmpleados2.idEmpleado)
        GridView1.Visible = True
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
        'btnActualizarr.Enabled = True
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        wucEmpleados2.idEmpleado = 0
        wucSucursales.idSucursal = 0
        wucIncidencias.idIncidencia = 0
        fecha.Text = ""
        Lmsg.Text = ""
        TxObservaciones.Text = ""
        GridView1.Visible = False
        'btnActualizarr.Visible = False
    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        If fecha.Text <> "" And wucIncidencias.idIncidencia <> 0 And wucEmpleados2.idEmpleado <> 0 And wucSucursales.idSucursal <> 0 Then
            Dim gp As New ctiCatalogos
            If IsNumeric(grdSR.Text) Then
                grdSR.Text = ""

            End If
            Dim gc As New ctiCatalogos
            Dim r() As String = gp.agregarAsigIncidencias(wucIncidencias.idIncidencia, wucEmpleados2.idEmpleado, FechaC.SelectedDate.ToString, TxObservaciones.Text, chkVer.Checked)
            GridView1.DataSource = gc.gvAsigIncidencias(wucEmpleados2.idEmpleado)
            gc = Nothing
            GridView1.DataBind()
            If r(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
            Else
                Lmsg.CssClass = "correcto"
                Dim sgr As New clsCTI
                grdSR.Text = sgr.seleccionarGridRow2(GridView1, CInt(r(1))).ToString
                gvPos = sgr.gridViewScrollPos(CInt(grdSR.Text))
                sgr = Nothing
                'btnActualizarr.Enabled = True
            End If
            Lmsg.Text = r(0)

        Else
            Lmsg.Text = "Error: Es necesario capturar los datos."
        End If
    End Sub
    Protected Sub btnActualizarr_Click(sender As Object, e As EventArgs) Handles btnActualizarr.Click
        Dim ap As New ctiCatalogos

        Dim idA As Integer = CInt(idDetalle.Text)

        Dim r As String = ap.actualizarAsigIncidencias(idA, wucIncidencias.idIncidencia, wucEmpleados2.idEmpleado, fecha.Text, TxObservaciones.Text, chkVer.Checked)
        GridView1.DataSource = ap.gvAsigIncidencias(wucEmpleados2.idEmpleado)
        ap = Nothing
            GridView1.DataBind()
            If r.StartsWith("Error") Then
                Lmsg.CssClass = "error"
            Else
                Lmsg.CssClass = "correcto"

            End If

            Dim gvp As New clsCTI
            grdSR.Text = gvp.seleccionarGridRow2(GridView1, idA)
            If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) > 0 Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
                gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
            Else
            btnActualizarr.Enabled = False
            btnGuardarNuevo.Enabled = True
            fecha.Text = "" : wucEmpleados2.idEmpleado = 0 : wucSucursales.idSucursal = 0 : wucIncidencias.idIncidencia = 0 : TxObservaciones.Text = ""
        End If
            gvp = Nothing
        Lmsg.Text = r
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Eliminar" Then
            Session("idz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(2).Text
            Session("dz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(3).Text
        ElseIf e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If

            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosAsigIncidencias(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(2).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                idDetalle.Text = datos(0)
                wucIncidencias.idIncidencia = CInt(datos(1))
                wucEmpleados2.idEmpleado = CInt(datos(2))
                fecha.Text = datos(3).ToString
                TxObservaciones.Text = datos(4).ToString
                chkVer.Checked = datos(5)
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing

            End If
            btnGuardarNuevo.Enabled = False
            btnActualizarr.Enabled = True
            Lmsg.Text = ""
        End If
    End Sub
    Protected Sub wucIncidencias_incidenciaSeleccionada(sender As Object, e As System.EventArgs) Handles wucIncidencias.incidenciaSeleccionada
        If wucIncidencias.idIncidencia = 7 Then

        End If
        If wucIncidencias.idIncidencia = 8 Then

        End If
        wucIncidencias.ddlAutoPostBack = True
    End Sub

End Class
