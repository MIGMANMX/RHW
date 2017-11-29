<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Salarios.aspx.vb" Inherits="Default2" %>
<%@ Register src="cti/wucPuestos.ascx" tagname="wucPuestos" tagprefix="uc2" %>
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

    <h3>Salarios</h3>
    <div id="listaDatos" style="margin-left: 120px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames ="idsalario" ForeColor="#333333" GridLines="None" Width="517px">
            <Columns>
                <asp:BoundField DataField="idsalario" ItemStyle-Font-Size="1" ItemStyle-Width="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                <asp:BoundField DataField="idpuesto" HeaderText="Puesto"  />
                <asp:BoundField DataField="hora" HeaderText="Hora"  />
                <asp:BoundField DataField="extra" HeaderText="Horas Extras"  />
                <asp:BoundField DataField="extratiple" HeaderText="Horas Extras Triples"  />
               
               <%-- <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />--%>
            </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#3088b0" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
        <br />
        <br />
    </div> <!-- listaDatos -->
    <div id="registroDatos">
        <table>
            <tr>
                <td colspan="2">
                    <h4>
                        Editar registro</h4>
                </td>
                <td>
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="106px" />               
                &nbsp;
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                </td>
               
                <td>
                    &nbsp;</td>
            </tr>
            <tr><td colspan="2"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td></tr>
                  
            <tr>
                <td class="auto-style1" style="width: 169px">Puesto:<br />
                    <uc2:wucpuestos ID="WucPuestos" runat="server" />
                </td>  
                 <td class="auto-style1" style="width: 169px">Hora Normal:<br />
                    <asp:TextBox ID="txtHoras" runat="server" MaxLength="40" Width="153px" CssClass="txtHora" />&nbsp;</td>      
            </tr>
            <tr>
               
                <td class="auto-style1" style="margin-left: 40px">Horas Extras:<br />
                    <asp:TextBox ID="txtHorasExtras" runat="server" MaxLength="40" Width="153px" CssClass="txtHorasExtras" /></td>
                <td class="auto-style1" style="margin-left: 40px">Horas Extras Triples:<br />
                    <asp:TextBox ID="txtHorasExtrasTiples" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" /></td>
            </tr>  
            </table>
    </div> <!-- registroDatos -->
</asp:Content>
