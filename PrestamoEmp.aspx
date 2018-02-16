<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="PrestamoEmp.aspx.vb" Inherits="PrestamoEmp" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucJornadas.ascx" tagname="wucjornadas" tagprefix="uc3" %>
<%@ Register src="cti/wucSuc.ascx" tagname="wucSuc" tagprefix="uc4" %>

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
        .auto-style2 {
            height: 705px;
            width: 1062px;
        }
        .auto-style6 {
            width: 153px;
        }
        .auto-style7 {
            width: 406px;
        }
  </style>
    <div id="contenedor" class="auto-style2">
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

    <h3>Prestamo de Personal</h3>
    <div id="izquierdo">
        <table>
            <tr>
                <td>Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" /><br />
                </td>
               <td>Empleado:<br />
                   <uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
               </td>
               
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idprestamo" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="617px">
            <Columns>
                <asp:BoundField DataField="idprestamo" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png"></asp:ButtonField>
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="Empleado" />
                <asp:BoundField DataField="sucursal" HeaderText="Sucursal" SortExpression="sucursal"  HtmlEncode="False" DataFormatString = "{0:d}"/>
               <asp:BoundField DataField="jornada" HeaderText="Jornada" SortExpression="jornada" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" HtmlEncode="False" DataFormatString = "{0:d}" />
                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" SortExpression="observaciones" />
                <asp:CheckBoxField DataField="verificado" HeaderText="Verificado" />
                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png"></asp:ButtonField> 
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
    <div id="derecho" class="auto-style7">
            <table>
            <tr>
                <td class="auto-style6">Sucursal:<asp:TextBox ID="TextBox1" runat="server" Visible="false" Width="126px" Height="24px"></asp:TextBox>
                    <br />
                     <uc4:wucsuc ID="wucSuc" runat="server" /><br />
                    </td>
              <td class="auto-style6">Jornada:<br />
                <uc3:wucjornadas ID="wucJornadas" runat="server" />
                     
                  <br />
                  </td>
                <td>
                    <div id="veri" runat="server">
                        <asp:CheckBox ID="chkVer" runat="server" Text="Autorizado" />
                    </div>
                </td>
            </tr>
    
            <tr>
                <td class="auto-style1">Fecha:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                 </td>
                <td class="auto-style1">Observaciones:<br />
                    <asp:TextBox ID="TextBox2" runat="server" Height="84px" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    
                

                    <td class="auto-style1">
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                        <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Buscar Registros" Width="108px" /></td>               
                    </td>
            </tr>  
            <tr>
                <td class="auto-style25" ><asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>               
                    <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>

                </td>
               <td class="auto-style25" >&nbsp;</td>
               
            </tr>
                <tr>
                    <td>

                        Nota:<br />
                    <asp:TextBox ID="Txnota" runat="server" CssClass="txtCaptura" MaxLength="40" Width="161px" Height="90px" TextMode="MultiLine" />
            
                                   
            
                    </td>
                </tr>
            </table>
        </div>
</div>
</asp:Content>