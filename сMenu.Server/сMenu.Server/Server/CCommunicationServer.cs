using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Xml;

using cMenu.Common.Base;
using cMenu.Common.ErrorHandling;
using cMenu.Communication.Server.Configuration;
using cMenu.Communication.Server.Environment;
using cMenu.Communication.Server;

namespace cMenu.Communication.Server
{
    public class CCommunicationServer : CBaseEntity
    {
        #region PROTECTED FIELDS
        protected bool _allowWebRequests = false;
        protected CCommunicationServerConfiguration _configuration;
        protected Uri _baseAddress;

        protected WebServiceHost _baseHost;
        protected BasicHttpBinding _basicBinding;
        protected WebHttpBinding _webBinding;

        protected ServiceMetadataBehavior _metadataBehaviour;
        protected WebHttpBehavior _webBehaviour;

        protected Type _serverInterfaceType;
        protected Type _serverPrimaryType;
        protected EnCommunicationServerStatus _status = EnCommunicationServerStatus.EDisabled;
        #endregion

        #region EVENTS
        public event OnCommunicationServerStatusChangedDelegate OnCommunicationServerStatusChanged; 
        #endregion

        #region PUBLIC FIELDS
        public CCommunicationServerConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                _configuration =  value;
            }
        }
        public Type ServerInterfaceType
        {
            get { return _serverInterfaceType; }
            set { _serverInterfaceType = value; }
        }
        public Type ServerPrimaryType
        {
            get { return _serverPrimaryType; }
            set { _serverPrimaryType = value; }
        }
        public EnCommunicationServerStatus Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                if (this.OnCommunicationServerStatusChanged != null)
                    this.OnCommunicationServerStatusChanged(this, this._status);
            }
        }
        public bool AllowWebRequests
        {
            get { return _allowWebRequests; }
            set { _allowWebRequests = value; }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected int _init()
        {
            try
            {
                this._baseAddress = new Uri(this._configuration.Address);
                this._baseHost = new WebServiceHost(this._serverPrimaryType, this._baseAddress);                

                this._metadataBehaviour = new ServiceMetadataBehavior();
                this._metadataBehaviour.HttpGetEnabled = true;
                this._metadataBehaviour.HttpGetUrl = this._baseAddress;
                this._baseHost.Description.Behaviors.Add(this._metadataBehaviour);                

                this._basicBinding = new BasicHttpBinding();
                this._basicBinding.MaxBufferPoolSize = Int32.MaxValue;
                this._basicBinding.MaxReceivedMessageSize = Int32.MaxValue;
                this._basicBinding.MaxBufferSize = Int32.MaxValue;
                this._basicBinding.TransferMode = TransferMode.Streamed;
                this._basicBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                this._basicBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                this._basicBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                this._basicBinding.ReaderQuotas.MaxDepth = 32;
                this._basicBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

                if (this._allowWebRequests)
                {
                    this._webBehaviour = new WebHttpBehavior();

                    this._webBinding = new WebHttpBinding();
                    this._webBinding.MaxBufferPoolSize = Int32.MaxValue;
                    this._webBinding.MaxReceivedMessageSize = Int32.MaxValue;
                    this._webBinding.MaxBufferSize = Int32.MaxValue;
                    this._webBinding.TransferMode = TransferMode.Streamed;
                    this._webBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    this._webBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                    this._webBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    this._webBinding.ReaderQuotas.MaxDepth = 32;
                    this._webBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                }
            }
            catch (Exception ex)
            { 
                CErrorHelper.sThrowException("ERROR_MSG_SERVER_INIT", ex);
                return -2;
            }

            try
            {
                this._baseHost.AddServiceEndpoint(this._serverInterfaceType, this._basicBinding, this._configuration.Address);
                if (this._allowWebRequests)
                {                                        
                    var Endpoint = this._baseHost.AddServiceEndpoint(_serverInterfaceType, this._webBinding, "Web");
                    Endpoint.Behaviors.Add(this._webBehaviour);
                }
            }
            catch (Exception ex)
            { 
                CErrorHelper.sThrowException("ERROR_MSG_SERVER_ENDPOINT", ex);
                return -2;
            }

            return -1;
        }
        #endregion

        #region CONSTRUCTORS & DESTRUCTORS
        public CCommunicationServer()
            : base(Guid.NewGuid())
        {
            this._name = this.GetType().ToString();
        }
        public CCommunicationServer(Guid ID)
            : base(ID)
        {
            this._name = this.GetType().ToString();
        }
        public CCommunicationServer(Guid ID, string Name)
            : base(ID, Name)
        { }
        ~CCommunicationServer()
        {
            this.Stop();
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int Initialize()
        {
            var R = this._init();
            if (R == -1)
            {
                this.Status = EnCommunicationServerStatus.EReady;
            }
            return R;
        }
        public int Start()
        {
            try
            { 
                this._baseHost.Open();
                this.Status = EnCommunicationServerStatus.EEnabled;
            }
            catch (Exception ex)
            { 
                CErrorHelper.sThrowException("ERROR_MSG_SERVER_START", ex);
                return -2;
            }

            return -1;
        }
        public int Stop()
        {
            try
            {
                this._baseHost.Close();
                this.Status = EnCommunicationServerStatus.EDisabled;
            }
            catch (Exception ex)
            {
                CErrorHelper.sThrowException("ERROR_MSG_SERVER_STOP", ex);
                return -2;
            }
            return -1;
        }
        #endregion
    }
}
