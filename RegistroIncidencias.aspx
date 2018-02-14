<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RegistroIncidencias.aspx.vb" Inherits="_RegistroIncidencias" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucIncidencias.ascx" tagname="wucIncidencias" tagprefix="uc3" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="cti/wucJornadas.ascx" tagname="wucjornadas" tagprefix="uc3" %>

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
          .auto-style12 {
            width: 609px;
        }
          .auto-style14 {
            margin-left: 0px;
            margin-right: 0px;
        }
          .auto-style15 {
            width: 208px;
        }
          .auto-style16 {
            width: 455px;
            height: 31px;
        }
        .auto-style17 {
            width: 208px;
            height: 31px;
        }
        .auto-style18 {
            width: 409px;
        }
        .auto-style19 {
            width: 455px
        }
        .auto-style20 {
            width: 374px;
        }
          </style>
      <div id="contenedor">
          <div id="izquierdo" class="auto-style18">
        <table class="auto-style20">
            <tr>
                <td class="auto-style19" id="suc" runat="server">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />
                </td>
                <td class="auto-style15">Empleado:<uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
                    <asp:TextBox ID="idDetalle" runat="server" Visible="False" Width="37px"></asp:TextBox>
                    <asp:TextBox ID="TIDPJ" runat="server" Visible="False" Width="37px"></asp:TextBox>
                </td>
         
            </tr>
            <tr>
            <td class="auto-style16">Incidencia:<br />
                <uc3:wucincidencias ID="wucIncidencias" runat="server" />
       
                 </td>
                 <td class="auto-style17" id="VER" runat="server">
            
                                   
            
                     <asp:CheckBox ID="chkVer" runat="server" Text="Autorizado" />
            
                                   
            
                 </td>     </tr>
            <tr>
            <td class="auto-style19"> 
                    Observaciones:<br />
                    <br />
                    <asp:TextBox ID="TxObservaciones" runat="server" CssClass="txtCaptura" MaxLength="40" Width="188px" Height="142px" TextMode="MultiLine" />
            
                                   
            
                    </td>
                 <td class="auto-style15">
            
                Dia:<br />
                    <asp:TextBox ID="fecha" runat="server" CssClass="txtCaptura" MaxLength="40" Width="113px" Enabled="False" />
            
                                   
            
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
            
          <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>
            
                                  
            
                 </td>     </tr>
            <tr>
                <td>

                    Nota:<br />
                    <asp:TextBox ID="Txnota" runat="server" CssClass="txtCaptura" MaxLength="40" Width="188px" Height="90px" TextMode="MultiLine" />
            
                                   
            
                </td> 
            </tr>
            <tr>
            <td class="auto-style19"> 
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" /> 

            </td>
                 <td class="auto-style15">
            
           <asp:Button ID="btnActualizarr" runat="server" CssClass="btn btn-warning btn-block btn-flat" Text="Actualizar" ToolTip="Actualizar" Width="108px" />
                   </td>     </tr>
            <tr>
                <td>
                      <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />                                
                </td>
                <td id="elim" runat="server">
                     <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Eliminar" ToolTip="Eliminar" Width="108px" />                                              
                </td>
            </tr>
        </table></div>



            <div id="derecho" class="auto-style12">
                  <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="iddetalle_incidencia" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="610px" Height="92px" AllowPaging="True" AllowSorting="True" PageSize="15" CssClass="auto-style14">
            <Columns>
                <asp:BoundField DataField="iddetalle_incidencia" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
<ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                <asp:BoundField DataField="iddetalle_incidencia" HeaderText="ID" SortExpression="iddetalle_incidencia" />
                <asp:BoundField DataField="incidencia" HeaderText="Incidencia" SortExpression="incidencia" />
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="empleado" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha"  HtmlEncode="False" DataFormatString = "{0:d}" />
                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" SortExpression="observaciones" />
                <asp:CheckBoxField DataField="verificado" HeaderText="Verificado" />
            <%--    <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />
          --%>      
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
