$(document).ready(function () {

    $('main')
        .addClass('container')
        .removeClass('container-fluid');

    $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {

        if (stepPosition === 'first') {
            $("#prev-btn").addClass('disabled');
        } else if (stepPosition === 'final') {
            $("#next-btn").addClass('disabled');
        } else {
            $("#prev-btn").removeClass('disabled');
            $("#next-btn").removeClass('disabled');
        }
    });

    $("#smartwizard").on("leaveStep", function (e, anchorObject, stepNumber, stepDirection) {

        var motoristaId = $('#MotoristaId').val();
        var veiculoId = $('#VeiculoId').val();
        var cte = $('#CTE').val();
        var bookingCsId = $('#BookingCsId').val();

        $('#msgErro').html('').addClass('invisivel');

        if (stepDirection === 'forward') {

            if (stepNumber == 0 && motoristaId === '') {

                $('#msgErro')
                    .html('Selecione o Motorista')
                    .removeClass('invisivel');
                return false;
            }

            if (stepNumber == 1 && veiculoId === '') {

                $('#msgErro')
                    .html('Selecione o Veículo')
                    .removeClass('invisivel');
                return false;
            }

            if (stepNumber == 1 && cte === '') {

                $('#msgErro')
                    .html('Informe o número do CTE')
                    .removeClass('invisivel');
                return false;
            }

            if (stepNumber == 2) {

                $('#btnConcluir').show();
            }
        }
    });

    $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        transitionEffect: 'fade',
        showStepURLhash: true,
        enableAllSteps: true,
        toolbarSettings: {
            toolbarPosition: 'bottom',
            toolbarButtonPosition: 'end'
        },
        lang: {
            next: 'Próximo',
            previous: 'Anterior'
        }
    });


});

$("#btnPesquisarMotorista").click(function () {

    event.preventDefault();

    $('#pesquisa-modal-motoristas')
        .data('toggle', 'MotoristaId')
        .modal('show');
});

function selecionarMotorista(id, descricao) {

    var toggle = $('#pesquisa-modal-motoristas').data('toggle');

    $('#pesquisa-modal-motoristas').modal('hide');

    $("#" + toggle)
        .empty()
        .append($('<option>', {
            value: id,
            text: descricao
        })).focus();

    obterDetalhesMotorista($('#MotoristaId').val());

    $("#ListaMotoristas").empty();
}

