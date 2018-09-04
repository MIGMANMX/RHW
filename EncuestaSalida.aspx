<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="EncuestaSalida.aspx.vb" Inherits="_EncuestaSalida" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucPuestos.ascx" tagname="wucPuestos" tagprefix="uc3" %>
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
      
        .auto-style2 {
          width: 1044px;
      }
      .auto-style3 {
            width: 241px;
        }
      
        .auto-style6 {
          width: 366px;
          height: 337px;
      }
                        
        .auto-style20 {
          width: 153px;
      }
      .auto-style23 {
          width: 151px;
      }
      .auto-style24 {
          width: 149px;
      }
      .auto-style25 {
          width: 140px;
      }
      .auto-style26 {
          width: 112px;
      }
      
        .auto-style28 {
          width: 671px;
      }
      
        .auto-style29 {
          width: 667px;
      }
      
        .auto-style30 {
          width: 207px;
      }
      .auto-style31 {
          width: 248px;
      }
      
        .auto-style33 {
        width: 663px;
    }
    .auto-style34 {
        width: 335px
    }
    .auto-style35 {
        width: 155px;
    }
    .auto-style36 {
        width: 670px;
    }
      
        .auto-style37 {
            width: 241px;
            height: 43px;
        }
        .auto-style38 {
            width: 155px;
            height: 43px;
        }
        .auto-style39 {
            width: 364px;
        }
        .auto-style40 {
            width: 241px;
            height: 37px;
        }
        .auto-style41 {
            width: 155px;
            height: 37px;
        }
      
        .auto-style42 {
            height: 22px;
        }
      
        </style>
    <div id="contenedor" class="auto-style2">
    <h3>Entrevista de Salida</h3>
    <div id="izquierdo" class="auto-style6">
        <table class="auto-style39">
            <tr>
                <td class="auto-style40" id="suc" runat="server">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                  <td class="auto-style41">Empleado:<br />
                      <uc2:wucempleados2 ID="wucEmpleados2" runat="server" />  
                  </td> 
            </tr>
            <tr>
                <td class="auto-style37">Fecha:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                 </td>
                <td class="auto-style38">               

                    <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Generar"  ToolTip="Buscar Registros" Width="90px" />

                    <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>

                    <asp:label ID="Lmsg0" runat="server" CssClass="error"></asp:label>

                    <asp:TextBox ID="grdSR" runat="server" Visible="false" Width="176px"></asp:TextBox>
                      <asp:TextBox ID="idEmpleadoTX" runat="server" Visible="False" Width="46px"></asp:TextBox>
                      <asp:TextBox ID="TxEmpleado" runat="server" Visible="False" Width="46px"></asp:TextBox>
                    </td>
            </tr>  
            <tr>
                <td class="auto-style3" ><asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
               <td class="auto-style35" >&nbsp;</td>
               
            </tr></table>
    </div> <!-- listaDatos -->


    <div id="derecho" class="auto-style36">
          Contesta con certeza estas preguntas, nos permitirá determinar los aspectos que nuestra empresa debe mejorar.<br />
        <hr />
          <table>Califica las siguientes preguntas como se te solicita:
             <tr>
                    <td class="auto-style25">
                        5 
                        <br />
                        EXCELENTE

                    </td>     
                    <td class="auto-style20">
                         4 
                         <br />
                         BUENO
                    </td>
                  <td class="auto-style23">
                        3 
                        <br />
                        REGULAR

                    </td>     
                    <td class="auto-style24">
                         2 
                         <br />
                         DEFICIENTE
                    </td>
                  <td class="auto-style26">
                         1 
                         <br />
                         NO EXISTE
                    </td>
            </tr>
             
              
            </table>
          ¿Cómo Calificarias?<br />
        <table class="auto-style28">
            <tr> 
                  <td>

                      1.- Carl&#39;s Jr como lugar de trabajo

                  </td>
                <td>

                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>

                </td>
              </tr>
            <tr> 
                  <td>

                      2.- Los procedimientos en las áreas de trabajo

                  </td>
                 <td>

                     <asp:DropDownList ID="DropDownList2" runat="server">
                         <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                         <asp:ListItem>4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                     </asp:DropDownList>

                </td>
              </tr>
            <tr> 
                  <td>

                      3.- Las condiciones de su lugar de trabajo</td>
                 <td>

                     <asp:DropDownList ID="DropDownList3" runat="server">
                         <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                         <asp:ListItem>4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                     </asp:DropDownList>

                </td>
              </tr>
            <tr> 
                  <td>

                      4.- La comunicación con sus superiores cuando tenias una queja</td>
                 <td>

                     <asp:DropDownList ID="DropDownList4" runat="server">
                         <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                         <asp:ListItem>4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                     </asp:DropDownList>

                </td>
              </tr>
            <tr> 
                  <td class="auto-style42">

                      5.- La comunicación dada cuando habia cambios en el trabajo que desempeñaba</td>
                 <td class="auto-style42">

                     <asp:DropDownList ID="DropDownList5" runat="server">
                         <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                         <asp:ListItem>4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                     </asp:DropDownList>

                </td>
              </tr>
            <tr> 
                  <td>

                      6.- El gerente le explico, cuales eran los objetivos de su trabajo y la manera en que los mediria</td>
                 <td>

                     <asp:DropDownList ID="DropDownList6" runat="server">
                         <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                         <asp:ListItem>4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                     </asp:DropDownList>

                </td>
                </tr>
                <tr> 
                  <td>

                      7.- La capacitación que se le dío en sus área</td>
                     <td>

                         <asp:DropDownList ID="DropDownList7" runat="server">
                             <asp:ListItem>1</asp:ListItem>
                             <asp:ListItem>2</asp:ListItem>
                             <asp:ListItem>3</asp:ListItem>
                             <asp:ListItem>4</asp:ListItem>
                             <asp:ListItem>5</asp:ListItem>
                         </asp:DropDownList>

                </td>
              </tr>
                <tr> 
                  <td>

                      8.- El respeto que le inspiro el gerente de la sucursal</td>
                     <td>

                         <asp:DropDownList ID="DropDownList8" runat="server">
                             <asp:ListItem>1</asp:ListItem>
                             <asp:ListItem>2</asp:ListItem>
                             <asp:ListItem>3</asp:ListItem>
                             <asp:ListItem>4</asp:ListItem>
                             <asp:ListItem>5</asp:ListItem>
                         </asp:DropDownList>

                </td>
              </tr>
            <tr> 
                  <td>

                      9.- Su ambiente de trabajo

                  </td>
                 <td>

                     <asp:DropDownList ID="DropDownList9" runat="server">
                         <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                         <asp:ListItem>4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                     </asp:DropDownList>

                </td>
              </tr>
        </table>
        <hr />
        <table class="auto-style29">A continuación anota los motivos para separte de tu trabajo
                <tr>
                    <td class="auto-style30">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Sueldo" />
                    </td>
                    <td class="auto-style31">
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="Relación con gerentes" />
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="Horario" />
                    </td>
                 </tr>
            <tr>
                    <td class="auto-style30">
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Prestaciones" />
                    </td>
                    <td class="auto-style31">
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="Otro trabajo" />
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="Volumen de trabajo" />
                    </td>
                 </tr>
            <tr>
                    <td class="auto-style30">
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Superación" />
                    </td>
                    <td class="auto-style31">
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="Cambio de residencia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox11" runat="server" Text="Malas relaciones" />
                    </td>
                 </tr>
            <tr>
                    <td class="auto-style30">
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="Motivación" />
                    </td>
                    <td class="auto-style31">
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="Mayor seguridad" />
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox12" runat="server" Text="Cambio de Actividad" />
                    </td>
                 </tr>
             </table>
        <hr />
        <table class="auto-style33">
            <tr>
                 <td class="auto-style34">¿Qué es lo más positivo de Carl&#39;s Jr?</td> 
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="col-xs-offset-0" TextMode="MultiLine" Width="369px"></asp:TextBox>
                 </td>     
            </tr>
            <tr>
                  <td class="auto-style34">¿Qué es lo más te gustaria que cambiaria Carl&#39;s Jr y porque?</td> 
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="369px"></asp:TextBox>
                  </td>      
            </tr>
            <tr>
                  <td class="auto-style34">¿Te gustaria volver a trabajar en Carl&#39;s Jr?</td> 
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Width="368px"></asp:TextBox>
                  </td>     
            </tr>
            <tr>
                 <td class="auto-style34">Si te vas a otra empresa, ¿Puedes darnos el nombre de la misma?</td> 
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Width="369px"></asp:TextBox>
                 </td>       
            </tr>
            <tr>
                  <td class="auto-style34">Comentarios:</td> 
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" Width="369px"></asp:TextBox>
                  </td>   
            </tr>
            
        </table>
    </div> <!-- registroDatos -->
</div>
         <rsweb:ReportViewer ID="Repo" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1043px">
             <LocalReport ReportPath="ReportEncuestaSalida.rdlc">
                 <DataSources>
                     <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                 </DataSources>
             </LocalReport>
         </rsweb:ReportViewer>
         <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="nomRHDataSetTableAdapters.vm_EncuestaSalidaTableAdapter">
             <SelectParameters>
                 <asp:ControlParameter ControlID="idEmpleadoTX" Name="idempleado" PropertyName="Text" Type="Int32" />
             </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

