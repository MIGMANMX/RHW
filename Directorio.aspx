<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Directorio.aspx.vb" Inherits="_Directorio" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucPuestos.ascx" tagname="wucPuestos" tagprefix="uc2" %>
<%@ Register src="cti/wucSuc.ascx" tagname="wucSuc" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        #contenedor{
            overflow:hidden
        }
        #izquierdo{
            float:left;
        }

         #derecho{
             float:right;
        }
      .auto-style1 {
          width: 598px;
      }
      .auto-style2 {
          width: 281px;
      }
      .auto-style5 {
          width: 265px;
      }
  </style>
    <div id="contenedor">
    <% If IsNumeric(Session("idz_e")) Then
            Response.Write("<div id=confirmar style='position:fixed; left:200; top:300; background-color:White; border-style:solid; border-width:1px; border-color:Black;'>")
            Response.Write("<table>")
            Response.Write("<tr><td rowspan=7 width=5 /><td height=6 /><td rowspan=7 width=6 /></tr>")
            Response.Write("<tr><td class=c_titulo>Confirmación</td></tr>")
            Response.Write("<tr><td height=6 /></tr>")
            Response.Write("<tr><td class=c_texto>¿Confirma la eliminación del empleado <b><i>" & Session("dz_e") & "</i></b> ?</td></tr>")
            Response.Write("<tr><td height=6 /></tr>")
            Response.Write("<tr><td align=center><input type=submit name=btnSi value='   Sí   ' class='boton' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
            Response.Write("<input type=submit name=btnNo value='   No   ' class='boton' /></td></tr>")
            Response.Write("<tr><td height=6 /></tr></table></div>")
        End If%>

    <h3>Empleados</h3>
    <div id="izquierdo">
        <table>
            <tr>
                <td>Sucursal:</td>
                <td><uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="separa10"></td>
                <td>Activos:</td>
                <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" AutoPostBack="true" /></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idempleado" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="381px">
            <Columns>
                <asp:BoundField DataField="idempleado" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png"></asp:ButtonField>
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="empleado" />
                <asp:BoundField DataField="puesto" HeaderText="Puesto" SortExpression="puesto" />       
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
    <div id="derecho">
        <table class="auto-style1">
            <tr>
                <td colspan="2">
                    <h4>
                        Editar registro del empleado
                      </h4>  </td>
                    <td>
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="90px" />
                </td>
               
                <td>
                    &nbsp;</td>
                
            </tr>
            <tr><td colspan="2"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td></tr>
            <tr>
                <td class="auto-style5">Empleado:<br />
                    <asp:TextBox ID="empleado" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" Enabled="False" /></td>
                <td class="auto-style2">&nbsp;</td>        
            </tr>
             <tr>
                <td class="auto-style5">Calle:<br />
                    <asp:TextBox ID="calle" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style2">Numero:&nbsp; Colonia:<br />
                    <asp:TextBox ID="numero" runat="server" CssClass="txtCaptura" MaxLength="40" Width="53px" />&nbsp;<asp:TextBox ID="colonia" runat="server" CssClass="txtCaptura" MaxLength="40" Width="107px" /></td>        
            </tr>
             <tr>
                <td class="auto-style5">CP:<br />
                    <asp:TextBox ID="cp" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style2">Telefono:<br />
                    <asp:TextBox ID="telefono" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>        
            </tr>
            <tr>
                <td class="auto-style5">Correo:<br />
                    <asp:TextBox ID="correo" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style2" id="baj" runat="server">&nbsp;</td>        
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>        
            </tr>
            
        </table>
    </div> <!-- registroDatos -->
</div>
</asp:Content>

