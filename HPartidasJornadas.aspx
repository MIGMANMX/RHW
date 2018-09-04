<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="HPartidasJornadas.aspx.vb" Inherits="_HPartidasJornadas" %>


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
    <style type="text/css">
        .auto-style3 {
            height: 16px;
            width: 97px;
        }
        .auto-style5 {
            width: 409px;
        }
        .auto-style6 {
            height: 26px;
            width: 97px;
        }
        .auto-style7 {
            height: 16px;
            width: 144px;
        }
        .auto-style9 {
            width: 115px;
        }
        .auto-style10 {
            height: 26px;
            width: 144px;
        }
        .auto-style11 {
            margin-top: 0px;
        }
    </style>
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
          .auto-style12 {
            width: 415px;
        }
          .auto-style13 {
             width: 407px;
         }
          .auto-style15 {
             width: 97px;
         }
         .auto-style16 {
             height: 45px;
             width: 237px;
         }
         .auto-style17 {
             height: 45px;
             width: 97px;
         }
         .auto-style18 {
             width: 237px;
         }
          </style>
      <div id="contenedor">
          <div id="izquierdo">
        <table class="auto-style5">
            <tr>
                <td class="auto-style7" id="suc" runat="server">Sucursal:<uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="auto-style3">Empleado:<uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
                    <asp:TextBox ID="idpartidas_jornadaT" runat="server" Visible="False" Width="37px"></asp:TextBox>
                    <asp:TextBox ID="TIDPJ" runat="server" Visible="False" Width="37px"></asp:TextBox>
                </td>
         
            </tr>
            <tr>
            <td class="auto-style10">Jornada:<br />
                <uc3:wucjornadas ID="wucJornadas" runat="server" />
            
                                   
            
                 <br />
            
                     <asp:TextBox ID="DiaS" runat="server" Visible="False"></asp:TextBox>
            
                     <asp:TextBox ID="THORA" runat="server" Visible="False"></asp:TextBox>
            
                <br />
          <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>
            
                                   
            
                 </td>
                 <td class="auto-style6">
            
                     Dia:<br />
                    <asp:TextBox ID="fecha" runat="server" CssClass="txtCaptura" MaxLength="40" Width="149px" Enabled="False" />
            
                                   
            
                     <br />
            
          <asp:Calendar ID="FechaC" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="27px" Width="160px" TitleFormat="Month" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
                     <br />
                 </td>     </tr>
           </table>
              <hr/>
              <table class="auto-style13"><tr>
                              <td class="auto-style16"> <asp:Label ID="Label1" runat="server" Text="Selecciona una fecha para añadir a toda la semana"></asp:Label>
 </td>
                 <td class="auto-style17">
            
                    <asp:Button ID="btnFechaSemana" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Toda la semana"  ToolTip="Agregar toda la semana" Enabled="true" Width="130px" /> 
                   
                     <br />
            
                 </td>     </tr>
            <tr>
                <td class="auto-style18">

                    Selecciona dia de descanso</td>
                <td class="auto-style15">
                    Descanso:<br />
                    <asp:TextBox ID="DescansoF" runat="server" CssClass="txtCaptura" MaxLength="40" Width="149px" Enabled="False" />
            
                                   
            
          <asp:Calendar ID="FechaD" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="27px" Width="160px" TitleFormat="Month" >
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
        </table></div>
            <div id="derecho" class="auto-style12">
                <asp:Calendar ID="Calendar1" runat="server" Width="396px" CssClass="auto-style11" Height="176px">
                <DayHeaderStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#FF9900" Font-Bold="True" ForeColor="White" />
            </asp:Calendar>
            
           
            </div> <!-- listaDatos -->  
                   
          <br />
            
    </div>
          <br />
          <table>
              <tr>
                  <td class="auto-style9">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" /> 
                   
                      <br />
                   
                  </td>
                   <td class="auto-style9">
                     <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />                
                   <br />
                   </td> 
                  <td class="auto-style9">
                      <asp:Button ID="btnActualizarr" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Actualizar" ToolTip="Actualizar" Width="108px" />                
                    <br />
                  </td> 
                    <td class="auto-style9">
                     <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-warning btn-block btn-flat" Text="Listado" ToolTip="Editar" Width="108px" />                
                   <br />
                    </td>
                  <td>

                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Eliminar"  ToolTip="Elminar registro" Enabled="true" Width="101px" /> 
                                      <br />
                  </td>
              </tr>
          </table>
          <table>
              <tr><td>
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idempleado" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="574px" Height="92px" AllowPaging="True" AllowSorting="True" PageSize="30">
            <Columns>
                <asp:BoundField DataField="idempleado" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
<ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                <asp:BoundField DataField="idpartidas_jornada" HeaderText="IdJornada" SortExpression="idpartidas_jornada" Visible="false" />
                
                <asp:BoundField DataField="jornada" HeaderText="Jornada" SortExpression="jornada" />
                <asp:BoundField DataField="inicio" HeaderText="Inicio" SortExpression="inicio" />
                <asp:BoundField DataField="fin" HeaderText="Fin" SortExpression="fin" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
             <%--<asp:ButtonField   ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />--%>
                
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#FF9933" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
                  </td>
                
                  </tr>
              </table>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
  
</asp:Content>

