﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Prenomina.aspx.vb" Inherits="Prenomina" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
              margin-right: 237px;
          }
        </style>
    <div id="contenedor" class="auto-style2">
        <h3>Prenomina</h3>
            <div id="izquierdo">
                 <table class="auto-style3">
           
            <tr>
                    <td style="width: 234px">
                       
                        Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    </td>
                 <td>
                    
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
               <tr>
                <td class="auto-style1" style="width: 234px; height: 68px">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />
                    <br />
                </td>
                <td style="height: 68px">  
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Buscar"  ToolTip="Buscar Registros" Width="120px" />          
                </td>
                   <td>
                       
                        <asp:Label ID="Mens" runat="server" Width="259px"></asp:Label>
                        <br />
                        <asp:TextBox ID="grdSR" runat="server" Visible="false" Width="176px" Height="23px"></asp:TextBox>
                    <asp:TextBox ID="tSuc" runat="server" Visible="False"></asp:TextBox>
                   </td>
                </tr>
        </table>
                <table>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="Repo" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="578px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1056px" CssClass="auto-style1">
                                <LocalReport ReportPath="ReportPrenomina.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="nomRHDataSetTableAdapters.vm_PrenominaTableAdapter"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    
                </table>
            </div>
            <div id="derecho">
            </div>
   </div>
</asp:Content>

