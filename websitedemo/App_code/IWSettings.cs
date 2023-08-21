using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace InWebo.ApiDemo
{
    public class IWSettings
    {
        public static String p12file = ConfigurationManager.AppSettings["certfile"]; // Specify here the name of your certificate file.
        public static String p12password = ConfigurationManager.AppSettings["passphrase"]; // This is the password to access your certificate file
        public static int serviceId = int.Parse(ConfigurationManager.AppSettings["serviceId"]); // This is the id of your service.
        public static String inWeboLogin = ConfigurationManager.AppSettings["inWeboLogin"]; //An activated inWebo user login name
    }
}