function obterDetalhesMotorista(motoristaId) {

    $('#pnlDetalhesMotorista').html('');

    $.get(urlBase + 'Agendamento/ObterDetalhesMotorista?motoristaId=' + motoristaId, function (resultado) {

        if (resultado) {
            $('#pnlDetalhesMotorista').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#MotoristaId').change(function () {

    obterDetalhesMotorista($(this).val());
});

$("#btnPesquisarVeiculo").click(function () {

    event.preventDefault();

    $('#pesquisa-modal-veiculos')
        .data('toggle', 'VeiculoId')
        .modal('show');
});

function selecionarVeiculo(id, descricao) {

    var toggle = $('#pesquisa-modal-veiculos').data('toggle');

    $('#pesquisa-modal-veiculos').modal('hide');

    $("#" + toggle)
        .empty()
        .append($('<option>', {
            value: id,
            text: descricao
        })).focus();

    obterDetalhesVeiculo($('#VeiculoId').val());

    $("#ListaVeiculos").empty();
}

function obterDetalhesVeiculo(veiculoId) {

    $('#pnlDetalhesVeiculo').html('');

    $.get(urlBase + 'Agendamento/ObterDetalhesVeiculo?veiculoId=' + veiculoId, function (resultado) {

        if (resultado) {

            $('#pnlDetalhesVeiculo').html(resultado);
            $('#pnlCTE').removeClass('invisivel');

            if ($('#Carreta').val() === '' || $('#Carreta').val() === '___-____') {

                $('#lnkAtualizarPlacas').attr('href', urlBase + 'Veiculos/Atualizar/' + $('#VeiculoId').val());

                $("#smartwizard :input").prop("disabled", true);

                $('#modal-veiculo-invalido')
                    .modal('show');
            } else {
                $("#smartwizard :input").prop("disabled", false);
            }
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#VeiculoId').change(function () {

    obterDetalhesVeiculo($(this).val());
});

$('#Reserva').on('keypress', function (e) {

    if (e.which == 13) {
        consultarReserva($(this).val());
    }
});

$('#Reserva').on('keydown', function (e) {

    if (e.which == 9) {
        consultarReserva($(this).val());
    }
});

function consultarReserva(reserva) {

    $('#validaReserva')
        .html('')
        .addClass('invisivel');

    limpar();

    $('#pnlItensReserva').html('');

    if (reserva === '') {

        $('#validaReserva').removeClass('invisivel');

        return;
    }

    $.get(urlBase + 'Agendamento/ObterDetalhesReserva?reserva=' + reserva, function (resultado) {

        if (resultado) {

            $('#BookingCsId').val(resultado.BookingCsId);
            $('#Navio').val(resultado.Navio);
            $('#ViagemId').val(resultado.ViagemId);
            $('#Viagem').val(resultado.Viagem);
            $('#Abertura').val(resultado.Abertura);
            $('#Fechamento').val(resultado.Fechamento);
            $('#Exportador').val(resultado.Exportador);

            $('#validaReserva').addClass('invisivel');

            obterItensReserva($('#Reserva').val(), $('#ViagemId').val());
            validarAgendamentoSemSaldo($('#Reserva').val(), $('#ViagemId').val());

            desabilitarCamposDanfe();
            desabilitarCamposDAT();
            desabilitarCamposDUE();

            $('#pnlReserva').removeClass('invisivel');
        }
    }).fail(function (data) {

        $('#validaReserva')
            .html(data.statusText)
            .removeClass('invisivel');

        limpar();
    });

    $('#btnConcluir')
        .prop('disabled', false);
}

function limpar() {

    limparCamposDanfe();
    limparCamposItem();
}

function limparCamposDanfe() {

    //var numero = '-';
    //var serie = '-';
    //var emissor = '-'
    //var data = '-';
    //var cfop = '-'
    //var valor = '-'
    //var qtd = '-';
    //var pesob = '-';
    //var danfe = '-';
    //var xml = '-';

    //$('#CFOP').val("");
    //$('#Danfe').val("");
    $('#numero').val('');
    //$('#serie').html("");
    //$('#data').html("");
    //$('#valor').html("");
    //$('#emisso').html("");
    //$('#qtd').html("");
    //$('#pesobruto').html("");
    //$('#xml').val("");

    //desabilitarCamposDanfe();
}

function limparCamposItem() {
    $('#QuantidadeItemReserva').val('');
    $('#Chassis').val('');
}

function obterItensReserva(reserva, viagemId) {

    $('#ItemReservaId').empty();

    $.get(urlBase + 'Agendamento/ObterItensReserva?reserva=' + reserva + '&viagemId=' + viagemId, function (resultado) {

        if (resultado) {

            resultado.forEach(function (item) {
                $('#ItemReservaId').append($('<option>', {
                    value: item.BookingCsItemId,
                    text: item.Descricao
                }));
            });
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    }).done(function () {
        obterDetalhesItem();
    });
}

function validarAgendamentoSemSaldo(reserva, viagemId) {

    $.get(urlBase + 'Agendamento/ObterSaldoItensReserva?reserva=' + reserva + '&viagemId=' + viagemId, function (resultado) {

        if (resultado != null) {

            if (parseInt(resultado) === 0) {

                $('#Reserva').val('');

                $('#lnkIrParaAgendamentos').attr('href', urlBase + 'Agendamento/Cadastrar');

                $('#modal-reserva-sem-saldo')
                    .modal('show');
            }
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#ItemReservaId').change(function () {

    obterDetalhesItem();
    limparCamposItem();
});

function validarQuantidade(id, target) {

    $('#pnlAlertaQuantidade').addClass('invisivel');

    if (parseInt(id) > 0) {

        if (target.value != null) {

            var quantidadeReserva = parseInt($('#quantidade-reserva-' + id).text());
            var quantidadeInformada = parseInt(target.value);

            if (quantidadeInformada > 0) {
                if (quantidadeInformada > quantidadeReserva) {

                    $('#pnlAlertaQuantidade').removeClass('invisivel');
                    target.focus();
                }
            }
        }
    }
}

function obterPeriodos() {

    $('#pnlPeriodos').html('');

    $.get(urlBase + 'Agendamento/ObterPeriodos', function (resultado) {

        if (resultado) {

            $('#pnlPeriodos').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}
function validarDanfe(campo) {    
    $('#pnlAlertaDanfe')
        .text('').hide();

    var texto = campo;
    var retorno = '';

    if (texto.length !== 44) {
        retorno = 'A chave da Danfe informada é inválida';
    }

    if (texto.length === 44) {

        if (!validarCNPJ(texto.substring(6, 20))) {
            retorno = 'DANFE inválida';
        }

        if (texto.substring(20, 22) !== '55') {
            retorno = 'DANFE inválida';
        }

        if (texto.substring(25, 34) === '000000000') {
            retorno = 'DANFE inválida';
        }
    }    
    return retorno;
}

function SelecionarPeriodo(id) {

    if (parseInt(id) > 0)
        $('#PeriodoId').val(id);
}

var agendamentoMensagemSucesso = function (data) {

    toastr.success('Agendamento cadastrado com sucesso!', 'Agendamento');

};

var agendamentoMensagemErro = function (xhr, status) {

    $('#btnConcluirAgendamento')
        .html('Concluir')
        .removeClass('disabled');

    if (xhr !== null && xhr.responseText !== '') {

        toastr.error('Falha ao cadastrar o Agendamento. Verifique mensagens.', 'Agendamento');

        var msg = $('#msgErro');

        msg.html('');
        msg.removeClass('invisivel');

        var resultado = JSON.parse(xhr.responseText);

        var mensagens = resultado.erros.map(function (erro) {
            return '<li>' + erro.ErrorMessage + '</li>'
        })

        msg.html(mensagens);

    } else {
        toastr.error(xhr.statusText, 'Agendamento');
    }
};

$('#btnAdicionarItemReserva').click(function () {

    event.preventDefault();

    $('#msgErro').html('').addClass('invisivel');

    if ($('#QuantidadeItemReserva').val() === '') {

        $('#msgErro')
            .html('Informe a Quantidade do item')
            .removeClass('invisivel');
        return;
    }

    //$('#btnAdicionarItemReserva')
    $('#btnAdicionarItemReserva')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        Reserva: $('#Reserva').val(),
        BookingCsItemId: $('#ItemReservaId').val(),
        Qtde: $('#QuantidadeItemReserva').val(),
        Chassis: $('#Chassis').val(),
        ClassificacaoId: $('#ClassificacaoId').val()
    };

    $.post(urlBase + 'Agendamento/CadastrarItemReserva', obj, function (resultado) {

        $('#tblItensReserva').html(resultado);

        obterDanfesPorItem(obj.BookingCsItemId);
        obterUploadsPorItem(obj.BookingCsItemId);

        obterPeriodos();

        limparCamposItem();

    }).fail(function (data) {

        var retorno = data.responseJSON;

        if (retorno.erros != null) {

            var msg = retorno.erros[0].ErrorMessage;

            $('#msgErro').html(msg).removeClass('invisivel');

        } else {
            if (data.statusText) {
                toastr.error(data.statusText, 'Agendamento');
            } else {
                toastr.error('Falha ao incluir a Danfe', 'Agendamento');
            }
        }

    }).always(function () {

        $('#btnAdicionarItemReserva')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');

    });
});

$('#btnAlterarItemReserva').click(function () {
    event.preventDefault();
    
    $('#msgErro').html('').addClass('invisivel');

    if ($('#QuantidadeItemReserva').val() === '') {

        $('#msgErro')
            .html('Informe a Quantidade do item')
            .removeClass('invisivel');
        return;
    }

    $('#btnAlterarItemReserva')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        Reserva: $('#Reserva').val(),
        BookingCsItemId: $('#ItemReservaId').val(),
        Qtde: $('#QuantidadeItemReserva').val(),
        Chassis: $('#Chassis').val(),
        ClassificacaoId: $('#ClassificacaoId').val(),
        Id: $('#Chassis').attr('data-id')
    };

    $.post(urlBase + 'Agendamento/AlterarItemReserva', obj, function (resultado) {

        $('#tblItensReserva').html(resultado);

        obterDanfesPorItem(obj.BookingCsItemId);
        obterUploadsPorItem(obj.BookingCsItemId);

        obterPeriodos();

        limparCamposItem();

    }).fail(function (data) {

        var retorno = data.responseJSON;

        if (retorno.erros != null) {

            var msg = retorno.erros[0].ErrorMessage;

            $('#msgErro').html(msg).removeClass('invisivel');

        } else {
            if (data.statusText) {
                toastr.error(data.statusText, 'Agendamento');
            } else {
                toastr.error('Falha ao incluir a Danfe', 'Agendamento');
            }
        }

    }).always(function () {

        $('#btnAlterarItemReserva').html('<i class="fas fa-save"></i>').removeClass('disabled');
        $('#btnAlterarItemReserva').hide();
        $('#btnAdicionarItemReserva').show();
    });
});
function excluirItemReserva(id, bookingCsItemId) {
    
    $('#modal-mensagem-item').text('Confirma a exclusão do item?');
   

    $('#del-modal-item')
        .data('id', id);

    $('#del-modal-item')
        .data('bookingCsItemId', bookingCsItemId);

    $('#del-modal-item')
        .modal('show');
}

function confirmarExclusaoItemReserva() {

    var _url = urlBase + 'Agendamento/ExcluirItemReserva';

    var _id = $('#del-modal-item')
        .data('id');

    var _bookingCsItemId = $('#del-modal-item')
        .data('bookingCsItemId');

    console.log(_id);
    console.log(_bookingCsItemId);

    $.post(_url, { id: _id }, function (resultado) {

        $('#tblItensReserva').html(resultado);

        obterDanfesPorItem(_bookingCsItemId);
        obterUploadsPorItem(_bookingCsItemId);

    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        } else {
            toastr.error('Falha ao excluir o item', 'Agendamento');
        }
    }).always(function () {

        $('#del-modal-item')
            .data('id', '0')
            .modal('hide');
    });
}

function selecionarItem(bookingCsItemId, qtde, chassis) {
    $('#msgErro')
        .html('')
        .addClass('invisivel');
    event.preventDefault();
   
    limparCamposItem();

    $.get(urlBase + 'Agendamento/ObterItemReservaPorId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#Reserva').val(resultado.Reserva);
            $('#ViagemId').val(resultado.ViagemId);

            $('#ItemReservaId').empty();

            $.get(urlBase + 'Agendamento/ObterItensReserva?reserva=' + resultado.Reserva + '&viagemId=' + resultado.ViagemId, function (resultadoItens) {

                if (resultadoItens) {

                    resultadoItens.forEach(function (item) {

                        $('#ItemReservaId').append($('<option>', {
                            value: item.BookingCsItemId,
                            text: item.Descricao
                        }));
                    });
                }

                if ($("#ItemReservaId option[value='" + bookingCsItemId + "']").val() === undefined) {
                    $('#ItemReservaId').append($('<option>', {
                        value: resultado.BookingCsItemId,
                        text: resultado.Descricao
                    }));
                }

                $('#ItemReservaId').val(bookingCsItemId);

                $('#QuantidadeItemReserva').val(qtde);
                $('#Chassis').val(chassis);

                $('#TiposDocumentos').empty();

                if (resultado.PackingList) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 1,
                        text: 'Packing List'
                    }));
                }

                if (resultado.DesenhoTecnico) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 2,
                        text: 'Desenho Técnico'
                    }));
                }

                if (resultado.ImagemCarga) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 3,
                        text: 'Imagem Carga'
                    }));
                }

                obterDanfesPorItem(bookingCsItemId);
                obterUploadsPorItem(bookingCsItemId);
                //mexendo
                habilitarCamposDanfe();
                habilitarCamposDAT();
                habilitarCamposDUE();

                $('#Chassis').attr('readonly', false);
                $('#danfe_pesquisada').focus();

                $('#btnAdicionarItemReserva').show();
                $('#btnAlterarItemReserva').hide();

            }).fail(function (data) {
                toastr.error(data.statusText, 'Agendamento');
            });
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}


