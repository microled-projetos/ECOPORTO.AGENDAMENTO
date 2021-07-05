﻿<%@ Page Title="" Language="vb" enableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ConsultarVeiculos.aspx.vb" Inherits="AgendamentoREDEX.ConsultarVeiculos" %>
        <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div id="Grid-Scroll" style="overflow: auto; margin-left: 20px; margin-right: 20px; height:500px; margin-top:10px;">
        <asp:Panel ID="PanelBarra" runat="server">
            <table align="left" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="padding-right: 20px;" height="22px">
                        <asp:Button ID="btNovo" runat="server" Font-Names="Verdana" Font-Size="14px" Text="Novo"
                            Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        <asp:Button ID="btPesquisar" runat="server" Font-Names="Verdana" Font-Size="14px"
                            Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
            <br />
        
        <center>
        <asp:Panel ID="PanelPesquisa" runat="server" Visible="False">
            <table id="tbcad" align="center" bgcolor="#999999" border="0" cellpadding="1" cellspacing="0"
                style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana;
                font-size: 14px;" width="1000px">
                <tr>
                    <td align="center" colspan="3" style="font-family: verdana; font-size: 18px; font-weight: bold;
                        color: #FFFFFF; border-right-style: dotted; border-bottom-style: dotted; border-right-width: 1px;
                        border-bottom-width: 1px; border-right-color: #999999; border-bottom-color: #999999;"
                        bgcolor="#076703">
                        Pesquisar Veículos
                    </td>
                </tr>
                <tr>
                    <td width="70px" align="left" bgcolor="White" style="border-right-style: dotted;
                        border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px;
                        border-right-color: #999999; border-bottom-color: #999999">
                        Cavalo:
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-bottom-style: dotted;
                        border-right-width: 1px; border-bottom-width: 1px; border-right-color: #999999;
                        border-bottom-color: #999999" width="100px">
                        <asp:DropDownList ID="cbFiltro1" runat="server" Font-Names="Verdana" Font-Size="14px" Width="500px"
                            Enabled="False">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="150px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999">
                        <asp:TextBox ID="txtPlacaCavalo" runat="server" Font-Names="Verdana" Font-Size="11px"
                             MaxLength="8" Width="500"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtPlacaCavalo_MaskedEditExtender" runat="server" 
                            ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            Mask="AAA-9999" TargetControlID="txtPlacaCavalo">
                        </asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="70px">
                        Carreta:
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="100px">
                        <asp:DropDownList ID="cbFiltro2" runat="server" Font-Names="Verdana" Font-Size="14px" Width="500px"
                            Enabled="False">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999" width="150px">
                        <asp:TextBox ID="txtPlacaCarreta" runat="server" Font-Names="Verdana" 
                            Font-Size="14px" Width="500px" MaxLength="8"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtPlacaCarreta_MaskedEditExtender" runat="server" 
                            ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            Mask="AAA-9999" TargetControlID="txtPlacaCarreta">
                        </asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="70px">
                        Modelo:
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="100px">
                        <asp:DropDownList ID="cbFiltro3" runat="server" Font-Names="Verdana" Font-Size="14px" Width="500px">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999" width="150px">
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="Numeric" Font-Names="Verdana"
                            Font-Size="14px" Style="margin-left: 0px" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="70px">
                        Cor:
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="100px">
                        <asp:DropDownList ID="cbFiltro4" runat="server" Font-Names="Verdana" Font-Size="14px" Width="500px"> 
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999" width="150px">
                        <asp:TextBox ID="txtCor" runat="server" CssClass="AlphaNumeric" Font-Names="Verdana"
                            Font-Size="14px" Style="margin-left: 0px" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="70px">
                        Chassi
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="100px">
                        <asp:DropDownList ID="cbFiltro5" runat="server" Font-Names="Verdana" Width="500">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999" width="150px">
                        <asp:TextBox ID="txtChassi" runat="server" CssClass="AlphaNumeric" Font-Names="Verdana"
                            font-size= "14px" Style="margin-left: 0px" Width="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="70px">
                        Tara
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="100px">
                        <asp:DropDownList ID="cbFiltro6" runat="server" Font-Names="Verdana" font-size= "14px" Width="500">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999" width="150px">
                        <asp:TextBox ID="txtTara" runat="server" CssClass="AlphaNumeric" Font-Names="Verdana"
                            font-size= "14px" Style="margin-left: 0px" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="70px">
                        Tipo
                    </td>
                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999;" width="100px">
                        <asp:DropDownList ID="cbFiltro7" runat="server" Enabled="False" Font-Names="Verdana" Width="500"
                            font-size= "14px">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999">
                        <asp:DropDownList ID="cbTipo" runat="server" Font-Names="Verdana" font-size= "14px"
                            Width="100%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="White" style="border-right: 1px dotted #999999;" colspan="3">
                        <asp:Button ID="btVoltar" runat="server" BackColor="WhiteSmoke" BorderColor="#999999"
                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                            font-size= "14px" Text="Voltar"
                            Width="75px" />
                        <asp:Button ID="btLimpar" runat="server" BackColor="WhiteSmoke" 
                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Names="Verdana" font-size= "14px" Text="Limpar" Width="75px" />
                        <asp:Button ID="btFiltrar" runat="server" BackColor="WhiteSmoke" BorderColor="#999999"
                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" font-size= "14px" Text="Pesquisar"
                            Width="75px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </center>
            <br />
        <asp:GridView ID="DgVeiculos" runat="server" AutoGenerateColumns="False" CellPadding="1"
            ForeColor="#333333" GridLines="Both" CellSpacing="1" Width="100%" EmptyDataText="Nenhum registro encontrado."
            ShowHeaderWhenEmpty="True" BackColor="#999999" Font-Names="Verdana" Font-Size="14px" AllowPaging="True" PageSize="20"
            DataKeyNames="AUTONUM,PLACA_CAVALO,PLACA_CARRETA,ID_TRANSPORTADORA">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="EDIT" Text="Editar" ImageUrl="~/imagens/editar.gif">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:ButtonField>


                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:TemplateField>


                <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PLACA CAVALO" DataField="PLACA_CAVALO">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PLACA CARRETA" DataField="PLACA_CARRETA">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="MODELO" HeaderText="MODELO">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="COR" HeaderText="COR">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TRANSPORTADORA" DataField="TRANSPORTADORA" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CHASSI" DataField="CHASSI">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TARA" DataField="TARA">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="RENAVAM" DataField="RENAVAM">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TIPO" DataField="TIPO">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ID_TRANSPORTADORA" HeaderText="ID_TRANSPORTADORA" Visible="False" />
                <asp:BoundField HeaderText="TRANSPORTADORA" DataField="TRANSPORTADORA">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataRowStyle BackColor="#E9E9E9" HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#076703"  />
            <PagerStyle CssClass="CorPadrao" ForeColor="White" Font-Size="12px" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
    
</asp:Content>
