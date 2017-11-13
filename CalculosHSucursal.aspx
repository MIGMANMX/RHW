<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="CalculosHSucursal.aspx.vb" Inherits="CalculosHSucursal" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>

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
          .auto-style13 {
             height: 967px;
             width: 967px;
         }
          </style>
      <div id="contenedor" class="auto-style13">
       <div id="izquierdo">
        <table>
          <tr>
            <td  id="suc" runat="server">Sucursal:<br />
                <uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
            <td><br />
               <asp:TextBox ID="idpartidas_jornadaT" runat="server" Visible="False" Width="37px"></asp:TextBox>
               <asp:TextBox ID="TIDPJ" runat="server" Visible="False" Width="37px"></asp:TextBox>
           </td>       
         </tr>
       </table>
           <table>
               <TR>
                  <asp:GridView ID="GridView1" runat="server" 
                    DataKeyNames ="chec" AutoGenerateColumns="False" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" Width="537px" Height="303px" AllowPaging="True"  PageSize="30">
                    <Columns>
                        <asp:BoundField DataField="idchequeo" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
                        <ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                        </asp:BoundField>

                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                        <asp:BoundField DataField="chec" HeaderText="Fecha y Hora" SortExpression="chec" />
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo" />
                
                   </Columns>
                    <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
                    <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#fbfbfb" />
                    <SelectedRowStyle BackColor="#fffcbf" />
                    <FooterStyle BackColor="#FF9933" Font-Size="1" Height="1" />
                    <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center"  />
                </asp:GridView>
              </TR>
           </table>
      </div>  
      <div id="derecho">
      </div>
    </div>
</asp:Content>
