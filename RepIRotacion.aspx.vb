
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class _RepIRotacion
    Inherits System.Web.UI.Page
    Dim IR As Integer
    Dim Ent As Integer
    Dim Sal As Integer
    Dim Per As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Session("idz_e") = ""
        tSuc.Text = wucSucursales.sucursal
        Mens.Text = ""
        Repo.ServerReport.Refresh()

        '''''''''''''Ocultar sucursales a Gerentes
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC

        If datos(0) = 2 Then
            wucSucursales.idSucursal = datos(1)
            wucSucursales.Visible = False
            suc.Visible = False
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If FFinal.Visible = True Then
            FFinal.Visible = False
        ElseIf FFinal.Visible = False Then
            FFinal.Visible = True
        End If
    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TxFechaInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False
        TxFechaFin.Text = DateAdd(DateInterval.Day, 13, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
    End Sub
    Protected Sub FFinal_SelectionChanged(sender As Object, e As EventArgs) Handles FFinal.SelectionChanged
        TxFechaFin.Text = FFinal.SelectedDate.ToString("yyyy-MM-dd")
        FFinal.Visible = False
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Repo.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then

                Dim dt1 As Date
                dt1 = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
                Dim dtf As Date
                dtf = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

                Dim dt2 As Date
                dt2 = DateAdd(DateInterval.Day, 1, dtf)
                dt2 = Format(CDate(dt2), "yyyy-MM-dd")
                TxFechaFin2.Text = dt2

                Rotacion()



                Dim p As New ReportParameter("Fecha1", dt1)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fecha2", dtf)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("E", Ent)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("S", Sal)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("P", Per)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("IR", IR)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                Repo.LocalReport.SetParameters(p)

                Repo.ServerReport.Refresh()
            Else
                Mens.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Mens.Text = "Error: Debes seleccionar una sucursal"
        End If

    End Sub
    Public Sub Rotacion()

        If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then

            Dim FechaFinal As Date
            Dim FechaFinal2 As Date
            FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
            FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")

            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosE(wucSucursales.idSucursal, TxFechaInicio.Text, TxFechaFin.Text)
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Mens.CssClass = "error"
                Mens.Text = datos(0)
            Else

                Ent = datos(0)
            End If


            Dim dsP2 As New ctiCatalogos
            Dim datos2() As String = dsP2.datosS(wucSucursales.idSucursal, TxFechaInicio.Text, TxFechaFin.Text)
            dsP2 = Nothing
            If datos2(0).StartsWith("Error") Then
                Mens.CssClass = "error"
                Mens.Text = datos2(0)
            Else

                Sal = datos2(0)
            End If

            Dim dsP3 As New ctiCatalogos
            Dim datos3() As String = dsP3.datosP(wucSucursales.idSucursal)
            dsP3 = Nothing
            If datos3(0).StartsWith("Error") Then
                Mens.CssClass = "error"
                Mens.Text = datos3(0)
            Else

                Per = datos3(0)
            End If


        Else
            Mens.Text = "Error: Falta Capturar Fecha"
        End If

        IR = ((Ent + Sal) / 2) / ((Per + (Per + Ent - Sal)) / 2)
    End Sub
End Class