function selecionarItemAlt(bookingCsItemId, qtde, chassis, id) {
    
    $('#msgErro')
        .html('')
        .addClass('invisivel');
    event.preventDefault();

    limparCamposItem();

    $.get(urlBase + 'Agendamento/ObterItemReservaPorId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#Reserva').val(resultado.Reserva);
            $('#ViagemId').val(resultado.ViagemId);

            $('#ItemReservaId').empty();

            $.get(urlBase + 'Agendamento/ObterItensReserva?reserva=' + resultado.Reserva + '&viagemId=' + resultado.ViagemId, function (resultadoItens) {

                if (resultadoItens) {

                    resultadoItens.forEach(function (item) {

                        $('#ItemReservaId').append($('<option>', {
                            value: item.BookingCsItemId,
                            text: item.Descricao
                        }));
                    });
                }

                if ($("#ItemReservaId option[value='" + bookingCsItemId + "']").val() === undefined) {
                    $('#ItemReservaId').append($('<option>', {
                        value: resultado.BookingCsItemId,
                        text: resultado.Descricao
                    }));
                }

                $('#ItemReservaId').val(bookingCsItemId);

                $('#QuantidadeItemReserva').val(qtde);
                $('#Chassis').val(chassis);
                $('#Chassis').attr('data-id', id);


                $('#TiposDocumentos').empty();

                if (resultado.PackingList) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 1,
                        text: 'Packing List'
                    }));
                }

                if (resultado.DesenhoTecnico) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 2,
                        text: 'Desenho Técnico'
                    }));
                }

                if (resultado.ImagemCarga) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 3,
                        text: 'Imagem Carga'
                    }));
                }

                

                $('#Chassis').attr('readonly', false);
                $('#danfe_pesquisada').focus();
                $('#btnAdicionarItemReserva').hide();
                $('#btnAlterarItemReserva').show();

            }).fail(function (data) {
                toastr.error(data.statusText, 'Agendamento');
            });
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function habilitarCamposDanfe() {

    $('#Danfe').prop('disabled', false);
    $('#CFOP').prop('disabled', false);
    $('#XmlDanfeCompleta').prop('disabled', false);
    $('#fileinput').prop('disabled', false);
    $('#btnAdicionarDanfe').removeClass('disabled');
    $('#danfe_pesquisada').prop('disabled', false);

}

