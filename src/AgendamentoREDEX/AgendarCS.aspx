<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AgendarCS.aspx.vb" Inherits="AgendamentoREDEX.AgendarCS" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(function () {
            var activeIndex = parseInt($('#<%=AccordionIndex.ClientID %>').val());

            $("#accordion").accordion({
                heightStyle: 'panel',
                navigation: true,
                active: activeIndex,
                change: function (event, ui) {
                    var index = $(this).children('h3').index(ui.newHeader);
                    $('#<%=AccordionIndex.ClientID %>').val(index);
                }                
            });
            
        });


        function novaReserva(id, prot) {
            var txt;
            var r = confirm("Agendamento Concluído com Sucesso! Deseja incluir mais reservas no agendamento?");
            if (r == true) {
                document.location.href = 'AgendarCS.aspx?id=' + id + ' &more=1&protocolo=' + prot + '';
            } else {
                document.location.href = 'ConsultarAgendamentosCargaSolta.aspx';
            }
        }

        $(document).ready(function () {
            var id = "0";
            var validaUpload = $('#MainContent_txtDANFE').val();

            if (id == "") {
                id = "0";
            }
            id = "<%= Request.QueryString("id") %>";

            //
            //if (validaUpload != "") {
            //    if (id == 0) {
            //        $('#MainContent_btnUploadNota').attr('disabled', false);
            //        $('#MainContent_txtArquivoDanfe').attr('disabled', false);
            //    }
            //    else {
            //        $('#MainContent_btnUploadNota').attr('disabled', true);
            //        $('#MainContent_txtArquivoDanfe').attr('disabled', true);
            //    }
                
            //}

            
            
            var msg = $("#<%=lblSemSaldo.ClientID%>");
            if (msg.textContent != null || msg.textContent != undefined) {
                $("#accordion").css('display', 'block');
                $("#<%=btnSalvar.ClientID%>").css('display', 'block');
                $("#<%=btnCancelar.ClientID%>").css('display', 'block');
            }

            $("#MainContent_txtEmailResponsavel").change(function () {
                var email = $(this).val();
                //executa funcao para validacao de email 

                var emailParts = email.split('@');
                var text = 'Por favor insira um email válido';

                //at least one @, catches error
                if (emailParts[1] == null || emailParts[1] == "" || emailParts[1] == undefined) {

                    alert(text);
                    $(this).val("");

                } else {

                    //split domain, subdomain and tld if existent
                    var emailDomainParts = emailParts[1].split('.');

                    //at least one . (dot), catches error
                    if (emailDomainParts[1] == null || emailDomainParts[1] == "" || emailDomainParts[1] == undefined) {

                        //$('#feedback').text(text);
                        alert(text);
                        $(this).val("");

                    } else {

                        //more than 2 . (dots) in emailParts[1]
                        if (!emailDomainParts[3] == null || !emailDomainParts[3] == "" || !emailDomainParts[3] == undefined) {

                            //$('#feedback').text(text);
                            alert(text);
                            $(this).val("");

                        } else {

                            //email user
                            if (/[^a-z0-9!#$%&'*+-/=?^_`{|}~]/i.test(emailParts[0])) {

                                //$('#feedback').text(text);
                                alert(text);
                                $(this).val("");

                            } else {

                                //double @
                                if (!emailParts[2] == null || !emailParts[2] == "" || !emailParts[2] == undefined) {

                                    //$('#feedback').text(text);
                                    alert(text);
                                    $(this).val("");

                                } else {

                                    //domain
                                    if (/[^a-z0-9-]/i.test(emailDomainParts[0])) {

                                        //$('#feedback').text(text);
                                        alert(text);
                                        $(this).val("");

                                    } else {

                                        //check for subdomain
                                        if (emailDomainParts[2] == null || emailDomainParts[2] == "" || emailDomainParts[2] == undefined) {

                                            //TLD
                                            if (/[^a-z]/i.test(emailDomainParts[1])) {

                                                //$('#feedback').text(text);
                                                alert(text);
                                                $(this).val("");

                                            } else {

                                                //$('#feedback').text(text);
                                                //alert("Email válido");

                                            }

                                        } else {

                                            //subdomain
                                            if (/[^a-z0-9-]/i.test(emailDomainParts[1])) {

                                                //$('#feedback').text(text);
                                                alert(text);
                                                $(this).val("");

                                            } else {

                                                //TLD
                                                if (/[^a-z]/i.test(emailDomainParts[2])) {

                                                    //$('#feedback').text(text);
                                                    alert(text);
                                                    $(this).val("");

                                                } else {

                                                    //$('#feedback').text(text);
                                                    //alert("Email válido");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //alert(email);
            });
            
        });
        $(document).ready(function () {
            $('#MainContent_TextBox1').hide();
            $('#MainContent_txtCNPJResponsavel').keyup(function () {       
                var strDados = "";
                var len = $(this).val().length;                
                if (len >= 11) {
                    var cnpj = $(this).val();                    
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "AgendarCS.aspx/GetCNPJ",
                        data: "{'cnpjID':'" + cnpj + "'}",
                        beforeSend: function () {

                            $('#MainContent_TextBox1').show();
                            $('#MainContent_TextBox1').empty();

                            $('#MainContent_TextBox1').append('<center><img src="imagens/loading.gif" border="0" width="60" height="60" /></center>');
                            

                        },
                        success: function (Dados) {                            
                            
                            $('#MainContent_TextBox1').empty();                            
                            $.each(Dados.d, function (i, item) {
                                var autonum = item.AUTONUM;
                                var cnpj = item.CNPJ;
                                var razaoSocial = item.RAZAO;     
                                var flagBloqueia = item.FLAG_BLOQUEIO_AGENDAMENTO;
                                
                                strDados = '<a style="font-size: 12px;" href="#" data-autonum=' + autonum + ' data-razao=' + razaoSocial + ' data-cnpj=' + cnpj + ' data-bloqueia =' + flagBloqueia +' name="link">' + razaoSocial + " - " + cnpj + '</a><br /><br />';
                                $('#MainContent_TextBox1').append(strDados);                              
                                $('[name="link"]').click(function () {
                                    //e.preventDefault();
                                    var cnpjL = $(this).attr('data-cnpj');
                                    var autoL = $(this).attr('data-autonum');
                                    var razaoL = $(this).attr('data-razao');
                                    var flagBloqueiaL = $(this).attr('data-bloqueia');
                                    
                                    if (flagBloqueiaL == 1) {
                                        $('#MainContent_btnSalvar').hide();
                                        toastr.success('Prezado cliente.<br/> <br />Seu agendamento não pode ser concluído, para mais informações favor entrar em contato com nossa Central de Relacionamento ao Cliente, através do e-mail crc@ecoportosantos.com.br ou telefone 13 3213-0010.');                                        
                                    }
                                    else {
                                        $('#MainContent_btnSalvar').show();
                                        $('#MainContent_txtCNPJResponsavel').val("");
                                        $('#MainContent_txtCNPJResponsavel').val(razaoL + " - " + cnpjL);
                                        $('#MainContent_txtCNPJResponsavel').attr('data-id', autoL);
                                        $('#MainContent_txtCNPJResponsavel').attr('data-cnpj', cnpjL);
                                        $('#MainContent_TextBox1').empty();
                                        $('#MainContent_TextBox1').hide();
                                        $('#MainContent_hiddenText').attr('value', autoL);
                                        $('#MainContent_hiddenTextCNPJ').attr('value', cnpjL);

                                    }
                            }
                            );
                                getVericaFlagBloqueiaAgendamento(cnpj);
                            });                        
                            
                        },
                        error: function (xhr, thrownError) {
                            alert(xhr.status + " - " + thrownError);
                        }
                    });
                }      
            });            
        });        
        
    </script>
    <center>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

       
        <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
            bgcolor="#076703" width="1000px">
            <tr>
                <td colspan="2" height="10">
                </td>
                
            </tr>
            <tr>
                <td colspan="2" style="border-style: solid; border-width: 1px; border-color: Black;
                    color: White; font-family: tahoma; font-size: 18px; font-weight: bold;" bgcolor="#076703">
                    <i class="fa fa-search">                        
                    </i>
                    Consulta de Reserva
                </td>
                <td style="padding: 0px; margin: 0px; border-color: Black; color: White; font-family: tahoma;
                    font-size: 18px; font-weight: bold; border-left-style: solid; border-left-width: 1px;
                    padding-left: 4px;" align="left" rowspan="2" colspan="2">
                    <asp:Button ID="btnSalvar" runat="server" Height="44px" Text="Salvar" Width="78px"
                        CssClass="Botao" Visible="false" />
                    <asp:Button ID="btnCancelar" runat="server" Height="44px" Text="Excluir" Width="78px"
                        CssClass="Botao" OnClientClick="return confirm('Confirma a exclusão do Agendamento?');"
                        Visible="false" />
                    <asp:Button ID="btnSair" runat="server" Height="44px" Text="Sair" Width="78px" CssClass="Botao" />
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                    Informe a Reserva:
                </td>
                <td style="border-style: solid; border-width: 1px; width: 406px;">
                    <asp:TextBox ID="txtReserva" runat="server" AutoPostBack="True" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left"><asp:Label ID="lblSemSaldo" runat="server" Text="Todos os itens dessa reserva estão com agendamento válido realizado." ForeColor="Red" Visible="false"></asp:Label></td>
            </tr>
        </table>
        <br />
        <div id="accordion" style="width: 1000px; text-align: left; ">
            <h3 style="font-size: 16px;" data-accordion="1">
                1. Informações da Reserva</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 16px; color: White;" bgcolor="#076703">
                            Informações da Reserva
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Navio:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblNavio" runat="server" Font-Size="14px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Viagem:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:Label ID="lblViagem" runat="server" Font-Size="14px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Exportador:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblExportador" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Dead Line:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:Label ID="lblDeadLine" runat="server" Font-Size="13px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 16px; color: White;" bgcolor="#076703">
                            Informações da Carga
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Tipo:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTipo" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Total:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:Label ID="lblTotal" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Status:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblStatus" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            M3:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:Label ID="lblM3" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Utilizados:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblUtilizados" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Peso Bruto:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:Label ID="lblPesoBruto" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Disponíveis:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblDisponiveis" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Volumes:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:Label ID="lblVolumes" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <h3 style="font-size: 16px;" data-accordion="3">
                2. Motorista / Veículo</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 16px; color: White;" bgcolor="#076703">
                            Motorista / Veículo
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Nome Motorista / CNH:
                        </td>
                        <td width="350px" style="border-style: solid; border-width: 1px">
                            <asp:TextBox ID="txtMotorista" runat="server" Width="99%" AutoPostBack="True" Font-Size="11px"></asp:TextBox>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Cavalo:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:DropDownList ID="cbCavalo" runat="server" Width="100px" DataTextField="PLACA_CAVALO"
                                DataValueField="PLACA_CAVALO" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            Transportadora:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTransportadora" runat="server"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Carreta:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:DropDownList ID="cbCarreta" runat="server" Width="100px" DataTextField="PLACA_CARRETA"
                                DataValueField="PLACA_CARRETA" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <h3 style="font-size: 16px;" data-accordion="4">
                3. Informações das Notas Fiscais</h3>
            <div>
                <table style="font-family: tahoma; font-size: 14px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="msgErroNF" runat="server" ForeColor="Red" Visible="false" Font-Size="14px" Font-Bold="true"
                                Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMsgSalvar" runat="server" BackColor="#FFFACD" CssClass="MsgSalvar"
                                Text="Atenção: O Agendamento ainda não foi salvo. Para vincular as Notas Fiscais, clique no botão Salvar."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 16px; color: White;" bgcolor="#076703">
                            Notas Fiscais
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-style: solid; border-width: 1px; font-size:14px;">
                            <table cellpadding="2" cellspacing="2" width="100%">
                                 <tr>
                                    <td colspan="6">Arquivo Xml Danfe</td>
                                </tr>
                                 <tr>
                                    <td colspan="6">
                                        <asp:FileUpload ID="txtArquivoDanfe" runat="server"></asp:FileUpload>
                                        <asp:Button ID="btnUploadNota" runat="server" Text="Upload" />
                                        <asp:TextBox ID="txtXml" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                    </td>                                    
                                </tr>
                                 <tr>
                                    <td colspan="6">
                                        <asp:Label ID="msgSucessoDanfe" runat="server" ForeColor="#00CC66" Visible="False" Font-Size="14px" Font-Bold="True"></asp:Label>
                                     </td>                                    
                                </tr>
                            
                                  <tr>
                                    <td colspan="6">
                                        DANFE:
                                    
                                    </td>
                                </tr>

                                 <tr>
                                    <td colspan="6">
                                        <asp:TextBox ID="txtDANFE" runat="server" Width="99%" Enabled="False" MaxLength="44"></asp:TextBox>
                                    
                                    </td>
                                </tr>

                                <tr bgcolor="White">
                                    
                                    <td >
                                        Número:
                                    </td>
                                    <td>
                                        Série:
                                    </td>
                                    <td colspan="4">
                                        Emissor:
                                    </td>
                                </tr>
                               
                               
                                <tr bgcolor="White">
                                    
                                    <td width="80px" ">
                                        <asp:TextBox ID="txtNumero" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td width="80px">
                                        <asp:TextBox ID="txtSerie" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtEmissor" runat="server" Width="98%" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td>
                                        Emissão:
                                    </td>
                                    <td>
                                        Qtde:
                                    </td>
                                    <td>
                                        Valor:
                                    </td>
                                    <td>
                                        Peso Bruto:
                                    </td>
                                    <td>
                                        Metragem Cúbica:
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:HiddenField ID="xCNPJ" runat="server" />
                                        <asp:HiddenField ID="xNome" runat="server" />
                                        <asp:HiddenField ID="xFant" runat="server" />
                                        <asp:HiddenField ID="xLgr" runat="server" />
                                        <asp:HiddenField ID="nro" runat="server" />
                                        <asp:HiddenField ID="xBairro" runat="server" />
                                        <asp:HiddenField ID="xMun" runat="server" />
                                        <asp:HiddenField ID="xUF" runat="server" />
                                        <asp:HiddenField ID="xCEP" runat="server" />
                                        <asp:HiddenField ID="xPais" runat="server" />
                                        <asp:HiddenField ID="xFone" runat="server" />
                                        <asp:HiddenField ID="xIE" runat="server" />
                                        <asp:HiddenField ID="xIM" runat="server" />

                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td width="100px">
                                        <asp:TextBox ID="txtEmissao" runat="server" Width="100px" Enabled="False" CssClass="calendario"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        <asp:TextBox ID="txtQtde" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValor" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPesoBruto" runat="server" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtM3" runat="server" Width="100px"></asp:TextBox>
                                    </td>                                    
                                    <td align="right">
                                        <asp:Button ID="btnSalvarNF" runat="server" Text="Salvar" Enabled="False" />
                                        <asp:Button ID="btnCancelarNF" runat="server" Text="Cancelar" Enabled="False" />
                                        <asp:Button ID="btnConcluirNF" runat="server" Text="Concluir" Enabled="False" />
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td colspan="6">
                                        <asp:GridView ID="dgConsultaNF" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            EmptyDataText="Nenhuma NF encontrada." ForeColor="#333333" ShowHeaderWhenEmpty="True"
                                            Width="100%" Font-Size="11px" DataKeyNames="AUTONUM">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Reserva" DataField="RESERVA">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="DANFE" DataField="DANFE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Número" DataField="NUMERO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Série" DataField="SERIE">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Emissor" DataField="EMISSOR" />
                                                <asp:BoundField HeaderText="Emissão" DataField="EMISSAO">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Qtde" DataField="QTDE" DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Valor" DataField="VALOR" DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Peso Bruto" DataField="PESO_BRUTO" DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="M3" DataField="M3" DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HyperLink 
                                                            ID="HyperLink1" 
                                                            runat="server" 
                                                            Target="_blank"
                                                            Visible='<%# IIf(Eval("ARQUIVO_DANFE") <> "X", True, False)  %>'
                                                            NavigateUrl='<%# "~/VisualizaDanfe.aspx?id=" & Eval("AUTONUM") & "&tipo=1" %>'>
                                                            <img src='<%= Page.ResolveUrl("~/imagens/nfe.jpg") %>' width="50px" height="50px"/>
                                                        </asp:HyperLink>
                                                         </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif" CommandName="EDITAR"
                                                            CommandArgument="<%# Container.DataItemIndex %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Confirma a exclusão da NF?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#076703" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <h3 style="font-size: 16px;" data-accordion="3">4. Responsável pelo pagamento</h3>
            <div> 
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">                    
                    <tr>
                        <td colspan="2" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 16px; color: White;" bgcolor="#076703">
                            Responsável pelo pagamento do serviço "Recebimento Exportação"
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#076703">
                            <button id="btnClickJson">Clique</button>
                        </td>
                    </tr>--%>
                    
                    <tr bgcolor="White"> 
                        <td align="left" style="border-style: solid; border-width: 1px; font-size:14px;">
                            Cnpj/CPF:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:TextBox ID="txtCNPJResponsavel" runat="server" Width="99%"></asp:TextBox>
                            <asp:Label ID="TextBox1" runat="server" Width="99%" CssClass="overflow"></asp:Label>
                            <asp:HiddenField ID="hiddenText" runat="server"> </asp:HiddenField>
                            <asp:HiddenField ID="hiddenTextCNPJ" runat="server"> </asp:HiddenField>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; color: #fff" bgcolor="red" height="25" colspan="2" align="center">
                            Se o responsável pelo pagamento do serviço de recebimento exportação não for o exportador favor indicar o responsável pelo pagamento
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td align="left" style="border-style: solid; border-width: 1px; font-size:14px;">
                            Email:
                        </td>
                        <td style="border-style: solid; border-width: 1px; font-size: 14px;">
                            <asp:TextBox ID="txtEmailResponsavel" runat="server" Width="99%" ></asp:TextBox>
                        </td>                        
                    </tr>
                    
                </table>
            </div>
            
            <h3 style="font-size: 16px;" data-accordion="5">
                5. Escolha do Período</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td>
                            <center>
                                <asp:Panel ID="pnGrid" runat="server" Height="150px" ScrollBars="Vertical" BorderStyle="None">
                                    <asp:GridView ID="dgConsultaPeriodos" runat="server" AutoGenerateColumns="False"
                                        BorderStyle="None" EmptyDataText="Nenhum período disponível." ForeColor="#333333"
                                        ShowHeaderWhenEmpty="True" Width="99%" Font-Size="16px" DataKeyNames="AUTONUM_GD_RESERVA,LIMITE_PESO,LIMITE_M3,LIMITE_VOLUMES">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="AUTONUM_GD_RESERVA" HeaderText="Codigo" Visible="False" />
                                            <asp:BoundField HeaderText="Período Inicial" DataField="PERIODO_INICIAL">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Período Final" DataField="PERIODO_FINAL">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Limite Peso" DataField="LIMITE_PESO">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Limite M³" DataField="LIMITE_M3">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Limite Volumes" DataField="LIMITE_VOLUMES">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Limite Veículos" DataField="LIMITE_CAMINHOES">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="cmdSelecionar" ImageUrl="~/imagens/check.png"
                                                        CommandName="SEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#076703" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </center>
                        </td>
                    </tr>       
                    </table>
            </div>
        </div>

        <div style="width:1000px; background-color:red; color: white; height:20px;line-height:20px; font-size:13px;">
            <p>Atenção: É necessário apresentar 03 vias do CTE e Danfe no Gate!</p>
        </div>

        <asp:Label ID="lblCodigoPatio" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoAgendamento" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoBooking" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoMotorista" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoVeiculo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoProtocolo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoNF" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblQuantidadeSelecionadaNF" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblM3SelecionadaNF" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPesoSelecionadaNF" runat="server" Visible="False"></asp:Label>
       	<asp:Label ID="LblResp" runat="server" Visible="False"></asp:Label>
       	<asp:Label ID="Lblidcnpj" runat="server" Visible="False"></asp:Label>
        <asp:HiddenField ID="AccordionIndex" runat="server" Value="0" />

        

         <asp:Panel ID="pnCadastro" runat="server" Visible="false" Style="background: white;"
            Width="1000px" Height="180px">
            <div align="center">
                <iframe id="frameCadastro" runat="server" width="1000px" height="180px"></iframe>
                <br />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="modalReserva" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="btnCancelar" DropShadow="true" PopupControlID="pnCadastro" PopupDragHandleControlID="panel3"
            TargetControlID="btnCancelar" OkControlID="btnCancelar">
        </cc1:ModalPopupExtender>



    </center>
</asp:Content>
