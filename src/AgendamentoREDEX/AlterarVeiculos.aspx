<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AlterarVeiculos.aspx.vb" Inherits="AgendamentoREDEX.AlterarVeiculos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<center>
    <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="450px" 
        bgcolor="#CCCCCC">
        <tr>
            <td align="center" class="CorPadrao" colspan="2" 
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" bgcolor="#B3C63C">
                Alterar Veículos</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Transportadora</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtID" runat="server" MaxLength="8" 
                    Font-Names="Verdana" Font-Size="11px" Visible="False" Width="20px"></asp:TextBox>
                <asp:TextBox ID="txtIDTransportadora" runat="server" MaxLength="8" 
                    Font-Names="Verdana" Font-Size="11px" Visible="False" Width="20px"></asp:TextBox>
                <asp:TextBox ID="txtTransportadora" runat="server" MaxLength="8" Width="300px" 
                    BackColor="White" BorderStyle="None" Enabled="False" Font-Names="Verdana" 
                    Font-Size="11px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Placa-Cavalo</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtPlacaCavalo" runat="server" MaxLength="8" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Placa" Enabled="False"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPlacaCavalo" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Placa-Carreta</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtPlacaCarreta" runat="server" MaxLength="8" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Placa" Enabled="False"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Tara Veículo</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtTaraVeiculo" runat="server" MaxLength="6" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Modelo</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtModelo" runat="server" MaxLength="25" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="AlphaNumeric"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Cor</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtCor" runat="server" MaxLength="20" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="AlphaNumeric"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Chassi</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtChassi" runat="server" MaxLength="17" Width="200px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="AlphaNumeric"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Renavam</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:TextBox ID="txtRenavam" runat="server" MaxLength="11" Width="200px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtRenavam" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White" align="left">
                Tipo de Carreta</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:DropDownList ID="cbTipoCarreta" runat="server" Width="205px" 
                    Font-Names="Verdana" Font-Size="11px">
                </asp:DropDownList>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="cbTipoCarreta" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="font-family: verdana; font-size: 11px" 
                bgcolor="White" height="30px">
                <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Retornar" Width="80px" CausesValidation="False" />
                <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Excluir" Width="80px" CausesValidation="False" 
                    onclientclick="return confirm('Confirma a exclusão do registro?');" />
                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Salvar" Width="80px" />

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

