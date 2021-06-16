﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Ecoporto.AgendamentoCS.WsBDCC {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WsSincronoSoap", Namespace="http://tecondi.com.br/services/TecondiBdccWs")]
    public partial class WsSincrono : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ConsultaCpfOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaRenavamOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaCrachaOperationCompleted;
        
        private System.Threading.SendOrPostCallback RegistraEntradaOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerificacaoSGBDCCOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListaAcessosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaCpfIntranetOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaCNPJIntranetOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaEmpresaPorCnpjOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WsSincrono() {
            this.Url = global::Ecoporto.AgendamentoCS.Properties.Settings.Default.Ecoporto_AgendamentoCS_WsBDCC_WsSincrono;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ConsultaCpfCompletedEventHandler ConsultaCpfCompleted;
        
        /// <remarks/>
        public event ConsultaRenavamCompletedEventHandler ConsultaRenavamCompleted;
        
        /// <remarks/>
        public event ConsultaCrachaCompletedEventHandler ConsultaCrachaCompleted;
        
        /// <remarks/>
        public event RegistraEntradaCompletedEventHandler RegistraEntradaCompleted;
        
        /// <remarks/>
        public event VerificacaoSGBDCCCompletedEventHandler VerificacaoSGBDCCCompleted;
        
        /// <remarks/>
        public event ListaAcessosCompletedEventHandler ListaAcessosCompleted;
        
        /// <remarks/>
        public event ConsultaCpfIntranetCompletedEventHandler ConsultaCpfIntranetCompleted;
        
        /// <remarks/>
        public event ConsultaCNPJIntranetCompletedEventHandler ConsultaCNPJIntranetCompleted;
        
        /// <remarks/>
        public event ConsultaEmpresaPorCnpjCompletedEventHandler ConsultaEmpresaPorCnpjCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ConsultaCpf", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TecondiBdccResponse ConsultaCpf(string cpf, int registraEntradaSemCracha, string cnpj, int autonomo) {
            object[] results = this.Invoke("ConsultaCpf", new object[] {
                        cpf,
                        registraEntradaSemCracha,
                        cnpj,
                        autonomo});
            return ((TecondiBdccResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaCpfAsync(string cpf, int registraEntradaSemCracha, string cnpj, int autonomo) {
            this.ConsultaCpfAsync(cpf, registraEntradaSemCracha, cnpj, autonomo, null);
        }
        
        /// <remarks/>
        public void ConsultaCpfAsync(string cpf, int registraEntradaSemCracha, string cnpj, int autonomo, object userState) {
            if ((this.ConsultaCpfOperationCompleted == null)) {
                this.ConsultaCpfOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaCpfOperationCompleted);
            }
            this.InvokeAsync("ConsultaCpf", new object[] {
                        cpf,
                        registraEntradaSemCracha,
                        cnpj,
                        autonomo}, this.ConsultaCpfOperationCompleted, userState);
        }
        
        private void OnConsultaCpfOperationCompleted(object arg) {
            if ((this.ConsultaCpfCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaCpfCompleted(this, new ConsultaCpfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ConsultaRenavam", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TecondiBdccResponse ConsultaRenavam(string renavam, int registraEntradaSemCracha) {
            object[] results = this.Invoke("ConsultaRenavam", new object[] {
                        renavam,
                        registraEntradaSemCracha});
            return ((TecondiBdccResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaRenavamAsync(string renavam, int registraEntradaSemCracha) {
            this.ConsultaRenavamAsync(renavam, registraEntradaSemCracha, null);
        }
        
        /// <remarks/>
        public void ConsultaRenavamAsync(string renavam, int registraEntradaSemCracha, object userState) {
            if ((this.ConsultaRenavamOperationCompleted == null)) {
                this.ConsultaRenavamOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaRenavamOperationCompleted);
            }
            this.InvokeAsync("ConsultaRenavam", new object[] {
                        renavam,
                        registraEntradaSemCracha}, this.ConsultaRenavamOperationCompleted, userState);
        }
        
        private void OnConsultaRenavamOperationCompleted(object arg) {
            if ((this.ConsultaRenavamCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaRenavamCompleted(this, new ConsultaRenavamCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ConsultaCracha", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TecondiBdccCrachaResponse ConsultaCracha(string cracha) {
            object[] results = this.Invoke("ConsultaCracha", new object[] {
                        cracha});
            return ((TecondiBdccCrachaResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaCrachaAsync(string cracha) {
            this.ConsultaCrachaAsync(cracha, null);
        }
        
        /// <remarks/>
        public void ConsultaCrachaAsync(string cracha, object userState) {
            if ((this.ConsultaCrachaOperationCompleted == null)) {
                this.ConsultaCrachaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaCrachaOperationCompleted);
            }
            this.InvokeAsync("ConsultaCracha", new object[] {
                        cracha}, this.ConsultaCrachaOperationCompleted, userState);
        }
        
        private void OnConsultaCrachaOperationCompleted(object arg) {
            if ((this.ConsultaCrachaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaCrachaCompleted(this, new ConsultaCrachaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/RegistraEntrada", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void RegistraEntrada(string numeroCracha, string tipoDocumento, string numeroDocumento, string local, string usuarioLogado, string ip) {
            this.Invoke("RegistraEntrada", new object[] {
                        numeroCracha,
                        tipoDocumento,
                        numeroDocumento,
                        local,
                        usuarioLogado,
                        ip});
        }
        
        /// <remarks/>
        public void RegistraEntradaAsync(string numeroCracha, string tipoDocumento, string numeroDocumento, string local, string usuarioLogado, string ip) {
            this.RegistraEntradaAsync(numeroCracha, tipoDocumento, numeroDocumento, local, usuarioLogado, ip, null);
        }
        
        /// <remarks/>
        public void RegistraEntradaAsync(string numeroCracha, string tipoDocumento, string numeroDocumento, string local, string usuarioLogado, string ip, object userState) {
            if ((this.RegistraEntradaOperationCompleted == null)) {
                this.RegistraEntradaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegistraEntradaOperationCompleted);
            }
            this.InvokeAsync("RegistraEntrada", new object[] {
                        numeroCracha,
                        tipoDocumento,
                        numeroDocumento,
                        local,
                        usuarioLogado,
                        ip}, this.RegistraEntradaOperationCompleted, userState);
        }
        
        private void OnRegistraEntradaOperationCompleted(object arg) {
            if ((this.RegistraEntradaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegistraEntradaCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/VerificacaoSGBDCC", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string VerificacaoSGBDCC() {
            object[] results = this.Invoke("VerificacaoSGBDCC", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void VerificacaoSGBDCCAsync() {
            this.VerificacaoSGBDCCAsync(null);
        }
        
        /// <remarks/>
        public void VerificacaoSGBDCCAsync(object userState) {
            if ((this.VerificacaoSGBDCCOperationCompleted == null)) {
                this.VerificacaoSGBDCCOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerificacaoSGBDCCOperationCompleted);
            }
            this.InvokeAsync("VerificacaoSGBDCC", new object[0], this.VerificacaoSGBDCCOperationCompleted, userState);
        }
        
        private void OnVerificacaoSGBDCCOperationCompleted(object arg) {
            if ((this.VerificacaoSGBDCCCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerificacaoSGBDCCCompleted(this, new VerificacaoSGBDCCCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ListaAcessos", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BdccAcesso[] ListaAcessos(string dataInicial, string dataFinal, string local) {
            object[] results = this.Invoke("ListaAcessos", new object[] {
                        dataInicial,
                        dataFinal,
                        local});
            return ((BdccAcesso[])(results[0]));
        }
        
        /// <remarks/>
        public void ListaAcessosAsync(string dataInicial, string dataFinal, string local) {
            this.ListaAcessosAsync(dataInicial, dataFinal, local, null);
        }
        
        /// <remarks/>
        public void ListaAcessosAsync(string dataInicial, string dataFinal, string local, object userState) {
            if ((this.ListaAcessosOperationCompleted == null)) {
                this.ListaAcessosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListaAcessosOperationCompleted);
            }
            this.InvokeAsync("ListaAcessos", new object[] {
                        dataInicial,
                        dataFinal,
                        local}, this.ListaAcessosOperationCompleted, userState);
        }
        
        private void OnListaAcessosOperationCompleted(object arg) {
            if ((this.ListaAcessosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListaAcessosCompleted(this, new ListaAcessosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ConsultaCpfIntranet", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TecondiBdccResponse ConsultaCpfIntranet(string cpf, int registraEntradaSemCracha) {
            object[] results = this.Invoke("ConsultaCpfIntranet", new object[] {
                        cpf,
                        registraEntradaSemCracha});
            return ((TecondiBdccResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaCpfIntranetAsync(string cpf, int registraEntradaSemCracha) {
            this.ConsultaCpfIntranetAsync(cpf, registraEntradaSemCracha, null);
        }
        
        /// <remarks/>
        public void ConsultaCpfIntranetAsync(string cpf, int registraEntradaSemCracha, object userState) {
            if ((this.ConsultaCpfIntranetOperationCompleted == null)) {
                this.ConsultaCpfIntranetOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaCpfIntranetOperationCompleted);
            }
            this.InvokeAsync("ConsultaCpfIntranet", new object[] {
                        cpf,
                        registraEntradaSemCracha}, this.ConsultaCpfIntranetOperationCompleted, userState);
        }
        
        private void OnConsultaCpfIntranetOperationCompleted(object arg) {
            if ((this.ConsultaCpfIntranetCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaCpfIntranetCompleted(this, new ConsultaCpfIntranetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ConsultaCNPJIntranet", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TecondiBdccResponse ConsultaCNPJIntranet(string cnpj, int autonomo) {
            object[] results = this.Invoke("ConsultaCNPJIntranet", new object[] {
                        cnpj,
                        autonomo});
            return ((TecondiBdccResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaCNPJIntranetAsync(string cnpj, int autonomo) {
            this.ConsultaCNPJIntranetAsync(cnpj, autonomo, null);
        }
        
        /// <remarks/>
        public void ConsultaCNPJIntranetAsync(string cnpj, int autonomo, object userState) {
            if ((this.ConsultaCNPJIntranetOperationCompleted == null)) {
                this.ConsultaCNPJIntranetOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaCNPJIntranetOperationCompleted);
            }
            this.InvokeAsync("ConsultaCNPJIntranet", new object[] {
                        cnpj,
                        autonomo}, this.ConsultaCNPJIntranetOperationCompleted, userState);
        }
        
        private void OnConsultaCNPJIntranetOperationCompleted(object arg) {
            if ((this.ConsultaCNPJIntranetCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaCNPJIntranetCompleted(this, new ConsultaCNPJIntranetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tecondi.com.br/services/TecondiBdccWs/ConsultaEmpresaPorCnpj", RequestNamespace="http://tecondi.com.br/services/TecondiBdccWs", ResponseNamespace="http://tecondi.com.br/services/TecondiBdccWs", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BdccCnpjEmpresa ConsultaEmpresaPorCnpj(string cnpj) {
            object[] results = this.Invoke("ConsultaEmpresaPorCnpj", new object[] {
                        cnpj});
            return ((BdccCnpjEmpresa)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaEmpresaPorCnpjAsync(string cnpj) {
            this.ConsultaEmpresaPorCnpjAsync(cnpj, null);
        }
        
        /// <remarks/>
        public void ConsultaEmpresaPorCnpjAsync(string cnpj, object userState) {
            if ((this.ConsultaEmpresaPorCnpjOperationCompleted == null)) {
                this.ConsultaEmpresaPorCnpjOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaEmpresaPorCnpjOperationCompleted);
            }
            this.InvokeAsync("ConsultaEmpresaPorCnpj", new object[] {
                        cnpj}, this.ConsultaEmpresaPorCnpjOperationCompleted, userState);
        }
        
        private void OnConsultaEmpresaPorCnpjOperationCompleted(object arg) {
            if ((this.ConsultaEmpresaPorCnpjCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaEmpresaPorCnpjCompleted(this, new ConsultaEmpresaPorCnpjCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tecondi.com.br/services/TecondiBdccWs")]
    public partial class TecondiBdccResponse {
        
        private string codigoRetornoField;
        
        private string descricaoRetornoField;
        
        /// <remarks/>
        public string CodigoRetorno {
            get {
                return this.codigoRetornoField;
            }
            set {
                this.codigoRetornoField = value;
            }
        }
        
        /// <remarks/>
        public string DescricaoRetorno {
            get {
                return this.descricaoRetornoField;
            }
            set {
                this.descricaoRetornoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tecondi.com.br/services/TecondiBdccWs")]
    public partial class BdccCnpjEmpresa {
        
        private string cnpjField;
        
        private string nomeField;
        
        private string situacaoField;
        
        /// <remarks/>
        public string Cnpj {
            get {
                return this.cnpjField;
            }
            set {
                this.cnpjField = value;
            }
        }
        
        /// <remarks/>
        public string Nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
            }
        }
        
        /// <remarks/>
        public string Situacao {
            get {
                return this.situacaoField;
            }
            set {
                this.situacaoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tecondi.com.br/services/TecondiBdccWs")]
    public partial class BdccAcesso {
        
        private string numeroCrachaField;
        
        private string tipoDocumentoField;
        
        private string numeroDocumentoField;
        
        private System.DateTime dataAcessoField;
        
        private string localizacaoField;
        
        private string usuarioLogadoField;
        
        private string ipField;
        
        /// <remarks/>
        public string NumeroCracha {
            get {
                return this.numeroCrachaField;
            }
            set {
                this.numeroCrachaField = value;
            }
        }
        
        /// <remarks/>
        public string TipoDocumento {
            get {
                return this.tipoDocumentoField;
            }
            set {
                this.tipoDocumentoField = value;
            }
        }
        
        /// <remarks/>
        public string NumeroDocumento {
            get {
                return this.numeroDocumentoField;
            }
            set {
                this.numeroDocumentoField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime DataAcesso {
            get {
                return this.dataAcessoField;
            }
            set {
                this.dataAcessoField = value;
            }
        }
        
        /// <remarks/>
        public string Localizacao {
            get {
                return this.localizacaoField;
            }
            set {
                this.localizacaoField = value;
            }
        }
        
        /// <remarks/>
        public string UsuarioLogado {
            get {
                return this.usuarioLogadoField;
            }
            set {
                this.usuarioLogadoField = value;
            }
        }
        
        /// <remarks/>
        public string Ip {
            get {
                return this.ipField;
            }
            set {
                this.ipField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tecondi.com.br/services/TecondiBdccWs")]
    public partial class TecondiBdccCrachaResponse {
        
        private string codigoRetornoField;
        
        private string descricaoRetornoField;
        
        private string codigoCrachaField;
        
        private string situacaoCrachaField;
        
        /// <remarks/>
        public string CodigoRetorno {
            get {
                return this.codigoRetornoField;
            }
            set {
                this.codigoRetornoField = value;
            }
        }
        
        /// <remarks/>
        public string DescricaoRetorno {
            get {
                return this.descricaoRetornoField;
            }
            set {
                this.descricaoRetornoField = value;
            }
        }
        
        /// <remarks/>
        public string CodigoCracha {
            get {
                return this.codigoCrachaField;
            }
            set {
                this.codigoCrachaField = value;
            }
        }
        
        /// <remarks/>
        public string SituacaoCracha {
            get {
                return this.situacaoCrachaField;
            }
            set {
                this.situacaoCrachaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ConsultaCpfCompletedEventHandler(object sender, ConsultaCpfCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaCpfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaCpfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TecondiBdccResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TecondiBdccResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ConsultaRenavamCompletedEventHandler(object sender, ConsultaRenavamCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaRenavamCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaRenavamCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TecondiBdccResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TecondiBdccResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ConsultaCrachaCompletedEventHandler(object sender, ConsultaCrachaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaCrachaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaCrachaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TecondiBdccCrachaResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TecondiBdccCrachaResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void RegistraEntradaCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void VerificacaoSGBDCCCompletedEventHandler(object sender, VerificacaoSGBDCCCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerificacaoSGBDCCCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerificacaoSGBDCCCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ListaAcessosCompletedEventHandler(object sender, ListaAcessosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListaAcessosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListaAcessosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BdccAcesso[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BdccAcesso[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ConsultaCpfIntranetCompletedEventHandler(object sender, ConsultaCpfIntranetCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaCpfIntranetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaCpfIntranetCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TecondiBdccResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TecondiBdccResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ConsultaCNPJIntranetCompletedEventHandler(object sender, ConsultaCNPJIntranetCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaCNPJIntranetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaCNPJIntranetCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TecondiBdccResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TecondiBdccResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ConsultaEmpresaPorCnpjCompletedEventHandler(object sender, ConsultaEmpresaPorCnpjCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaEmpresaPorCnpjCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaEmpresaPorCnpjCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BdccCnpjEmpresa Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BdccCnpjEmpresa)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591