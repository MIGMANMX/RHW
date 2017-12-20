﻿
Imports RHLogica

Partial Class Principal
    Inherits System.Web.UI.MasterPage

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Session.Clear()
        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("idusuario")) Then
            Response.Redirect("Default.aspx", True)
        End If
        direc.Visible = False
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        If datos(0) >= 3 And datos(0) <= 6 Then
            Response.Redirect("Personal.aspx", True)
        End If
        If datos(0) = 2 Then
            direc.Visible = True
            catalogo.Visible = False
            catalogo3.Visible = False
            catalogo2.Visible = False
            catalogo1.Visible = False
            nomina.Visible = False
            acc.Visible = False
            jor.Visible = False
            rep.Visible = True
            che.Visible = False
            rep1.Visible = False
            rep2.Visible = False
            rep3.Visible = True
            incide.Visible = False
            regInidencia.Visible = True
            dia.Visible = False
            EHJ.Visible = False
            confi.Visible = False
        End If

        If datos(0) = 7 Then
            direc.Visible = True
            catalogo.Visible = True
            catalogo2.Visible = True
            catalogo1.Visible = True
            nomina.Visible = True
            acc.Visible = True
            jor.Visible = True
            rep.Visible = True
            che.Visible = True
            rep1.Visible = True
            rep2.Visible = True
            rep3.Visible = True
            incide.Visible = True
            regInidencia.Visible = True
            dia.Visible = True
            EHJ.Visible = True
            confi.Visible = True
        End If
    End Sub
End Class