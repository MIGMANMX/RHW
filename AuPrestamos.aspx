<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="AuPrestamos.aspx.vb" Inherits="AuPrestamos" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" /--%>
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
          .auto-style15 {
        width: 148px;
    }
          </style>
      <div id="contenedor">
          <div id="izquierdo" >
              <table>
                  <tr>
                      <td>

                          Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />

                      </td>
                  </tr>
                  <tr>
                      <td>  
                          <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" /> 
                      </td>
                      <td></td>
                      <td class="auto-style15">
                       
                          <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                       
                         <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Autorizado" ToolTip="Autorizado" Width="108px" />                
                   
                      </td>
                      <td>&nbsp;&nbsp;</td>
                      <td>
                           <asp:Button ID="btnQuitar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="No Autorizado" ToolTip="No Autorizado" Width="122px" />                
                   
                      </td>
                  </tr>
                  <asp:Label ID="Lmsg" runat="server"></asp:Label>
                  </table>
               <table>
            <tr>
                <td>


                    <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idprestamo" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="1110px">
            <Columns>
                  <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkRow" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="idprestamo" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
               
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="Empleado" />
                <asp:BoundField DataField="sucursal" HeaderText="Sucursal" SortExpression="sucursal"  HtmlEncode="False" DataFormatString = "{0:d}"/>
               <asp:BoundField DataField="jornada" HeaderText="Jornada" SortExpression="jornada" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" HtmlEncode="False" DataFormatString = "{0:d}" />
                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" SortExpression="observaciones" />
                <asp:CheckBoxField DataField="verificado" HeaderText="Verificado" />
                
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#3088b0" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
  
              </td>
            </tr>
         </table>
       </div>
    </div>
</asp:Content>
