sap.ui.define([
    "ui5/carro/controller/BaseController",
    "sap/ui/model/json/JSONModel",
    "ui5/carro/model/formatter"

], function (BaseController, JSONModel, Formatter) {
    "use strict";

    var NomeDaAPI = "Vendas"
    var idDoFiltroCpf = "FiltroCpf"
    var idDoFiltroNome = "FiltroNome"
    var quantidadeDeCaracteresDoCpf = 14
    var quantidadeDeCaracteresDoTelefone = 15
    var idDoFiltroTelefone = "FiltroTelefone"
    var idDOFiltroDataFinal = "FiltroDataFinal"
    var url = "http://localhost:5071/api/Vendas"    
    var idDoFiltroDataInicial = "FiltroDataInicial"


    return BaseController.extend("ui5.carro.controller.ListagemVenda", {
        formatter: Formatter,
        carregarListaDeVendas() {
            fetch(url)
                .then((res) => res.json())
                .then((data) => {
                    const jsonModel = new JSONModel(data)

                    this.getView().setModel(jsonModel, NomeDaAPI);
                })
                .catch((err) => console.error(err));
        },

        onInit() {
            this.carregarListaDeVendas();
        },

        coletarNome() {
            const nome = this.oView.byId(idDoFiltroNome).getValue();
            return nome;
        },

        coletarCpf() {
            const cpf = this.oView.byId(idDoFiltroCpf).getValue();
            if (cpf.length < quantidadeDeCaracteresDoCpf) return '';
            return cpf;
        },

        coletarTelefone() {
            const telefone = this.oView.byId(idDoFiltroTelefone).getValue();
            if (telefone.length < quantidadeDeCaracteresDoTelefone) return '';
            return telefone;
        },

        coletarDataInicial() {
            const dataInicial = this.oView.byId(idDoFiltroDataInicial).getValue();
            if (!dataInicial) return '';
            return dataInicial;
        },

        coletarDataFinal() {
            const dataFinal = this.oView.byId(idDOFiltroDataFinal).getValue();
            if (!dataFinal) return '';
            return dataFinal;
        },

        aoFiltrar() {
            let urlDoFiltro = url + "?";
            const cpf = this.coletarCpf();
            const nome = this.coletarNome();
            const telefone = this.coletarTelefone();
            const dataFinal = this.coletarDataFinal();
            const dataInicial = this.coletarDataInicial();

            if (nome) {
                urlDoFiltro += "Nome=" + nome + "&";
            }

            if (cpf) {
                urlDoFiltro += "Cpf=" + cpf + "&";
            }

            if (telefone) {
                urlDoFiltro += "Telefone=" + telefone + "&";
            }

            if (dataInicial && dataFinal) {
                urlDoFiltro += "DataDeCompraInicial=" + dataInicial + "&" + "DataDeCompraFInal=" + dataFinal;
            }

            else if (dataInicial) {
                urlDoFiltro += "DataDeCompraInicial=" + dataInicial;
            }

            else if (dataFinal) {
                urlDoFiltro += "DataDeCompraFinal=" + dataFinal;
            }

            fetch(urlDoFiltro)
                .then((res) => res.json())
                .then((data) => {
                    const jsonModel = new JSONModel(data)

                    this.getView().setModel(jsonModel, NomeDaAPI);
                })
                .catch((err) => console.error(err));
        },

        adicionarVenda() {
            this.getRouter().navTo("appAdicionarVenda", {}, true);
        }
    });
});