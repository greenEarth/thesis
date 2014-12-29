using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

public partial class About : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cloudConnectionString"].ConnectionString);
    int id = 0;
    int size = 0;
    int piece_size = 0;
    int q_count=6;
    string name;
    string counter_no;
    string extn;

   static int range;
   static int new_range;
   static long imagesize;
   static string filename1;
   static string fileext;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        string query = "select f_extn,f_name,id from file_store where username='"+Session["user_name"]+"';";
        //random_code();
        startup(query);
    }
    public void random_code()
    {
        string username = Session["user_name"].ToString();
        int Count=0;
        while (Count < q_count)
        {
            Random rnd = new Random();
            counter_no= counter_no+rnd.Next(10);
            Count++;
        }
        Session["random_digit"] = counter_no;
        //string uu = Session["random_digit"].ToString();
    }

    public void mail_message()
    {
        SqlCommand cm3 = new SqlCommand("select e_mail from user_info where username='"+Session["user_name"].ToString()+"'", cn);
            cn.Open();
      
            DataSet ds3 = new DataSet();
              SqlDataAdapter da3 = new SqlDataAdapter(cm3);
           cn.Close();
           da3.Fill(ds3, "userid");

        MailMessage mail_message = new MailMessage("harishnagar123@gmail.com", "to_text.Text");
        mail_message.Subject = "6 Digit Secret Password ";//Session["random_digit"].ToString();
        mail_message.Body = "This Mail is auto generated.<br/> Your 6 digit secret password for downloading your file is <br/>"+counter_no.ToString();
        mail_message.IsBodyHtml = true;
        //mail_message.Body = mail_message.Body + "<br/> Greating from <br/>Harish<br/><img src =\"images/logo.jpg\" width=\"100px\" height=\"100px\">";
        //if (att_file.HasFile)
        //{
        //    att_file.SaveAs(Server.MapPath("attachment") + "/" + att_file.FileName);
        //    Attachment att = new Attachment(Server.MapPath("attachment") + "/" + att_file.FileName);
        //    mail_message.Attachments.Add(att);
        //}
        SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587);
        NetworkCredential net_cred = new NetworkCredential("harishnagar123@gmail.com","*********");
        smpt.EnableSsl = true;
        smpt.Credentials = net_cred;
        smpt.Send(mail_message);
    }
    public void startup(string query)
    {
        SqlCommand cmd = new SqlCommand(query, cn);////first should always be a extension nothing else
        cn.Open();
        DataSet ds = new DataSet();
        //ds.Tables.Clear();
        DataTable dataTable = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cn.Close();
        da.Fill(ds, "r");
        int count = ds.Tables[0].Rows.Count;
        int stop = 0;
        GridView1.DataSource = ds.Tables["r"];
        GridView1.RowStyle.Width = 2000;
        GridView1.DataBind();


        GridView1.RowStyle.Wrap = true;
        int col = 0, rows = 0;
        string filename = "null";
        TableCell cell = new TableCell();
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            GridViewRow row = GridView1.Rows[j];

            row.Cells.Clear();


            for (int jk = 0; jk < GridView1.Rows.Count; jk++)
            {
                if (jk < 5)
                {
                    cell = new TableCell();
                    row.Controls.Add(cell);
                }
            }
        }
        int i = 0;
        for (int a = 0; a < GridView1.Rows.Count; a++)
        {
            if (stop <= count)
            {

                col = 0;
                GridViewRow row = GridView1.Rows[a];


                for (int c = 0; c < 5; c++, i++)
                {
                    if (c < GridView1.Rows.Count && i < count)
                    {
                        ImageButton b = new ImageButton();

                        //b.Text = name;
                        b.ID = ds.Tables[0].Rows[i][2].ToString();
                        //l.Text = "idgnrgr" + a;
                        if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                            b.ImageUrl = "~/pdf_icon.png";
                        else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                            b.ImageUrl = "~/doc.png";
                        else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                            b.ImageUrl = "~/ppt.png";
                        else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                            b.ImageUrl = "~/xls.png";
                        else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                            b.ImageUrl = "~/txt-3.png";
                        else
                            b.ImageUrl = "~/unknown.png";
                        //GridView1.Columns[GridView1.Col
                        int pp = (ds.Tables[0].Rows[i][1].ToString()).LastIndexOf('.');
                        filename = (ds.Tables[0].Rows[i][1].ToString().Substring(0, pp)).ToString();
                        b.Attributes.Add("ID", ds.Tables[0].Rows[i][2].ToString());
                        string id = b.Attributes["ID"];
                       
                        b.Attributes.Add("Text", ds.Tables[0].Rows[i][1].ToString());
                        name = b.Attributes["Text"];
                        b.Style.Add("height", "64px");
                        b.Style.Add("wiidth", "64px");
                        b.Style.Add("margin", "40px");
                        b.Style.Add("FlatStyle", "Flat");
                        b.Style.Add("margin-right", "40px");
                        b.ToolTip = "Click to download this file";
                        b.Click += new ImageClickEventHandler(b_Click);
                        ////Attribute added here//////////////////////////////////
                        b.Attributes.Add("onclick", "window.open('test.aspx?id="+id+"&name="+name+"',null,'left=200, top=30, height=550, width= 500, status=no, resizable= yes, scrollbars= yes,toolbar= no,location= no, menubar= no');");
                        //b.OnClientClick = "ButtonClick('" + b.ClientID + "')";
                        row.Cells[c].Controls.Add(b);

                        if (GridView1.Rows.Count == 1)
                        {
                            GridViewRow row1 = GridView1.Rows[a];
                            Label l = new Label();
                            //b.Text = name;
                            //b.ID = "id" + i;
                            l.Text = filename;
                            if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                l.Text = filename;
                            else
                                l.Text = filename;

                            row1.Cells[c].Controls.Add(l);
                        }
                        else if (c < GridView1.Rows.Count && i < count && a < GridView1.Rows.Count)
                        {
                            GridViewRow row1 = GridView1.Rows[a + 1];
                            Label l = new Label();
                            //b.Text = name;
                            //b.ID = "id" + i;
                            l.Text = filename;
                            if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                l.Text = filename;
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                l.Text = filename;
                            else
                                l.Text = filename;

                            row1.Cells[c].Controls.Add(l);

                        }


                        stop++;

                    }


                }
                a++;

            }
            else
                break;


        }


    }
    //////retrieval fo files
    void b_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton b = sender as ImageButton;
        string id = b.Attributes["ID"];
        name = b.Attributes["Text"];
        random_code();
       
        //mail_message();
       
        
        //throw new NotImplementedException();
       
        //if (Session["get_file"] == "yes")
        //{
        //    string id = b.Attributes["ID"];
        //    name = b.Attributes["Text"];
        //    extn = name.Split('.')[1].ToString();
        //    SqlCommand cm = new SqlCommand("select COUNT(f_id),(select  f_size from file_store where id=" + id + "),(select  top 1 len(f_content) from file_piece where f_id=" + id + "group by f_content)from file_piece where f_id=" + id, cn);
        //    cn.Open();
        //    byte[] c; //= new byte[Convert.ToInt32(piece_size)];
        //    //c.Initialize();
        //    DataSet ds = new DataSet();
        //    //ds.Tables.Clear();
        //    DataTable dataTable = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cm);
        //    cn.Close();
        //    da.Fill(ds, "rs");
        //    //size = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //    //int temp_range = size / 102400;
        //    int temp_range = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //    size = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
        //    piece_size = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
        //    //byte[] c = new byte[204800];
        //    c = new byte[Convert.ToInt32(piece_size)];

        //    for (int i = 0; i < temp_range; i++)//(int i = 0; i <= temp_range; i++)
        //    {
        //        cn.Open();
        //        SqlCommand sq = new SqlCommand("retrieve", cn);
        //        sq.CommandType = CommandType.StoredProcedure;

        //        SqlParameter f_id = new SqlParameter("@f_id", SqlDbType.Decimal, 10);
        //        f_id.Value = Convert.ToDecimal(id);
        //        sq.Parameters.Add(f_id);

        //        SqlParameter f_name = new SqlParameter("@name", SqlDbType.VarChar, 50);
        //        f_name.Value = (name + i).ToString();
        //        sq.Parameters.Add(f_name);

        //        SqlParameter content = new SqlParameter("@cont", SqlDbType.VarBinary);
        //        content.Direction = ParameterDirection.Output;
        //        content.Value = c;
        //        sq.Parameters.Add(content);


        //        sq.ExecuteNonQuery();
        //        cn.Close();

        //        c = (byte[])content.Value;
        //        //byte[] fill = new byte[102400];
        //        byte[] fill = new byte[(Convert.ToInt32(piece_size) / 2)];
        //        fill.Initialize();
        //        for (int iy = 0; iy < (Convert.ToInt32(piece_size) / 2); iy++)
        //        {
        //            //if(iy%2!=0)
        //            try
        //            {
        //                if (iy == 0)
        //                    fill[iy] = c[iy];
        //                else
        //                    fill[iy] = c[iy * 2];
        //                //Response.Write(iy.ToString());
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.Write(ex.Message + iy.ToString());
        //            }
        //        }

        //        if (i != temp_range)
        //        {
        //            if (i == 0)
        //                using (FileStream stream = new FileStream(Server.MapPath(@"~/files/retrieved/") + name, FileMode.Create, FileAccess.Write, FileShare.Read))
        //                    stream.Write(fill, 0, (Convert.ToInt32(piece_size) / 2));//102400
        //            else
        //                using (FileStream stream = new FileStream(Server.MapPath(@"~/files/retrieved/") + name, FileMode.Append, FileAccess.Write, FileShare.Read))
        //                    stream.Write(fill, 0, (Convert.ToInt32(piece_size) / 2));
        //        }
        //        else
        //            using (FileStream stream = new FileStream(Server.MapPath(@"~/files/retrieved/") + name, FileMode.Append, FileAccess.Write, FileShare.Read))
        //                stream.Write(fill, 0, size - temp_range * (Convert.ToInt32(piece_size) / 2));
        //    }

        //    Response.ContentType = extn;
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
        //    Response.TransmitFile(Server.MapPath(@"~/files/retrieved/") + name);
        //    Response.Flush();

        //    string fileName = (Server.MapPath(@"~/files/retrieved/") + name);
        //    string Path = fileName;
        //    FileInfo file = new FileInfo(Path);
        //    if (file.Exists)
        //    {
        //        file.Delete();
        //    }
        //    Response.End();
        //}
    }

    public void ask_user_delete()
    {
        GridView1.Visible = false;
        Panel1.Visible = true;

        {
            SqlCommand cmd = new SqlCommand("select f_extn,f_name,id from file_store where username='" + Session["user_name"] + "';", cn);////first should always be a extension nothing else
            cn.Open();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            cn.Close();
            da1.Fill(ds1, "delete");
            int count = ds1.Tables[0].Rows.Count;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Name"), new DataColumn("id"), new DataColumn("Extension") });
            for (int i = 0; i < count; i++)
            {
                dt.Rows.Add(ds1.Tables[0].Rows[i][1].ToString(), ds1.Tables[0].Rows[i][2].ToString(), ds1.Tables[0].Rows[i][0].ToString());
            }
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();

        }
    }

    protected void Download_files_btn_Click(object sender, ImageClickEventArgs e)
    {
        upload_panel.Visible = false;
        GridView1.Visible = false;
        Panel1.Visible = true;
        ask_user_delete();
        // Download_files_btn.Attributes.Add("onclick","window.open('Child.aspx',null,'left=200, top=30, height=550, width= 500, status=no, resizable= yes, scrollbars= yes,toolbar= no,location= no, menubar= no');");

        // Response.Write("  <script language='javascript'> window.open('test.aspx','null','width=102,Height=72,fullscreen=0,location=0,scrollbars=0,menubar=0,toolbar=0'); </script>");
        //  Response.Write("  <script language='javascript'> window.open('test.aspx','null','help:no;dialogHeight:490px;dialogWidth:600px;scroll:no;status:no);</script>");
        //string queryString = "test.aspx";
        //string newWin = "window.open('" + queryString + "');";
        //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //OpenNewWindow("test.aspx");
    }

    protected void DeleteSelected_Click(object sender, EventArgs e)
    {
        bool atLeastOneRowDeleted = false;
        foreach (GridViewRow row in GridView2.Rows)
        {          
            CheckBox cb = (CheckBox)row.FindControl("chkRow");
            if (cb != null && cb.Checked)
            {              
                atLeastOneRowDeleted = true;
                string id = (row.Cells[2].FindControl("lbl_id") as Label).Text;
                DeleteFiles(id);
               // messagse_label.Text += string.Format("You have deleted file_id={0}<br />",id);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You have deleted file having ID : " + id + ".')", true);
            }
        }
        if (atLeastOneRowDeleted == false)
            //messagse_label.Text = "You havn't selected any file for deletion...";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You haven't selectes any file for deletion....')", true);
          //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowStatus", "javascript:alert('You haven't selectes any file for deletion....');", true);
        else
        {
            ask_user_delete();
            GridView2.DataBind();
        }
    }
    public void DeleteFiles(string File_id)
    {
        int id = Convert.ToInt32(File_id);
        SqlConnection cn = new SqlConnection("server=localhost\\database1;integrated security=true;initial catalog=cloud");
        SqlCommand sq = new SqlCommand("Delete_Files", cn);
        sq.CommandType = CommandType.StoredProcedure;

        SqlParameter files_ids = new SqlParameter("@id", SqlDbType.Decimal, 5);
        files_ids.Value = id;
        sq.Parameters.Add(files_ids);
        cn.Open();
        sq.ExecuteNonQuery();
        cn.Close();
    }

    public void OpenNewWindow(string url)
    {

        ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));

    }    
    protected void upload_files_btn_Click(object sender, ImageClickEventArgs e)
    {
        upload_panel.Visible = true; ;
        GridView1.Visible = false;
        Panel1.Visible = false ;
        
        //GridView1.Visible = true;
        //Panel1.Visible = false;
        //Response.Redirect("upload_files.aspx");
    }
    protected void recent_files_btn_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.Visible = true;
        Panel1.Visible = false;
        upload_panel.Visible = false;
       
        
        SqlCommand cmd = new SqlCommand("select top 5 f_extn,f_name,cr_date,id from file_store where ( username='"+
            Session["user_name"].ToString()+"' ) order by cr_date", cn);////first should always be a extension nothing else and id is the size
        cn.Open();
        DataSet ds = new DataSet();
        //ds.Tables.Clear();
        DataTable dataTable = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cn.Close();
        da.Fill(ds, "r");
        int count = ds.Tables[0].Rows.Count;
        int stop = 0;
        GridView1.DataSource = ds.Tables["r"];
        GridView1.RowStyle.Width = 2000;
        GridView1.DataBind();



        int col = 0, rows = 0;
        string filename = "null";
        TableCell cell = new TableCell();
        if (GridView1.Rows.Count != 1)
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                GridViewRow row = GridView1.Rows[j];

                row.Cells.Clear();


                for (int jk = 0; jk < GridView1.Rows.Count; jk++)
                {
                    if (jk < 5)
                    {
                        cell = new TableCell();
                        row.Controls.Add(cell);
                    }
                }
            }
        }
        else
            startup("select top 5 f_extn,f_name,cr_date,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' ) order by cr_date");
        int i = 0;
        if (GridView1.Rows.Count != 1)
        {
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                if (stop <= count)
                {

                    col = 0;
                    GridViewRow row = GridView1.Rows[a];


                    for (int c = 0; c < 5; c++, i++)
                    {

                        if (c < GridView1.Rows.Count && i < count)
                        {
                            ImageButton b = new ImageButton();

                            //b.Text = name;
                            b.ID = b.ID = ds.Tables[0].Rows[i][3].ToString();
                            //l.Text = "idgnrgr" + a;
                            if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                b.ImageUrl = "~/pdf_icon.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                b.ImageUrl = "~/doc.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                b.ImageUrl = "~/ppt.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                b.ImageUrl = "~/xls.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                b.ImageUrl = "~/txt-3.png";
                            else
                                b.ImageUrl = "~/unknown.png";
                            //GridView1.Columns[GridView1.Col
                            int pp = (ds.Tables[0].Rows[i][1].ToString()).LastIndexOf('.');
                            filename = (ds.Tables[0].Rows[i][1].ToString().Substring(0, pp)).ToString() + "<br>" + ds.Tables[0].Rows[i][2].ToString();
                            b.Attributes.Add("ID", ds.Tables[0].Rows[i][3].ToString());
                            b.Attributes.Add("Text", ds.Tables[0].Rows[i][1].ToString());
                            b.Style.Add("height", "64px");
                            b.Style.Add("wiidth", "64px");
                            b.Style.Add("margin", "40px");
                            b.Style.Add("FlatStyle", "Flat");
                            b.Style.Add("margin-right", "40px");
                            b.ToolTip = "Click to download this file";
                            b.Click += new ImageClickEventHandler(b_Click);
                            //b.OnClientClick = "ButtonClick('" + b.ClientID + "')";
                            row.Cells[c].Controls.Add(b);

                            if (c < GridView1.Rows.Count && i < count && a < GridView1.Rows.Count)
                            {

                                GridViewRow row1 = GridView1.Rows[a + 1];
                                Label l = new Label();
                                //b.Text = name;
                                //b.ID = "id" + i;
                                l.Text = filename;
                                if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                    l.Text = filename;
                                else
                                    l.Text = filename;

                                row1.Cells[c].Controls.Add(l);

                            }


                            stop++;

                        }


                    }
                    a++;

                }
                else
                    break;


            }
        }
    }
    protected void sort_name_btn_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.Visible = true;
        Panel1.Visible = false;
        upload_panel.Visible = false;
        SqlCommand cmd = new SqlCommand("select f_extn,f_name,size,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' ) order by f_name", cn);////first should always be a extension nothing else and id is the size
        cn.Open();
        DataSet ds = new DataSet();
        DataTable dataTable = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cn.Close();
        da.Fill(ds, "r");
        int count = ds.Tables[0].Rows.Count;
        int stop = 0;
        GridView1.DataSource = ds.Tables["r"];
        GridView1.RowStyle.Width = 2000;
        GridView1.DataBind();



        int col = 0, rows = 0;
        string filename = "null";
        TableCell cell = new TableCell();
        if (GridView1.Rows.Count != 1)
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                GridViewRow row = GridView1.Rows[j];

                row.Cells.Clear();


                for (int jk = 0; jk < GridView1.Rows.Count; jk++)
                {
                    if (jk < 5)
                    {
                        cell = new TableCell();
                        row.Controls.Add(cell);
                    }
                }
            }
        }
        else
            startup("select top 5 f_extn,f_name,cr_date,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' ) order by f_name");
        int i = 0;
        if (GridView1.Rows.Count != 1)
        {
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                if (stop <= count)
                {

                    col = 0;
                    GridViewRow row = GridView1.Rows[a];


                    for (int c = 0; c < 5; c++, i++)
                    {
                        if (c < GridView1.Rows.Count && i < count)
                        {
                            ImageButton b = new ImageButton();

                            //b.Text = name;
                            b.ID = b.ID = ds.Tables[0].Rows[i][3].ToString();
                            //l.Text = "idgnrgr" + a;
                            if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                b.ImageUrl = "~/pdf_icon.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                b.ImageUrl = "~/doc.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                b.ImageUrl = "~/ppt.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                b.ImageUrl = "~/xls.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                b.ImageUrl = "~/txt-3.png";
                            else
                                b.ImageUrl = "~/unknown.png";
                            //GridView1.Columns[GridView1.Col
                            int pp = (ds.Tables[0].Rows[i][1].ToString()).LastIndexOf('.');
                            filename = (ds.Tables[0].Rows[i][1].ToString().Substring(0, pp)).ToString();
                            b.Attributes.Add("ID", ds.Tables[0].Rows[i][3].ToString());
                            b.Attributes.Add("Text", ds.Tables[0].Rows[i][1].ToString());
                            b.Style.Add("height", "64px");
                            b.Style.Add("wiidth", "64px");
                            b.Style.Add("margin", "40px");
                            b.Style.Add("FlatStyle", "Flat");
                            b.Style.Add("margin-right", "40px");
                            b.ToolTip = "Click to download this file";
                            b.Click += new ImageClickEventHandler(b_Click);
                            //b.OnClientClick = "ButtonClick('" + b.ClientID + "')";
                            row.Cells[c].Controls.Add(b);

                            if (c < GridView1.Rows.Count && i < count && a < GridView1.Rows.Count)
                            {
                                GridViewRow row1 = GridView1.Rows[a + 1];
                                Label l = new Label();
                                //b.Text = name;
                                //b.ID = "id" + i;
                                l.Text = filename;
                                if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                    l.Text = filename;
                                else
                                    l.Text = filename;

                                row1.Cells[c].Controls.Add(l);

                            }


                            stop++;

                        }


                    }
                    a++;

                }
                else
                    break;


            }
        }
    }
    protected void sort_size_btn_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.Visible = true;
        Panel1.Visible = false;
        upload_panel.Visible = false;
        SqlCommand cmd = new SqlCommand("select f_extn,f_name,size,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' )  order by size", cn);////first should always be a extension nothing else and id is the size
        cn.Open();
        DataSet ds = new DataSet();
        //ds.Tables.Clear();
        DataTable dataTable = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cn.Close();
        da.Fill(ds, "r");
        int count = ds.Tables[0].Rows.Count;
        int stop = 0;
        GridView1.DataSource = ds.Tables["r"];
        GridView1.RowStyle.Width = 2000;
        GridView1.DataBind();



        int col = 0, rows = 0;
        string filename = "null";
        TableCell cell = new TableCell();
        if (GridView1.Rows.Count != 1)
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                GridViewRow row = GridView1.Rows[j];

                row.Cells.Clear();


                for (int jk = 0; jk < GridView1.Rows.Count; jk++)
                {
                    if (jk < 5)
                    {
                        cell = new TableCell();
                        row.Controls.Add(cell);
                    }
                }
            }
        }
        else
            startup("select top 5 f_extn,f_name,cr_date,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' ) order by size");
        int i = 0;
        if (GridView1.Rows.Count != 1)
        {
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                if (stop <= count)
                {

                    col = 0;
                    GridViewRow row = GridView1.Rows[a];


                    for (int c = 0; c < 5; c++, i++)
                    {
                        if (c < GridView1.Rows.Count && i < count)
                        {
                            ImageButton b = new ImageButton();

                            //b.Text = name;
                            b.ID = b.ID = ds.Tables[0].Rows[i][3].ToString();                        //l.Text = "idgnrgr" + a;
                            if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                b.ImageUrl = "~/pdf_icon.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                b.ImageUrl = "~/doc.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                b.ImageUrl = "~/ppt.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                b.ImageUrl = "~/xls.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                b.ImageUrl = "~/txt-3.png";
                            else
                                b.ImageUrl = "~/unknown.png";
                            //GridView1.Columns[GridView1.Col
                            string tt = (Convert.ToDouble(ds.Tables[0].Rows[i][2]) / (1024 * 1024)).ToString();
                            tt = tt.Remove(6);
                            int pp = (ds.Tables[0].Rows[i][1].ToString()).LastIndexOf('.');
                            filename = (ds.Tables[0].Rows[i][1].ToString().Substring(0, pp)).ToString() + "<br>  (" + tt + "   MB )";
                            b.Attributes.Add("ID", ds.Tables[0].Rows[i][3].ToString());
                            b.Attributes.Add("Text", ds.Tables[0].Rows[i][1].ToString());
                            b.Style.Add("height", "64px");
                            b.Style.Add("wiidth", "64px");
                            b.Style.Add("margin", "40px");
                            b.Style.Add("FlatStyle", "Flat");
                            b.Style.Add("margin-right", "40px");
                            b.ToolTip = "Click to download this file";
                            b.Click += new ImageClickEventHandler(b_Click);
                            //b.OnClientClick = "ButtonClick('" + b.ClientID + "')";
                            row.Cells[c].Controls.Add(b);

                            if (c < GridView1.Rows.Count && i < count && a < GridView1.Rows.Count)
                            {
                                GridViewRow row1 = GridView1.Rows[a + 1];
                                Label l = new Label();
                                //b.Text = name;
                                //b.ID = "id" + i;
                                l.Text = filename;
                                if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                    l.Text = filename;
                                else
                                    l.Text = filename;

                                row1.Cells[c].Controls.Add(l);

                            }


                            stop++;

                        }


                    }
                    a++;

                }
                else
                    break;
            }
        }
    }
    protected void sort_type_btn_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.Visible = true;
        Panel1.Visible = false;
        upload_panel.Visible = false;
        SqlCommand cmd = new SqlCommand("select f_extn,f_name,size,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' )  order by f_extn", cn);////first should always be a extension nothing else and id is the size
        cn.Open();
        DataSet ds = new DataSet();
        //ds.Tables.Clear();
        DataTable dataTable = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cn.Close();
        da.Fill(ds, "r");
        int count = ds.Tables[0].Rows.Count;
        int stop = 0;
        GridView1.DataSource = ds.Tables["r"];
        GridView1.RowStyle.Width = 2000;
        GridView1.DataBind();



        int col = 0, rows = 0;
        string filename = "null";
        TableCell cell = new TableCell();
        if (GridView1.Rows.Count != 1)
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                GridViewRow row = GridView1.Rows[j];

                row.Cells.Clear();


                for (int jk = 0; jk < GridView1.Rows.Count; jk++)
                {
                    if (jk < 5)
                    {
                        cell = new TableCell();
                        row.Controls.Add(cell);
                    }
                }
            }
        }
        else
            startup("select top 5 f_extn,f_name,cr_date,id from file_store where ( username='" +
            Session["user_name"].ToString() + "' ) order by size");
        int i = 0;
        if (GridView1.Rows.Count != 1)
        {
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                if (stop <= count)
                {

                    col = 0;
                    GridViewRow row = GridView1.Rows[a];


                    for (int c = 0; c < 5; c++, i++)
                    {
                        if (c < GridView1.Rows.Count && i < count)
                        {
                            ImageButton b = new ImageButton();

                            //b.Text = name;
                            b.ID = b.ID = ds.Tables[0].Rows[i][3].ToString();
                            //l.Text = "idgnrgr" + a;
                            if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                b.ImageUrl = "~/pdf_icon.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                b.ImageUrl = "~/doc.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                b.ImageUrl = "~/ppt.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                b.ImageUrl = "~/xls.png";
                            else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                b.ImageUrl = "~/txt-3.png";
                            else
                                b.ImageUrl = "~/unknown.png";
                            //GridView1.Columns[GridView1.Col
                            int pp = (ds.Tables[0].Rows[i][1].ToString()).LastIndexOf('.');
                            filename = (ds.Tables[0].Rows[i][1].ToString().Substring(0, pp)).ToString();
                            b.Attributes.Add("ID", ds.Tables[0].Rows[i][3].ToString());
                            b.Attributes.Add("Text", ds.Tables[0].Rows[i][1].ToString());
                            b.Style.Add("height", "64px");
                            b.Style.Add("wiidth", "64px");
                            b.Style.Add("margin", "40px");
                            b.Style.Add("FlatStyle", "Flat");
                            b.Style.Add("margin-right", "40px");
                            b.ToolTip = "Click to download this file";
                            b.Click += new ImageClickEventHandler(b_Click);
                            //b.OnClientClick = "ButtonClick('" + b.ClientID + "')";
                            row.Cells[c].Controls.Add(b);

                            if (c < GridView1.Rows.Count && i < count && a < GridView1.Rows.Count)
                            {
                                GridViewRow row1 = GridView1.Rows[a + 1];
                                Label l = new Label();
                                //b.Text = name;
                                //b.ID = "id" + i;
                                l.Text = filename;
                                if (ds.Tables[0].Rows[i][0].ToString() == ".pdf" || ds.Tables[0].Rows[i][0].ToString() == ".PDF")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".doc" || ds.Tables[0].Rows[i][0].ToString() == ".docx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".ppt" || ds.Tables[0].Rows[i][0].ToString() == ".pptx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".xls" || ds.Tables[0].Rows[i][0].ToString() == ".xlsx")
                                    l.Text = filename;
                                else if (ds.Tables[0].Rows[i][0].ToString() == ".txt" || ds.Tables[0].Rows[i][0].ToString() == ".TXT")
                                    l.Text = filename;
                                else
                                    l.Text = filename;

                                row1.Cells[c].Controls.Add(l);

                            }


                            stop++;

                        }


                    }
                    a++;

                }
                else
                    break;
            }
        }
    }

   
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //byte[] a;
        //byte[] pic;//=new byte[204800];
        //byte[] c = new byte[ 524343323 ];
        //pic.Initialize();
       
          if (! FileUpload1.HasFile||!FileUpload2.HasFile)//this.FileUpload1.HasFile || !this.FileUpload2.HasFile)
        {
            //Label7.Text = "No File/Photo Selected...";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No File/Photo Selected...')", true);
              //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('No File/Photo Selected...');", true);
        }
        else
        {
            // //FileUpload1.SaveAs(Server.MapPath(@"~/temp/") + FileUpload1.FileName);
            // //string image_ext = System.IO.Path.GetExtension(FileUpload1.FileName);
            fileext = System.IO.Path.GetExtension(FileUpload2. FileName);
            try
            {
                Bitmap image11;
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                filename1 = Path.GetFileName(FileUpload2.PostedFile.FileName);

                FileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                string ImagePath = Server.MapPath(@"Files/" + filename);
                //string ImagePath = FileUpload1.FileName;

                FileUpload2.SaveAs(Server.MapPath("Files/" + filename1));
                string pathSource = Server.MapPath(@"Files/" + filename1);
                // string pathSource = FileUpload2.FileName;
                image11 = new Bitmap(ImagePath, true);
                //image1 = new Bitmap(@"E:\Users\Public\Pictures\Sample Pictures\1.jpg", true);
                // string pathSource = @"I:\praveen\praveen_code\imp_links.txt";

                FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                // Session["file_size"] = numBytesToRead;
                fsSource.Read(bytes, 0, numBytesToRead);

                //int ImageSize= FileUpload1.PostedFile.ContentLength;
                range = FileUpload2.PostedFile.ContentLength;
                imagesize = FileUpload1.PostedFile.InputStream.Length;
                //int TotalPixel = image11.Width * image11.Height;
                //int size_of_file_in_image = (int)((TotalPixel * 3) / 8);
                //int TotalFilesNeeded = (int)((numBytesToRead / size_of_file_in_image) + 1);
                //int count = TotalFilesNeeded;
                int TotalPixel = image11.Width * image11.Height;
                int size_of_file_in_image = Convert.ToInt32(TotalPixel / 3);
                int TotalFilesNeeded = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(numBytesToRead) / Convert.ToDecimal(size_of_file_in_image)));
                int count = TotalFilesNeeded;
                //if(TotalFilesNeeded==1)
                //    save_file(image11, pathSource, count);
                //else

                byte[][] byte_2_array = new byte[TotalFilesNeeded][];

                for (int j = 0; j < TotalFilesNeeded; j++)
                {
                    if (j < TotalFilesNeeded - 1)
                    {
                        byte_2_array[j] = new byte[size_of_file_in_image];
                        for (int x = 0; x < byte_2_array[j].Length; x++)
                        {
                            byte_2_array[j][x] = bytes[j * size_of_file_in_image + x];
                            //if((j*byte_2_array[j].Length + x)<bytes.Length)
                            //Label2.Text = Label2.Text + "<br/>" + (j * size_of_file_in_image + x).ToString() + ": " + byte_2_array[j][x];
                        }
                        //Response.Write("Error");
                    }
                    else
                    {
                        byte_2_array[j] = new byte[numBytesToRead - j * size_of_file_in_image];
                        for (int x = 0; x < byte_2_array[j].Length; x++)
                        {
                            byte_2_array[j][x] = bytes[j * size_of_file_in_image + x];
                            //Label2.Text = Label2.Text + "<br/>" + (j * size_of_file_in_image + x).ToString() + ": " + byte_2_array[j][x];
                        }
                    }
                }

                for (int i = 0; i < TotalFilesNeeded; i++)
                {
                    image11 = new Bitmap(ImagePath, true);
                    //save_file(image11, pathSource,count);
                    encrypt(image11, ref byte_2_array[i], i + 1);
                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('file has been uploaded...')", true);
               
                //saving_files(a, pic, file_path, filename, fileext);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Unable to upload file. See message ->" + ex.ToString() + "')", true);
               
                //Response.Write(ex.Message);
            }
            //}
        }
    }


    public void encrypt(Bitmap image1, ref byte[] bytes, int count)
    {
        int i = 0, j;
        int bytes_index = 0;
        byte byte_read;
        int read_byte_flag = 0;
        char[] byte_array = new char[8];

        for (i = 0; i < image1.Width; i++)
        {
            for (j = 0; j < image1.Height; j++)
            {
                Color pixel_color = image1.GetPixel(i, j);

                byte red = Convert.ToByte(pixel_color.R);
                byte green = Convert.ToByte(pixel_color.G);
                byte blue = Convert.ToByte(pixel_color.B);

                char[] red_array = Convert.ToString(red, 2).PadLeft(8, '0').ToArray();
                char[] green_array = Convert.ToString(green, 2).PadLeft(8, '0').ToArray();
                char[] blue_array = Convert.ToString(blue, 2).PadLeft(8, '0').ToArray();

                if (read_byte_flag == 0 && bytes_index < bytes.Length)
                {
                    byte_read = bytes[bytes_index++];
                    //Label4.Text = Label4.Text + "<br/>" + "i= " + i.ToString() +" j =" +j.ToString() + " " +bytes_index.ToString()+ " : " + byte_read.ToString();
                    byte_array = Convert.ToString(byte_read, 2).PadLeft(8, '0').ToArray();
                }

                red_array[7] = byte_array[read_byte_flag++];
                green_array[7] = byte_array[read_byte_flag++];
                if (read_byte_flag < 8)
                {
                    blue_array[7] = byte_array[read_byte_flag++];
                }
                else
                    read_byte_flag = 0;

                string red_str = new string(red_array);
                string green_str = new string(green_array);
                string blue_str = new string(blue_array);

                int red_num = Convert.ToInt32(red_str, 2);
                int green_num = Convert.ToInt32(green_str, 2);
                int blue_num = Convert.ToInt32(blue_str, 2);

                image1.SetPixel(i, j, Color.FromArgb(pixel_color.A, red_num, green_num, blue_num));

                //if (bytes_index == 72)
                //    Response.Write("Value is 72");

                if (bytes_index == bytes.Length && read_byte_flag == 0)
                {
                    //Response.Write("<br/>byte_index=" + bytes_index.ToString() + "   flag: " + read_byte_flag.ToString());
                    //Response.Flush();
                    image1.Save(Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp", ImageFormat.MemoryBmp);
                    image1.Dispose();

                    //Response.Flush();
                    //string  aaaa= image1.Size.ToString();
                    System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp");
                    ImageConverter imageConverter = new ImageConverter();
                    byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
                    save_into_table(imageByte, count);
                    image1.Dispose();
                    string fileNamedel = (Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp");
                    string Path = fileNamedel;
                    FileInfo file = new FileInfo(Path);
                    if (file.Exists)
                    {
                        //file.Delete();
                    }
                    //Response.End();
                    return;
                }
            }

        }

        image1.Save(Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp", ImageFormat.MemoryBmp);
        image1.Dispose();
        System.Drawing.Image image11 = System.Drawing.Image.FromFile(Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp");
        ImageConverter imageConverter1 = new ImageConverter();
        byte[] imageByte1 = (byte[])imageConverter1.ConvertTo(image11, typeof(byte[]));
        save_into_table(imageByte1, count);
        image1.Dispose();

    }
   
    public void save_into_table(byte[] c, int count)
    {
        SqlConnection cn = new SqlConnection("server=localhost\\database1;integrated security=true;initial catalog=cloud");
        SqlCommand sq = new SqlCommand("save_file", cn);
        sq.CommandType = CommandType.StoredProcedure;

        SqlParameter user_name = new SqlParameter("@user", SqlDbType.VarChar, 15);
        user_name.Value = Session["user_name"].ToString();
        sq.Parameters.Add(user_name);

        SqlParameter name = new SqlParameter("@nam", SqlDbType.VarChar, 300);
        name.Value = filename1;
        sq.Parameters.Add(name);

        SqlParameter counts = new SqlParameter("@count", SqlDbType.Decimal, 2);
        counts.Value = count;
        sq.Parameters.Add(counts);

        SqlParameter piece_ = new SqlParameter("@piece", SqlDbType.VarChar, 300);
        piece_.Value = filename1 + count.ToString();
        sq.Parameters.Add(piece_);

        SqlParameter f_extn = new SqlParameter("extn", SqlDbType.VarChar, 10);
        f_extn.Value = fileext.ToString();
        sq.Parameters.Add(f_extn);

        SqlParameter f_size = new SqlParameter("@size", SqlDbType.Decimal, 10);
        f_size.Value = range;
        sq.Parameters.Add(f_size);

        SqlParameter date = new SqlParameter("@cr_date", SqlDbType.VarChar, 50);
        date.Value = (System.DateTime.Now).ToString();
        sq.Parameters.Add(date);

        SqlParameter f_cont = new SqlParameter("@cont", SqlDbType.Image, c.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, c);

        //SqlParameter f_cont = new SqlParameter("@cont", SqlDbType.Image);
        // f_cont.Value = c;
        sq.Parameters.Add(f_cont);

        cn.Open();
        sq.ExecuteNonQuery();
        cn.Close();

    }
   
}
