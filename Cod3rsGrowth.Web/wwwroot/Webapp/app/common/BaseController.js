sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/core/routing/History",
	"sap/ui/core/UIComponent",
	"sap/m/MessageBox"

], function (Controller, History, UIComponent, MessageBox) {
	"use strict";

	var voltarUmaPagina = -1;
	var RotaListagem = "appListagem";

	return Controller.extend("ui5.carro.controller.BaseController", {
		getRouter() {
			return UIComponent.getRouterFor(this);
		},

		aoClicarNoBotaoVoltarDeveRetornarATelaDeListagem() {
			var history, previousHash;

			history = History.getInstance();
			previousHash = history.getPreviousHash();

			if (previousHash !== undefined) {
				window.history.go(voltarUmaPagina);
			} else {
				this.getRouter().navTo(RotaListagem, {}, true);
			}
		},

		processarEvento(action) {
			try {
				var promise = action();

				if (promise && typeof promise.catch === 'function') {
					promise.catch(error => MessageBox.error(error.message));
				}
			} catch (error) {
				MessageBox.error(error.message);
			}
		},

		_erroNaRequisicaoDaApi(erroRfc) {
			const mensagemErroPrincipal = erroRfc.Extensions.Erros.join(', ');
			const mensagemErroCompleta = `<p><strong>Status:</strong> ${erroRfc.Status}</p>` +
				`<p><strong>Detalhes:</strong><br/> ${erroRfc.Detail}</p>` +
				"<p>Para mais ajuda acesse <a href='//www.sap.com' target='_top'>aqui</a>.";

			MessageBox.error(mensagemErroPrincipal, {
				title: "Error",
				details: mensagemErroCompleta,
				styleClass: "sapUiResponsivePadding--header sapUiResponsivePadding--content sapUiResponsivePadding--footer",
				dependentOn: this.getView()
			});
		}
	});

});