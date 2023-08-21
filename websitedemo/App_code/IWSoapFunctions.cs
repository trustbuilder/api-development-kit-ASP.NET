using log4net;
using System;
using System.Configuration;
using System.Net;

namespace InWebo.ApiDemo
{
    using Authentication;
    using Provisioning;

    public class IWSoapFunctions
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IWSoapFunctions));

        private long serviceId = -1;

        private System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = null;

        public static String proxyHost = ConfigurationManager.AppSettings["proxyHost"];
        public static String proxyUser = ConfigurationManager.AppSettings["proxyUser"];
        public static String proxyPassword = ConfigurationManager.AppSettings["proxyPassword"];
        public static String proxyDomain = ConfigurationManager.AppSettings["proxyDomain"];

        private Provisioning _provisioningWS = null;

        private Provisioning provisioningWS
        {
            get
            {
                if (_provisioningWS == null)
                {
                    _provisioningWS = new Provisioning();
                    if (proxyHost != null && !proxyHost.Equals(""))
                    {
                        WebProxy proxy = new WebProxy(proxyHost);
                        NetworkCredential cred = new NetworkCredential(proxyUser, proxyPassword, proxyDomain);
                        proxy.Credentials = cred;
                        _provisioningWS.Proxy = proxy;
                        _provisioningWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        
                    }
                    _provisioningWS.ClientCertificates.Add(certificate);
                }
                return _provisioningWS;
            }
            set
            {
                _provisioningWS = value;
            }
        }

        private AuthenticationService _authWS = null;

        private AuthenticationService authWS
        {
            get
            {
                if (_authWS == null)
                {
                    _authWS = new AuthenticationService();
                    if (certificate != null)
                    {
                        _authWS.ClientCertificates.Add(certificate);
                    }
                }
                return _authWS;
            }
            set
            {
                _authWS = value;
            }
        }

        const string RESULT_OK = "OK";
        const string RESULT_NOK = "NOK";

        const int RET_OK = 1;
        const int RET_ERROR = -1;
        const int RET_QUIT = 0;

        public IWSoapFunctions(string p12File, string p12Password, long serviceId)
        {
            this.serviceId = serviceId;
            try
            {
                certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(@p12File, p12Password);
            }
            catch (SystemException e)
            {
                log.Error(e.Message);
                throw (e);
            }
           
        }

        //Authentication Functions
        public String soapAuthenticate(string login, string code)
        {
            string result = "NOK: request failed";
            try
            {
                result = authWS.authenticate(login, serviceId.ToString(), code);
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return result;
            }
            return result;
        }

        //User Management Functions
        public IWUserList loginsQuery(int offset, int maxResults, int querySort)
        {
            IWUserList list = new IWUserList();
            try
            {
                LoginsQueryResult queryResult = provisioningWS.loginsQuery(0, serviceId, offset, maxResults, querySort);
                if (!queryResult.err.StartsWith(RESULT_OK))
                {
                      return list;
                }
                list = IWUserList.FromQueryResult(queryResult);
          
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);

                return list;
            }
            return list;
        }

        public IWUser loginQuery(int loginId)
        {
            IWUser user = new IWUser();
            this.errcode = null;
            try
            {
                LoginQueryResult queryResult = provisioningWS.loginQuery(0, loginId);
                this.errcode = queryResult.err;
                if (!queryResult.err.StartsWith(RESULT_OK))
                {
                    return user;

                }
                user = IWUser.FromQueryResult(queryResult);
                
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return user;
            }
            return user;
        }

        public IWUserList loginSearch(String loginName, int exactMatch, int offset, int maxResults, int querySort)
        {
            IWUserList list = new IWUserList();
            this.errcode = null;
            try
            {
                LoginSearchResult queryResult = provisioningWS.loginSearch(0, serviceId, loginName, exactMatch, offset, maxResults, querySort);
                this.errcode = queryResult.err;
                if (!queryResult.err.StartsWith(RESULT_OK))
                {                    
                    return list;
                }
                list = IWUserList.FromQueryResult(queryResult);
            }
            catch (System.Net.WebException e)
            {
                this.errcode = "NOK:connectivity";
                this.exception = e.StackTrace;
                return list;
            }
            return list;
        }

        public IWUser loginCreate(string loginName, string firstname, string lastname, string mail, int status, int role, int codeType, string lang)
        {
            IWUser u = new IWUser();
            string phone = "";
            string extraFields = "";
            int access = 1;
            try
            {
                LoginCreateResult queryResult = provisioningWS.loginCreate(0, serviceId, loginName, firstname, lastname, mail, phone, status, role, access, codeType, lang, extraFields);
                this.errcode = queryResult.err;
                if (!queryResult.err.Equals(RESULT_OK))
                {
                    return u;
                }
                u.Id = queryResult.id;
                u.Login = loginName;
                u.Mail = mail;
                u.Role = role;
                u.Code = queryResult.code;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return u;
            }
            return u;
        }

        public String loginGetCodeFromLink(string longCode)
        {
            try
            {
                string queryResult = provisioningWS.loginGetCodeFromLink(longCode);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return RESULT_NOK;
            }
        }

        public String[] loginGetInfoFromLink(string longCode)
        {

            String[] loginInfo = new string[3];

            try
            {
                LoginCreateResult queryResult = provisioningWS.loginGetInfoFromLink(longCode);
                loginInfo = new string[]  {queryResult.err, queryResult.code, queryResult.id.ToString()};
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                loginInfo = new string[] { RESULT_NOK, "", "" };
            }

            return loginInfo;
        }

        public String loginUpdate(int loginId, string loginName, string firstname, string lastname, string mail, int status, int role)
        {
            string phone = "";
            string extraFields = "";
            try
            {
                string queryResult = provisioningWS.loginUpdate(0, serviceId, loginId, loginName, firstname, lastname, mail, phone, status, role, extraFields);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return RESULT_NOK;
            }
            
        }

        public String loginRestore(int loginId)
        {
            try
            {
                string queryResult = provisioningWS.loginRestore(0, serviceId, loginId);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK";
            }
        }

        public String loginResetPwd(int loginId, int codetype)
        {
            try
            {
                string queryResult = provisioningWS.loginResetPwdExtended(0, serviceId, loginId, codetype);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK:connectivity";
            }
        }

        public String loginSendByMail(int loginId)
        {
            try
            {
                string queryResult = provisioningWS.loginSendByMail(0, serviceId, loginId);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }
        }

        public String loginActivateCode(int loginId)
        {
            try
            {
                string queryResult = provisioningWS.loginActivateCode(0, serviceId, loginId);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }
        }

        public String loginDelete(int loginId)
        {
            try
            {
                string queryResult = provisioningWS.loginDelete(0, serviceId, loginId);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }
        }

        //Device Management
        public String loginAddDevice(int loginId, int codetype)
        {
            try
            {
                string queryResult = provisioningWS.loginAddDevice(0, serviceId, loginId, codetype);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }

        }

        public String loginDeleteMobileToken(int ToolId)
        {

            try
            {
                string queryResult = provisioningWS.loginDeleteTool(0, serviceId, ToolId, "ma");
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }

        }

        public String loginDeleteCloudToken(int ToolId)
        {

            try
            {
                string queryResult = provisioningWS.loginDeleteTool(0, serviceId, ToolId, "ca");
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }

        }

        public String loginDeleteMaccess(int ToolId)
        {

            try
            {
                string queryResult = provisioningWS.loginDeleteTool(0, serviceId, ToolId, "mac");
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }

        }

        //User Group Management
        public IWServiceGroupList serviceGroupsQuery(int offset, int maxResults)
        {
            IWServiceGroupList list = new IWServiceGroupList();
            this.errcode = null;
            try
            {
                ServiceGroupsQueryResult queryResult = provisioningWS.serviceGroupsQuery(0, serviceId, offset, maxResults);
                this.errcode = queryResult.err;

                if (!queryResult.err.StartsWith(RESULT_OK))
                {
                    return list;
                }
                list = IWServiceGroupList.FromServiceGroupsQuery(queryResult);
            }
            catch (System.Net.WebException e)
            {
                this.errcode = "NOK:connectivity";
                this.exception = e.StackTrace;
                return list;
            }
            return list;
        }

        public IWUserList loginsQueryByGroup(int groupId, int offset, int maxResults, int querySort)
        {
            IWUserList list = new IWUserList();
            try
            {
                LoginsQueryResult queryResult = provisioningWS.loginsQueryByGroup(0, groupId, offset, maxResults, querySort);
                this.errcode = queryResult.err;

                if (!queryResult.err.StartsWith(RESULT_OK))
                {
                    return list;
                }
                list = IWUserList.FromQueryByGroupResult(queryResult);

            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);

                return list;
            }
            return list;
        }

        public string groupMembershipCreate(int groupId, int loginId, int roleId)
        {
            try
            {
                string queryResult = provisioningWS.groupAccountCreate(0, groupId, loginId, roleId);
                log.Info("groupMembershipCreate groupId " + groupId.ToString() + " loginId " + loginId.ToString() + " result " + queryResult);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed";
            }
        }

        public string groupMembershipDelete(int groupId, int loginId)
        {
            try
            {
                string queryResult = provisioningWS.groupAccountDelete(0, groupId, loginId);
                log.Info("groupMembershipDelete groupId " + groupId.ToString() + " loginId " + loginId.ToString() + " result " + queryResult);
                return queryResult;
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                return "NOK: request failed"; ;
            }
        }

        public string errcode { get; set; }

        public string exception { get; set; }
    }
}
