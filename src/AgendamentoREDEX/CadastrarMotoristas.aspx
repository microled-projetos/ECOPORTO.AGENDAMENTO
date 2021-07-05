﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CadastrarMotoristas.aspx.vb" Inherits="AgendamentoREDEX.CadastrarMotoristas" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <center>

    <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="750px" bgcolor="#CCCCCC">
        <tr>
            <td colspan="2" height="10"></td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="font-family: verdana; font-size: 16px;
                font-weight: bold; color: #FFFFFF" bgcolor="#076703">
                Cadastrar Motoristas
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                Transportadora
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtIDTransportadora" runat="server" MaxLength="8" Width="20px" Font-Names="Verdana"
                    Font-Size="11px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtTransportadora" runat="server" MaxLength="8" Width="500px" BackColor="White"
                    BorderStyle="None" Enabled="False" Font-Names="Verdana" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                CNH
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtCNH" runat="server" MaxLength="11" Width="400px" Font-Names="Verdana"
                    Font-Size="14" CssClass="Numeric" AutoPostBack="True"></asp:TextBox>
                &nbsp;<asp:Button ID="btConsultarCNH" runat="server" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="14px" Text="Consultar" CausesValidation="False" />
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCNH"
                    ErrorMessage="RequiredFieldValidator" Font-Names="Verdana" ForeColor="Red" 
                    Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                Validade CNH
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtValidadeCNH" runat="server" MaxLength="10" Width="200px" Font-Names="Verdana"
                    Font-Size="14px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtValidadeCNH_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtValidadeCNH">
                </asp:MaskedEditExtender>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValidadeCNH"
                    ErrorMessage="RequiredFieldValidator" Font-Names="Verdana" ForeColor="Red" 
                    Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtValidadeCNH" Display="Dynamic" 
                    ErrorMessage="RegularExpressionValidator" ForeColor="Red" 
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">* Data Inválida</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                Nome
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Width="400px" Font-Names="Verdana"
                    Font-Size="14px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNome"
                    ErrorMessage="RequiredFieldValidator" Font-Names="Verdana" ForeColor="Red" 
                    Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                RG
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtRG" runat="server" MaxLength="12" Width="400px" Font-Names="Verdana"
                    Font-Size="14px" CssClass="Numeric"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRG"
                    ErrorMessage="RequiredFieldValidator" Font-Names="Verdana" ForeColor="Red" 
                    Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                CPF
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtCPF" runat="server" MaxLength="10" Width="400px" Font-Names="Verdana"
                    Font-Size="14px" Height="19px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtCPF_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="999,999,999-99" TargetControlID="txtCPF">
                </asp:MaskedEditExtender>
                &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="txtCPF" ErrorMessage="CompareValidator" ForeColor="Red" 
                    Operator="NotEqual" ValueToCompare="___.___.___-__">* Campo Obrigatório</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                Celular
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtCelular" runat="server" MaxLength="15" Width="400px" Font-Names="Verdana"
                    Font-Size="14px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtCelular_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="(99) 9999-99999" TargetControlID="txtCelular">
                </asp:MaskedEditExtender>
            &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="txtCelular" ErrorMessage="CompareValidator" ForeColor="Red" 
                    Operator="NotEqual" ValueToCompare="(__) ____-_____">* Campo Obrigatório</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                Nextel ID
            </td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtNextel" runat="server" MaxLength="10" Width="400px" Font-Names="Verdana"
                    Font-Size="14px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNextel"
                    ErrorMessage="RequiredFieldValidator" Font-Names="Verdana" ForeColor="Red" 
                    Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 14px" width="100px" bgcolor="White">
                Número MOP</td>
            <td style="font-family: verdana; font-size: 14px" bgcolor="White">
                <asp:TextBox ID="txtNumeroMOP" runat="server" MaxLength="10" Width="400px" Font-Names="Verdana"
                    Font-Size="16px" CssClass="Numeric"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="font-family: verdana; font-size: 14px" bgcolor="White"
                height="30px">
                <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="14px" Text="Retornar" Width="80px"
                    CausesValidation="False" />
                <asp:Button ID="btLimpar" runat="server" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="14px" Text="Limpar" 
                    Width="80px" CausesValidation="False" />
                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="14px" Text="Salvar" Width="80px" />
                <asp:Panel ID="PanelMsg" runat="server" Visible="false">
                    <br />
                    <asp:Label ID="lblMsgOK" runat="server" ForeColor="Green" Font-Bold="False"></asp:Label>
                    <asp:Label ID="lblMsgErro" runat="server" ForeColor="Red" Font-Bold="False"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
    </center>
</asp:Content>
