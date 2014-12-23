using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Text;
using System.IO;



public partial class Account_Register : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            captcha();
        //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }
    
    int success = 0; 
    private Random rand = new Random();
    
    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        //FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

        //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
        //if (String.IsNullOrEmpty(continueUrl))
        //{
        //    continueUrl = "~/";
        //}
        //Response.Redirect(continueUrl);
    }


    protected void CreateUserButton_Click(object sender, EventArgs e)
    {
        //Response.Write(Session["verify"].ToString());
        if (captcha_code_text.Text == Session["verify"].ToString().ToUpper())
        {
            SqlConnection cn = new SqlConnection("Server=597f3a08-c665-4427-95bd-a409006df944.sqlserver.sequelizer.com;Database=db597f3a08c665442795bda409006df944;User ID=cqpzrlpskabvtqyq;Password=tb8QvJjmVLkqLGemi6wMiLvsTxGj8hCbBi7o3hDDCyjdqteY6XQWrsDQNP56banM;");
            cn.Open();

            SqlCommand sq = new SqlCommand("saving", cn);
            sq.CommandType = CommandType.StoredProcedure;

            SqlParameter user_name = new SqlParameter("@user", SqlDbType.VarChar, 15);
            user_name.Value = UserName.Text; ;
            sq.Parameters.Add(user_name);

            SqlParameter password = new SqlParameter("@pass", SqlDbType.VarChar, 30);
            password.Value = Password.Text;
            sq.Parameters.Add(password);


            SqlParameter dob = new SqlParameter("@DOB", SqlDbType.VarChar, 30);
            dob.Value = dob_text.Text;
            sq.Parameters.Add(dob);

            SqlParameter dor = new SqlParameter("@DOR", SqlDbType.VarChar, 30);
            dor.Value = DateTime.Now.ToShortDateString();
            sq.Parameters.Add(dor);

            SqlParameter address = new SqlParameter("@add", SqlDbType.VarChar, 80);
            address.Value = address_text.Text;
            sq.Parameters.Add(address);

            SqlParameter contact = new SqlParameter("@cont", SqlDbType.Decimal, 10);
            contact.Value = contact_text.Text;
            sq.Parameters.Add(contact);

            SqlParameter e_mail = new SqlParameter("@mail", SqlDbType.VarChar, 30);
            e_mail.Value = Email.Text;
            sq.Parameters.Add(e_mail);

            //SqlParameter picture = new SqlParameter("@pic", SqlDbType.VarBinary);
            //picture.Value = FileUpload1.FileBytes;
            //sq.Parameters.Add(picture);

            success = sq.ExecuteNonQuery();
            Label5.ForeColor = System.Drawing.Color.Black;
            cn.Close();
            if (success == 1)
                Response.Redirect("~/Login.aspx");
        }
        else
        {
            Label5.ForeColor = System.Drawing.Color.Red;
        }
    }

    public void captcha()
    {
        //Bitmap bmap = new Bitmap(118,50);
        //Graphics grfs = Graphics.FromImage(bmap);
        //grfs.Clear(Color.White);
        Random rnd = new Random();
        //grfs.DrawLine(Pens.Black, rnd.Next(0, 50), rnd.Next(10, 30), rnd.Next(0, 200), rnd.Next(0, 50));
        //grfs.DrawRectangle(Pens.Blue, rnd.Next(0, 20), rnd.Next(0, 20), rnd.Next(50, 80), rnd.Next(0, 20));
        //grfs.DrawLine(Pens.Red, rnd.Next(0, 50), rnd.Next(10, 30), rnd.Next(0, 200), rnd.Next(0, 20));
        string str = string.Format("{0:X}", rnd.Next(100000, 999999));
        Session["verify"] = str.ToLower();
        Font fnt = new Font("Arial", 20, FontStyle.Bold);
        //grfs.DrawString(str, fnt, Brushes.Red, 30,20);
        
       // grfs.DrawString(text.Substring(0,3),fnt, Brushes.Navy, 20, 20);
        //grfs.DrawString(text.Substring(3),fnt, Brushes.Navy, 50, 20);
        
        //grfs.Dispose();

        //string str_path = Server.MapPath("~/Account/aaa.jpg");
        //bmap.Save(str_path, ImageFormat.Jpeg);
        //bmap.Dispose();
        //Image1.ImageUrl = "~/Account/aaa.jpg";// Server.MapPath("~/Account/aaa.bmp");
        Image1.Text = str;
        //Image1.DataBind();
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        captcha();
    }
}
