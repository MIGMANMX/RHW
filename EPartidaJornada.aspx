<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="EPartidaJornada.aspx.vb" Inherits="EPartidaJornada" %>



<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucJornadas.ascx" tagname="wucJornadas" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
             width: 280px;
         }
         .auto-style2 {
             width: 630px;
         }
         .auto-style3 {
             width: 382px;
         }
          .auto-style4 {
             width: 234px;
             height: 45px;
         }
         .auto-style5 {
             height: 45px;
         }
          .auto-style6 {
             width: 242px;
             height: 45px;
         }
         .auto-style7 {
             width: 351px;
             height: 45px;
         }
         .auto-style8 {
             width: 360px;
         }
          </style>
      <div id="contenedor">
          <div id="izquierdo" class="auto-style2">
              <table>
             
                  
                  <tr>
                      <td id="suc">
                          Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />
                    <asp:TextBox ID="idpartidas_jornadaT" runat="server" CssClass="txtCaptura" MaxLength="40" Width="149px" Enabled="False" Visible="False" />            
                   
                      </td>
                      <td>
                           Empleado:<br />
                  <uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
                      </td>
                      <td>
                          <br />
                      &nbsp;&nbsp;&nbsp;&nbsp;
                      </td>
                      <td>
                          
                           <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Buscar"  ToolTip="Buscar datos" Enabled="true" Width="108px" />
               
                      </td>                    
                  </tr>
                        <tr>
                    <td class="auto-style4">
                       
                        Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    </td>
                 <td class="auto-style5">
                    
                     Fecha Fin:<br />
                    <asp:TextBox ID="TxFechaFin" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" />

                    <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    <asp:TextBox ID="TxFechaFin2" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" Visible="False" />

                                    

                    </td>
            </tr>
                     <tr>
                <td>
                    <asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
                <td>
                    <asp:Calendar ID="FFinal" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
         
              </table>
        <table>    
            <tr>
                <td class="auto-style1">
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idempleado" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="White" GridLines="None" Width="632px" Height="92px" AllowPaging="True" AllowSorting="True" PageSize="30">
            <Columns>
                <asp:BoundField DataField="idpartidas_jornada" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
<ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />  
                <asp:BoundField DataField="jornada" HeaderText="Jornada" SortExpression="jornada" />
                <asp:BoundField DataField="inicio" HeaderText="Inicio" SortExpression="inicio" />
                <asp:BoundField DataField="fin" HeaderText="Fin" SortExpression="fin" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" HtmlEncode="False" DataFormatString = "{0:d}"/>
                 <asp:CheckBoxField DataField="completar" HeaderText="C Entrada" />
                 <asp:CheckBoxField DataField="completarfin" HeaderText="C Salida" />
                 <asp:CheckBoxField DataField="completarhsal" HeaderText="C Hora Salida" />
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="White" Font-Size="1" Height="1" />
            <PagerSettings FirstPageText="" Mode="NumericFirstLast" PageButtonCount="15" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
                      
                </td>
            </tr>           
            </table>
          </div>

            <div id="derecho" class="auto-style3">
                <table class="auto-style8">
                       <tr>
            <td class="auto-style7">
                    Jornada:<br />
                <uc3:wucjornadas ID="wucJornadas" runat="server" />
                     
                  <br />
                     <asp:CheckBox ID="chk" runat="server" Font-Size="Medium" Text="Completar Entrada Jornada" Font-Italic="False" />
                     <br />
                     <asp:CheckBox ID="chksalida" runat="server" Font-Size="Medium" Text="Completar Salida Jornada" Font-Italic="False" />
                     <br />
                     <asp:CheckBox ID="chkhsal" runat="server" Font-Size="Medium" Text="Completar Hora Salida " Font-Italic="False" />
                     <br />
                <br />
          <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>
          
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>         
                 </td>
                 <td class="auto-style6">
            
                     Dia:<br />
                    <asp:TextBox ID="fecha" runat="server" CssClass="txtCaptura" MaxLength="40" Width="69px" Enabled="False" />                              
                     <br />
                     <br />
                     <br />
                     <br />
                     <br />
                     <br />
                 </td>               
            </tr> 
                     <tr>
                <td class="auto-style7">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" />
                </td>
                <td>

                    <asp:Button ID="btnActualizarr" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Actualizar" ToolTip="Actualizar" Width="108px" />

                </td>
            </tr>
              </table>
            </div>
          <!-- listaDatos -->                     
              </div>  
</asp:Content>

