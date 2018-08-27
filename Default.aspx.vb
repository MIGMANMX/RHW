
Imports System.Data.SqlClient

Partial Class _Default
    Inherits System.Web.UI.Page
    Public Sucursales As Integer = 0
    Public Empleados As Integer = 0
    Public Empresas As Integer = 0
    Public Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load



        'Conexion y busqueda de registros
        Using dbC As New SqlConnection
            dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrDbo").ToString
            dbC.Open()
            Dim cmd As New SqlCommand("SELECT count(sucursal) as num FROM Sucursales ", dbC)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            Dim dsP As String()
            If rdr.Read Then
                'Lectura de registros
                ReDim dsP(1)
                dsP(0) = rdr("num").ToString
                Sucursales = dsP(0)
            End If
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using


        'Conexion y busqueda de registros
        Using dbC As New SqlConnection
            dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrDbo").ToString
            dbC.Open()
            Dim cmd As New SqlCommand("SELECT count(empresa) as num FROM Empresas", dbC)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            Dim dsP As String()
            If rdr.Read Then
                'Lectura de registros
                ReDim dsP(1)
                dsP(0) = rdr("num").ToString
                Empresas = dsP(0)
            End If
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using


        'Conexion y busqueda de registros
        Using dbC As New SqlConnection
            dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrDbo").ToString
            dbC.Open()
            Dim cmd As New SqlCommand("SELECT count(empleado) as num  FROM Empleados where activo=1", dbC)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            Dim dsP As String()
            If rdr.Read Then
                'Lectura de registros
                ReDim dsP(3)
                dsP(0) = rdr("num").ToString
                Empleados = dsP(0)
            End If
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using
    End Sub
End Class