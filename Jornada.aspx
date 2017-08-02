﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Jornada.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <% If IsNumeric(Session("idz_e")) Then
            Response.Write("<div id=confirmar style='position:fixed; left:200; top:300; background-color:White; border-style:solid; border-width:1px; border-color:Black;'>")
            Response.Write("<table>")
            Response.Write("<tr><td rowspan=7 width=5 /><td height=6 /><td rowspan=7 width=6 /></tr>")
            Response.Write("<tr><td class=c_titulo>Confirmación</td></tr>")
            Response.Write("<tr><td height=6 /></tr>")
              Response.Write("<tr><td class=c_texto>¿Confirma la eliminación :    <b><i>" & Session("dz_e") & "</i></b> ?</td></tr>")
            Response.Write("<tr><td height=6 /></tr>")
            Response.Write("<tr><td align=center><input type=submit name=btnSi value='   Sí   ' class='boton' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
            Response.Write("<input type=submit name=btnNo value='   No   ' class='boton' /></td></tr>")
            Response.Write("<tr><td height=6 /></tr></table></div>")
        End If%>

    <h3>Horario de Jornadas</h3>
    <div id="listaDatos" style="margin-left: 120px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames ="idjornada" ForeColor="#333333" GridLines="None" Width="517px">
            <Columns>
                <asp:BoundField DataField="idjornada" ItemStyle-Font-Size="1" ItemStyle-Width="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                <asp:BoundField DataField="jornada" HeaderText="Jornada"  />
                <asp:BoundField DataField="inicio" HeaderText="Inicio"  />
                <asp:BoundField DataField="fin" HeaderText="Fin"  />
                <asp:BoundField DataField="color" HeaderText="Color"  />
                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />
            </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#3088b0" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
    </div> <!-- listaDatos -->
    <div id="registroDatos">
        <table>
            <tr>
                <td colspan="3">
                    <h4>
                        Editar registro<span class="agregarElemento"> de la jornada </span>&nbsp;<span class="agregarElemento">
                            </span>
                    </h4>
                </td>
                <td>
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="90px" />
                </td>
               
                <td>
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                </td>
            </tr>
            <tr><td colspan="2"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td></tr>
            <tr>
                <td class="auto-style1">Jornada:</td>
                <td class="auto-style1"><asp:TextBox ID="jornada" runat="server" MaxLength="40" Width="250px" CssClass="txtCaptura" /></td>
                <td rowspan="2" class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style1">Hora de Inicio:</td>
                <td class="auto-style1"><asp:TextBox ID="inicio" runat="server" MaxLength="40" Width="250px" CssClass="txtCaptura" /></td>
                <td rowspan="2" class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style1">Hora de Fin:</td>
                <td class="auto-style1"><asp:TextBox ID="fin" runat="server" MaxLength="40" Width="250px" CssClass="txtCaptura" /></td>
                <td rowspan="2" class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style1">Color:</td>
                <td class="auto-style1"><asp:TextBox ID="color" runat="server" MaxLength="40" Width="250px" CssClass="txtCaptura" /></td>
                <td rowspan="2" class="auto-style1">&nbsp;</td>
            </tr>
        </table>
    </div> <!-- registroDatos -->
</asp:Content>