function desabilitarCamposDanfe() {

    $('#Danfe').prop('disabled', true);
    $('#fileinput').prop('disabled', true)
    $('#CFOP').prop('disabled', true);
    $('#XmlDanfeCompleta').prop('disabled', true);
    $('#CFOP').prop('disabled', true);
    $('#btnAdicionarDanfe').addClass('disabled');
    $('#danfe_pesquisada').prop('disabled', true);
}

function habilitarCamposDUE() {
    $('#txtDUE').prop('disabled', false);
    $('#btnAdicionarDUE').removeClass('disabled');
}
function desabilitarCamposDUE() {
    $('#txtDUE').prop('disabled', true);
    $('#btnAdicionarDUE').addClass('disabled');
}
function habilitarCamposDAT() {
    $('#txtDAT').prop('disabled', false);
    $('#btnAdicionarDAT').removeClass('disabled');
}
function desabilitarCamposDAT() {
    $('#txtDAT').prop('disabled', true);
    $('#btnAdicionarDAT').addClass('disabled');
}



function obterDanfesPorItem(bookingCsItemId) {

    $.get(urlBase + 'Agendamento/ObterDanfesPorItemId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#tblDanfes').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}
function obterDUEPorItem(bookingCsItemId) {
    $.get(urlBase + 'Agendamento/ObteDUEPorItemId?bookingCsId=' + bookingCsItemId, function (resultado) {
        if (resultado) {
            $('#tabelaDUE').html(resultado);
        }
    });
}
function obterDATPorItem(bookingCsItemId) {
    $.get(urlBase + 'Agendamento/ObteDATPorItemId?bookingCsId=' + bookingCsItemId, function (resultado) {
        if (resultado) {
            $('#tabelaDAT').html(resultado);
        }
    });
}
function obterUploadDanfesCarregadas(idTransportadora) {

    $.get(urlBase + 'Agendamento/ObterUploadDanfesNfe?idTransportadora=' + idTransportadora, function (resultado) {

        if (resultado) {

            console.log(resultado);
            //$('#tblDanfes').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function obterUploadsPorItem(bookingCsItemId) {

    $.get(urlBase + 'Agendamento/ObterUploadsPorItemId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#tblDocumentos').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#btnAdicionarDUE').click(function () {

    //$('#btnAdicionarDUE')
    //    .html('<i class="fa fa-spinner fa-spin"></i>')
    //    .addClass('disabled');

    var DUE = $('#txtDUE').val();
    var agId = $('#Id').val();    
    var BookingCsItemId = $('#ItemReservaId').val();

    var inserir = {
        DUE: DUE,
        BookingCsItemId: BookingCsItemId, 
    }

    if (DUE == "") {
        toastr.error('O campo DUE não pode estar em branco', 'Agendamento');

        $(this)
            .html('SALVAR <i class="fas fa-save"></i>')
            .removeClass('disabled');
    }
    else if (DUE.length < 14) {
        toastr.error('O campo DUE tem que ter 14 caracteres', 'Agendamento');
        $(this)
            .html('SALVAR <i class="fas fa-save"></i>')
            .removeClass('disabled');
    }
    else if ($('#ItemReservaId').val() === '') {
        toastr.error('Selecione um item', 'Agendamento');        
        $(this)
            .html('SALVAR <i class="fas fa-save"></i>')
            .removeClass('disabled');
    }
    else {
        $.post('/Agendamento/CadastrarDUE', inserir, function (resultado) {

            $(this)
                .html('SALVAR <i class="fas fa-save"></i>')
                .removeClass('disabled');   

            $('#tabelaDUE').show();
            $('#tblItensDUE').show();
            $('#tblItensDUE').html(resultado);

                    

        }).fail(function (data) {

            var retorno = data.responseJSON;

            if (retorno.erros != null) {

                var msg = retorno.erros[0].ErrorMessage;

                $('#msgErro').html(msg).removeClass('invisivel');
                $("#tblItensDUE td").empty();
                
            } else {
                if (data.statusText) {
                    toastr.error(data.statusText, 'Agendamento');
                } else {
                    toastr.error('Falha ao incluir a DUE', 'Agendamento');
                }
            }

            toastr.error(data.statusText, 'Agendamento');
            $(this)
                .html('SALVAR <i class="fas fa-save"></i>')
                .removeClass('disabled');
        }).always(function () {
            $(this)
                .html('SALVAR <i class="fas fa-save"></i>')
                .removeClass('disabled');


        });        
    }
});
$('#btnAdicionarDAT').click(function () {
    //$('#btnAdicionarDAT')
    //    .html('<i class="fa fa-spinner fa-spin"></i>')
    //    .addClass('disabled');

    var DAT = $('#txtDAT').val();
    var agId = $('#Id').val();
    var BookingCsItemId = $('#ItemReservaId').val();
    
    var inserir = {
        DAT: DAT,
        BookingCsItemId: BookingCsItemId, 
    }

    if (DAT == "") {
        toastr.error('O campo DAT não pode estar em branco', 'Agendamento');

        $(this)
            .html('SALVAR <i class="fas fa-save"></i>')
            .removeClass('disabled');
    }
    else if (DAT.length < 11) {
        toastr.error('O campo DAT tem que ter 11 caracteres', 'Agendamento');
        $(this)
            .html('SALVAR <i class="fas fa-save"></i>')
            .removeClass('disabled');
    }
    else if ($('#ItemReservaId').val() === '') {
        toastr.error('Selecione um item', 'Agendamento');
        $(this)
            .html('SALVAR <i class="fas fa-save"></i>')
            .removeClass('disabled');
    }
    else {
        $.post('/Agendamento/CadastrarDAT', inserir, function (resultado) {

            $(this)
                .html('SALVAR <i class="fas fa-save"></i>')
                .removeClass('disabled');

            $('#tabelaDAT').show();
            $('#tblItensDAT').show();
            $('#tblItensDAT').html(resultado);



        }).fail(function (data) {

            var retorno = data.responseJSON;

            if (retorno.erros != null) {

                var msg = retorno.erros[0].ErrorMessage;

                $('#msgErro').html(msg).removeClass('invisivel');
                $("#tblItensDAT td").empty();

            } else {
                if (data.statusText) {
                    toastr.error(data.statusText, 'Agendamento');
                } else {
                    toastr.error('Falha ao incluir a DAT', 'Agendamento');
                }
            }

            toastr.error(data.statusText, 'Agendamento');
            $(this)
                .html('SALVAR <i class="fas fa-save"></i>')
                .removeClass('disabled');
        }).always(function () {
            $(this)
                .html('SALVAR <i class="fas fa-save"></i>')
                .removeClass('disabled');


        });
    }

});
$('#btnAdicionarDanfe').click(function () {
    event.preventDefault();
    
    $('#msgErro')
        .html('')
        .addClass('invisivel');

    if ($('#ItemReservaId').val() === '') {

        $('#msgErro')
            .html('Selecione um item')
            .removeClass('invisivel');
        return;
    }
    
    var retorno = validarDanfe($('#Danfe').val());

    if (retorno !== '') {

        $('#msgErro')
            .html(retorno)
            .removeClass('invisivel');
        $("#tblItensDanfe td").empty();
        limparCamposDanfe();
        $('#danfe_pesquisada').focus();

        return;
    }

    if ($('#CFOP').val() === '') {

        $('#msgErro')
            .html('O CFOP é obrigatório')
            .removeClass('invisivel');

        return;
    }

    $('#btnAdicionarDanfe')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        Danfe: $('#Danfe').val(),
        Reserva: $('#Reserva').val(),
        CFOP: $('#CFOP').val(),
        CTE: $('#CTE').val(),
        xml: $('#xml').val(),
        BookingCsItemId: $('#ItemReservaId').val()
    };

    $.post(urlBase + 'Agendamento/CadastrarDanfes', obj, function (resultado) {

        $('#tblDanfes').html(resultado);

        limparCamposDanfe();

    }).fail(function (data) {

        var retorno = data.responseJSON;

        if (retorno.erros != null) {

            var msg = retorno.erros[0].ErrorMessage;

            $('#msgErro').html(msg).removeClass('invisivel');
            $("#tblItensDanfe td").empty();
            $("#danfe_pesquisada").val("");
            $("#danfe_pesquisada").focus();
        } else {
            if (data.statusText) {
                toastr.error(data.statusText, 'Agendamento');
            } else {
                toastr.error('Falha ao incluir a Danfe', 'Agendamento');
            }
        }
    }).always(function () {

        $('#btnAdicionarDanfe')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');

        $("#tblItensDanfe td").empty();
        $("#danfe_pesquisada").val("");
        $("#danfe_pesquisada").focus();
        desabilitarCamposDanfe();
    });
});
function loadFile() {

    var input, file, fr;

    if (typeof window.FileReader !== 'function') {
        bodyAppend("p", "The file API isn't supported on this browser yet.");
        return;
    }

    input = document.getElementById('fileinput');
    if (!input) {
        bodyAppend("p", "Um, couldn't find the fileinput element.");
    }
    else if (!input.files) {
        bodyAppend("p", "This browser doesn't seem to support the `files` property of file inputs.");
    }
    else if (!input.files[0]) {
        bodyAppend("p", "Please select a file before clicking 'Load'");
    }
    else {
        file = input.files[0];
        fr = new FileReader();
        fr.onload = receivedText;
        fr.readAsText(file);       
    }

    function receivedText() {
        //console.log(fr.result);
        
        var doc = parser.parseFromString(fr.result, "text/xml");
        
        //document.getElementById("xmlDanfeCompleta").innerHTML = fr.result;        
        
        $('#xml').val(fr.result);
        
        console.log(doc.childNodes[0]);        
        
        shownode(doc.childNodes[0]);
        
    }
}
//lendo os dados do xml
function shownode(node) {    



    
    var nfe = node.getElementsByTagName('chNFe')[0].innerHTML;    
    var numero = node.getElementsByTagName('cNF')[0].innerHTML;
    var serie = node.getElementsByTagName('serie')[0].innerHTML;
    var emissor = node.getElementsByTagName('CNPJ')[0].innerHTML;
    var data = node.getElementsByTagName('dhEmi')[0].innerHTML;
    var cfop = node.getElementsByTagName('CFOP')[0].innerHTML;
    var valor = node.getElementsByTagName('vNF')[0].innerHTML;
    var qtd = node.getElementsByTagName('indTot')[0].innerHTML;   
    
    
    //if (node.getElementsByTagName('pesoB') != 'undefined') {
    //    pesob = node.getElementsByTagName('pesoB')[0].innerHTML;
    //    alert(pesob);
    //}
    //else {
    //    alert('re');
    //}

    $('#CFOP').val(cfop);
    $('#Danfe').hide();
    $('#Danfe').val(nfe);
    $('#numero').html(numero);
    $('#serie').html(serie);
    $('#data').html(data);
    $('#valor').html(valor);
    $('#emisso').html(emissor);
    $('#qtd').html(qtd);
    var pesob = 0;
    $('#pesobruto').html(pesob);
    pesob = node.getElementsByTagName('pesoB')[0].innerHTML;
    $('#pesobruto').html(pesob);
    
}
if (typeof (DOMParser) == 'undefined') {
    DOMParser = function () { }
    DOMParser.prototype.parseFromString = function (str, contentType) {
        if (typeof (ActiveXObject) != 'undefined') {
            var xmldata = new ActiveXObject('MSXML.DomDocument');
            xmldata.async = false;
            xmldata.loadXML(str);
            return xmldata;
        } else if (typeof (XMLHttpRequest) != 'undefined') {
            var xmldata = new XMLHttpRequest;
            if (!contentType) {
                contentType = 'application/xml';
            }
            xmldata.open('GET', 'data:' + contentType + ';charset=utf-8,' + encodeURIComponent(str), false);
            if (xmldata.overrideMimeType) {
                xmldata.overrideMimeType(contentType);
            }
            xmldata.send(null);
            return xmldata.responseXML;
        }
    }
}

var xmlString = "<root><thing attr='val'/></root>";
var parser = new DOMParser();
var doc = parser.parseFromString(xmlString, "text/xml");
//console.log(doc.childNodes.length);
//console.log(doc.nodeName["serie"]);
//upload danfe
//$('#btnUploadDanfe').on("click", function () {
//    alert("ok");
//    $("input").trigger("click")
//    var selectedFile = document.getElementById('input').files[0];
//    console.log(selectedFile);
//    //var has_selected_file = $('#upld').filter(function () {
//    //    return $.trim(this.value) != ''
//    //}).length > 0;
//    //if (has_selected_file) {
//    //    $('#frm').submit();
//    //}
//    //else {
//    //    alert('Selecione um arquivo!');
//    //}
//});

function selecionarDanfe(danfe, cfop) {

    event.preventDefault();

    $('#Danfe').val(danfe);
    $('#CFOP').val(cfop);
}

function excluirDanfe(id, bookingCsItemId) {

    $('#modal-mensagem-danfe').text('Confirma a exclusão da Danfe?');

    $('#del-modal-danfe')
        .data('id', id);

    $('#del-modal-danfe')
        .data('bookingCsItemId', bookingCsItemId);

    $('#del-modal-danfe')
        .modal('show');
    limpar();

}

function excluirItemDUE(id, bookingCSItemId) {
   
    $('#modal-mensagem-due').text('Confirm a a exclusão da DUE ? ');
    $('#del-modal-due').attr('data-id', id);
    $('#del-modal-due').attr('data-bookingCsItemId', bookingCSItemId);
    $('#del-modal-due').modal('show');
}

function excluirItemDAT(id, bookingCSItemId) {    
    $('#modal-mensagem-dat').text('Confirm a exclusão da DAT ? ');
    $('#del-modal-dat').attr('data-id', id);
    $('#del-modal-dat').attr('data-bookingCSItemId', bookingCSItemId);
    $('#del-modal-dat').modal('show');
}

function confirmarExclusaoDanfe(id) {

    var _url = urlBase + 'Agendamento/ExcluirDanfe';

    var _id = $('#del-modal-danfe')
        .data('id');

    var _bookingCsItemId = $('#del-modal-danfe')
        .data('bookingCsItemId');

    $.post(_url, { id: _id, bookingCsItemId: _bookingCsItemId }, function (resultado) {
        $('#tblDanfes').html(resultado);
    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        } else {
            toastr.error('Falha ao excluir a Danfe', 'Agendamento');
        }
    }).always(function () {

        $('#del-modal-danfe')
            .data('id', '0')
            .modal('hide');
    });
    limpar();
}

