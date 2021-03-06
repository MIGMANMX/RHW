﻿
Imports RHLogica

Partial Class PrestamoEmp

    Inherits System.Web.UI.Page
    Dim Fech As Date
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC

        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True
        End If
        FIngreso.Visible = False

        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 8 Then
                chkVer.Checked = False
                veri.Visible = False
                Txnota.Enabled = False
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
            If datos(0) = 2 Then
                chkVer.Checked = False
                veri.Visible = False
                Txnota.Enabled = False
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                wucEmpleados2.ddlDataSource(datos(1))
                gvds = Nothing
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
        Else
            wucEmpleados2.ddlAutoPostBack = True
        End If

        If Request("btnSi") <> "" Then
            Dim ec As New ctiCalendario
            Dim err As String = ec.eliminarPrestamo(CInt(Session("idz_e")))
            GridView1.DataSource = ec.gvPrestamo(wucEmpleados2.idEmpleado)
            ec = Nothing
            GridView1.DataBind()
            If err.StartsWith("Error") Then
                Lmsg.CssClass = "error"
                grdSR.Text = ""
                btnActualizar.Enabled = False
            Else
                Lmsg.CssClass = "correcto"
                grdSR.Text = ""
                btnActualizar.Enabled = False
                'puesto.Text = ""
                'wucEmpleados2.idEmpleado = 0
                'dropLTipo.SelectedValue = 0
                'fecha_ingreso.Text = ""
                'observaciones.Text = ""
                'cantidad.Text = ""
            End If
            Lmsg.Text = err
        End If
        Session("idz_e") = ""

    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada

        Dim gvds As New ctiWUC
        wucEmpleados2.ddlDataSource(wucSucursales.idSucursal)
        GridView1.Visible = False
        gvds = Nothing
        wucEmpleados2.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
        wucJornadas.idJornada = 0
        wucSuc.idSucursal = 0
        TxFechaInicio.Text = ""
        TextBox2.Text = ""
        chkVer.Checked = False
    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado
        Dim gvds As New ctiCalendario
        GridView1.DataSource = gvds.gvPrestamo(wucEmpleados2.idEmpleado)
        GridView1.Visible = True
        gvds = Nothing

        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
        wucJornadas.idJornada = 0
        wucSuc.idSucursal = 0
        TxFechaInicio.Text = ""
        TextBox2.Text = ""
        chkVer.Checked = False
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TxFechaInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False
    End Sub
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If TxFechaInicio.Text <> "" And TextBox2.Text <> "" Then
            Dim ap As New ctiCalendario
            Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)

            Dim FF As Date
            FF = TxFechaInicio.Text.ToString
            Convert.ToDateTime(FF)

            Dim r As String = ap.actualizarPrestamo((CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)), wucEmpleados2.idEmpleado, wucSuc.idSucursal, wucJornadas.idJornada, FF.ToString("MM/dd/yyyy"), TextBox2.Text, chkVer.Checked, Txnota.Text)
            GridView1.DataSource = ap.gvPrestamo(wucEmpleados2.idEmpleado)
            ap = Nothing
            GridView1.DataBind()
            If r.StartsWith("Error") Then
                Lmsg.CssClass = "error"
            Else
                Lmsg.CssClass = "correcto"
                'Limpiar
                ' wucEmpleados2.idEmpleado = 0
                wucJornadas.idJornada = 0
                wucSuc.idSucursal = 0
                TxFechaInicio.Text = ""
                TextBox2.Text = ""
                chkVer.Checked = False
                btnGuardarNuevo.Enabled = True
            End If
            Dim gvp As New clsCTI
            grdSR.Text = gvp.seleccionarGridRow(GridView1, (CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)))
            GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
            gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
            gvp = Nothing
            Lmsg.Text = r
        Else
            Lmsg.Text = "Falta capturar un dato"
        End If
    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        If TxFechaInicio.Text <> "" And TextBox2.Text <> "" And wucJornadas.idJornada <> 0 Then
            If IsNumeric(grdSR.Text) Then
                grdSR.Text = ""
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            End If
            Dim nota As String
            If Txnota.Text = "" Then
                nota = "Sin Verificar"
            End If
            Dim gc As New ctiCalendario
            Dim FF As Date
            FF = TxFechaInicio.Text.ToString
            Convert.ToDateTime(FF)
            Dim r() As String = gc.agregarPrestamo(wucEmpleados2.idEmpleado, wucSuc.idSucursal, wucJornadas.idJornada, FF.ToString("MM/dd/yyyy"), TextBox2.Text, chkVer.Checked, nota)
            GridView1.DataSource = gc.gvPrestamo(wucEmpleados2.idEmpleado)
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

                ' wucEmpleados2.idEmpleado = 0
                wucJornadas.idJornada = 0
                wucSuc.idSucursal = 0
                TxFechaInicio.Text = ""
                TextBox2.Text = ""
                chkVer.Checked = False
                FIngreso.SelectedDates.Clear()
            End If
            Lmsg.Text = r(0)
        Else
            Lmsg.Text = "Falta capturar un dato"
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
            Dim dsP As New ctiCalendario
            Dim datos() As String = dsP.datosPrestamo(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                wucEmpleados2.idEmpleado = datos(0)
                wucSuc.idSucursal = datos(1)
                wucJornadas.idJornada = datos(2)
                TxFechaInicio.Text = Convert.ToDateTime(datos(3)).ToString("dd/MM/yyyy")
                Fech = Convert.ToDateTime(datos(3)).ToString("yyyy-MM-dd")
                TextBox2.Text = datos(4)
                chkVer.Checked = datos(5)
                Txnota.Text = datos(6)
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing
                btnActualizar.Enabled = True
                btnGuardarNuevo.Enabled = False
            End If
        End If
    End Sub
End Class

