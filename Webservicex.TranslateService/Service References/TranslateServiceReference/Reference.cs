﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace Webservicex.TranslateServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.webservicex.net", ConfigurationName="TranslateServiceReference.TranslateServiceSoap")]
    public interface TranslateServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.webservicex.net/Translate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.IAsyncResult BeginTranslate(Webservicex.TranslateServiceReference.Language LanguageMode, string Text, System.AsyncCallback callback, object asyncState);
        
        string EndTranslate(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18046")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.webservicex.net")]
    public enum Language {
        
        /// <remarks/>
        EnglishTOChinese,
        
        /// <remarks/>
        EnglishTOFrench,
        
        /// <remarks/>
        EnglishTOGerman,
        
        /// <remarks/>
        EnglishTOItalian,
        
        /// <remarks/>
        EnglishTOJapanese,
        
        /// <remarks/>
        EnglishTOKorean,
        
        /// <remarks/>
        EnglishTOPortuguese,
        
        /// <remarks/>
        EnglishTOSpanish,
        
        /// <remarks/>
        ChineseTOEnglish,
        
        /// <remarks/>
        FrenchTOEnglish,
        
        /// <remarks/>
        FrenchTOGerman,
        
        /// <remarks/>
        GermanTOEnglish,
        
        /// <remarks/>
        GermanTOFrench,
        
        /// <remarks/>
        ItalianTOEnglish,
        
        /// <remarks/>
        JapaneseTOEnglish,
        
        /// <remarks/>
        KoreanTOEnglish,
        
        /// <remarks/>
        PortugueseTOEnglish,
        
        /// <remarks/>
        RussianTOEnglish,
        
        /// <remarks/>
        SpanishTOEnglish,
        
        /// <remarks/>
        SpanishToFrench,
        
        /// <remarks/>
        PortugueseToFrench,
        
        /// <remarks/>
        ItalianToFrench,
        
        /// <remarks/>
        GreekToFrench,
        
        /// <remarks/>
        GermanToFrench,
        
        /// <remarks/>
        FrenchToGreek,
        
        /// <remarks/>
        FrenchToItalian,
        
        /// <remarks/>
        FrenchToPortuguese,
        
        /// <remarks/>
        FrenchToDutch,
        
        /// <remarks/>
        FrenchToSpanish,
        
        /// <remarks/>
        EnglishToRussian,
        
        /// <remarks/>
        EnglishToDutch,
        
        /// <remarks/>
        DutchToEnglish,
        
        /// <remarks/>
        DutchToFrench,
        
        /// <remarks/>
        GreekToEnglish,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TranslateServiceSoapChannel : Webservicex.TranslateServiceReference.TranslateServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TranslateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public TranslateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TranslateServiceSoapClient : System.ServiceModel.ClientBase<Webservicex.TranslateServiceReference.TranslateServiceSoap>, Webservicex.TranslateServiceReference.TranslateServiceSoap {
        
        private BeginOperationDelegate onBeginTranslateDelegate;
        
        private EndOperationDelegate onEndTranslateDelegate;
        
        private System.Threading.SendOrPostCallback onTranslateCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public TranslateServiceSoapClient() {
        }
        
        public TranslateServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TranslateServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TranslateServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TranslateServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<TranslateCompletedEventArgs> TranslateCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Webservicex.TranslateServiceReference.TranslateServiceSoap.BeginTranslate(Webservicex.TranslateServiceReference.Language LanguageMode, string Text, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginTranslate(LanguageMode, Text, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string Webservicex.TranslateServiceReference.TranslateServiceSoap.EndTranslate(System.IAsyncResult result) {
            return base.Channel.EndTranslate(result);
        }
        
        private System.IAsyncResult OnBeginTranslate(object[] inValues, System.AsyncCallback callback, object asyncState) {
            Webservicex.TranslateServiceReference.Language LanguageMode = ((Webservicex.TranslateServiceReference.Language)(inValues[0]));
            string Text = ((string)(inValues[1]));
            return ((Webservicex.TranslateServiceReference.TranslateServiceSoap)(this)).BeginTranslate(LanguageMode, Text, callback, asyncState);
        }
        
        private object[] OnEndTranslate(System.IAsyncResult result) {
            string retVal = ((Webservicex.TranslateServiceReference.TranslateServiceSoap)(this)).EndTranslate(result);
            return new object[] {
                    retVal};
        }
        
        private void OnTranslateCompleted(object state) {
            if ((this.TranslateCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.TranslateCompleted(this, new TranslateCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void TranslateAsync(Webservicex.TranslateServiceReference.Language LanguageMode, string Text) {
            this.TranslateAsync(LanguageMode, Text, null);
        }
        
        public void TranslateAsync(Webservicex.TranslateServiceReference.Language LanguageMode, string Text, object userState) {
            if ((this.onBeginTranslateDelegate == null)) {
                this.onBeginTranslateDelegate = new BeginOperationDelegate(this.OnBeginTranslate);
            }
            if ((this.onEndTranslateDelegate == null)) {
                this.onEndTranslateDelegate = new EndOperationDelegate(this.OnEndTranslate);
            }
            if ((this.onTranslateCompletedDelegate == null)) {
                this.onTranslateCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnTranslateCompleted);
            }
            base.InvokeAsync(this.onBeginTranslateDelegate, new object[] {
                        LanguageMode,
                        Text}, this.onEndTranslateDelegate, this.onTranslateCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override Webservicex.TranslateServiceReference.TranslateServiceSoap CreateChannel() {
            return new TranslateServiceSoapClientChannel(this);
        }
        
        private class TranslateServiceSoapClientChannel : ChannelBase<Webservicex.TranslateServiceReference.TranslateServiceSoap>, Webservicex.TranslateServiceReference.TranslateServiceSoap {
            
            public TranslateServiceSoapClientChannel(System.ServiceModel.ClientBase<Webservicex.TranslateServiceReference.TranslateServiceSoap> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginTranslate(Webservicex.TranslateServiceReference.Language LanguageMode, string Text, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = LanguageMode;
                _args[1] = Text;
                System.IAsyncResult _result = base.BeginInvoke("Translate", _args, callback, asyncState);
                return _result;
            }
            
            public string EndTranslate(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("Translate", _args, result)));
                return _result;
            }
        }
    }
}
