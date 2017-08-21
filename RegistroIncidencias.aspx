<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RegistroIncidencias.aspx.vb" Inherits="_RegistroIncidencias" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucIncidencias.ascx" tagname="wucIncidencias" tagprefix="uc3" %>

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
          .auto-style12 {
            width: 542px;
        }
          .auto-style13 {
            width: 219px;
        }
          .auto-style14 {
            margin-left: 0px;
            margin-right: 0px;
        }
          </style>
      <div id="contenedor">
          <div id="izquierdo">
        <table class="auto-style5">
            <tr>
                <td class="auto-style13" id="suc" runat="server">Sucursal:<uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="auto-style3">Empleado:<uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
                    <asp:TextBox ID="idDetalle" runat="server" Visible="False" Width="37px"></asp:TextBox>
                    <asp:TextBox ID="TIDPJ" runat="server" Visible="False" Width="37px"></asp:TextBox>
                </td>
         
            </tr>
            <tr>
            <td class="auto-style13">Incidencia:<br />
                <uc3:wucincidencias ID="wucIncidencias" runat="server" />
            
                                   
            
                 <br />
                <br />
                Dia:<br />
                    <asp:TextBox ID="fecha" runat="server" CssClass="txtCaptura" MaxLength="40" Width="113px" />
            
                                   
            
                    <br />
            
                                   
            
                 <br />
                <br />
          <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>
            
                                   
            
                 </td>
                 <td class="auto-style6">
            
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
                 </td>     </tr>
            <tr>
            <td class="auto-style13"> Observaciones:<br />
                    <asp:TextBox ID="TxObservaciones" runat="server" CssClass="txtCaptura" MaxLength="40" Width="261px" />
            
                                   
            
                    </td>
                 <td class="auto-style6">
            
                     <br />
                </td>     </tr>
            <tr>
            <td class="auto-style13"> 
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" /> 
                   
                     <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />                
                   
                      </td>
                 <td class="auto-style6">
            
           <asp:Button ID="btnActualizarr" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Actualizar" ToolTip="Actualizar" Width="108px" />
                   </td>     </tr>
        </table></div>



            <div id="derecho" class="auto-style12">
                  <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="iddetalle_incidencia" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="512px" Height="92px" AllowPaging="True" AllowSorting="True" PageSize="15" CssClass="auto-style14">
            <Columns>
                <asp:BoundField DataField="iddetalle_incidencia" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
<ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                <asp:BoundField DataField="iddetalle_incidencia" HeaderText="idIncidencia" SortExpression="iddetalle_incidencia" />
                <asp:BoundField DataField="idincidencia" HeaderText="Incidencia" SortExpression="idincidencia" />
                <asp:BoundField DataField="idempleado" HeaderText="idempleado" SortExpression="idempleado" />
                <asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" />
                <asp:BoundField DataField="observaciones" HeaderText="observaciones" SortExpression="observaciones" />
                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />
                
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#FF9933" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
            <br />
            </div> <!-- listaDatos -->  
                   
                                      
                   
    </div>
          <br />
          <table>
              <tr><td>
      
                  </td>
                
                  </tr>
              </table>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
  
</asp:Content>
