<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="CalculoHoras.aspx.vb" Inherits="CalculoHoras" %>
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
        .auto-style1 {
            width: 480px;
        }
        .auto-style2 {
            width: 512px;
            height: 163px;
        }
        .auto-style3 {
            width: 226px;
        }
        .auto-style4 {
            width: 480px;
            height: 56px;
        }
        .auto-style5 {
            height: 56px;
        }
        .auto-style6 {
        height: 117px;
    }
    .auto-style7 {
        width: 226px;
        height: 50px;
    }
    .auto-style8 {
        width: 480px;
        height: 50px;
    }
    .auto-style9 {
        width: 1046px;
        height: 428px;
        margin-bottom: 0px;
    }
    .auto-style10 {
        width: 387px;
    }
        </style>
    <div id="contenedor" class="auto-style9">
    <h3>Calculo de Horas</h3>
    <div id="izquierdo" class="auto-style10">
        <table>
            <tr>
                <td>Sucursal:</td>
                <td><uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="separa10"></td>
                <td>&nbsp;</td>
                <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" AutoPostBack="true" Visible="False" /></td>
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
                <asp:BoundField DataField="clave_att" HeaderText="Clave" SortExpression="clave_att" />              
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


    <div id="derecho" class="auto-style2">
        <table>       
            <tr>
                <td class="auto-style4">Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" /></td>
                <td class="auto-style4">Fecha Fin:<br />
                    <asp:TextBox ID="TxFechaFin" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" /></td>
                <td class="auto-style5">

                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Buscar"  ToolTip="Buscar Registros" Enabled="false" Width="108px" />               
                    <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>

                </td>
            </tr>  
            <tr>
                    <td class="auto-style6">
                          <asp:GridView ID="GridView2" runat="server" 
            DataKeyNames ="idempleado" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="381px">
            <Columns>
                <asp:BoundField DataField="idempleado" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png"></asp:ButtonField>
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="empleado" />
                <asp:BoundField DataField="puesto" HeaderText="Puesto" SortExpression="puesto" />              
                <asp:BoundField DataField="clave_att" HeaderText="Clave" SortExpression="clave_att" />              
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#3088b0" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
                    </td>
            </tr>
            <tr>
                <td class="auto-style3">Horas trabajadas:<br />
                    <asp:TextBox ID="TxHorasTrabajadas" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" />&nbsp;</td>
                 <td class="auto-style1" style="width: 169px">Horas Extras:<br />
                    <asp:TextBox ID="TxHorasExtras" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" />&nbsp;</td>
                 <td>
                     
                    <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Generar"  ToolTip="Generar" Enabled="false" Width="108px" />               
                    
                 </td>
            </tr>
            <tr>
                <td class="auto-style7">Descanso:<br />
                    <asp:TextBox ID="inicio" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" />&nbsp;</td>
                <td class="auto-style8">Tiempo de Break:<br />
                    <asp:TextBox ID="fin" runat="server" MaxLength="40" Width="153px" CssClass="txtCaptura" /></td>
                
            </tr>
            </table>
    </div>
        
    
   </div>
</asp:Content>

