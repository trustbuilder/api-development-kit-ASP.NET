using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace InWebo.ApiDemo
{

    class IWWebClient : WebClient
    {
        private System.Security.Cryptography.X509Certificates.X509Certificate certificate;
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.ClientCertificates.Add(certificate);
            return request;
        }
        public void setcertificate(System.Security.Cryptography.X509Certificates.X509Certificate certificate)
        {
            this.certificate = certificate;
        }

    }

    public class IWRestFunctions
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IWRestFunctions));
        
        private long serviceId = -1;
        
        private System.Security.Cryptography.X509Certificates.X509Certificate certificate = null;

        private string baseRestUrl = "https://api.myinwebo.com/FS?";

        public IWRestFunctions(string p12File, string p12Password, long serviceId)
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

        public string execRestCall(string actionUrlPart, string returnedOKField) {

            string finalUrl = baseRestUrl + actionUrlPart;

            log.Info(finalUrl);

            IWWebClient wc = new IWWebClient();
            wc.setcertificate(certificate);

            string result = "";

            try
            {
                string s = wc.DownloadString(finalUrl);

                log.Info(s);

                //Deserializing json string retrieved from REST call
                Dictionary<string, string> apiResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);

                if (apiResponse["err"] == "OK")
                {
                    result = apiResponse[returnedOKField];
                }
                else
                {
                    result = apiResponse["err"];
                }
            }
            catch (System.Net.WebException e)
            {
                log.Error(e.Message);
                result = "NOK: request failed";
            }

            return result;
        }
    }
}