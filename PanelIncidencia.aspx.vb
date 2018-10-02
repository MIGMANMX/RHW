
Imports RHLogica

Partial Class _PanelIncidencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckBox1.AutoPostBack = True
        CheckBox2.AutoPostBack = True
        CheckBox3.AutoPostBack = True
        CheckBox4.AutoPostBack = True
        CheckBox5.AutoPostBack = True
        CheckBox6.AutoPostBack = True
        Suc.Visible = False
        CheckBox4.Enabled = False
        CheckBox5.Enabled = False
        CheckBox6.Enabled = False

        Dim dsP As New ctiPanel
        Dim datos() As String = dsP.datosIncidencias()
        dsP = Nothing

        CheckBox4.Checked = datos(1)
        CheckBox5.Checked = datos(2)
        CheckBox6.Checked = datos(3)
        If datos(4) = 0 Then
            wucSucursales1.idSucursal = 1
            SucD.Visible = False
        Else
            wucSucursales1.idSucursal = datos(4)
            SucD.Visible = True

        End If

    End Sub
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Then
            Dim ap As New ctiPanel
            Dim r As String = ap.actualizarIncidencias(CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, wucSucursales.idSucursal)
            If r.StartsWith("Error") Then
                Mens.CssClass = "error"
            Else
                Mens.CssClass = "error"
            End If
            CheckBox1.AutoPostBack = True
            CheckBox2.AutoPostBack = True
            CheckBox3.AutoPostBack = True
            CheckBox4.AutoPostBack = True
            CheckBox5.AutoPostBack = True
            CheckBox6.AutoPostBack = True
            Response.Redirect("PanelIncidencia.aspx")

        ElseIf CheckBox3.Checked = True And wucSucursales.idSucursal <> 0 Then
            Dim ap As New ctiPanel
            Dim r As String = ap.actualizarIncidencias(CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, wucSucursales.idSucursal)
            If r.StartsWith("Error") Then
                Mens.CssClass = "error"
            Else
                Mens.CssClass = "error"
            End If
            CheckBox1.AutoPostBack = True
            CheckBox2.AutoPostBack = True
            CheckBox3.AutoPostBack = True
            CheckBox4.AutoPostBack = True
            CheckBox5.AutoPostBack = True
            CheckBox6.AutoPostBack = True
            Response.Redirect("PanelIncidencia.aspx")
        Else
            Mens.Text = "Error: Selecciona una sucursal"
            CheckBox3.Checked = False
        End If
    End Sub
    Protected Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox2.Enabled = False
            CheckBox2.Checked = False
            CheckBox1.Enabled = False
            CheckBox1.Checked = False
            Suc.Visible = True
            wucSucursales.idSucursal = 0
        ElseIf CheckBox3.Checked = False Then
            CheckBox2.Enabled = True
            'CheckBox2.Checked = True
            CheckBox1.Enabled = True
            'CheckBox1.Checked = True
        End If
        CheckBox3.AutoPostBack = True
    End Sub

    Protected Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox3.Enabled = False
            CheckBox3.Checked = False
            CheckBox1.Enabled = False
            CheckBox1.Checked = False
            Suc.Visible = False
            wucSucursales.idSucursal = 0
        ElseIf CheckBox2.Checked = False Then
            CheckBox3.Enabled = True
            'CheckBox3.Checked = True
            CheckBox1.Enabled = True
            'CheckBox1.Checked = True
        End If
        CheckBox2.AutoPostBack = True
    End Sub
    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Enabled = False
            CheckBox2.Checked = False
            CheckBox3.Enabled = False
            CheckBox3.Checked = False
            Suc.Visible = False
        ElseIf CheckBox1.Checked = False Then
            CheckBox2.Enabled = True
            'CheckBox2.Checked = True
            CheckBox3.Enabled = True
            'CheckBox3.Checked = True
        End If
        CheckBox1.AutoPostBack = True
    End Sub
End Class
