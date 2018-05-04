<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Empleados.aspx.vb" Inherits="_Empleados" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucPuestos.ascx" tagname="wucPuestos" tagprefix="uc2" %>
<%@ Register src="cti/wucSuc.ascx" tagname="wucSuc" tagprefix="uc3" %>
<%@ Register src="cti/wucTipoJornada.ascx" tagname="wucTipoJornada" tagprefix="uc4" %>

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
      .auto-style3 {
          height: 24px;
          width: 185px;
      }
      .auto-style5 {
          width: 177px;
      }
      .auto-style6 {
          height: 24px;
      }
      .auto-style7 {
          width: 109px;
      }
      .auto-style8 {
          width: 1115px;
      }
      .auto-style9 {
          width: 256px;
      }
      .auto-style10 {
          width: 140px;
      }
      .auto-style11 {
          height: 24px;
          width: 179px;
      }
      .auto-style14 {
          width: 168px;
      }
      .auto-style15 {
          width: 171px;
      }
      .auto-style16 {
          width: 228px;
      }
      .auto-style17 {
          width: 215px;
      }
      .auto-style18 {
          width: 185px;
      }
      .auto-style19 {
          width: 179px;
      }
  </style>
    <div id="contenedor" class="auto-style8">
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
                <td class="auto-style6">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style6">&nbsp;&nbsp;</td>
                <td class="auto-style6">Activos:<asp:CheckBox ID="chkActivo" runat="server" Checked="true" AutoPostBack="true" /></td>
                <td>&nbsp;&nbsp;</td>
                <td class="auto-style7">Baja:<asp:CheckBox ID="chkBaja" runat="server" AutoPostBack="true" />
                    </td>
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
                <asp:CheckBoxField DataField="activo" HeaderText="Activo" />
                <asp:BoundField DataField="clave_att" HeaderText="Clave" SortExpression="clave_att" />
                 <asp:CheckBoxField DataField="baja" HeaderText="Baja" />
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
    <div id="derecho">
        <table class="auto-style1">
            <tr>
                <td colspan="2">
                    <h4 class="auto-style9">
                        Editar registro del empleado
                                </h4>  </td>
                    
               
                <td>
                    </td>
                
            </tr>
            <tr>
                <td>
                  
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                  
                    <br />
                    </td>
                <td class="auto-style19">
                      <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="108px" />
                    </td>  
                <td>
                    <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>
                  
                    <br />
                  
                    </td>      
            </tr>
            <tr>
                <td class="auto-style18">Empleado:<br />
                    <asp:TextBox ID="empleado" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style19">Puesto:<br />
                    <uc2:wucpuestos ID="WucPuestos" runat="server" /></td>  
                <td>

                    Sucursal:<br />
                    <uc3:wucsuc ID="wucSuc" runat="server" />

                </td>      
            </tr>
            
            <tr>
                <td class="auto-style18">Activo:<asp:checkbox ID="activo" runat="server" Checked="True" /> <br />
                    Baja:&nbsp;<asp:checkbox ID="baja" runat="server" />
                    </td>
             
            <td class="auto-style19">Clave:<br />
                                    <asp:TextBox ID="claveTX" runat="server" CssClass="txtCaptura" MaxLength="40" Width="99px" style="margin-left: 0" Enabled="False" />
            </td>
                <td>
                    Tipo de jornada:<br />
                  <uc4:wucTipoJornada ID="wucTipoJornada" runat="server" />
                </td>
            </tr>

            <tr>
                <td class="auto-style3">NSS:<br />
                    <asp:TextBox ID="nss" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style11">RFC:<br />
                    <asp:TextBox ID="rfc" runat="server" CssClass="txtCaptura" MaxLength="40" Width="162px" /></td>
                <td>Curp:<br />
                      <asp:TextBox ID="curp" runat="server" CssClass="txtCaptura" MaxLength="40" Width="162px" /></td>
                </td>
            </tr>
             <tr>
                <td class="auto-style18">Fecha de Ingreso:<br />
                    <asp:TextBox ID="fecha_ingreso" runat="server" CssClass="txtCaptura" MaxLength="40" Width="135px" />

                    
                    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                    <br />

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
                <td class="auto-style19">Fecha de Nacimiento:<br />
                    <asp:TextBox ID="fecha_nacimiento" runat="server" CssClass="txtCaptura" MaxLength="40" Width="130px" />

                     &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                    <br />

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
            </table>
        <hr />
        <table>Datos de Contacto:
             <tr>

                <td class="auto-style5">Calle:<br />
                    <asp:TextBox ID="calle" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style10">Numero:<br />

                    <asp:TextBox ID="numero" runat="server" CssClass="txtCaptura" MaxLength="40" Width="53px" /></td>        
            <td class="auto-style15"> Colonia:<br /> <asp:TextBox ID="colonia" runat="server" CssClass="txtCaptura" MaxLength="40" Width="107px" />  </td>
             </tr>
             <tr>
                <td class="auto-style5">CP:<br />
                    <asp:TextBox ID="cp" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style10">Telefono:<br />
                    <asp:TextBox ID="telefono" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>   
                    <td class="auto-style15">Correo:<asp:TextBox ID="correo" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>     
            </tr>
            </table>
        <hr />
        <table>Datos de Emergencia:
            <tr>
               <td class="auto-style16">Nombre:<br />
                    <asp:TextBox ID="nombreTxt" runat="server" CssClass="txtCaptura" MaxLength="40" Width="189px" /></td>
                <td class="auto-style14">Telefono:<br />
                    <asp:TextBox ID="telefonoTxt" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
             
                 </tr>
             </table>
        <hr />
        <table>Datos de Baja
            <tr>
                 <td class="auto-style17" id="baj" runat="server">Fecha de Baja:<br />
                    <asp:TextBox ID="fecha_baja" runat="server" CssClass="txtCaptura" MaxLength="40" Width="152px" />

                     &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                    <br />

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
                <td class="auto-style16">Nota:<br />
                    <asp:TextBox ID="notaTxt" runat="server" CssClass="txtCaptura" MaxLength="40" Width="234px" Height="137px" TextMode="MultiLine" /></td>   
            </tr>
            
        </table>
    </div> <!-- registroDatos -->
</div>
</asp:Content>

