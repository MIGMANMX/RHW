<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Empleados.aspx.vb" Inherits="_Default" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
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
         Response.Write("<tr><td class=c_texto>¿Confirma la eliminación del empleado <b><i>" & Session("dz_e") & "</i></b> ?</td></tr>")
         Response.Write("<tr><td height=6 /></tr>")
         Response.Write("<tr><td align=center><input type=submit name=btnSi value='   Sí   ' class='boton' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
         Response.Write("<input type=submit name=btnNo value='   No   ' class='boton' /></td></tr>")
         Response.Write("<tr><td height=6 /></tr></table></div>")
     End If%>

    <h3>Empleados</h3>
    <div id="listaDatos">
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
            ForeColor="#333333" GridLines="None" Width="614px">
            <Columns>
                <asp:BoundField DataField="idempleado" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png"></asp:ButtonField>
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="empleado" />
                <asp:BoundField DataField="puesto" HeaderText="Puesto" SortExpression="puesto" />
                
                 <asp:BoundField DataField="nss" HeaderText="Nss" SortExpression="nss" />
                <asp:BoundField DataField="fecha_ingreso" HeaderText="fecha_ingreso" SortExpression="fecha_ingreso" />
                 <asp:BoundField DataField="rfc" HeaderText="rfc" SortExpression="rfc" />
              <%--   <asp:BoundField DataField="fecha_nacimiento" HeaderText="fecha_nacimiento" SortExpression="fecha_nacimiento" />
                 <asp:BoundField DataField="calle" HeaderText="calle" SortExpression="calle" />
                 <asp:BoundField DataField="numero" HeaderText="numero" SortExpression="numero" />
                <asp:BoundField DataField="colonia" HeaderText="colonia" SortExpression="colonia" />
                <asp:BoundField DataField="cp" HeaderText="cp" SortExpression="cp" />
                <asp:BoundField DataField="telefono" HeaderText="telefono" SortExpression="telefono" />
                <asp:BoundField DataField="correo" HeaderText="correo" SortExpression="correo" />
                 <asp:BoundField DataField="fecha_baja" HeaderText="fecha_baja" SortExpression="fecha_baja" />--%>

                <asp:CheckBoxField DataField="activo" HeaderText="Activo" />
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
    <div id="registroDatos">
        <table>
            <tr>
                <td colspan="2">
                    <h4>
                        Editar registro del empleado
                      </h4>  </td>
                    <td>
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="90px" />
                </td>
               
                <td>
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                </td>
                
            </tr>
            <tr><td colspan="2"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td></tr>
            <tr>
                <td>Empleado:</td>
                <td><asp:TextBox ID="empleado" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
            <tr>
                <td>Puesto:</td>
                <td><uc2:wucpuestos ID="WucPuestos" runat="server" /></td>
            </tr>
            <tr>
                <td>Activo:</td>
                <td><asp:checkbox ID="activo" runat="server" /></td>
            </tr>
            <tr>
                <td>Sucursal:</td>
                <td><uc1:wucsucursales ID="wucSucursal" runat="server" /></td>
            </tr>
             <tr>
                <td>nss:</td>
                <td><asp:TextBox ID="nss" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
             <tr>
                <td>fecha_ingreso:</td>
                <td><asp:TextBox ID="fecha_ingreso" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" />

                    
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                <asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="161px" TitleFormat="Month" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>

                </td>        
            </tr>
             <tr>
                <td>rfc:</td>
                <td><asp:TextBox ID="rfc" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
             <tr>
                <td>fecha_nacimiento:</td>
                <td><asp:TextBox ID="fecha_nacimiento" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" />

                     <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                <asp:Calendar ID="CFNacimiento" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="161px" TitleFormat="Month" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>

                </td>        
            </tr>
             <tr>
                <td>calle:</td>
                <td><asp:TextBox ID="calle" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
             <tr>
                <td>numero:</td>
                <td><asp:TextBox ID="numero" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
             <tr>
                <td>colonia:</td>
                <td><asp:TextBox ID="colonia" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
             <tr>
                <td>cp:</td>
                <td><asp:TextBox ID="cp" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
             <tr>
                <td>telefono:</td>
                <td><asp:TextBox ID="telefono" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
            <tr>
                <td>correo:</td>
                <td><asp:TextBox ID="correo" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" /></td>        
            </tr>
            <tr>
                <td>fecha_baja:</td>
                <td><asp:TextBox ID="fecha_baja" runat="server" CssClass="txtCaptura" MaxLength="40" Width="300px" />

                     <asp:ImageButton ID="ImageButton3" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                <asp:Calendar ID="CFBaja" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="161px" TitleFormat="Month" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
                    
                </td>        
            </tr>
            
        </table>
    </div> <!-- registroDatos -->
</asp:Content>

