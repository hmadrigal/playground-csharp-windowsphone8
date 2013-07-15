﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace HomeWork6.AirportMap.WebServiceX.AirportServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.webserviceX.NET", ConfigurationName="WebServiceX.AirportServiceReference.airportSoap")]
    public interface airportSoap {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.webserviceX.NET/getAirportInformationByISOCountryCode", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.IAsyncResult BegingetAirportInformationByISOCountryCode(string CountryAbbrviation, System.AsyncCallback callback, object asyncState);
        
        string EndgetAirportInformationByISOCountryCode(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.webserviceX.NET/getAirportInformationByCityOrAirportName", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.IAsyncResult BegingetAirportInformationByCityOrAirportName(string cityOrAirportName, System.AsyncCallback callback, object asyncState);
        
        string EndgetAirportInformationByCityOrAirportName(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.webserviceX.NET/GetAirportInformationByCountry", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.IAsyncResult BeginGetAirportInformationByCountry(string country, System.AsyncCallback callback, object asyncState);
        
        string EndGetAirportInformationByCountry(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.webserviceX.NET/getAirportInformationByAirportCode", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.IAsyncResult BegingetAirportInformationByAirportCode(string airportCode, System.AsyncCallback callback, object asyncState);
        
        string EndgetAirportInformationByAirportCode(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface airportSoapChannel : HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class getAirportInformationByISOCountryCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public getAirportInformationByISOCountryCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class getAirportInformationByCityOrAirportNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public getAirportInformationByCityOrAirportNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class GetAirportInformationByCountryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetAirportInformationByCountryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class getAirportInformationByAirportCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public getAirportInformationByAirportCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class airportSoapClient : System.ServiceModel.ClientBase<HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap>, HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap {
        
        private BeginOperationDelegate onBegingetAirportInformationByISOCountryCodeDelegate;
        
        private EndOperationDelegate onEndgetAirportInformationByISOCountryCodeDelegate;
        
        private System.Threading.SendOrPostCallback ongetAirportInformationByISOCountryCodeCompletedDelegate;
        
        private BeginOperationDelegate onBegingetAirportInformationByCityOrAirportNameDelegate;
        
        private EndOperationDelegate onEndgetAirportInformationByCityOrAirportNameDelegate;
        
        private System.Threading.SendOrPostCallback ongetAirportInformationByCityOrAirportNameCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetAirportInformationByCountryDelegate;
        
        private EndOperationDelegate onEndGetAirportInformationByCountryDelegate;
        
        private System.Threading.SendOrPostCallback onGetAirportInformationByCountryCompletedDelegate;
        
        private BeginOperationDelegate onBegingetAirportInformationByAirportCodeDelegate;
        
        private EndOperationDelegate onEndgetAirportInformationByAirportCodeDelegate;
        
        private System.Threading.SendOrPostCallback ongetAirportInformationByAirportCodeCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public airportSoapClient() {
        }
        
        public airportSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public airportSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public airportSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public airportSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public event System.EventHandler<getAirportInformationByISOCountryCodeCompletedEventArgs> getAirportInformationByISOCountryCodeCompleted;
        
        public event System.EventHandler<getAirportInformationByCityOrAirportNameCompletedEventArgs> getAirportInformationByCityOrAirportNameCompleted;
        
        public event System.EventHandler<GetAirportInformationByCountryCompletedEventArgs> GetAirportInformationByCountryCompleted;
        
        public event System.EventHandler<getAirportInformationByAirportCodeCompletedEventArgs> getAirportInformationByAirportCodeCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.BegingetAirportInformationByISOCountryCode(string CountryAbbrviation, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegingetAirportInformationByISOCountryCode(CountryAbbrviation, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.EndgetAirportInformationByISOCountryCode(System.IAsyncResult result) {
            return base.Channel.EndgetAirportInformationByISOCountryCode(result);
        }
        
        private System.IAsyncResult OnBegingetAirportInformationByISOCountryCode(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string CountryAbbrviation = ((string)(inValues[0]));
            return ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).BegingetAirportInformationByISOCountryCode(CountryAbbrviation, callback, asyncState);
        }
        
        private object[] OnEndgetAirportInformationByISOCountryCode(System.IAsyncResult result) {
            string retVal = ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).EndgetAirportInformationByISOCountryCode(result);
            return new object[] {
                    retVal};
        }
        
        private void OngetAirportInformationByISOCountryCodeCompleted(object state) {
            if ((this.getAirportInformationByISOCountryCodeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.getAirportInformationByISOCountryCodeCompleted(this, new getAirportInformationByISOCountryCodeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void getAirportInformationByISOCountryCodeAsync(string CountryAbbrviation) {
            this.getAirportInformationByISOCountryCodeAsync(CountryAbbrviation, null);
        }
        
        public void getAirportInformationByISOCountryCodeAsync(string CountryAbbrviation, object userState) {
            if ((this.onBegingetAirportInformationByISOCountryCodeDelegate == null)) {
                this.onBegingetAirportInformationByISOCountryCodeDelegate = new BeginOperationDelegate(this.OnBegingetAirportInformationByISOCountryCode);
            }
            if ((this.onEndgetAirportInformationByISOCountryCodeDelegate == null)) {
                this.onEndgetAirportInformationByISOCountryCodeDelegate = new EndOperationDelegate(this.OnEndgetAirportInformationByISOCountryCode);
            }
            if ((this.ongetAirportInformationByISOCountryCodeCompletedDelegate == null)) {
                this.ongetAirportInformationByISOCountryCodeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OngetAirportInformationByISOCountryCodeCompleted);
            }
            base.InvokeAsync(this.onBegingetAirportInformationByISOCountryCodeDelegate, new object[] {
                        CountryAbbrviation}, this.onEndgetAirportInformationByISOCountryCodeDelegate, this.ongetAirportInformationByISOCountryCodeCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.BegingetAirportInformationByCityOrAirportName(string cityOrAirportName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegingetAirportInformationByCityOrAirportName(cityOrAirportName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.EndgetAirportInformationByCityOrAirportName(System.IAsyncResult result) {
            return base.Channel.EndgetAirportInformationByCityOrAirportName(result);
        }
        
        private System.IAsyncResult OnBegingetAirportInformationByCityOrAirportName(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string cityOrAirportName = ((string)(inValues[0]));
            return ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).BegingetAirportInformationByCityOrAirportName(cityOrAirportName, callback, asyncState);
        }
        
        private object[] OnEndgetAirportInformationByCityOrAirportName(System.IAsyncResult result) {
            string retVal = ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).EndgetAirportInformationByCityOrAirportName(result);
            return new object[] {
                    retVal};
        }
        
        private void OngetAirportInformationByCityOrAirportNameCompleted(object state) {
            if ((this.getAirportInformationByCityOrAirportNameCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.getAirportInformationByCityOrAirportNameCompleted(this, new getAirportInformationByCityOrAirportNameCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void getAirportInformationByCityOrAirportNameAsync(string cityOrAirportName) {
            this.getAirportInformationByCityOrAirportNameAsync(cityOrAirportName, null);
        }
        
        public void getAirportInformationByCityOrAirportNameAsync(string cityOrAirportName, object userState) {
            if ((this.onBegingetAirportInformationByCityOrAirportNameDelegate == null)) {
                this.onBegingetAirportInformationByCityOrAirportNameDelegate = new BeginOperationDelegate(this.OnBegingetAirportInformationByCityOrAirportName);
            }
            if ((this.onEndgetAirportInformationByCityOrAirportNameDelegate == null)) {
                this.onEndgetAirportInformationByCityOrAirportNameDelegate = new EndOperationDelegate(this.OnEndgetAirportInformationByCityOrAirportName);
            }
            if ((this.ongetAirportInformationByCityOrAirportNameCompletedDelegate == null)) {
                this.ongetAirportInformationByCityOrAirportNameCompletedDelegate = new System.Threading.SendOrPostCallback(this.OngetAirportInformationByCityOrAirportNameCompleted);
            }
            base.InvokeAsync(this.onBegingetAirportInformationByCityOrAirportNameDelegate, new object[] {
                        cityOrAirportName}, this.onEndgetAirportInformationByCityOrAirportNameDelegate, this.ongetAirportInformationByCityOrAirportNameCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.BeginGetAirportInformationByCountry(string country, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetAirportInformationByCountry(country, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.EndGetAirportInformationByCountry(System.IAsyncResult result) {
            return base.Channel.EndGetAirportInformationByCountry(result);
        }
        
        private System.IAsyncResult OnBeginGetAirportInformationByCountry(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string country = ((string)(inValues[0]));
            return ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).BeginGetAirportInformationByCountry(country, callback, asyncState);
        }
        
        private object[] OnEndGetAirportInformationByCountry(System.IAsyncResult result) {
            string retVal = ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).EndGetAirportInformationByCountry(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetAirportInformationByCountryCompleted(object state) {
            if ((this.GetAirportInformationByCountryCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetAirportInformationByCountryCompleted(this, new GetAirportInformationByCountryCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetAirportInformationByCountryAsync(string country) {
            this.GetAirportInformationByCountryAsync(country, null);
        }
        
        public void GetAirportInformationByCountryAsync(string country, object userState) {
            if ((this.onBeginGetAirportInformationByCountryDelegate == null)) {
                this.onBeginGetAirportInformationByCountryDelegate = new BeginOperationDelegate(this.OnBeginGetAirportInformationByCountry);
            }
            if ((this.onEndGetAirportInformationByCountryDelegate == null)) {
                this.onEndGetAirportInformationByCountryDelegate = new EndOperationDelegate(this.OnEndGetAirportInformationByCountry);
            }
            if ((this.onGetAirportInformationByCountryCompletedDelegate == null)) {
                this.onGetAirportInformationByCountryCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetAirportInformationByCountryCompleted);
            }
            base.InvokeAsync(this.onBeginGetAirportInformationByCountryDelegate, new object[] {
                        country}, this.onEndGetAirportInformationByCountryDelegate, this.onGetAirportInformationByCountryCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.BegingetAirportInformationByAirportCode(string airportCode, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegingetAirportInformationByAirportCode(airportCode, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap.EndgetAirportInformationByAirportCode(System.IAsyncResult result) {
            return base.Channel.EndgetAirportInformationByAirportCode(result);
        }
        
        private System.IAsyncResult OnBegingetAirportInformationByAirportCode(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string airportCode = ((string)(inValues[0]));
            return ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).BegingetAirportInformationByAirportCode(airportCode, callback, asyncState);
        }
        
        private object[] OnEndgetAirportInformationByAirportCode(System.IAsyncResult result) {
            string retVal = ((HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap)(this)).EndgetAirportInformationByAirportCode(result);
            return new object[] {
                    retVal};
        }
        
        private void OngetAirportInformationByAirportCodeCompleted(object state) {
            if ((this.getAirportInformationByAirportCodeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.getAirportInformationByAirportCodeCompleted(this, new getAirportInformationByAirportCodeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void getAirportInformationByAirportCodeAsync(string airportCode) {
            this.getAirportInformationByAirportCodeAsync(airportCode, null);
        }
        
        public void getAirportInformationByAirportCodeAsync(string airportCode, object userState) {
            if ((this.onBegingetAirportInformationByAirportCodeDelegate == null)) {
                this.onBegingetAirportInformationByAirportCodeDelegate = new BeginOperationDelegate(this.OnBegingetAirportInformationByAirportCode);
            }
            if ((this.onEndgetAirportInformationByAirportCodeDelegate == null)) {
                this.onEndgetAirportInformationByAirportCodeDelegate = new EndOperationDelegate(this.OnEndgetAirportInformationByAirportCode);
            }
            if ((this.ongetAirportInformationByAirportCodeCompletedDelegate == null)) {
                this.ongetAirportInformationByAirportCodeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OngetAirportInformationByAirportCodeCompleted);
            }
            base.InvokeAsync(this.onBegingetAirportInformationByAirportCodeDelegate, new object[] {
                        airportCode}, this.onEndgetAirportInformationByAirportCodeDelegate, this.ongetAirportInformationByAirportCodeCompletedDelegate, userState);
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
        
        protected override HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap CreateChannel() {
            return new airportSoapClientChannel(this);
        }
        
        private class airportSoapClientChannel : ChannelBase<HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap>, HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap {
            
            public airportSoapClientChannel(System.ServiceModel.ClientBase<HomeWork6.AirportMap.WebServiceX.AirportServiceReference.airportSoap> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BegingetAirportInformationByISOCountryCode(string CountryAbbrviation, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = CountryAbbrviation;
                System.IAsyncResult _result = base.BeginInvoke("getAirportInformationByISOCountryCode", _args, callback, asyncState);
                return _result;
            }
            
            public string EndgetAirportInformationByISOCountryCode(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("getAirportInformationByISOCountryCode", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BegingetAirportInformationByCityOrAirportName(string cityOrAirportName, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = cityOrAirportName;
                System.IAsyncResult _result = base.BeginInvoke("getAirportInformationByCityOrAirportName", _args, callback, asyncState);
                return _result;
            }
            
            public string EndgetAirportInformationByCityOrAirportName(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("getAirportInformationByCityOrAirportName", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetAirportInformationByCountry(string country, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = country;
                System.IAsyncResult _result = base.BeginInvoke("GetAirportInformationByCountry", _args, callback, asyncState);
                return _result;
            }
            
            public string EndGetAirportInformationByCountry(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("GetAirportInformationByCountry", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BegingetAirportInformationByAirportCode(string airportCode, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = airportCode;
                System.IAsyncResult _result = base.BeginInvoke("getAirportInformationByAirportCode", _args, callback, asyncState);
                return _result;
            }
            
            public string EndgetAirportInformationByAirportCode(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("getAirportInformationByAirportCode", _args, result)));
                return _result;
            }
        }
    }
}
