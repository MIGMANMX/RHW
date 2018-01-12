
Imports RHLogica

Partial Class DiaFestivo
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        Session("menu") = "C"
        Lmsg.Text = "" : gvPos = 0
        If Not Page.IsPostBack Then
            Dim gvds As New ctiCatalogos
            GridView1.DataSource = gvds.gvDiaFestivo
            gvds = Nothing
            GridView1.DataBind()
        End If
        If Request("btnSi") <> "" Then
            Dim ec As New ctiCatalogos
            Dim err As String = ec.eliminarDiaFestivo(CInt(Session("idz_e")))
            GridView1.DataSource = ec.gvDiaFestivo
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
                TDia.Text = ""
                TFecha.Text = ""
            End If
            Lmsg.Text = err
        End If
        Session("idz_e") = ""
    End Sub
    Protected Sub FechaC_SelectionChanged(sender As Object, e As EventArgs) Handles FechaC.SelectionChanged
        TFecha.Text = FechaC.SelectedDate.ToString("dd/MM/yyyy")
    End Sub
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim ap As New ctiCatalogos
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)
        Dim FF As Date
        FF = TFecha.Text.ToString
        Convert.ToDateTime(FF)
        Dim r As String = ap.actualizarDiaFestivo(idA, TDia.Text, FF.ToString("MM/dd/yyyy"))
        GridView1.DataSource = ap.gvDiaFestivo
        ap = Nothing
        GridView1.DataBind()
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            TDia.Text = ""
            TFecha.Text = ""
        End If
        Dim gvp As New clsCTI
        grdSR.Text = gvp.seleccionarGridRow(GridView1, idA)
        GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
        gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
        gvp = Nothing
        Lmsg.Text = r
    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        If TDia.Text <> "" Or TFecha.Text <> "" Then
            If IsNumeric(grdSR.Text) Then
                grdSR.Text = ""
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            End If
            Dim gc As New ctiCatalogos
            Dim FF As Date
            FF = TFecha.Text.ToString
            Convert.ToDateTime(FF)
            Dim r() As String = gc.agregarDiaFestivo(TDia.Text, FF.ToString("MM/dd/yyyy"))
            GridView1.DataSource = gc.gvDiaFestivo
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
                TDia.Text = ""
                TFecha.Text = ""
            End If
            Lmsg.Text = r(0)
        Else
            Lmsg.Text = "Error: Falta capturar"
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
            Dim datos() As String = dsP.datosDiaFestivo(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                TDia.Text = datos(0)
                TFecha.Text = Convert.ToDateTime(datos(1)).ToString("dd/MM/yyyy")
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
