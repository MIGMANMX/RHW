
Imports System.Data.SqlClient
Imports RHLogica

Partial Class Prenomina
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Mens.Text = "" : gvPos = 0
        Session("idz_e") = ""
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
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim idempleado As Integer = 0
        Dim empleado As String = ""

        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                ''''Funcion para generar datos

                ''''Generar grid
                'Dim ec As New ctiCalculo
                'GridView1.DataSource = ec.gvCalculoSucursal()
                'ec = Nothing
                'GridView1.DataBind()


                'Segun el empleado insertar las horas
                'Funcion que sume las horasnormales
                'Buscar horas extras
                'Buscar horas extras3
                'Buscar dias Festivos
                'Buscar dias Descanso
                'TotalHoras
                'Calcular el importe Normal
                '

                Dim dbc3 As New SqlConnection
                dbc3.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbc3.Open()

                'Consulta de empleados por sucursal
                Dim cmd3 As New SqlCommand("SELECT idempleado,empleado FROM vm_EmpleadoSucursal  WHERE idsucursal =@idsucursal", dbc3)
                cmd3.Parameters.AddWithValue("idsucursal", wucSucursales.idSucursal)
                Dim rdr3 As SqlDataReader = cmd3.ExecuteReader
                'Inicio de ciclo
                While rdr3.Read
                    idempleado = rdr3("idempleado").ToString
                    empleado = rdr3("empleado").ToString

                End While

            Else
                Mens.Text = "Error: Falta Capturar Fecha"
            End If
        Else
            Mens.Text = "Error: Falta Capturar Empleado"
        End If
    End Sub

End Class
