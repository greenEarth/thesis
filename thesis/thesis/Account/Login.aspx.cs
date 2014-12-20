using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;


public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }
    protected void login_click(object sender, EventArgs e)
    {
        SqlConnection cn = new SqlConnection("server=localhost\\database1;integrated security=true;initial catalog=cloud");
        cn.Open();

        SqlCommand sq = new SqlCommand("login_auth", cn);
        sq.CommandType = CommandType.StoredProcedure;

        SqlParameter user_name = new SqlParameter("@user_name", SqlDbType.VarChar, 15);
        user_name.Value = username.Text;
        sq.Parameters.Add(user_name);

        SqlParameter password = new SqlParameter("@pass", SqlDbType.VarChar, 30);
        password.Value = Password.Text;
        sq.Parameters.Add(password);

        SqlParameter result = new SqlParameter("@result", SqlDbType.Decimal, 1);
        result.Direction = ParameterDirection.Output;
        result.Value = 0;
        sq.Parameters.Add(result);

        sq.ExecuteNonQuery();
        cn.Close();
        if (Convert.ToInt32(result.Value) == 1)
        {
            Label1.Text = "";
            FormsAuthentication.RedirectFromLoginPage(username.Text, true);
            Session["user_name"] = username.Text;
            Response.Redirect("~/About.aspx");

        }
        else
        {
            Label1.Text = "Incorrect Username/Password...";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }
}
