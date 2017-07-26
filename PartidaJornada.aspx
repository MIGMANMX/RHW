<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="PartidaJornada.aspx.vb" Inherits="_Default" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados.ascx" tagname="wucEmpleados" tagprefix="uc2" %>
<%@ Register src="cti/wucJornadas.ascx" tagname="wucJornadas" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #registroDatos {
            width: 777px;
        }
        .auto-style4 {
            height: 25px;
        }
        .auto-style5 {
            width: 188px;
            height: 25px;
        }
        .auto-style6 {
            height: 25px;
            width: 168px;
        }
        .auto-style7 {
            width: 168px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="listaDatos">
        <table>
            <tr>
                <td>Sucursal:</td>
                <td><uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="separa10"></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" AutoPostBack="true" Visible="False" /><asp:TextBox ID="idjornada" runat="server" CssClass="txtCaptura" MaxLength="40" Width="59px" Height="16px" /><asp:TextBox ID="idempleado" runat="server" CssClass="txtCaptura" MaxLength="40" Width="69px" Height="19px" /></td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idempleado" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="451px" Height="71px">
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
    <div id="registroDatos">
        <table>
      <tr>
                <td class="auto-style6">Dia:<br />
                    <asp:TextBox ID="fecha" runat="server" CssClass="txtCaptura" MaxLength="40" Width="105px" />
            
                <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/img/favicon.ico" Width="19px" />                   
            
                 </td>
                 <td class="auto-style6">
            
                     Empleado:<br />
                     <asp:TextBox ID="empleado" runat="server" CssClass="txtCaptura" MaxLength="40" Width="150px" />
                 </td>
                <td class="auto-style4">
                    Jornada:<br />
                    <uc3:wucjornadas ID="wucJornadas" runat="server" /></td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style4"></td>
                <td class="auto-style5"></td>
   
   </tr>
             <tr>
                  <td>
            
          <asp:Calendar ID="FechaC" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="16px" Width="166px" TitleFormat="Month" >
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
                &nbsp;</td> 
                      
             </tr>  
             <tr>
                <td class="auto-style7">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" />
                 </td>
                <td>
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                 </td>        
            </tr>
             <tr>
                <td class="auto-style7">
                </td>
               
                <td>
                    &nbsp;</td>
                  </tr>
        
            </table>
              <asp:Calendar ID="Calendar1" runat="server">
                <DayHeaderStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#FF9900" Font-Bold="True" ForeColor="White" />
            </asp:Calendar>
     </div>
</asp:Content>

