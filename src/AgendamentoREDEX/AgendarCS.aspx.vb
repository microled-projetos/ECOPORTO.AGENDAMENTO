Imports System.Data.OleDb
Imports System.IO
Imports System.Xml
Imports System.Web.Services
Imports System.ComponentModel



Public Class AgendarCS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ConsultarCavalos()
            ConsultarTransportadora()
            ConsultarPeriodos()
            Me.btnUploadNota.Enabled = False
            Me.txtArquivoDanfe.Enabled = False
            Me.txtCNPJResponsavel.Enabled = False
            Me.txtEmailResponsavel.Enabled = False

            If Request.QueryString("id") IsNot Nothing Then

                Me.txtCNPJResponsavel.Enabled = True
                Me.txtEmailResponsavel.Enabled = True

                Dim SQL As New StringBuilder

                SQL.Append("SELECT ")
                SQL.Append("  A.AUTONUM, ")
                SQL.Append("  B.NOME, ")
                SQL.Append("  B.CNH, ")
                SQL.Append("  A.AUTONUM_MOTORISTA, ")
                SQL.Append("  A.AUTONUM_VEICULO, ")
                SQL.Append("  TO_CHAR(A.DATA_AGENDAMENTO,'DD/MM/YYYY') DATA_AGENDAMENTO, ")
                SQL.Append("  C.PLACA_CAVALO, ")
                SQL.Append("  C.PLACA_CARRETA, ")
                SQL.Append("  A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, ")
                SQL.Append("  case when A.STATUS='GE' then 'Gerado' ELSE  'Impresso'  END AS STATUS, ")
                SQL.Append("  BOO.REFERENCE RESERVA, ")
                SQL.Append("  A.AUTONUM_GD_RESERVA, ")
                SQL.Append("  A.EMAIL_FAT, ")
                SQL.Append("  A.CLIENTE_FAT , AUTONUM_BOOKING ")
                SQL.Append("FROM ")
                SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  REDEX.TB_BOOKING BOO  ON A.AUTONUM_BOOKING = BOO.AUTONUM_BOO ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
                SQL.Append("WHERE A.AUTONUM = " & Request.QueryString("id").ToString())

                Dim Ds As New DataTable
                Ds = Banco.List(SQL.ToString())
                Dim Periodo As String

                If Ds IsNot Nothing Then
                    If Ds.Rows.Count > 0 Then

                        Me.txtReserva.Text = Ds.Rows(0)("RESERVA").ToString()
                        txtReserva_TextChanged(sender, e)
                        Me.lblCodigoBooking.Text = Ds.Rows(0)("AUTONUM_BOOKING").ToString()

                        Me.lblCodigoMotorista.Text = Ds.Rows(0)("AUTONUM_MOTORISTA").ToString()
                        Me.lblCodigoVeiculo.Text = Ds.Rows(0)("AUTONUM_VEICULO").ToString()

                        Me.txtMotorista.Text = Ds.Rows(0)("NOME").ToString()
                        Me.cbCavalo.Text = Ds.Rows(0)("PLACA_CAVALO").ToString()

                        ConsultarCarretas(Me.cbCavalo.Text)
                        Me.cbCarreta.Text = Ds.Rows(0)("PLACA_CARRETA").ToString()

                        ConsultarQuantidades()
                        ConsultarPeriodos()

                        Me.lblCodigoPeriodo.Text = Ds.Rows(0)("AUTONUM_GD_RESERVA").ToString()
                        Periodo = Banco.ExecuteScalar("SELECT TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text)

                        For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                            Linha.BackColor = Drawing.Color.Transparent
                        Next

                        For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                            If Linha.Cells(1).Text & " - " & Linha.Cells(2).Text = Periodo Then
                                Linha.BackColor = Drawing.Color.MistyRose
                            End If
                        Next

                        If Request.QueryString("more") IsNot Nothing Then
                            If Request.QueryString("more") = "1" Then
                                Me.txtMotorista.Enabled = False
                                Me.cbCarreta.Enabled = False
                                Me.cbCavalo.Enabled = False
                                Me.dgConsultaPeriodos.Enabled = False
                                Me.txtReserva.Text = String.Empty

                            End If
                        Else
                            Me.lblCodigoAgendamento.Text = Ds.Rows(0)("AUTONUM").ToString()
                            ConsultarNF(Me.lblCodigoAgendamento.Text)
                            HabilitaCamposNF(True)
                        End If

                        If Not IsDBNull(Ds.Rows(0)("EMAIL_FAT")) Then
                            Me.txtEmailResponsavel.Text = Ds.Rows(0)("EMAIL_FAT")
                        Else
                            Me.txtEmailResponsavel.Text = ""
                        End If


                        Dim SQLParceiro As New StringBuilder

                        SQLParceiro.Append("SELECT RAZAO,CGC as CNPJ,AUTONUM FROM REDEX.TB_Cad_Parceiros WHERE AUTONUM = " & Ds.Rows(0)("CLIENTE_FAT"))

                        Dim DsParceiro As New DataTable
                        DsParceiro = Banco.List(SQLParceiro.ToString())

                        If DsParceiro IsNot Nothing Then
                            If DsParceiro.Rows.Count > 0 Then
                                Me.txtCNPJResponsavel.Text = DsParceiro.Rows(0)("RAZAO") + " - " + DsParceiro.Rows(0)("CNPJ")
                                Me.hiddenText.Value = DsParceiro.Rows(0)("AUTONUM")
                            Else
                                Me.txtCNPJResponsavel.Text = Ds.Rows(0)("CLIENTE_FAT")
                                Me.hiddenText.Value = ""
                            End If
                        End If



                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Registro inexistente!','ConsultarAgendamentosCargaSolta.aspx');", True)
                    End If
                End If



            Else


                Me.lblDeadLine.Text = String.Empty
                Me.lblExportador.Text = String.Empty
                Me.lblM3.Text = String.Empty
                Me.lblNavio.Text = String.Empty
                Me.lblPesoBruto.Text = String.Empty
                Me.lblStatus.Text = String.Empty
                Me.lblTipo.Text = String.Empty
                Me.lblTotal.Text = String.Empty
                Me.lblViagem.Text = String.Empty
                Me.lblVolumes.Text = String.Empty





            End If

        End If

        If Not String.IsNullOrEmpty(Me.lblCodigoBooking.Text) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
        End If

    End Sub

    Private Sub ConsultarQuantidades()

        Dim SQL As New StringBuilder

        SQL.Append("SELECT RESERVA, ")
        SQL.Append("       SUM(UTILIZADOS) UTILIZADOS ")
        SQL.Append("FROM ")
        SQL.Append("  (SELECT A.RESERVA, ")
        SQL.Append("    (SELECT NVL(SUM(QTDE),0) ")
        SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
        SQL.Append("    WHERE REDEX.TB_AGENDAMENTO_WEB_CS_NF.AUTONUM_AGENDAMENTO = A.AUTONUM ")
        SQL.Append("    ) UTILIZADOS ")
        SQL.Append("  FROM REDEX.TB_AGENDAMENTO_WEB_CS A ")
        SQL.Append("  WHERE UPPER(A.RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' ")
        SQL.Append("  )Q ")
        SQL.Append("GROUP BY RESERVA ")

        Dim Ds As New DataTable
        Ds = Banco.List(SQL.ToString())

        If Ds IsNot Nothing Then
            If Ds.Rows.Count > 0 Then
                Me.lblUtilizados.Text = Ds.Rows(0)("UTILIZADOS").ToString()
                Me.lblDisponiveis.Text = (Nnull(Me.lblTotal.Text, 0) - Nnull(Me.lblUtilizados.Text, 0))
            End If
        End If

    End Sub

    Public Sub ConsultarCavalos()

        Me.cbCavalo.DataSource = Banco.List("SELECT PLACA_CAVALO FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
        Me.cbCavalo.DataBind()

        Me.cbCavalo.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarCarretas(ByVal Cavalo As String)

        Me.cbCarreta.DataSource = Banco.List("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & " AND PLACA_CAVALO = '" & Me.cbCavalo.Text & "'")
        Me.cbCarreta.DataBind()

        Me.cbCarreta.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarTransportadora()
        Me.lblTransportadora.Text = Banco.ExecuteScalar("SELECT RAZAO FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
    End Sub

    Protected Sub txtReserva_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtReserva.TextChanged

        Dim SQL As New StringBuilder

        Me.lblSemSaldo.Visible = False

        If Not String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) And
            Not String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then

            'If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE REFERENCE = '" & Me.txtReserva.Text & "'")) > 1 Then
            '    frameCadastro.Attributes("src") = "EscolheReserva.aspx?reserva=" & Me.txtReserva.Text
            '    pnCadastro.Visible = True                
            '    modalReserva.Show()
            '    Exit Sub
            'End If
            Me.txtCNPJResponsavel.Enabled = True
            Me.txtEmailResponsavel.Enabled = True
            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE (FLAG_DTA = 1 OR FLAG_RETIRADA_CARGA = 'E') AND REFERENCE = '" & Me.txtReserva.Text & "'")) > 1 Then
                Me.txtCNPJResponsavel.Visible = False
                Me.txtEmailResponsavel.Visible = False
            End If

            Dim ret As String = String.Empty

            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE REFERENCE = '" & Me.txtReserva.Text.ToUpper() & "'")) = 0 Then
                If My.Settings.LerXmlCraft = 1 Then
                    Try
                        ret = Craft.ImportarXMLCraft(Me.txtReserva.Text.ToUpper())
                        If ret <> String.Empty Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('" & ret & "');", True)
                        End If
                    Catch ex As Exception
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('" & ex.Message & "');", True)
                    End Try
                End If
            End If

            Dim Ds As New DataTable

            SQL.Clear()
            SQL.Append("SELECT")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    QUANTIDADE, ")
            SQL.Append("    VOLUMES, ")
            SQL.Append("    PESO, ")
            SQL.Append("    METRAGEM_CUBICA, ")
            SQL.Append("    NAVIO, ")
            SQL.Append("    NVOCC, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    NVOCC, ")
            SQL.Append("    AUTONUM_VIAGEM, ")
            SQL.Append("    NUM_VIAGEM, ")
            SQL.Append("    DT_DEAD_LINE, ")
            SQL.Append("    FLAG_CS, ")
            SQL.Append("    FLAG_CNTR, ")
            SQL.Append("    PATIO, ")
            SQL.Append("    QTD_REGISTRO ")
            SQL.Append("FROM ")
            SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_DADOS_BOO ")
            SQL.Append(" WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' AND NVL(FLAG_CS,0) = 1")

            Ds = Banco.List(SQL.ToString())

            If Ds IsNot Nothing Then
                If Ds.Rows.Count > 0 Then
                    For Each Linha As DataRow In Ds.Rows

                        Me.lblDeadLine.Text = Linha("DT_DEAD_LINE").ToString()
                        Me.lblExportador.Text = Linha("EXPORTADOR").ToString()
                        Me.lblM3.Text = Linha("METRAGEM_CUBICA").ToString()
                        Me.lblNavio.Text = Linha("NAVIO").ToString()
                        Me.lblPesoBruto.Text = Linha("PESO").ToString()
                        Me.lblStatus.Text = "Liberado"
                        Me.lblPesoBruto.Text = Linha("PESO").ToString()
                        Me.lblTipo.Text = "Carga Solta"
                        Me.lblTotal.Text = Linha("VOLUMES").ToString()
                        Me.lblViagem.Text = Linha("NUM_VIAGEM").ToString()
                        Me.lblVolumes.Text = Linha("VOLUMES").ToString()
                        Me.lblCodigoBooking.Text = Linha("AUTONUM").ToString()
                        Me.lblCodigoPatio.Text = Linha("PATIO").ToString()

                        If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING_CARGA WHERE FLAG_CS = 1 AND AUTONUM_BOO = " & Me.lblCodigoBooking.Text)) = 0 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('A Reserva informada (" & Me.txtReserva.Text.ToUpper() & ") não é de Carga Solta.','AgendarCS.aspx');", True)
                            Exit Sub
                        End If

                        'Dim SQLExportador As New StringBuilder
                        'Dim DsExportador As DataTable


                        'Query para pegar o CGC DO parceiro já cadastrado e  somente deverá ser assim quando o exportador for diferente de vazio
                        'ver esse trecho do codigo como será feito, pois é onde preenche o campo do cnpj 
                        'SQLExportador.Append(" SELECT RAZAO, CGC, AUTONUM FROM REDEX.TB_Cad_Parceiros WHERE RAZAO = '" + Linha("EXPORTADOR").ToString() + "' ")

                        'DsExportador = Banco.List(SQLExportador.ToString())
                        'If DsExportador.Rows.Count > 0 Then
                        '    Me.txtCNPJResponsavel.Text = DsExportador.Rows(0)("Razao") + " - " + DsExportador.Rows(0)("CGC")
                        '    Me.hiddenText.Value = DsExportador.Rows(0)("AUTONUM")
                        'End If
                        'ver esse trecho do codigo como será feito, pois é onde preenche o campo do cnpj



                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion1", "document.getElementById('accordion').style.display = 'block';", True)
                        Me.txtMotorista.Focus()

                        ConsultarQuantidades()

                        If Val(Me.lblTotal.Text) > 0 Then
                            If Val(Me.lblUtilizados.Text) >= (Val(Me.lblTotal.Text) - Val(Linha("QTD_REGISTRO").ToString())) Then
                                Me.lblSemSaldo.Visible = True
                                If Request.QueryString("id") Is Nothing Then
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion2", "document.getElementById('accordion').style.display = 'none';", True)
                                Else
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion3", "document.getElementById('accordion').style.display = 'block';", True)
                                End If
                            End If
                        End If

                        Me.btnSalvar.Visible = True
                        Me.btnCancelar.Visible = True
                        Me.txtCNPJResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Enabled = True
                        ConsultarPeriodos()

                    Next

                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva inválida!');", True)
                End If
            End If

        End If

        If Me.lblCodigoBooking.Text = String.Empty Then

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva não encontrada!');", True)

            Me.lblDeadLine.Text = String.Empty
            Me.lblExportador.Text = String.Empty
            Me.lblM3.Text = String.Empty
            Me.lblNavio.Text = String.Empty
            Me.lblPesoBruto.Text = String.Empty
            Me.lblStatus.Text = String.Empty
            Me.lblTipo.Text = String.Empty
            Me.lblTotal.Text = String.Empty
            Me.lblViagem.Text = String.Empty
            Me.lblVolumes.Text = String.Empty

        End If

    End Sub

    Protected Sub txtMotorista_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMotorista.TextChanged
        Dim SQL As New StringBuilder

        If Not String.IsNullOrEmpty(Me.txtMotorista.Text.Trim()) And
                Not String.IsNullOrWhiteSpace(Me.txtMotorista.Text.Trim()) Then
            Me.txtMotorista.Text = Banco.ExecuteScalar("SELECT DISTINCT NOME || ' - ' || CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE (UPPER(NOME) LIKE '%" & Me.txtMotorista.Text.ToUpper() & "%' OR CNH = '" & Me.txtMotorista.Text & "') AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
            Me.AccordionIndex.Value = 1
        End If

        Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
        If Me.lblCodigoMotorista.Text > "0" Then
            Dim Ds As New DataTable
            SQL.Clear()
            SQL.Append(" SELECT a.AUTONUM FROM  OPERADOR.TB_AG_MOTORISTAS A")
            SQL.Append(" LEFT JOIN OPERADOR.TB_MOTORISTAS B ON REPLACE(REPLACE(A.CPF,'.',''),'-','') = REPLACE(REPLACE(B.CPF,'.',''),'-','')")
            SQL.Append("  WHERE  NVL(B.FLAG_INATIVO, 0)=1 and a.AUTONUM = " & Me.lblCodigoMotorista.Text)
            Ds = Banco.List(SQL.ToString())
            If Ds.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Motorista bloqueado no Terminal.');", True)
                Me.txtMotorista.Text = ""
                Exit Sub
            End If
        End If
    End Sub

    Private Function ValidarSaldoRestante(ByVal Qtde As String) As Boolean

        Dim SQL As New StringBuilder
        Dim Ds As New DataTable


        If Nnull(Me.lblCodigoAgendamento.Text, 0) = 0 Then
            Return True
        End If

        If String.IsNullOrEmpty(Me.lblCodigoPeriodo.Text) Or Val(Me.lblCodigoPeriodo.Text) = 0 Then
            Return True
        End If

        If Not String.IsNullOrEmpty(Me.lblCodigoPeriodo.Text) And Val(Me.lblCodigoPeriodo.Text) > 0 Then
            If Me.lblCodigoBooking.Text > "" Then
                Dim DsDes As New DataTable
                Dim sSql As String
                Dim Contar As Integer

                SQL.Clear()
                If txtReserva.Text = "" Then
                    sSql = "SELECT reference FROM REDEX.TB_BOOKING WHERE autonum_boo = " & lblCodigoBooking.Text & "  "
                    txtReserva.Text = Banco.List(sSql).Rows(0)(0).ToString
                End If

                sSql = "Select count(1) contar From redex.TB_RESERVA_PERIODO WHERE AUTONUM_BOO=" + Me.lblCodigoBooking.Text
                DsDes = Banco.List(sSql)
                Contar = Convert.ToInt32(DsDes.Rows(0)("contar").ToString())

                sSql = "SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_DES"
                sSql = sSql & "  WHERE  "
                sSql = sSql & "  PATIO =" & Val(Me.lblCodigoPatio.Text)
                DsDes = Banco.List(sSql)
                SQL.Append(" SELECT AUTONUM_GD_RESERVA ")
                SQL.Append(" FROM (")
                SQL.Append("  SELECT A.AUTONUM_GD_RESERVA, ")
                If DsDes.Rows.Count > 0 Then
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                        SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_M3,0)        end   - NVL(AG.M3,0) LIMITE_M3, ")
                    Else
                        SQL.Append("  0 LIMITE_M3, ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                        SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_PESO,0)      end   - NVL(AG.PESO,0)  LIMITE_PESO, ")
                    Else
                        SQL.Append(" 0  LIMITE_PESO, ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                        SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_VOLUMES,0)   end   - (NVL(AG.QTD,0) +" & Qtde & ") LIMITE_VOLUMES,  ")
                    Else
                        SQL.Append(" 0 LIMITE_VOLUMES,  ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                        SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_CAMINHOES,0) end   - NVL(CAM.QTD,0) LIMITE_CAMINHOES  ")
                    Else
                        SQL.Append("  0 LIMITE_CAMINHOES  ")
                    End If
                Else
                    SQL.Append("  NVL(A.LIMITE_M3,0)       - NVL(AG.M3,0) LIMITE_M3, ")
                    SQL.Append("  NVL(A.LIMITE_PESO,0)     - NVL(AG.PESO,0)  LIMITE_PESO, ")
                    SQL.Append("  NVL(A.LIMITE_VOLUMES,0)  - NVL(AG.QTD,0) LIMITE_VOLUMES,  ")
                    SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVLL(A.LIMITE_CAMINHOES,0) end   - NVL(CAM.QTD,0) LIMITE_CAMINHOES  ")
                End If
                SQL.Append("  FROM REDEX.TB_GD_RESERVA A ")
                SQL.Append("   LEFT JOIN ( Select * From redex.TB_RESERVA_PERIODO WHERE AUTONUM_BOO=" + Me.lblCodigoBooking.Text + ") RL ON RL.AUTONUM_GD_RESERVA=A.AUTONUM_GD_RESERVA ")
                SQL.Append("  LEFT JOIN (")
                SQL.Append("    SELECT NVL(SUM(QTDE),0)  AS QTD, NVL(SUM(M3),0) M3,NVL(SUM(PESO_BRUTO),0) PESO, AUTONUM_GD_RESERVA ")
                SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
                SQL.Append("    INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS ON TB_AGENDAMENTO_WEB_CS_NF.AUTONUM_AGENDAMENTO = TB_AGENDAMENTO_WEB_CS.AUTONUM ")
                If Contar > 0 Then
                    SQL.Append(" WHERE AUTONUM_BOOKING =" + Me.lblCodigoBooking.Text)
                End If
                SQL.Append("    GROUP BY AUTONUM_GD_RESERVA")
                SQL.Append("  ) AG On A.AUTONUM_GD_RESERVA = AG.AUTONUM_GD_RESERVA")
                SQL.Append("  LEFT JOIN (")
                If Me.lblCodigoVeiculo.Text > "0" Then
                    SQL.Append(" Select Case when max(NVL(cm.qtde,0))=0 then SUM(1)+1 else ")
                    SQL.Append(" 0 End QTD, cs.AUTONUM_GD_RESERVA  ")
                    SQL.Append(" From redex.TB_AGENDAMENTO_WEB_CS  cs ")
                    SQL.Append(" Left Join( select distinct 1 qtde , AUTONUM_GD_RESERVA from ")
                    SQL.Append(" redex.TB_AGENDAMENTO_WEB_CS where AUTONUM_GD_RESERVA=" & Me.lblCodigoPeriodo.Text & " AND ")
                    SQL.Append(" AUTONUM_VEICULO = " & Me.lblCodigoVeiculo.Text & " AND autonum <>" & Val(Me.lblCodigoAgendamento.Text) & ")  cm ")
                    SQL.Append(" On cs.AUTONUM_GD_RESERVA=cm.AUTONUM_GD_RESERVA ")
                    SQL.Append(" WHERE CS.AUTONUM_GD_RESERVA=" & Me.lblCodigoPeriodo.Text & " And  CS.autonum <>" & Val(Me.lblCodigoAgendamento.Text) & "")
                    SQL.Append(" Group BY CS.AUTONUM_GD_RESERVA   ")
                Else
                    SQL.Append("    Select NVL(SUM(1),0)+1 QTD, AUTONUM_GD_RESERVA")
                    SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS Where ")
                    SQL.Append("    AUTONUM_GD_RESERVA=" & Me.lblCodigoPeriodo.Text & " And  autonum <>" & Val(Me.lblCodigoAgendamento.Text) & "")
                    SQL.Append("    Group BY CS.AUTONUM_GD_RESERVA   ")
                End If
                SQL.Append("  ) CAM On A.AUTONUM_GD_RESERVA = CAM.AUTONUM_GD_RESERVA")
                SQL.Append("  WHERE A.SERVICO_GATE = 'A' AND A.TIPO = 'CS' AND A.PERIODO_INICIAL >= SYSDATE")
                If Val(Me.lblCodigoPatio.Text) <> 0 Then
                    SQL.Append("  AND A.PATIO = " & Val(Me.lblCodigoPatio.Text))
                Else
                    SQL.Append(" AND A.PATIO = 9999 ")
                End If
                SQL.Append("  ) R ")
                SQL.Append("  WHERE AUTONUM_GD_RESERVA > 0 ")

                If DsDes.Rows.Count > 0 Then
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                        SQL.Append(" AND LIMITE_M3 >= 0 ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                        SQL.Append(" AND LIMITE_PESO >= 0 ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                        SQL.Append(" AND LIMITE_VOLUMES >= 0 ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                        SQL.Append(" AND LIMITE_CAMINHOES >= 0 ")
                    End If
                    If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) = 0 And Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) = 0 And Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) = 0 And Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) = 0 Then
                        SQL.Append(" AND LIMITE_CAMINHOES >= 0 ")
                    End If
                End If
                SQL.Append(" AND (LIMITE_M3 >= 0 or LIMITE_PESO >= 0 or LIMITE_VOLUMES >= 0 or LIMITE_CAMINHOES >= 0 ) ")

                SQL.Append(" AND AUTONUM_GD_RESERVA=" & Me.lblCodigoPeriodo.Text)
                Ds = Banco.List(SQL.ToString())

                If Ds IsNot Nothing Then
                    If Ds.Rows.Count > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Else
            Return False
        End If

    End Function
    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvar.Click

        Dim SQL As New StringBuilder
        Dim Entrada As Boolean
        Dim CodAntVeic As Long
        Dim ContaVeic As Long
        Dim CodPer As Long

        Try
            Entrada = True
            CodAntVeic = 0
            CodPer = 0
            Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM  OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
            If Val(Me.lblCodigoAgendamento.Text) > 0 Then
                CodPer = Banco.ExecuteScalar("SELECT nvl(AUTONUM_GD_RESERVA,0) FROM redex.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM  = " & Val(Me.lblCodigoAgendamento.Text))
                CodAntVeic = Banco.ExecuteScalar("SELECT nvl(AUTONUM_VEICULO,0) FROM redex.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM  = " & Val(Me.lblCodigoAgendamento.Text))
                ContaVeic = Banco.ExecuteScalar("SELECT Count(1) FROM redex.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM_GD_RESERVA=" & Val(CodPer) & " AND AUTONUM_VEICULO = " & Val(CodAntVeic))
                If ContaVeic > 1 And Me.LblResp.Text = "" Then
                    'Exit Sub
                End If
            End If



            If CodPer <> Val(Me.lblCodigoPeriodo.Text) Or CodPer = 0 Then
                If CodPer <> 0 Then
                    SQL.Append("UPDATE ")
                    SQL.Append("   Redex.TB_AGENDAMENTO_WEB_CS ")
                    SQL.Append("  SET ")
                    SQL.Append("    AUTONUM_GD_RESERVA = 0 ")
                    SQL.Append("  WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text)
                    Banco.BeginTransaction(SQL.ToString())
                End If
                If Val(Me.lblCodigoAgendamento.Text) > 0 Then
                    Dim totalq = Banco.ExecuteScalar("Select  sum(qtde) From redex.TB_AGENDAMENTO_WEB_CS_NF where AUTONUM_AGENDAMENTO =" & Me.lblCodigoAgendamento.Text)
                    Entrada = ValidarSaldoRestante(totalq)
                End If
            End If

            If Val(Me.lblCodigoAgendamento.Text) = 0 Then
                Entrada = ValidarSaldoRestante("0")
            End If
            If Me.btnSalvar.Text = "Concluir" Then
                If Val(Me.lblCodigoPeriodo.Text) = 0 Then
                    Entrada = False
                End If
            End If
            If Entrada Then

                If Me.btnSalvar.Text = "Concluir" Then

                    If String.IsNullOrEmpty(Me.txtEmailResponsavel.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmailResponsavel.Text.Trim()) Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O email é de preenchimento obrigatório.');", True)
                        Me.AccordionIndex.Value = 3
                        Me.txtCNPJResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Focus()
                        Exit Sub
                    End If

                    If String.IsNullOrEmpty(Me.txtEmailResponsavel.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmailResponsavel.Text.Trim()) Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O email é de preenchimento obrigatório.');", True)
                        Me.AccordionIndex.Value = 3
                        Me.txtCNPJResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Focus()
                        Exit Sub
                    End If



                    If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE (FLAG_DTA = 1 OR FLAG_RETIRADA_CARGA = 'E') AND REFERENCE = '" & Me.txtReserva.Text & "'")) > 1 Then


                    End If

                    If Val(Me.hiddenText.Value) = 0 Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O CNPJ é de preenchimento obrigatório');", True)
                        Me.AccordionIndex.Value = 3
                        Me.txtCNPJResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Focus()
                        Exit Sub
                    End If

                    If Val(Me.lblCodigoPeriodo.Text) > 0 Then
                        If Val(Me.lblCodigoAgendamento.Text) > 0 Then
                            Dim totalq = Banco.ExecuteScalar("Select  sum(qtde) From redex.TB_AGENDAMENTO_WEB_CS_NF where AUTONUM_AGENDAMENTO =" & Me.lblCodigoAgendamento.Text)
                            If ValidarSaldoRestante(totalq) = False Then
                                ConsultarPeriodos()
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Saldo insuficiente. Escolha outro período.');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    SQL.Append("UPDATE ")
                    SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS ")
                    SQL.Append("  SET ")
                    SQL.Append("    AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text & " ")
                    SQL.Append(" ,EMAIL_FAT = '" & Me.txtEmailResponsavel.Text & "' ")
                    SQL.Append(" ,CLIENTE_FAT = " & Me.hiddenText.Value & " ")
                    SQL.Append("  WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text)

                    If Banco.BeginTransaction(SQL.ToString()) Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "novaReserva(" & Me.lblCodigoAgendamento.Text & "," & Me.lblCodigoProtocolo.Text & ");", True)
                        Exit Sub
                    End If

                    ' Me.txtCNPJResponsavel.Text = String.Empty
                    ' Me.txtCNPJResponsavel.Enabled = False
                    ' Me.hiddenText.Value = String.Empty
                    '
                    '           Me.txtEmailResponsavel.Text = String.Empty
                    '           Me.txtEmailResponsavel.Enabled = False

                End If

                If ValidarCampos() Then

                    If Request.QueryString("id") Is Nothing Or Request.QueryString("more") IsNot Nothing Then

                        If Val(Me.lblCodigoPeriodo.Text) > 0 Then
                            If ValidarSaldoRestante(0) = False Then
                                ConsultarPeriodos()
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Saldo insuficiente. Escolha outro período.');", True)
                                Exit Sub
                            End If
                        End If

                        Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())

                        Dim Ds As New DataTable
                        SQL.Clear()
                        SQL.Append(" SELECT count(1) FROM  OPERADOR.TB_AG_MOTORISTAS A")
                        SQL.Append(" INNER JOIN OPERADOR.TB_MOTORISTAS B ON REPLACE(REPLACE(A.CPF,'.',''),'-','') = REPLACE(REPLACE(B.CPF,'.',''),'-','')")
                        SQL.Append("  WHERE  NVL(B.FLAG_INATIVO, 0)=1 and a.AUTONUM = " & Me.lblCodigoMotorista.Text)
                        Ds = Banco.List(SQL.ToString())
                        If Ds.Rows.Count > 0 Then
                            '     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Motorista bloqueado no Terminal.');", True)
                            '    Exit Sub

                        End If
                        Me.lblCodigoAgendamento.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_CS.NEXTVAL FROM DUAL")

                        If Not String.IsNullOrEmpty(Me.lblCodigoAgendamento.Text.Trim()) And Not String.IsNullOrWhiteSpace(Me.lblCodigoAgendamento.Text.Trim()) Then

                            Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                            Me.lblCodigoProtocolo.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL FROM DUAL")

                            SQL.Clear()
                            SQL.Append("INSERT INTO ")
                            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS ")
                            SQL.Append("  ( ")
                            SQL.Append("    AUTONUM, ")
                            SQL.Append("    RESERVA, ")
                            SQL.Append("    AUTONUM_MOTORISTA, ")
                            SQL.Append("    AUTONUM_VEICULO, ")
                            SQL.Append("    AUTONUM_TRANSPORTADORA, ")
                            SQL.Append("    AUTONUM_GD_RESERVA, ")
                            SQL.Append("    AUTONUM_BOOKING, ")
                            SQL.Append("    DATA_AGENDAMENTO, ")
                            SQL.Append("    STATUS, ")
                            SQL.Append("    NUM_PROTOCOLO, ")
                            SQL.Append("    ANO_PROTOCOLO, ")
                            SQL.Append("    EMAIL_FAT, ")
                            SQL.Append("    CLIENTE_FAT ")
                            SQL.Append("  ) ")
                            SQL.Append("  VALUES ")
                            SQL.Append("  ( ")
                            SQL.Append("  " & Me.lblCodigoAgendamento.Text & ", ")
                            SQL.Append("  '" & Me.txtReserva.Text.ToUpper() & "', ")
                            SQL.Append("  " & Me.lblCodigoMotorista.Text & ", ")
                            SQL.Append("  " & Me.lblCodigoVeiculo.Text & ", ")
                            SQL.Append("  " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & ", ")
                            SQL.Append("  " & Nnull(Me.lblCodigoPeriodo.Text, 0) & ", ")
                            SQL.Append("  " & Nnull(Me.lblCodigoBooking.Text, 0) & ", ")
                            SQL.Append("  SYSDATE, ")
                            SQL.Append("  'GE', ")

                            If Request.QueryString("more") IsNot Nothing Then
                                If Request.QueryString("protocolo") IsNot Nothing Then
                                    SQL.Append("  " & Request.QueryString("protocolo").ToString() & ", ")
                                End If
                            Else
                                SQL.Append("  " & Me.lblCodigoProtocolo.Text & ", ")
                            End If

                            SQL.Append("  " & Now.Year & " ,")
                            SQL.Append("  '" & Me.txtEmailResponsavel.Text & "', ")
                            SQL.Append("  '" & Me.hiddenText.Value & "' ")
                            SQL.Append("  ) ")

                            If Banco.BeginTransaction(SQL.ToString()) Then
                                'If Agendamento.InsereAgendamentoNaFila(0, Val(Me.lblCodigoPeriodo.Text), Val(Me.lblCodigoBooking.Text), Val(Me.lblCodigoAgendamento.Text), TipoAgendamento.CARGA_SOLTA_DESCARGA) Then
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento criado com sucesso! Vincule as Notas Fiscais utlizando os campos abaixo.');", True)
                                Me.AccordionIndex.Value = 2
                                Me.btnSalvar.Text = "Concluir"
                                Me.lblMsgSalvar.BackColor = System.Drawing.Color.FromName("#C1FFC1")
                                Me.lblMsgSalvar.Text = "Digite os Dados das Notas Fiscais e vincule-os ao agendamento clicando no botão Salvar."

                                ConsultarNF(Me.lblCodigoAgendamento.Text)
                                HabilitaCamposNF(True)
                                'End If
                            Else
                                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Erro ao criar um novo agendamento. Tente Novamente.');", True)
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao criar um novo agendamento. Tente Novamente.');", True)
                            End If

                        End If

                    Else

                        Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                        Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                        Me.lblCodigoProtocolo.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL FROM DUAL")

                        If Val(Me.lblCodigoPeriodo.Text) > 0 Then
                            Dim totalq = Banco.ExecuteScalar("Select  sum(qtde) From redex.TB_AGENDAMENTO_WEB_CS_NF where AUTONUM_AGENDAMENTO =" & Me.lblCodigoAgendamento.Text)

                            If ValidarSaldoRestante(totalq) = False Then
                                ConsultarPeriodos()
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Saldo insuficiente. Escolha outro período.');", True)
                                Exit Sub
                            End If
                        End If

                        If Me.hiddenText.Value = 0 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Digite o CNPJ do responsável pelo Faturamento e click na Razão social.');", True)
                            Exit Sub
                        End If

                        SQL.Append("UPDATE ")
                        SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS ")
                        SQL.Append("  SET ")
                        SQL.Append("    AUTONUM_MOTORISTA = " & Me.lblCodigoMotorista.Text & ", ")
                        SQL.Append("    AUTONUM_VEICULO = " & Me.lblCodigoVeiculo.Text & ", ")
                        SQL.Append("    NUM_PROTOCOLO = " & Me.lblCodigoProtocolo.Text & ", ")
                        SQL.Append("    ANO_PROTOCOLO = " & Now.Year & ", ")
                        SQL.Append("    STATUS = 'GE', ")
                        SQL.Append("    AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text & ", ")
                        SQL.Append("    EMAIL_FAT = '" & Me.txtEmailResponsavel.Text & "', ")
                        SQL.Append("    CLIENTE_FAT = " & Me.hiddenText.Value & " ")
                        SQL.Append("  WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text)

                        If Banco.BeginTransaction(SQL.ToString()) Then
                            'If Agendamento.InsereAgendamentoNaFila(0, Val(Me.lblCodigoPeriodo.Text), Val(Me.lblCodigoBooking.Text), Val(Me.lblCodigoAgendamento.Text), TipoAgendamento.CARGA_SOLTA_DESCARGA) Then
                            Me.lblMsgSalvar.BackColor = System.Drawing.Color.FromName("#C1FFC1")
                            Me.lblMsgSalvar.Text = "Digite os Dados das Notas Fiscais e vincule-os ao agendamento clicando no botão Salvar."
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento alterado com sucesso!','ConsultarAgendamentosCargaSolta.aspx');", True)
                            'End If
                        Else
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao alterar o agendamento. Tente Novamente.');", True)
                        End If

                    End If

                End If
            End If

        Catch ex As Exception
            Err.Clear()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao alterar o agendamento. Tente Novamente.');", True)
        End Try


    End Sub

    Private Function ObterNomeMotorista(ByVal Nome As String) As String

        Dim Cadeia() As String = Nome.Split("-")

        If Cadeia.Count > 0 Then
            Return Cadeia(0).ToString().Trim()
        End If

        Return String.Empty

    End Function

    Private Function ValidarCampos() As Boolean

        If String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Nenhuma Reserva Informada.');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhuma Reserva Informada.');", True)
            Me.txtReserva.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.txtMotorista.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtMotorista.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O campo Nome do Motorista é obrigatório!');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Nome do Motorista é obrigatório!');", True)
            Me.AccordionIndex.Value = 1
            Me.txtMotorista.Focus()
            Return False
        End If


        If String.IsNullOrEmpty(Me.cbCavalo.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.cbCavalo.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Selecione a Placa do Cavalo.');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Placa do Cavalo.');", True)
            Me.AccordionIndex.Value = 1
            Me.cbCavalo.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.cbCarreta.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.cbCarreta.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Selecione a Placa da Carreta.');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Placa da Carreta.');", True)
            Me.AccordionIndex.Value = 1
            Me.cbCavalo.Focus()
            Return False
        End If
        If Me.lblCodigoAgendamento.Text <> String.Empty Then
            If String.IsNullOrEmpty(Me.txtEmailResponsavel.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmailResponsavel.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O email é de preenchimento obrigatório.');", True)
                Me.AccordionIndex.Value = 3
                Me.txtEmailResponsavel.Focus()
                Return False
            End If

            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE (FLAG_DTA = 1 OR FLAG_RETIRADA_CARGA = 'E') AND REFERENCE = '" & Me.txtReserva.Text & "'")) > 1 Then

                If String.IsNullOrEmpty(Me.txtCNPJResponsavel.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtCNPJResponsavel.Text.Trim()) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O CNPJ é de preenchimento obrigatório');", True)
                    Me.AccordionIndex.Value = 3


                    Me.txtCNPJResponsavel.Enabled = True
                    Me.txtEmailResponsavel.Enabled = True
                    Me.txtCNPJResponsavel.Focus()
                    Return False
                End If

            End If
        ElseIf Request.QueryString("id") IsNot Nothing Then
            If String.IsNullOrEmpty(Me.txtEmailResponsavel.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmailResponsavel.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O email é de preenchimento obrigatório.');", True)
                Me.AccordionIndex.Value = 3

                Me.txtCNPJResponsavel.Enabled = True
                Me.txtEmailResponsavel.Enabled = True
                Me.txtEmailResponsavel.Focus()
                Return False
            End If

            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE (FLAG_DTA = 1 OR FLAG_RETIRADA_CARGA = 'E') AND REFERENCE = '" & Me.txtReserva.Text & "'")) > 1 Then

                If String.IsNullOrEmpty(Me.txtCNPJResponsavel.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtCNPJResponsavel.Text.Trim()) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O CNPJ é de preenchimento obrigatório');", True)
                    Me.AccordionIndex.Value = 3
                    Me.txtCNPJResponsavel.Focus()
                    Return False
                End If

            End If
        End If


        Return True

    End Function

    Private Function ValidarCamposNF() As Boolean

        Dim QuantidadeCadastrada As Double = 0.0
        Dim QuantidadePesoCadastrada As Double = 0.0
        Dim QuantidadeM3Cadastrada As Double = 0.0
        Dim QuantidadeRegistrada As Integer = 0

        Dim DsDes As New DataTable

        Me.msgErroNF.Visible = False
        Me.msgErroNF.Text = String.Empty

        Dim SQL As New StringBuilder

        SQL.Clear()
        SQL.Append("SELECT")
        SQL.Append("    QTD_REGISTRO ")
        SQL.Append("FROM ")
        SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_DADOS_BOO ")
        SQL.Append(" WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' AND NVL(FLAG_CS,0) = 1")

        Dim DsReg As New DataTable
        DsReg = Banco.List(SQL.ToString())

        If DsReg IsNot Nothing Then
            If DsReg.Rows.Count > 0 Then
                QuantidadeRegistrada = Val(DsReg.Rows(0)("QTD_REGISTRO").ToString())
            End If
        End If

        If String.IsNullOrEmpty(Me.txtDANFE.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtDANFE.Text.Trim()) Then

            DsDes = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_DES")

            If String.IsNullOrEmpty(Me.txtNumero.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtNumero.Text.Trim()) Then
                Me.msgErroNF.Text = "O campo Número é obrigatório"
                Me.AccordionIndex.Value = 2
                Me.txtNumero.Focus()
                Return False
            Else
                If Not IsNumeric(Me.txtNumero.Text) Then
                    Me.msgErroNF.Text = "O campo Número não é um valor Numérico."
                    Me.AccordionIndex.Value = 2
                    Me.txtNumero.Focus()
                    Return False
                End If
            End If

            If String.IsNullOrEmpty(Me.txtSerie.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtSerie.Text.Trim()) Then
                Me.msgErroNF.Text = "O campo Série é obrigatório."
                Me.AccordionIndex.Value = 2
                Me.txtSerie.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(Me.txtEmissor.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmissor.Text.Trim()) Then
                Me.msgErroNF.Text = "O campo Emissor é obrigatório."
                Me.AccordionIndex.Value = 2
                Me.txtEmissor.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(Me.txtEmissao.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmissao.Text.Trim()) Then
                Me.msgErroNF.Text = "O campo Emissão é obrigatório."
                Me.AccordionIndex.Value = 2
                Me.txtEmissao.Focus()
                Return False
            Else
                If Not IsDate(Me.txtEmissao.Text) Then
                    Me.msgErroNF.Text = "O campo Emissão não é uma Data Válida."
                    Me.AccordionIndex.Value = 2
                    Me.txtEmissao.Focus()
                    Return False
                End If
            End If


            If String.IsNullOrEmpty(Me.txtValor.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtValor.Text.Trim()) Or Nnull(Me.txtValor.Text.Trim(), 0) = 0 Then
                Me.msgErroNF.Text = "O campo Valor é obrigatório."
                Me.AccordionIndex.Value = 2
                Me.txtValor.Focus()
                Return False
            Else
                If Not IsNumeric(Me.txtValor.Text) Then
                    Me.msgErroNF.Text = "O campo Valor deve ser um valor Numérico."
                    Me.AccordionIndex.Value = 2
                    Me.txtValor.Focus()
                    Return False
                End If
            End If


            SQL.Clear()

            If Request.QueryString("id") Is Nothing And (Val(Me.lblQuantidadeSelecionadaNF.Text) + Val(Me.lblM3SelecionadaNF.Text) + Val(Me.lblPesoSelecionadaNF.Text)) = 0 Then
                SQL.Append("SELECT NVL(QTDE,0) QTDE, NVL(PESO_BRUTO,0) PESO_BRUTO,NVL(M3,0) M3 FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
                SQL.Append("  WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
            Else
                If Me.lblCodigoNF.Text <> String.Empty Then
                    SQL.Append("SELECT NVL(QTDE,0) QTDE, NVL(PESO_BRUTO,0) PESO_BRUTO,NVL(M3,0) M3 FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
                    SQL.Append("  WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text & " AND AUTONUM <> " & Me.lblCodigoNF.Text)
                Else
                    SQL.Append("SELECT NVL(QTDE,0) QTDE, NVL(PESO_BRUTO,0) PESO_BRUTO,NVL(M3,0) M3 FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
                    SQL.Append("  WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
                End If
            End If


            Dim ds As New DataTable
            ds = Banco.List(SQL.ToString())

            If ds IsNot Nothing Then
                If ds.Rows.Count > 0 Then
                    QuantidadeCadastrada = PPonto(Math.Round(Convert.ToDouble(Nnull(ds.Rows(0)("QTDE").ToString(), 0)), 2))
                    QuantidadePesoCadastrada = PPonto(Math.Round(Convert.ToDouble(Nnull(ds.Rows(0)("PESO_BRUTO").ToString(), 0)), 2))
                    QuantidadeM3Cadastrada = PPonto(Math.Round(Convert.ToDouble(Nnull(ds.Rows(0)("M3").ToString(), 0)), 2))
                End If
            End If

            'If DsDes IsNot Nothing Then
            If Convert.ToInt32(Nnull(DsDes.Rows(0)("FLAG_CS_M3").ToString(), 0)) > 0 Then

                If Nnull(Me.lblM3.Text, 0) <> 0 Then

                    If String.IsNullOrEmpty(Me.txtM3.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtM3.Text.Trim()) Then
                        Me.msgErroNF.Text = "O campo Metragem Cúbica (M3) é obrigatório."
                        Me.AccordionIndex.Value = 2
                        Me.txtM3.Focus()
                        Return False
                    End If

                    If Not IsNumeric(Nnull(Me.txtM3.Text, 0)) Then
                        Me.msgErroNF.Text = "O campo Metragem Cúbica não é um valor Numérico."
                        Me.AccordionIndex.Value = 2
                        Me.txtM3.Focus()
                        Return False
                    End If

                    If Val(Me.txtM3.Text) + Val(QuantidadeM3Cadastrada) > Val(Me.lblM3.Text) Then
                        Me.msgErroNF.Text = "Você informou um Volume(m³) superior ao Volume informado na Carga Solta (" & String.Format("{0:N2}", Convert.ToDouble(Replace(Replace(Me.lblM3.Text, ".", ""), ",", "."))) & "m³)."
                        Me.AccordionIndex.Value = 2
                        Me.txtM3.Focus()
                        Return False
                    End If

                End If

            End If

            If Convert.ToInt32(Nnull(DsDes.Rows(0)("FLAG_CS_PESO").ToString(), 0)) > 0 Then

                If Nnull(Me.lblPesoBruto.Text, 0) <> 0 Then

                    If String.IsNullOrEmpty(Me.txtPesoBruto.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtPesoBruto.Text.Trim()) Or Nnull(Me.txtPesoBruto.Text.Trim(), 0) = 0 Then
                        Me.msgErroNF.Text = "O campo Peso Bruto é obrigatório.');"
                        Me.AccordionIndex.Value = 2
                        Me.txtPesoBruto.Focus()
                        Return False
                    End If

                    If Not IsNumeric(Nnull(Me.txtPesoBruto.Text, 0)) Then
                        Me.msgErroNF.Text = "O campo Peso deve ser um valor Numérico.');"
                        Me.AccordionIndex.Value = 2
                        Me.txtPesoBruto.Focus()
                        Return False
                    End If

                    If Val(Me.txtPesoBruto.Text) + Val(QuantidadePesoCadastrada) > Val(Me.lblPesoBruto.Text) Then
                        Me.msgErroNF.Text = "Você informou um valor superior ao Peso Bruto informado na Carga Solta (" & String.Format("{0:N2}", Convert.ToDouble(Me.lblPesoBruto.Text)) & "Kg).')"
                        Me.AccordionIndex.Value = 2
                        Me.txtPesoBruto.Focus()
                        Return False
                    End If

                End If

            End If

            'If Convert.ToInt32(Nnull(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString(), 0)) > 0 Then

            If Not IsNumeric(Nnull(Me.txtQtde.Text, 0)) Then
                Me.msgErroNF.Text = "O campo Quantidade (Qtde) da NF é um valor Numérico."
                Me.AccordionIndex.Value = 2
                Me.txtQtde.Focus()
                Return False
            Else

                If Not Val(Me.txtQtde.Text.Trim()) > 0 Then
                    Me.msgErroNF.Text = "O campo Quantidade (Qtde) da NF é obrigatório."
                    Me.AccordionIndex.Value = 2
                    Me.txtQtde.Focus()
                    Return False
                End If

                ConsultarQuantidades()
                '
                If (Val(Me.txtQtde.Text) + Val(QuantidadeCadastrada)) > (Nnull(Me.lblTotal.Text, 0) - Val(QuantidadeRegistrada)) Then
                    Me.msgErroNF.Text = "Você informou uma Quantidade de Volumes superior ao limite disponível da carga"
                    Me.AccordionIndex.Value = 2
                    Me.txtQtde.Focus()
                    Return False
                End If

            End If

            'End If

            'End If

        Else

            If Me.txtDANFE.Text.Length <> 44 Then
                Me.msgErroNF.Text = "DANFE inválido"
                Me.AccordionIndex.Value = 2
                Me.txtDANFE.Focus()
                Return False
            End If

            If Not IsNumeric(Me.txtDANFE.Text) Then
                Me.msgErroNF.Text = "O campo DANFE não é um valor Numérico"
                Me.AccordionIndex.Value = 2
                Me.txtDANFE.Focus()
                Return False
            End If

        End If

        Return True

    End Function

    Private Function ExisteDuplicidadeNF(ByVal CodigoAgendamento As String, ByVal CodigoNF As String) As Boolean

        Dim SQL As New StringBuilder

        SQL.Append("SELECT COUNT(1) FROM ")
        SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
        SQL.Append("  WHERE AUTONUM > 0 ")

        If String.IsNullOrEmpty(Me.txtDANFE.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtDANFE.Text.Trim()) Then
            SQL.Append("    AND NUMERO = " & Nnull(Me.txtNumero.Text, 0) & " ")
            SQL.Append("    AND SERIE = '" & Me.txtSerie.Text & "' ")
            SQL.Append("    AND EMISSOR = '" & Me.txtEmissor.Text & "' ")
            SQL.Append("    AND EMISSAO = TO_DATE('" & Me.txtEmissao.Text & "','DD/MM/YYYY') ")
            SQL.Append("    AND QTDE = " & Nnull(Me.txtQtde.Text, 0) & " ")
            SQL.Append("    AND VALOR = " & Nnull(Replace(Replace(Me.txtValor.Text, ".", ""), ",", "."), 0) & " ")
            SQL.Append("    AND PESO_BRUTO = " & Nnull(Replace(Replace(Me.txtPesoBruto.Text, ".", ""), ",", "."), 0) & " ")
            SQL.Append("    AND M3 = " & Nnull(Replace(Replace(Me.txtM3.Text, ".", ""), ",", "."), 0) & " ")
        Else
            SQL.Append("    AND DANFE = '" & Me.txtDANFE.Text & "' ")
        End If

        SQL.Append("    AND AUTONUM_AGENDAMENTO = " & CodigoAgendamento)

        If Not String.IsNullOrEmpty(CodigoNF) And
            Not String.IsNullOrWhiteSpace(CodigoNF) Then
            SQL.Append("  AND AUTONUM <> " & CodigoNF)
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(SQL.ToString())) > 0 Then
            Return True
        End If

        Return False

    End Function

    Protected Sub btnSalvarNF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvarNF.Click

        Me.AccordionIndex.Value = 2

        Dim SQL As New StringBuilder


        If Not ValidarCamposNF() Then
            Me.msgErroNF.Visible = True
        End If



        If Me.lblCodigoNF.Text = String.Empty Then

            If String.IsNullOrEmpty(Me.txtXml.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Arquivo XML de DANFE não informado. Selecione o Arquivo e clique em UPLOAD!');", True)
                Exit Sub
            End If

            If ExisteDuplicidadeNF(Me.lblCodigoAgendamento.Text, String.Empty) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Já existe uma outra NF cadastrada com os mesmos dados.');", True)
                Exit Sub
            End If

            SQL.Append("INSERT INTO ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
            SQL.Append("  ( ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    AUTONUM_AGENDAMENTO, ")
            SQL.Append("    DANFE, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    NUMERO, ")
            SQL.Append("    SERIE, ")
            SQL.Append("    EMISSOR, ")
            SQL.Append("    EMISSAO, ")
            SQL.Append("    QTDE, ")
            SQL.Append("    VALOR, ")
            SQL.Append("    PESO_BRUTO, ")
            SQL.Append("    M3, ")
            SQL.Append("    ARQUIVO_DANFE")
            SQL.Append("  ) ")
            SQL.Append("  VALUES ")
            SQL.Append("  ( ")
            SQL.Append("    REDEX.SEQ_AGENDAMENTO_WEB_CS_NF.NEXTVAL, ")
            SQL.Append("    :CodigoAgendamento, ")
            SQL.Append("    :Danfe, ")
            SQL.Append("    :Reserva, ")
            SQL.Append("    :Numero, ")
            SQL.Append("    :Serie, ")
            SQL.Append("    :Emissor, ")
            SQL.Append("    :Emissao, ")
            SQL.Append("    :Qtde, ")
            SQL.Append("    :Valor, ")
            SQL.Append("    :PesoBruto, ")
            SQL.Append("    :M3, ")
            SQL.Append("    :Xml ")
            SQL.Append("  ) ")

            Using Con As New OleDbConnection(Banco.ConnectionString())
                Using Cmd As New OleDbCommand(SQL.ToString(), Con)

                    Cmd.Parameters.Add(New OleDbParameter("CodigoAgendamento", Me.lblCodigoAgendamento.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Danfe", Me.txtDANFE.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Reserva", Me.txtReserva.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Numero", Me.txtNumero.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Serie", Me.txtSerie.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissor", Me.txtEmissor.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissao", Convert.ToDateTime(Me.txtEmissao.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Qtde", Me.txtQtde.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Valor", PPonto(Me.txtValor.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("PesoBruto", PPonto(Me.txtPesoBruto.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("M3", PPonto(Me.txtM3.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Xml", Me.txtXml.Text))

                    Con.Open()

                    Try
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Me.msgErroNF.Text = ex.Message
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Cadastrar a NF. Tente Novamente.');", True)
                    End Try

                End Using
            End Using

        Else

            If ExisteDuplicidadeNF(Me.lblCodigoAgendamento.Text, Me.lblCodigoNF.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Já existe uma outra NF cadastrada com os mesmos dados.');", True)
                Exit Sub
            End If

            SQL.Append("UPDATE ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_NF ")
            SQL.Append("  SET ")
            SQL.Append("    NUMERO = :Numero, ")
            SQL.Append("    SERIE = :Serie, ")
            SQL.Append("    EMISSOR = :Emissor, ")
            SQL.Append("    EMISSAO = :Emissao, ")
            SQL.Append("    QTDE = :Qtde, ")
            SQL.Append("    VALOR = :Valor, ")
            SQL.Append("    PESO_BRUTO = :PesoBruto, ")
            SQL.Append("    M3 = :M3, ")
            SQL.Append("    Danfe = :Danfe, ")
            SQL.Append("    Arquivo_Danfe = :Xml ")
            SQL.Append("  WHERE AUTONUM = :CodigoNF")

            Using Con As New OleDbConnection(Banco.ConnectionString())
                Using Cmd As New OleDbCommand(SQL.ToString(), Con)

                    Cmd.Parameters.Add(New OleDbParameter("Numero", Me.txtNumero.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Serie", Me.txtSerie.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissor", Me.txtEmissor.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissao", Convert.ToDateTime(Me.txtEmissao.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Qtde", Me.txtQtde.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Valor", PPonto(Me.txtValor.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("PesoBruto", PPonto(Me.txtPesoBruto.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("M3", PPonto(Me.txtM3.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Danfe", Me.txtDANFE.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Xml", Me.txtXml.Text))
                    Cmd.Parameters.Add(New OleDbParameter("CodigoNF", Me.lblCodigoNF.Text))

                    Con.Open()

                    Try
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Me.msgErroNF.Text = ex.Message
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao atualizar a NF. Tente Novamente.');", True)
                    End Try

                End Using
            End Using

        End If

        ConsultarNF(Me.lblCodigoAgendamento.Text)
        ConsultarPeriodos()
        VerificaDadosParceiro()

        If Me.dgConsultaNF.Rows.Count > 0 Then
            Me.btnCancelarNF.Enabled = True
        Else
            Me.btnCancelarNF.Enabled = False
        End If

        Me.lblCodigoNF.Text = String.Empty
        Me.lblQuantidadeSelecionadaNF.Text = String.Empty
        Me.lblQuantidadeSelecionadaNF.Text = String.Empty
        Me.lblM3SelecionadaNF.Text = String.Empty
        Me.lblPesoSelecionadaNF.Text = String.Empty

        Me.AccordionIndex.Value = 3

    End Sub

    Private Sub ConsultarNF(ByVal CodigoAgendamento As String)

        If Not String.IsNullOrEmpty(CodigoAgendamento) Or Not String.IsNullOrWhiteSpace(CodigoAgendamento) Then
            Me.dgConsultaNF.DataSource = Banco.List("SELECT AUTONUM,RESERVA,DANFE,NUMERO,SERIE,EMISSOR,TO_CHAR(EMISSAO,'DD/MM/YYYY') EMISSAO,QTDE,VALOR,PESO_BRUTO,M3,NVL(ARQUIVO_DANFE, 'X') ARQUIVO_DANFE  FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF WHERE AUTONUM_AGENDAMENTO = " & CodigoAgendamento)
            Me.dgConsultaNF.DataBind()
        End If

    End Sub

    Protected Sub dgConsultaNF_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaNF.RowCommand

        Me.AccordionIndex.Value = 2

        If Me.dgConsultaNF.Rows.Count > 0 Then

            Dim Index As Integer = e.CommandArgument
            Dim ID As String = Me.dgConsultaNF.DataKeys(Index)("AUTONUM").ToString()

            If e.CommandName = "DEL" Then
                If Not String.IsNullOrEmpty(ID) And
                    Not String.IsNullOrWhiteSpace(ID) Then
                    If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF WHERE AUTONUM = " & ID) Then

                        ConsultarNF(Me.lblCodigoAgendamento.Text)
                        ConsultarPeriodos()

                        Me.lblQuantidadeSelecionadaNF.Text = String.Empty
                        Me.lblM3SelecionadaNF.Text = String.Empty
                        Me.lblPesoSelecionadaNF.Text = String.Empty

                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Excluir a NF. Tente Novamente.');", True)
                    End If
                End If
            End If

            If e.CommandName = "EDITAR" Then

                If Not String.IsNullOrEmpty(Me.lblCodigoAgendamento.Text.Trim()) And
                    Not String.IsNullOrWhiteSpace(Me.lblCodigoAgendamento.Text.Trim()) Then

                    Dim Ds As New DataTable
                    Ds = Banco.List("SELECT AUTONUM,DANFE,NUMERO,SERIE,EMISSOR,TO_CHAR(EMISSAO,'DD/MM/YYYY') EMISSAO,QTDE,VALOR,PESO_BRUTO,M3,ARQUIVO_DANFE FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF WHERE AUTONUM = " & ID)

                    If Ds IsNot Nothing Then
                        If Ds.Rows.Count > 0 Then

                            Me.lblCodigoNF.Text = Ds.Rows(0)("AUTONUM").ToString()
                            Me.txtDANFE.Text = Ds.Rows(0)("DANFE").ToString()
                            Me.txtNumero.Text = Ds.Rows(0)("NUMERO").ToString()
                            Me.txtSerie.Text = Ds.Rows(0)("SERIE").ToString()
                            Me.txtEmissor.Text = Ds.Rows(0)("EMISSOR").ToString()
                            Me.txtEmissao.Text = Ds.Rows(0)("EMISSAO").ToString()
                            Me.txtQtde.Text = Ds.Rows(0)("QTDE").ToString()
                            Me.txtXml.Text = Ds.Rows(0)("ARQUIVO_DANFE").ToString()
                            Me.txtValor.Text = Replace(Replace(Nnull(Ds.Rows(0)("VALOR").ToString(), 0), ".", ""), ",", ".")
                            Me.txtPesoBruto.Text = Replace(Replace(Nnull(Ds.Rows(0)("PESO_BRUTO").ToString(), 0), ".", ""), ",", ".")
                            Me.txtM3.Text = Replace(Replace(Nnull(Ds.Rows(0)("M3").ToString(), 0), ".", ""), ",", ".")

                            Me.lblQuantidadeSelecionadaNF.Text = Me.txtQtde.Text
                            Me.lblM3SelecionadaNF.Text = Me.txtM3.Text
                            Me.lblPesoSelecionadaNF.Text = Me.txtPesoBruto.Text

                        End If
                    End If

                End If
            End If

        End If

    End Sub

    Protected Sub btnConcluirNF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConcluirNF.Click
        Me.AccordionIndex.Value = 3
    End Sub

    Protected Sub btnCancelarNF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelarNF.Click

        Me.lblCodigoNF.Text = String.Empty
        Me.txtDANFE.Text = String.Empty
        Me.txtNumero.Text = String.Empty
        Me.txtSerie.Text = String.Empty
        Me.txtEmissor.Text = String.Empty
        Me.txtEmissao.Text = String.Empty
        Me.txtQtde.Text = String.Empty
        Me.txtValor.Text = String.Empty
        Me.txtPesoBruto.Text = String.Empty
        Me.txtM3.Text = String.Empty
        Me.lblQuantidadeSelecionadaNF.Text = String.Empty

    End Sub

    Private Sub HabilitaCamposNF(ByVal Habilita As Boolean)

        If Habilita Then

            Me.txtPesoBruto.Enabled = True
            Me.txtM3.Enabled = True
            Me.btnSalvarNF.Enabled = True
            Me.btnConcluirNF.Enabled = True
            Me.btnUploadNota.Enabled = True
            Me.txtArquivoDanfe.Enabled = True

            Dim DsDes As New DataTable
            DsDes = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_DES")

            If DsDes IsNot Nothing Then
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) = 0 Then
                    'Me.txtM3.Enabled = False
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) = 0 Then
                    'Me.txtPesoBruto.Enabled = False
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) = 0 Then
                    'Me.txtQtde.Enabled = False
                End If
            End If

        Else
            Me.txtDANFE.Enabled = False
            Me.txtNumero.Enabled = False
            Me.txtSerie.Enabled = False
            Me.txtEmissor.Enabled = False
            Me.txtEmissao.Enabled = False
            Me.txtQtde.Enabled = False
            Me.txtValor.Enabled = False
            Me.txtPesoBruto.Enabled = False
            Me.txtM3.Enabled = False
            Me.btnSalvarNF.Enabled = False
            Me.btnConcluirNF.Enabled = False
        End If

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click

        If Me.lblCodigoAgendamento.Text <> String.Empty Then
            If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text) Then
                Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento Excluído com Sucesso!','ConsultarAgendamentosCargaSolta.aspx');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='AgendarCS.aspx';", True)
        End If

    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSair.Click

        If Request.QueryString("id") IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
        Else
            If Me.lblCodigoAgendamento.Text <> String.Empty Then
                If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text) Then
                    Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
            End If
        End If

    End Sub



    Public Sub ConsultarPeriodos()

        Dim SQL As New StringBuilder
        Dim DsDes As New DataTable
        Dim sSql As String
        Dim Contar As Integer

        If Me.lblCodigoBooking.Text > "" Then

            sSql = "Select count(1) contar From redex.TB_RESERVA_PERIODO WHERE AUTONUM_BOO=" + Me.lblCodigoBooking.Text
            DsDes = Banco.List(sSql)
            Contar = Convert.ToInt32(DsDes.Rows(0)("contar").ToString())

            sSql = "SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM Redex.TB_AGENDAMENTO_WEB_PERIODO_DES "
            sSql = sSql & "  WHERE PATIO =" & Val(Me.lblCodigoPatio.Text)
            DsDes = Banco.List(sSql)

            SQL.Append(" SELECT DISTINCT AUTONUM_GD_RESERVA, PERIODO_INICIAL, PERIODO_FINAL, ")
            SQL.Append(" CASE WHEN LIMITE_PESO < 0 THEN 0 ELSE LIMITE_PESO END LIMITE_PESO,")
            SQL.Append(" CASE WHEN LIMITE_VOLUMES < 0 THEN 0 ELSE LIMITE_VOLUMES END LIMITE_VOLUMES,")
            SQL.Append(" CASE WHEN LIMITE_M3 < 0 THEN 0 ELSE LIMITE_M3 END LIMITE_M3,")
            SQL.Append(" CASE WHEN LIMITE_CAMINHOES < 0 THEN 0 ELSE LIMITE_CAMINHOES END LIMITE_CAMINHOES,PERIODO_ORDER")
            SQL.Append(" FROM (")
            SQL.Append("  SELECT A.AUTONUM_GD_RESERVA,A.PERIODO_INICIAL PERIODO_ORDER, ")
            SQL.Append("  TO_CHAR(A.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
            SQL.Append("  TO_CHAR(A.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
            If DsDes.Rows.Count > 0 Then
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                    SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_M3,0)        end   - NVL(AG.M3,0) LIMITE_M3, ")
                Else
                    SQL.Append("   0 LIMITE_M3, ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                    SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_PESO,0)      end   - NVL(AG.PESO,0)  LIMITE_PESO, ")
                Else
                    SQL.Append(" 0  LIMITE_PESO, ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                    SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_VOLUMES,0)   end   - NVL(AG.QTD,0) LIMITE_VOLUMES,  ")
                Else
                    SQL.Append("  0 LIMITE_VOLUMES,  ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                    SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_CAMINHOES,0) end   - NVL(CAM.QTD,0) LIMITE_CAMINHOES  ")
                Else
                    SQL.Append("  0 LIMITE_CAMINHOES  ")
                End If
            Else
                SQL.Append("  NVL(A.LIMITE_M3,0)       - NVL(AG.M3,0) LIMITE_M3, ")
                SQL.Append("  NVL(A.LIMITE_PESO,0)     - NVL(AG.PESO,0)  LIMITE_PESO, ")
                SQL.Append("  NVL(A.LIMITE_VOLUMES,0)  - NVL(AG.QTD,0) LIMITE_VOLUMES,  ")
                SQL.Append("  case when NVL(rl.limite,0)>0 then rl.limite else NVL(A.LIMITE_CAMINHOES,0) end   - NVL(CAM.QTD,0) LIMITE_CAMINHOES  ")
            End If
            SQL.Append("  FROM Redex.TB_GD_RESERVA A ")
            SQL.Append("   LEFT JOIN ( Select * From redex.TB_RESERVA_PERIODO WHERE AUTONUM_BOO=" + Me.lblCodigoBooking.Text + ") RL ON RL.AUTONUM_GD_RESERVA=A.AUTONUM_GD_RESERVA ")
            SQL.Append("  LEFT JOIN (")
            SQL.Append("    SELECT NVL(SUM(QTDE),0) AS QTD, NVL(SUM(M3),0) M3, NVL(SUM(PESO_BRUTO),0) PESO, AUTONUM_GD_RESERVA ")
            SQL.Append("    FROM Redex.TB_AGENDAMENTO_WEB_CS_NF ")
            SQL.Append("    LEFT JOIN Redex.TB_AGENDAMENTO_WEB_CS ON TB_AGENDAMENTO_WEB_CS_NF.AUTONUM_AGENDAMENTO = TB_AGENDAMENTO_WEB_CS.AUTONUM ")
            If Contar > 0 Then
                SQL.Append(" WHERE AUTONUM_BOOKING =" + Me.lblCodigoBooking.Text)
            End If
            SQL.Append("    GROUP BY AUTONUM_GD_RESERVA")
            SQL.Append("  ) AG On A.AUTONUM_GD_RESERVA = AG.AUTONUM_GD_RESERVA")
            SQL.Append("  LEFT JOIN (")
            Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
            If Me.lblCodigoVeiculo.Text > "0" Then
                SQL.Append(" Select Case when max(NVL(cm.qtde,0))=0 then SUM(1) else ")
                SQL.Append(" 0 End QTD, cs.AUTONUM_GD_RESERVA  ")
                SQL.Append(" From redex.TB_AGENDAMENTO_WEB_CS  cs ")
                SQL.Append(" Left Join( select distinct 1 qtde , AUTONUM_GD_RESERVA from ")
                SQL.Append(" redex.TB_AGENDAMENTO_WEB_CS where ")
                SQL.Append(" AUTONUM_VEICULO = " & Me.lblCodigoVeiculo.Text & " And AUTONUM_GD_RESERVA > 0)  cm ")
                SQL.Append(" On cs.AUTONUM_GD_RESERVA=cm.AUTONUM_GD_RESERVA ")
                SQL.Append(" Group BY CS.AUTONUM_GD_RESERVA   ")
            Else
                SQL.Append("    Select SUM(1) QTD, AUTONUM_GD_RESERVA")
                SQL.Append("    FROM  Redex.TB_AGENDAMENTO_WEB_CS")
                SQL.Append("    GROUP BY AUTONUM_GD_RESERVA")
            End If
            SQL.Append("  ) CAM ")
            SQL.Append("   On A.AUTONUM_GD_RESERVA = CAM.AUTONUM_GD_RESERVA")
            SQL.Append(" WHERE  A.SERVICO_GATE = 'A' AND A.TIPO = 'CS' AND TO_CHAR(A.PERIODO_INICIAL,'YYYYMMDDHH24MI') >= TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') ")
            If Val(Me.lblCodigoPatio.Text) <> 0 Then
                SQL.Append("  AND A.PATIO = " & Val(Me.lblCodigoPatio.Text))
            Else
                SQL.Append(" AND A.PATIO = 9999 ")
            End If

            Dim HoraAtual = DateTime.Now.ToString("yyyyMMdd")
            SQL.Append("    AND TO_CHAR(A.periodo_inicial,'YYYYMMDD HH24:MI:SS') > '" & HoraAtual & " 23:59:59' ")


            SQL.Append("  ) R ")
            SQL.Append("  WHERE AUTONUM_GD_RESERVA > 0 ")

            If DsDes.Rows.Count > 0 Then
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                    SQL.Append(" AND LIMITE_M3 > 0 ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                    SQL.Append(" AND LIMITE_PESO > 0 ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                    SQL.Append(" AND LIMITE_VOLUMES > 0 ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                    SQL.Append(" AND LIMITE_CAMINHOES > 0 ")
                End If
                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) = 0 And Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) = 0 And Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) = 0 And Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) = 0 Then
                    SQL.Append(" AND LIMITE_CAMINHOES > 0 ")
                End If
            End If
            SQL.Append(" AND (LIMITE_M3 > 0 or LIMITE_PESO > 0 or LIMITE_VOLUMES > 0 or LIMITE_CAMINHOES > 0 ) ")

            SQL.Append("ORDER BY TO_DATE(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') ")
            Me.dgConsultaPeriodos.DataSource = Banco.List(SQL.ToString())
            Me.dgConsultaPeriodos.DataBind()
        Else
            SQL.Clear()
        End If
    End Sub


    Protected Sub dgConsultaPeriodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaPeriodos.RowCommand

        Dim SQL As New StringBuilder

        Me.AccordionIndex.Value = 3

        If Nnull(Me.lblCodigoAgendamento.Text, 0) = 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O agendamento ainda não foi salvo. Clique no Botão Salvar.','');", True)
            Exit Sub
        End If

        If Me.dgConsultaPeriodos.Rows.Count > 0 Then

            Dim ID As String = Me.dgConsultaPeriodos.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("AUTONUM_GD_RESERVA").ToString()

            Dim LimitePesoPeriodo As Decimal = 0.0
            Dim LimiteM3Periodo As Decimal = 0.0
            Dim LimiteVolumesPeriodo As Decimal = 0.0

            Dim QuantidadeVolumeCadastrada As Decimal = 0.0
            Dim QuantidadePesoCadastrada As Decimal = 0.0
            Dim QuantidadeM3Cadastrada As Decimal = 0.0

            Dim ds As New DataTable
            ds = Banco.List("SELECT NVL(LIMITE_PESO,0) LIMITE_PESO,NVL(LIMITE_VOLUMES,0) LIMITE_VOLUMES,NVL(LIMITE_M3,0) LIMITE_M3 FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA = " & ID)

            If ds IsNot Nothing Then
                If ds.Rows.Count > 0 Then
                    LimitePesoPeriodo = Math.Round(Convert.ToDecimal(ds.Rows(0)("LIMITE_PESO").ToString()), 2)
                    LimiteM3Periodo = Math.Round(Convert.ToDecimal(ds.Rows(0)("LIMITE_M3").ToString()), 2)
                    LimiteVolumesPeriodo = Math.Round(Convert.ToDecimal(ds.Rows(0)("LIMITE_VOLUMES").ToString()), 2)
                End If
            End If

            Dim Periodo As String = String.Empty

            If e.CommandName = "SEL" Then
                If Not String.IsNullOrEmpty(ID) And Not String.IsNullOrWhiteSpace(ID) Then

                    ds = Banco.List("SELECT NVL(QTDE,0) QTDE, NVL(PESO_BRUTO,0) PESO_BRUTO,NVL(M3,0) M3 FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)

                    If ds IsNot Nothing Then
                        If ds.Rows.Count > 0 Then

                            QuantidadeVolumeCadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("QTDE").ToString()), 2)
                            QuantidadePesoCadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("PESO_BRUTO").ToString()), 2)
                            QuantidadeM3Cadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("M3").ToString()), 2)

                            Dim DsDes As New DataTable
                            DsDes = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_DES")

                            If DsDes IsNot Nothing Then

                                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                                    If Math.Round(Nnull(QuantidadeM3Cadastrada, 0), 2) > Math.Round(Convert.ToDecimal(Nnull(LimiteM3Periodo, 0)), 2) Then
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Volume informado nas Notas Fiscais (" & QuantidadeM3Cadastrada & "m³) ultrapassa o limite de Peso do Período (" & LimiteM3Periodo & "m³).');", True)
                                        Exit Sub
                                    End If
                                End If

                                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                                    If Math.Round(Nnull(QuantidadePesoCadastrada, 0), 2) > Math.Round(Convert.ToDecimal(Nnull(LimitePesoPeriodo, 0)), 2) Then
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Peso Bruto informado nas Notas Fiscais (" & QuantidadePesoCadastrada & "Kg) ultrapassa o limite de Peso do Período (" & LimitePesoPeriodo & "Kg).');", True)
                                        Exit Sub
                                    End If
                                End If

                                If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                                    If Math.Round(Nnull(QuantidadeVolumeCadastrada, 0), 2) > Math.Round(Convert.ToDecimal(Nnull(LimiteVolumesPeriodo, 0)), 2) Then
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Volume informado nas Notas Fiscais (" & QuantidadeVolumeCadastrada & ") ultrapassa o limite de Volumes do Período (" & LimiteVolumesPeriodo & ").');", True)
                                        Exit Sub
                                    End If
                                End If
                            End If

                        End If
                    End If

                    Me.lblCodigoPeriodo.Text = ID
                    Periodo = Banco.ExecuteScalar("SELECT TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA = " & ID)

                    For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                        Linha.BackColor = Drawing.Color.Transparent
                    Next

                    For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                        If Linha.Cells(1).Text & " - " & Linha.Cells(2).Text = Periodo Then
                            Linha.BackColor = Drawing.Color.MistyRose
                        End If
                    Next

                    If Request.QueryString("id") IsNot Nothing Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Você escolheu o Período: " & Periodo & ". Clique no Botão Salvar para concluir o agendamento.');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Você escolheu o Período: " & Periodo & ". Clique no Botão Concluir para terminar o agendamento.');", True)
                    End If

                End If
            End If

        End If

    End Sub

    Protected Sub btnUploadNota_Click(sender As Object, e As EventArgs) Handles btnUploadNota.Click

        Me.AccordionIndex.Value = 2

        msgSucessoDanfe.Text = ""
        msgErroNF.Text = ""

        msgSucessoDanfe.Visible = False
        msgErroNF.Visible = False

        Me.msgErroNF.Visible = False
        Me.msgSucessoDanfe.Visible = False

        If txtArquivoDanfe.HasFile Then

            If Not txtArquivoDanfe.FileName.ToUpper().EndsWith("XML") Then

                Me.msgErroNF.Text = "Tipo de arquivo não permitido"
                Me.msgErroNF.Visible = True

                Exit Sub

            End If

            If txtArquivoDanfe.PostedFile.ContentType <> "text/xml" Then
                Me.msgErroNF.Text = "O arquivo informado não é um XML válido"
                Me.msgErroNF.Visible = True

                Exit Sub

            End If

            Dim NomeArquivo As String = String.Empty
            Dim TamanhoArquivo As Integer = 0

            If txtArquivoDanfe.HasFile Then

                NomeArquivo = Path.GetFileName(Me.txtArquivoDanfe.PostedFile.FileName)
                TamanhoArquivo = Me.txtArquivoDanfe.PostedFile.ContentLength

                If TamanhoArquivo > 5242880 Then

                    Me.msgErroNF.Text = "O tamanho do arquivo ultrapassa 5MB"
                    Me.msgErroNF.Visible = True

                    Exit Sub

                End If

                Try

                    Dim doc As New XmlDocument()
                    doc.Load(Me.txtArquivoDanfe.PostedFile.InputStream)

                    Dim ns As New XmlNamespaceManager(doc.NameTable)
                    ns.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe")

                    txtXml.Text = doc.InnerXml

                    Dim dadosDanfe = doc.SelectNodes("//nfe:infProt", ns)

                    For Each dadoDanfe As XmlNode In dadosDanfe
                        For Each childNode As XmlNode In dadoDanfe.ChildNodes
                            If childNode.Name = "chNFe" Then
                                Me.txtDANFE.Text = childNode.InnerText
                            End If
                        Next
                    Next

                    If Me.txtDANFE.Text.Length = 0 Then
                        Dim chaveDanfe = doc.SelectSingleNode("//nfe:infNFe", ns)
                        If chaveDanfe.Attributes.Count = 2 Then
                            Me.txtDANFE.Text = chaveDanfe.Attributes.GetNamedItem("Id").Value

                            If Me.txtDANFE.Text.Contains("NFe") Then
                                Me.txtDANFE.Text = Me.txtDANFE.Text.Replace("NFe", "")
                            End If
                        End If
                    End If

                    If Me.txtDANFE.Text.Length <> 44 Then

                        Me.msgErroNF.Text = "Danfe inválida"
                        Me.msgErroNF.Visible = True

                        Exit Sub

                    End If

                    Dim dadosEmit = doc.SelectNodes("//nfe:emit", ns)

                    For Each dadoEmit As XmlNode In dadosEmit
                        For Each childNode As XmlNode In dadoEmit.ChildNodes

                            If childNode.Name = "xNome" Then
                                Me.txtEmissor.Text = childNode.InnerText
                                Me.xNome.Value = childNode.InnerText

                            End If
                            If childNode.Name = "CNPJ" Then
                                'Me.txtCNPJResponsavel.Text = childNode.InnerText
                                Me.xCNPJ.Value = childNode.InnerText
                            End If

                            If childNode.Name = "xFant" Then
                                Me.xFant.Value = childNode.InnerText
                            End If



                            If childNode.Name = "IE" Then
                                Me.xIE.Value = childNode.InnerText
                            End If

                            If childNode.Name = "IM" Then
                                Me.xIM.Value = childNode.InnerText
                            End If

                        Next
                    Next

                    Dim dadosEnderEmit = doc.SelectNodes("//nfe:enderEmit", ns)

                    For Each dadoEnderEmit As XmlNode In dadosEnderEmit
                        For Each childNode As XmlNode In dadoEnderEmit.ChildNodes

                            If childNode.Name = "xLgr" Then
                                Me.xLgr.Value = childNode.InnerText
                            End If

                            If childNode.Name = "nro" Then
                                Me.nro.Value = childNode.InnerText
                            End If

                            If childNode.Name = "xBairro" Then
                                Me.xBairro.Value = childNode.InnerText
                            End If

                            If childNode.Name = "xMun" Then
                                Me.xMun.Value = childNode.InnerText
                            End If

                            If childNode.Name = "UF" Then
                                Me.xUF.Value = childNode.InnerText
                            End If

                            If childNode.Name = "CEP" Then
                                Me.xCEP.Value = childNode.InnerText
                            End If

                            If childNode.Name = "xPais" Then
                                Me.xPais.Value = childNode.InnerText
                            End If

                            If childNode.Name = "fone" Then
                                Me.xFone.Value = childNode.InnerText
                            End If

                        Next
                    Next

                    Dim dadosNota = doc.SelectNodes("//nfe:ide", ns)

                    For Each dadoNfe As XmlNode In dadosNota
                        For Each childNode As XmlNode In dadoNfe.ChildNodes
                            If childNode.Name = "nNF" Then
                                Me.txtNumero.Text = childNode.InnerText
                            End If
                            If childNode.Name = "serie" Then
                                Me.txtSerie.Text = childNode.InnerText
                            End If
                            If childNode.Name = "dhEmi" Then
                                Me.txtEmissao.Text = Convert.ToDateTime(childNode.InnerText).AddHours(3).ToString("dd/MM/yyyy")
                            End If
                        Next
                    Next

                    Dim dadosProduto = doc.SelectNodes("//nfe:prod", ns)

                    Dim SomaQuantidade As Decimal
                    Dim SomaValor As Decimal

                    For Each dadoProd As XmlNode In dadosProduto
                        For Each childNode As XmlNode In dadoProd.ChildNodes
                            If childNode.Name = "vProd" Then
                                SomaValor = SomaValor + Convert.ToDecimal(childNode.InnerText.Replace(".", ","))
                            End If
                            If childNode.Name = "qTrib" Then
                                'SomaQuantidade = SomaQuantidade + Convert.ToDecimal(childNode.InnerText.Replace(".", ","))
                            End If
                        Next
                    Next

                    Me.txtValor.Text = SomaValor
                    Me.txtQtde.Text = SomaQuantidade

                    Dim dadosVolumes = doc.SelectNodes("//nfe:transp", ns)

                    For Each dadoVolume As XmlNode In dadosVolumes
                        For Each childNode As XmlNode In dadoVolume.ChildNodes
                            If childNode.Name = "vol" Then

                                For Each tag As XmlNode In childNode.ChildNodes
                                    If tag.Name = "qVol" Then
                                        Me.txtQtde.Text = tag.InnerText
                                    End If
                                    If tag.Name = "pesoB" Then
                                        Me.txtPesoBruto.Text = tag.InnerText
                                    End If
                                Next

                            End If
                        Next
                    Next

                    Me.txtCNPJResponsavel.Enabled = True
                    Me.txtEmailResponsavel.Enabled = True

                Catch ex As Exception
                    Me.msgErroNF.Text = ex.Message
                    Me.msgErroNF.Visible = True
                End Try

            End If

        End If

    End Sub
    Private Sub VerificaDadosParceiro()

        Dim CnpjFormat = mascaraCNPJ_CPF(Me.xCNPJ.Value)
        Dim idParceiroNextVal As Integer
        Dim strNomeParceiroNextVal As String

        If Convert.ToInt32(Banco.ExecuteScalar("Select COUNT(1) FROM REDEX.TB_CAD_PARCEIROS WHERE CGC = '" & CnpjFormat & "' ")) = 1 Then


            Dim SQLParceiro As New StringBuilder

            SQLParceiro.Append("SELECT AUTONUM FROM REDEX.TB_Cad_Parceiros WHERE CGC = '" & CnpjFormat & "' ")

            Dim DsParceiro As New DataTable
            DsParceiro = Banco.List(SQLParceiro.ToString())

            If DsParceiro IsNot Nothing Then
                If DsParceiro.Rows.Count > 0 Then
                    Me.hiddenText.Value = DsParceiro.Rows(0)("AUTONUM")

                    Dim sb As New StringBuilder
                    sb.AppendLine(" UPDATE REDEX.TB_BOOKING SET AUTONUM_EXPORTADOR = '" & DsParceiro.Rows(0)("AUTONUM") & "' WHERE  AUTONUM_BOO =  " & Me.lblCodigoBooking.Text)
                    'lembrar de colocar o replace para substituir as aspas simples.

                    If Banco.BeginTransaction(sb.ToString()) Then
                        Me.txtCNPJResponsavel.Text = Me.xNome.Value & "-" & mascaraCNPJ_CPF(Me.xCNPJ.Value)

                        Me.txtCNPJResponsavel.Enabled = True
                        Me.txtEmailResponsavel.Enabled = True
                    Else

                    End If
                End If
            End If
        Else
            Dim sb As New StringBuilder
            Dim sb1 As New StringBuilder

            idParceiroNextVal = Convert.ToString(Banco.ExecuteScalar(" SELECT REDEX.SEQ_TB_CAD_PARCEIROS.NEXTVAL from dual "))

            strNomeParceiroNextVal = Me.xNome.Value

            sb.Append(" INSERT INTO REDEX.TB_CAD_PARCEIROS  ")
            sb.Append(" ( ")
            sb.Append(" AUTONUM, ")
            sb.Append(" RAZAO, ")
            sb.Append(" FANTASIA, ")
            sb.Append(" CGC, ")
            sb.Append(" FONE1, ")
            sb.Append(" LOGRADOURO, ")
            sb.Append(" BAIRRO, ")
            sb.Append(" CIDADE, ")
            sb.Append(" ESTADO,  ")
            sb.Append(" PAIS, ")
            sb.Append(" IE, ")
            sb.Append(" IM ")
            sb.Append(" ) ")
            sb.Append(" VALUES ")
            sb.Append(" ( ")
            sb.Append(" " & idParceiroNextVal & " , ")
            sb.Append(" '" & Me.xNome.Value & "', ")
            sb.Append(" '" & Me.xFant.Value & "', ")
            sb.Append(" '" & CnpjFormat & "', ")
            sb.Append(" '" & Me.xFone.Value & "', ")
            sb.Append(" '" & Me.xLgr.Value & "', ")
            sb.Append(" '" & Me.xBairro.Value & "', ")
            sb.Append(" '" & Me.xMun.Value & "', ")
            sb.Append(" '" & Me.xUF.Value & "', ")
            sb.Append(" '" & Me.xPais.Value & "', ")
            sb.Append(" '" & Me.xIE.Value & "', ")
            sb.Append(" '" & Me.xIM.Value & "' ")
            sb.Append(" ) ")



            If Banco.BeginTransaction(sb.ToString()) Then

            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao verificar da. Tente Novamente.');", True)
            End If



            sb1.AppendLine(" UPDATE REDEX.TB_BOOKING SET AUTONUM_EXPORTADOR = '" & idParceiroNextVal & "' WHERE  AUTONUM_BOO =  " & Me.lblCodigoBooking.Text)

            If Banco.BeginTransaction(sb.ToString()) Then

                Me.txtCNPJResponsavel.Text = Me.xNome.Value & "-" & mascaraCNPJ_CPF(Me.xCNPJ.Value)

                Me.hiddenText.Value = idParceiroNextVal

                Me.txtCNPJResponsavel.Enabled = True
                Me.txtEmailResponsavel.Enabled = True

            End If

        End If
    End Sub
    Public Function UpdateCNPJEmail()
        'Me.lblCodigoAgendamento.Text

        'Dim sb1 As New StringBuilder
        'sb1.AppendLine(" UPDATE REDEX.TB_BOOKING SET AUTONUM_EXPORTADOR = '" & idParceiroNextVal & "' WHERE  AUTONUM_BOO =  " & Me.lblCodigoBooking.Text)
        'Banco.BeginTransaction(sb.ToString())

    End Function

    Public Function mascaraCNPJ_CPF(doc As String) As String
        Dim mascara As New MaskedTextProvider("00\.000\.000/0000-00")

        If doc.Length = 11 Then
            mascara = New MaskedTextProvider("000\.000\.000-00")
        End If

        mascara.Set(doc)
        Return mascara.ToString()
    End Function

    Public Class CNPJ
        Public Property AUTONUM As String
        Public Property RAZAO As String
        Public Property CGC As String
        Public Property CNPJ As String
        Public Property FLAG_BLOQUEIO_AGENDAMENTO As Integer

    End Class

    <WebMethod>
    Public Shared Function GetCNPJ(ByVal cnpjID As String) As CNPJ()

        Dim Ds As DataTable
        Dim i As Integer = 0

        Ds = Banco.List("Select AUTONUM, RAZAO, CGC as CNPJ, NVL(FLAG_BLOQUEIO_AGENDAMENTO, 0) as FLAG_BLOQUEIO_AGENDAMENTO  FROM REDEX.Tb_Cad_parceiros WHERE (FLAG_EXPORTADOR=1  OR FLAG_NVOCC = 1) And FLAG_ATIVO=1 And replace(replace(replace(CGC, '.', ''), '-', ''), '/', '') = replace(replace(replace('" & cnpjID & "', '.', ''), '-', ''), '/', '') ")
        Dim cnpj As CNPJ() = New CNPJ(Ds.Rows.Count) {}

        If Ds IsNot Nothing Then
            If Ds.Rows.Count > 0 Then
                For Each linha As DataRow In Ds.Rows

                    cnpj(i) = New CNPJ()

                    cnpj(i).RAZAO = linha.Item("RAZAO").ToString()
                    cnpj(i).CNPJ = linha.Item("CNPJ").ToString()
                    cnpj(i).AUTONUM = linha.Item("AUTONUM").ToString()
                    cnpj(i).FLAG_BLOQUEIO_AGENDAMENTO = linha.Item("FLAG_BLOQUEIO_AGENDAMENTO").ToString()

                    i += 1

                Next
            End If
        End If
        Return cnpj
    End Function

    Protected Sub cbCavalo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCavalo.SelectedIndexChanged

        If Me.cbCavalo.Text <> String.Empty Then
            Me.ConsultarCarretas(Me.cbCavalo.Text)
            Me.AccordionIndex.Value = 1
        End If

    End Sub

    Protected Sub cbCarreta_onSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCarreta.SelectedIndexChanged

        If Me.cbCarreta.Text <> String.Empty Then
            Me.ConsultarPeriodos()
            Me.AccordionIndex.Value = 1
        End If
    End Sub

    Private Sub cbCarreta_Unload(sender As Object, e As EventArgs) Handles cbCarreta.Unload

    End Sub

    Private Sub txtCNPJResponsavel_DataBinding(sender As Object, e As EventArgs) Handles txtCNPJResponsavel.DataBinding

    End Sub
End Class