function confirmarExclusaoDUE(id) {
    var url = urlBase + 'Agendamento/ExcluirDUE';
    var id = $('#del-modal-due').attr('data-id');    
    var bookingCsItemId = $('#del-modal-due').attr('data-bookingCSItemId');
    
    $.post(url, { id: id, bookingCsItemId: bookingCsItemId }, function (result) {
        $('#tblItensDUE').html(result);
        //$.toastr.success('Dados inseridos com sucesso');
    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        }
        else {
            toastr.error('Falha ao excluir DUE', 'Agendamento')
        }
    }).always(function () {
        $('#del-modal-due').data('id', '0').modal('hide');
    });

}

function confirmarExclusaoDAT() {
    var url = urlBase + 'Agendamento/ExcluirDAT';
    var id = $('#del-modal-dat').attr('data-id');
    var bookingCsItemId = $('#del-modal-dat').attr('data-bookingCSItemId');
    
    $.post(url, { id: id, bookingCsItemId: bookingCsItemId }, function (result) {
        $('#tblItensDAT').html(result);
    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        }
        else {
            toastr.error('Falha ao excluir DAT', 'Agendamento');
        }
    }).always(function () {
        $('#del-modal-dat').data('id', '0').modal('hide');
    });
}

function excluirUpload(id) {

    $('#modal-mensagem-upload').text('Confirma a exclusão do Upload?');

    $('#del-modal-upload')
        .data('id', id);

    $('#del-modal-upload')
        .modal('show');
}

