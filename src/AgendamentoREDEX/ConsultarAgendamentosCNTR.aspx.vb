Public Class ConsultarAgendamentosCNTR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        If Request.QueryString("erro") IsNot Nothing Then
            If Request.QueryString("erro").ToString().Equals("1") Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhum período foi selelecionado para o Agendamento.');", True)
            End If
        End If

    End Sub

    Private Sub Consultar(Optional ByRef Filtro As String = "")

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM_GD_CNTR, ")
        SQL.Append("    AUTONUM_TRANSPORTADORA, ")
        SQL.Append("    MOTORISTA, ")
        SQL.Append("    CNH, ")
        SQL.Append("    VEICULO, ")
        SQL.Append("    RESERVA, ")
        SQL.Append("    NAVIO_VIAGEM, ")
        SQL.Append("    EXPORTADOR, ")
        SQL.Append("    DEAD_LINE, ")
        SQL.Append("    PORTO_DESTINO, ")
        SQL.Append("    CONTEINER, ")
        SQL.Append("    PROTOCOLO, ")
        SQL.Append("    STATUS, ")
        SQL.Append("    PERIODO ")
        SQL.Append("FROM ")
        SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_CONS_CN ")
        SQL.Append("WHERE AUTONUM_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & " ")
        SQL.Append(Filtro)
        SQL.Append(" ORDER BY ")
        SQL.Append("    PROTOCOLO DESC ")

        Me.dgConsulta.DataSource = Banco.List(SQL.ToString())
        Me.dgConsulta.DataBind()

        If (dgConsulta.Rows.Count > 0) Then
            dgConsulta.HeaderRow.TableSection = TableRowSection.TableHeader
            dgConsulta.UseAccessibleHeader = True
            dgConsulta.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = Me.dgConsulta.DataKeys(Index)("AUTONUM_GD_CNTR").ToString()
        Dim BOO = Banco.ExecuteScalar("SELECT NVL(MAX(REFERENCE),'') FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & ID)
        Dim ID_BOO = Banco.ExecuteScalar("SELECT NVL(MAX(AUTONUM_BOO),0) FROM REDEX.TB_BOOKING WHERE REFERENCE = '" & BOO.ToString() & "'")

        If e.CommandName = "DEL" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then

                If Banco.ExecuteScalar("SELECT STATUS FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & ID) = "GE" Then
                    If Banco.BeginTransaction("DELETE FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & ID) Then
                        Consultar()
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Erro ao Excluir o Agendamento. Tente Novamente.');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O Agendamento não pode ser excluído pois já foi impresso.');", True)
                End If

            End If
        End If

        If e.CommandName = "EDITAR" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then
                If Banco.ExecuteScalar("SELECT STATUS FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & ID) = "GE" Then
                    Response.Redirect("AgendarCNTR.aspx?id=" & ID)
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O Agendamento não pode ser excluído pois já foi impresso.');", True)
                End If
            End If
        End If

        If e.CommandName = "PROTOCOLO" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then

                Dim Itens = Banco.List("SELECT ARQUIVO_DANFE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_NF WHERE AUTONUM_AGENDAMENTO = " & ID)

                Dim ExisteItemSemDanfe As Boolean

                If Itens.Rows.Count = 0 Then
                    ExisteItemSemDanfe = True
                End If

                For Each Item As DataRow In Itens.Rows
                    If String.IsNullOrWhiteSpace(Item("ARQUIVO_DANFE").ToString()) Then
                        ExisteItemSemDanfe = True
                    End If
                Next
                If Banco.BeginTransaction("Update REDEX.TB_gd_conteiner Set autonum_reserva=" & ID_BOO.ToString() & " where autonum_gd_cntr=" & ID) Then

                End If


                If Banco.ExecuteScalar("Select count(1) from REDEX.Tb_gd_conteiner a  inner join redex.tb_booking  b On a.autonum_RESERVA= b.autonum_BOO  where  b.flag_bagagem=1  And a.autonum_gd_cntr = " & ID) > 0 Then
                    ExisteItemSemDanfe = False
                End If

                If ExisteItemSemDanfe Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('É necessário fazer o upload da Danfe para imprimir o protocolo');", True)
                    Exit Sub
                End If

                Response.Redirect("ProtocoloCNTR.aspx?protocolo=" & ID)

            End If
        End If

    End Sub

    Protected Sub btnAgendar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgendar.Click
        Response.Redirect("AgendarCNTR.aspx")
    End Sub

    Protected Sub btPesquisar2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar2.Click

        Dim SQL As New StringBuilder()

        If Not txtReserva.Text = String.Empty Then
            SQL.Append(" AND RESERVA = '" & txtReserva.Text.Trim.ToUpper().Replace("'", "") & "' ")
        End If

        If Not txtConteiner.Text = String.Empty Then
            SQL.Append(" AND CONTEINER = '" & txtConteiner.Text.Trim.ToUpper() & "' ")
        End If

        If Not cbTipo.Text = String.Empty Then
            SQL.Append(" AND CONTEINER LIKE '%" & cbTipo.Text.Trim.ToUpper() & "%' ")
        End If

        If Not cbTamanho.Text = String.Empty Then
            SQL.Append(" AND CONTEINER LIKE '%" & cbTamanho.Text & "%' ")
        End If

        If Not txtPlacaCV.Text = String.Empty Then
            SQL.Append(" AND VEICULO LIKE '%" & txtPlacaCV.Text.Trim.ToUpper() & "%' ")
        End If

        If Not txtPlacaCR.Text = String.Empty Then
            SQL.Append(" AND VEICULO LIKE '%" & txtPlacaCR.Text.Trim.ToUpper() & "%' ")
        End If

        If Not txtNavio.Text = String.Empty Then
            If cbFiltro1.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(NAVIO_VIAGEM) = '" & txtNavio.Text.Trim.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro1.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(NAVIO_VIAGEM) LIKE '%" & txtNavio.Text.Trim.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        If Not txtMotorista.Text = String.Empty Then
            If cbFiltro2.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(MOTORISTA) = '" & txtMotorista.Text.Trim.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro2.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(MOTORISTA) LIKE '%" & txtMotorista.Text.Trim.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        Consultar(SQL.ToString())

    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1
        cbTamanho.SelectedIndex = -1
        cbTipo.SelectedIndex = -1

        txtReserva.Text = String.Empty
        txtPlacaCV.Text = String.Empty
        txtPlacaCR.Text = String.Empty
        txtNavio.Text = String.Empty
        txtMotorista.Text = String.Empty
        txtConteiner.Text = String.Empty

        Consultar()

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Me.pnFiltro.Visible = False
    End Sub

    Protected Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPesquisar.Click
        Me.pnFiltro.Visible = True
    End Sub

End Class