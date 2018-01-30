
Imports RHLogica

Partial Class AuParticulares
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        wucSucursales.idSucursal = 0
        GridView1.Visible = False
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        btnGuardarNuevo.Enabled = True
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC

        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"

            wucSucursales.ddlAutoPostBack = True

        End If
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada

        GridView1.Visible = False
        Dim gvds As New ctiCalendario
        GridView1.DataSource = gvds.gvParticularesCHK(wucSucursales.idSucursal)
        GridView1.Visible = True
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If

    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        Dim a As Integer = 0
        For Each row As GridViewRow In GridView1.Rows

            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    TextBox1.Text = row.Cells(2).Text

                    Dim ap As New ctiCalendario
                    Dim r As String = ap.actualizarParticularesCHK(row.Cells(2).Text, True)
                    GridView1.DataSource = ap.gvParticularesCHK(wucSucursales.idSucursal)
                    ap = Nothing
                    GridView1.DataBind()
                    If r.StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"

                    End If

                    Dim gvp As New clsCTI
                    grdSR.Text = gvp.seleccionarGridRow2(GridView1, row.Cells(2).Text)
                    If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) > 0 Then
                        GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
                        gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
                    Else
                        '    btnActualizarr.Enabled = False
                        '    btnGuardarNuevo.Enabled = True
                        '    fecha.Text = "" : wucEmpleados2.idEmpleado = 0 : wucSucursales.idSucursal = 0 : wucIncidencias.idIncidencia = 0 : TxObservaciones.Text = ""
                    End If
                    gvp = Nothing
                    Lmsg.Text = r
                End If
            End If

        Next
    End Sub
    Protected Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        Dim a As Integer = 0
        For Each row As GridViewRow In GridView1.Rows

            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    TextBox1.Text = row.Cells(2).Text

                    Dim ap As New ctiCalendario
                    Dim r As String = ap.actualizarParticularesCHK(row.Cells(2).Text, False)
                    GridView1.DataSource = ap.gvParticularesCHK(wucSucursales.idSucursal)
                    ap = Nothing
                    GridView1.DataBind()
                    If r.StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"

                    End If

                    Dim gvp As New clsCTI
                    grdSR.Text = gvp.seleccionarGridRow2(GridView1, row.Cells(2).Text)
                    If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) > 0 Then
                        GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
                        gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
                    Else
                        '    btnActualizarr.Enabled = False
                        '    btnGuardarNuevo.Enabled = True
                        '    fecha.Text = "" : wucEmpleados2.idEmpleado = 0 : wucSucursales.idSucursal = 0 : wucIncidencias.idIncidencia = 0 : TxObservaciones.Text = ""
                    End If
                    gvp = Nothing
                    Lmsg.Text = r
                End If
            End If

        Next
    End Sub
End Class
