<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="PanelIncidencia.aspx.vb" Inherits="_PanelIncidencia" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        #contenedor{
            overflow:hidden
        }
        #izquierdo{
             float:right;
        }

         #derecho{
            
             float:left;
        }
          
          .auto-style1 {
          height: 316px;
            width: 936px;
        }
          
          .auto-style2 {
          width: 172px;
      }
          
          .auto-style3 {
            width: 644px;
        }
          
          .auto-style4 {
            width: 172px;
            height: 22px;
        }
        .auto-style6 {
            width: 180px;
        }
          
          .auto-style7 {
            width: 146px;
        }
          
          </style>
      <div id="contenedor" class="auto-style1">
          <h3>Configuración de Incidencias</h3>
          <div id="izquierdo" class="auto-style3">
              <h5>Registro de Configuración de Incidencias</h5>
                <table>
                    <tr>
                        <td class="auto-style2">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Todo Abierto" />
                        </td>
                    </tr><tr>
                        <td class="auto-style2">
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Normal" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="Sucursal especifica" />
                        </td>
                        <td id="Suc" runat="server">
                            <uc1:wucsucursales ID="wucSucursales" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Actualizar" ToolTip="Actualizar" Width="108px" />                
                   
<asp:Label ID="Mens" runat="server" Width="196px"></asp:Label>
                   
                        </td>
                    </tr>
                </table>
         </div>

         <div id="derecho" class="auto-style6">
             <h5 class="auto-style7">Datos Guardados</h5>
             <table>
                    <tr>
                        <td class="auto-style2">
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="Todo Abierto" />
                        </td>
                    </tr><tr>
                        <td class="auto-style2">
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="Normal" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="Sucursal especifica" />
                        </td>
                    </tr>
                    <tr>
                        <td id="SucD" runat="server">
                            <uc1:wucsucursales ID="wucSucursales1" runat="server" />
                   
                        </td>
                    </tr>
                </table>
         </div>

      </div>
  
</asp:Content>

