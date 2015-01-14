using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Text;
using System.IO;



public partial class Account_ForgotPassword : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }


    protected void emailExistsOrNot(object sender, ServerValidateEventArgs args)
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cloudConnectionString"].ConnectionString);
        SqlCommand cm3 = new SqlCommand("select username, password from user_info where e_mail='" + args.Value + "'", cn);
        cn.Open();

        SqlDataReader reader = cm3.ExecuteReader();
        while (reader.Read())
        {
            if (reader.FieldCount != 0)
                args.IsValid = true;
                
        }
        cn.Close();
        args.IsValid = false;
    }

    protected void forgotPassButton_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> userPassMap = new Dictionary<string, string>();
        this.Page.Validate();
        if (!this.Page.IsValid) {
            return;
        }
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cloudConnectionString"].ConnectionString);
        SqlCommand cm3 = new SqlCommand("select username, password from user_info where e_mail='" + email.Text + "'", cn);
        cn.Open();

        SqlDataReader reader = cm3.ExecuteReader();
        while (reader.Read())
        {
            if (reader.FieldCount != 0)
                userPassMap.Add(reader.GetString(0), reader.GetString(1));

        }
        if (userPassMap.Count != 0)
        {
            sendMail(userPassMap);
        }
    
        cn.Close();        
    }

    private void sendMail(Dictionary<string, string> userPassMap) {
        MailMessage mail_message = new MailMessage("stagano4bit@gmail.com", email.Text);
        mail_message.Subject = "Your login credentials for 4-bit steganography project ";
        mail_message.Body = "This Mail is auto generated.<br/> Your login credentials are as follows:<br/>";
        mail_message.IsBodyHtml = true;
        foreach (KeyValuePair<string, string> entry in userPassMap)
        {
            mail_message.Body = mail_message.Body + "<br/>username:" + entry.Key + " password:" + entry.Value+"<br/>";
        }
        SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587);
        NetworkCredential net_cred = new NetworkCredential("stagano4bit@gmail.com", "hello@1234");
        smpt.EnableSsl = true;
        smpt.Credentials = net_cred;
        smpt.Send(mail_message);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email is sent sueesfully.')", true);
    }
}