function confirmarExclusaoUpload(id) {

    var _url = urlBase + 'Agendamento/ExcluirUpload';

    var _id = $('#del-modal-upload')
        .data('id');

    $.post(_url, { id: _id }, function (resultado) {
        $('#tblDocumentos').html(resultado);
    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        } else {
            toastr.error('Falha ao excluir o Upload', 'Agendamento');
        }
    }).always(function () {

        $('#del-modal-upload')
            .data('id', '0')
            .modal('hide');
    });
}

function obterDetalhesItem() {

    var bookingCsItemId = $('#ItemReservaId').val();
    var reserva = $('#Reserva').val();

    //$('#Chassis').prop('readonly', true);
    $('#Chassis').prop('readonly', false);
    $('#TiposDocumentos').empty();

    if (!isNumero(bookingCsItemId)) {
        toastr.error('Nenhum item encontrado para a reserva ' + reserva, 'Agendamento');
        return;
    }

    $.get(urlBase + 'Agendamento/ObterDetalhesItem?bookingCsItemId=' + bookingCsItemId + '&reserva=' + reserva, function (resultado) {

        if (resultado) {

            if (resultado.Veiculo) {
                $('#Chassis').prop('readonly', false);               
            }

            if (parseInt(resultado.ClassificacaoId) === 2) {
                $('#Chassis').prop('readonly', false);                
            }

            if (resultado.TipoCarga === 'BS') {
                //$('#Chassis').prop('readonly', true); Comentado para que possa atualizar os dados do chassi
                $('#Chassis').prop('readonly', false);
            }
            $('#ClassificacaoId').val(resultado.ClassificacaoId);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#btnAdicionarUpload').click(function (e) {

    var form = $('#frmCadastrarDocumentos');

    var formData = new FormData();
    var totalFiles = document.getElementById("Upload").files.length;

    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementById("Upload").files[i];
        formData.append("Upload", file);
        formData.append("TiposDocumentos", $('#TiposDocumentos').val());
        formData.append("BookingCsItemId", $('#ItemReservaId').val());
    }

    e.preventDefault();
    e.stopImmediatePropagation();

    $('#btnAdicionarUpload')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var xhr = new XMLHttpRequest();
    xhr.open('POST', urlBase + 'Agendamento/CadastrarDocumento');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {

                $('#tblDocumentos').html(xhr.responseText);

                $("#Upload").val('');

                $('#btnAdicionarUpload')
                    .html('<i class="fas fa-plus"></i>')
                    .removeClass('disabled');

            } else if (xhr.status == 400) {

                var retorno = JSON.parse(xhr.response);

                if (retorno != null) {
                    if (retorno.erros != null) {

                        var msg = retorno.erros[0].ErrorMessage;

                        $('#msgErro').html(msg).removeClass('invisivel');

                    }
                } else {
                    if (xhr.statusText) {
                        toastr.error(data.statusText, 'Agendamento');
                    } else {
                        toastr.error('Falha ao incluir a Danfe', 'Agendamento');
                    }
                }

                $('#btnAdicionarUpload')
                    .html('<i class="fas fa-plus"></i>')
                    .removeClass('disabled');
            }
        }
    };
    xhr.send(formData);
});
function obterArquivoDanfe(danfe) {

    danfe = $('#danfe_pesquisada').val();

    if (danfe !=='') {
        $.get(urlBase + 'Agendamento/ObterDanfeArquivo?danfe=' + danfe, function (resultado) {
            $('#msgErro')
                .html('')
                .addClass('invisivel');
            if (resultado) {

                var nfe = resultado;
                var node = parseXml(nfe.ArquivoXml);

                var numero = node.getElementsByTagName('cNF')[0].innerHTML;
                var serie = node.getElementsByTagName('serie')[0].innerHTML;
                var emissor = node.getElementsByTagName('CNPJ')[0].innerHTML;
                var data = node.getElementsByTagName('dhEmi')[0].innerHTML;
                var cfop = node.getElementsByTagName('CFOP')[0].innerHTML;
                var valor = node.getElementsByTagName('vNF')[0].innerHTML;
                var qtd = node.getElementsByTagName('indTot')[0].innerHTML;
                var pesob = node.getElementsByTagName('pesoB')[0].innerHTML;

                $('#CFOP').val(cfop);
                $('#Danfe').hide();
                $('#Danfe').val(nfe.Danfe);
                $('#numero').html(numero);
                $('#serie').html(serie);
                $('#data').html(data);
                $('#valor').html(valor);
                $('#emisso').html(emissor);
                $('#qtd').html(qtd);
                $('#pesobruto').html(pesob);
                $('#xml').val(nfe.ArquivoXml);
            } else {
                $('#msgErro')
                    .html('<p>Danfe nao encontrada!</p>')
                    .removeClass('invisivel');
            }
        }).fail(function (data) {
            toastr.error(data.statusText, 'Agendamento');

        });
    }
    
    function parseXml(xml) {
        var dom = null;
        if (window.DOMParser) {
            try {
                dom = (new DOMParser()).parseFromString(xml, "text/xml");
            }
            catch (e) { dom = null; }
        }
        else if (window.ActiveXObject) {
            try {
                dom = new ActiveXObject('Microsoft.XMLDOM');
                dom.async = false;
                if (!dom.loadXML(xml)) // parse error ..

                    window.alert(dom.parseError.reason + dom.parseError.srcText);
            }
            catch (e) { dom = null; }
        }
        else
            alert("cannot parse xml string!");
        return dom;
    }
    //convert XML to JSON
}
