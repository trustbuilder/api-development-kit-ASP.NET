using System;
using System.Web;

public partial class Default2 : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Default2));

    protected void Page_Load(object sender, EventArgs e)
    {

        errorOutput.Visible = false;

        if (Request.QueryString["error"] != null) {
            string error = Request.QueryString["error"];
            string errorMessage = HttpUtility.UrlDecode(error);

            errorOutput.InnerText = errorMessage;
            errorOutput.Visible = true;
        }

        BackButton.NavigateUrl = "Default.aspx";
    }
}
