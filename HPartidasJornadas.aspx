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
        .auto-style1 {
            width: 183px;
            height: 15px;
        }
        .auto-style3 {
            height: 15px;
            width: 97px;
        }
        .auto-style5 {
            width: 798px;
        }
        .auto-style6 {
            height: 26px;
            width: 97px;
        }
        .auto-style7 {
            height: 15px;
            width: 144px;
        }
        .auto-style8 {
            width: 183px;
            height: 26px;
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
      <div id="listaDatos">
        <table class="auto-style5">
            <tr>
                <td class="auto-style7">Sucursal:<uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="auto-style3">Empleado:<uc2:wucempleados2 ID="wucEmpleados2" runat="server" /></td>
         
                <td class="auto-style1">
                    <asp:TextBox ID="idpartidas_jornadaT" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <td class="auto-style10">Dia:<br />
                    <asp:TextBox ID="fecha" runat="server" CssClass="txtCaptura" MaxLength="40" Width="149px" />
            
                                   
            
                    Jornada:<uc3:wucjornadas ID="wucJornadas" runat="server" />
            
                                   
            
                 </td>
                 <td class="auto-style6">
            
          <asp:Calendar ID="FechaC" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="16px" Width="168px" TitleFormat="Month" >
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
            <td class="auto-style8">
                <asp:Calendar ID="Calendar1" runat="server" Width="396px" CssClass="auto-style11" Height="176px">
                <DayHeaderStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#FF9900" Font-Bold="True" ForeColor="White" />
            </asp:Calendar>
            </td>
        </table>
          <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>
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
                     <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-warning btn-block btn-flat" Text="Editar" ToolTip="Editar" Width="108px" />                
                    <br />
                   </td> 
                    <td class="auto-style9">
                      <asp:Button ID="btnActualizarr" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Actualizar" ToolTip="Actualizar" Width="108px" />                
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
                <asp:BoundField DataField="idpartidas_jornada" HeaderText="IdJornada" SortExpression="idpartidas_jornada" />
                <asp:BoundField DataField="jornada" HeaderText="Jornada" SortExpression="jornada" />
                <asp:BoundField DataField="inicio" HeaderText="Inicio" SortExpression="inicio" />
                <asp:BoundField DataField="fin" HeaderText="Fin" SortExpression="fin" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />
                
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
    </div> <!-- listaDatos --> 
</asp:Content>
