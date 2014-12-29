using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Drawing;

using System.Drawing.Imaging;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    string id = HttpContext.Current.Request.QueryString["id"].ToString();
    string name = HttpContext.Current.Request.QueryString["name"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string uu = Session["user_name"].ToString(); ;
    }

    int size = 0;
    int piece_size = 0;
    string extn;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cloudConnectionString"].ConnectionString);

    public void decrypt()
    {
        extn = name.Split('.')[1].ToString();
       // SqlCommand cm = new SqlCommand("select COUNT(f_id),(select size from file_store where id=" + id + "),(select  top 1 len(f_content) from file_piece where f_id=" + id + "group by f_content)from file_piece where f_id=" + id, cn);
        SqlCommand cm = new SqlCommand("select COUNT(f_id),(select size from file_store where id=" + id + ") from file_piece where f_id=" + id , cn);
        cn.Open();
        DataSet ds = new DataSet();
        DataTable dataTable = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cm);
       
        da.Fill(ds, "rs");

        int temp_range = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        size = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
        

        byte[] bytes = new byte[Convert.ToInt32(size)];//Session["file_size"])];
        cn.Close();
        byte[] c = new byte[Convert.ToInt32(piece_size)];
        string pathSource;
        int i = 0;
        string ffilename = name + "."+extn;
        pathSource = Server.MapPath("Files/") + ffilename;/// +ii.ToString();
        FileStream fsSource = new FileStream(pathSource, FileMode.Create, FileAccess.Write, FileShare.Read);
        for (int ii = 1; ii <= temp_range; ii++)//(int i = 0; i <= temp_range; i++)
        {
            cn.Open();
            SqlCommand cm1 = new SqlCommand("select datalength(f_content) from file_piece where( f_id="+id+" and piece_name='"+name+ii.ToString()+"');", cn);
            DataSet ds1 = new DataSet();
           // DataTable dataTable = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cm1);
            da1.Fill(ds1, "rs1");
            piece_size = Convert.ToInt32(ds1.Tables["rs1"].Rows[0][0]);
           c = new byte[Convert.ToInt32(piece_size)];
            // FileStream fsSource = new FileStream(pathSource, FileMode.Create, FileAccess.Write);
        //FileStream fsSource = new FileStream(pathSource, FileMode.Create, FileAccess.Write, FileShare.Read);
           
            SqlCommand sq = new SqlCommand("retrieve", cn);
            sq.CommandType = CommandType.StoredProcedure;

            SqlParameter f_id = new SqlParameter("@f_id", SqlDbType.Decimal, 10);
            f_id.Value = Convert.ToDecimal(id);
            sq.Parameters.Add(f_id);

            SqlParameter f_name = new SqlParameter("@name", SqlDbType.VarChar, 50);
            f_name.Value = (name + ii).ToString();
            sq.Parameters.Add(f_name);
            
            SqlParameter content = new SqlParameter("@cont", SqlDbType.VarBinary);
            content.Direction = ParameterDirection.Output;
            content.Value = c;
            sq.Parameters.Add(content);


           sq.ExecuteNonQuery();
            Bitmap image = null;
                          
            MemoryStream ms = new MemoryStream((byte [])content.Value);
                image = new Bitmap(ms);
           image.Save(Server.MapPath("DecryptedFiles/") +  name+ii.ToString() + ".bmp", ImageFormat.MemoryBmp);
            //Bitmap image1;
            cn.Close();
            image.Dispose();
        
            //FileStream fsSource = new FileStream(pathSource, FileMode.Create, FileAccess.Write);
                //fsSource.Write(bytes, 0, bytes.Length);
              //  fsSource.Flush();
               // fsSource.Close();
            //for (int z = 1; z <=temp_range /*Convert.ToInt32(Session["files_needed"])*/; z++)//replace with number of image files created
            {
                //byte [] bytes1;

                Bitmap image1 = new Bitmap(Server.MapPath("DecryptedFiles/") + name + ii.ToString()+".bmp", true);
                int TotalPixel = image1.Width * image1.Height;
                int size_of_file_in_image = (int)((TotalPixel * 3) / 8);

                int x = 0, y = 0, bit_potision = 0;
                char[] bit_array = new char[8];


                for (x = 0; x < image1.Width; x++)
                {
                    for (y = 0; y < image1.Height; y++)
                    {
                        Color pixel_color = image1.GetPixel(x, y);

                        byte red = Convert.ToByte(pixel_color.R);
                        byte green = Convert.ToByte(pixel_color.G);
                        byte blue = Convert.ToByte(pixel_color.B);

                        char[] red_array = Convert.ToString(red, 2).PadLeft(8, '0').ToArray();
                        char[] green_array = Convert.ToString(green, 2).PadLeft(8, '0').ToArray();
                        char[] blue_array = Convert.ToString(blue, 2).PadLeft(8, '0').ToArray();

                        bit_array[bit_potision++] = red_array[7];
                        bit_array[bit_potision++] = green_array[7];
                        if (bit_potision < 8)
                            bit_array[bit_potision++] = blue_array[7];

                        if (bit_potision == 8 && i < bytes.Length)
                        {
                            bit_potision = 0;
                            string byte_str = new string(bit_array);
                            //bytes1[i] = Convert.ToByte(byte_str, 2);
                            //bytes[(z - 1) * size_of_file_in_image + i] = Convert.ToByte(byte_str, 2);//bytes1[i];
                            bytes[i] = Convert.ToByte(byte_str, 2);//bytes1[i];
                            //if(i<100)
                            //Label1.Text = Label1.Text + "<br/>" + i.ToString() + ": " + bytes[i];

                            i++;

                        }

                        if (i == bytes.Length)
                        {
                            fsSource.Write(bytes, 0, bytes.Length);
                            fsSource.Flush();
                            fsSource.Close();


                            Response.ContentType = extn;
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
                            Response.TransmitFile(pathSource);
                            Response.Flush();

                            string fileName = (pathSource);
                            string Path = fileName;
                            FileInfo file = new FileInfo(Path);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                            this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
                            Response.End();
                            Label2.Text = "Your File is Downloading.....";

                            //stream.Write(bytes, 0, bytes.Length);      
                            return;
                        }
                    }
                }
            }
        }

        fsSource.Write(bytes, 0, bytes.Length);
        fsSource.Flush();
        fsSource.Close();


        Response.ContentType = extn;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
        Response.TransmitFile(pathSource);
        Response.Flush();

        string fileName1 = (pathSource);
        string Path1 = fileName1;
        FileInfo file1 = new FileInfo(Path1);
        if (file1.Exists)
        {
            file1.Delete();
        }
        this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        Response.End();
        Label2.Text = "Your File is Downloading.....";
        cn.Close();
    }

    protected void get_file_btn_Click(object sender, EventArgs e)
    {

        try
        {
            ImageButton b = sender as ImageButton;
            
                if (TextBox1.Text == "123456")//Session["random_digit"].ToString())
               {
                    decrypt();
            //        //string id = b.Attributes["ID"];
            //        //name = b.Attributes["Text"];
            //        extn = name.Split('.')[1].ToString();
            //        SqlCommand cm = new SqlCommand("select COUNT(f_id),(select  f_size from file_store where id=" + id + "),(select  top 1 len(f_content) from file_piece where f_id=" + id + "group by f_content)from file_piece where f_id=" + id, cn);
            //        cn.Open();
            //        byte[] c; //= new byte[Convert.ToInt32(piece_size)];
            //        //c.Initialize();
            //        DataSet ds = new DataSet();
            //        //ds.Tables.Clear();
            //        DataTable dataTable = new DataTable();
            //        SqlDataAdapter da = new SqlDataAdapter(cm);
            //        cn.Close();
            //        da.Fill(ds, "rs");
            //        //size = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //        //int temp_range = size / 102400;
            //        int temp_range = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //        size = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            //        piece_size = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            //        //byte[] c = new byte[204800];
            //        c = new byte[Convert.ToInt32(piece_size)];

            //        for (int i = 0; i < temp_range; i++)//(int i = 0; i <= temp_range; i++)
            //        {
            //            cn.Open();
            //            SqlCommand sq = new SqlCommand("retrieve", cn);
            //            sq.CommandType = CommandType.StoredProcedure;

            //            SqlParameter f_id = new SqlParameter("@f_id", SqlDbType.Decimal, 10);
            //            f_id.Value = Convert.ToDecimal(id);
            //            sq.Parameters.Add(f_id);

            //            SqlParameter f_name = new SqlParameter("@name", SqlDbType.VarChar, 50);
            //            f_name.Value = (name + i).ToString();
            //            sq.Parameters.Add(f_name);

            //            SqlParameter content = new SqlParameter("@cont", SqlDbType.VarBinary);
            //            content.Direction = ParameterDirection.Output;
            //            content.Value = c;
            //            sq.Parameters.Add(content);


            //            sq.ExecuteNonQuery();
            //            cn.Close();

            //            c = (byte[])content.Value;
            //            //byte[] fill = new byte[102400];
            //            byte[] fill = new byte[(Convert.ToInt32(piece_size) / 2)];
            //            fill.Initialize();
            //            for (int iy = 0; iy < (Convert.ToInt32(piece_size) / 2); iy++)
            //            {
            //                //if(iy%2!=0)
            //                try
            //                {
            //                    if (iy == 0)
            //                        fill[iy] = c[iy];
            //                    else
            //                        fill[iy] = c[iy * 2];
            //                    //Response.Write(iy.ToString());
            //                }
            //                catch (Exception ex)
            //                {
            //                    Response.Write(ex.Message + iy.ToString());
            //                }
            //            }

            //            if (i != temp_range)
            //            {
            //                if (i == 0)
            //                    using (FileStream stream = new FileStream(Server.MapPath(@"~/files/retrieved/") + name, FileMode.Create, FileAccess.Write, FileShare.Read))
            //                        stream.Write(fill, 0, (Convert.ToInt32(piece_size) / 2));//102400
            //                else
            //                    using (FileStream stream = new FileStream(Server.MapPath(@"~/files/retrieved/") + name, FileMode.Append, FileAccess.Write, FileShare.Read))
            //                        stream.Write(fill, 0, (Convert.ToInt32(piece_size) / 2));
            //            }
            //            else
            //                using (FileStream stream = new FileStream(Server.MapPath(@"~/files/retrieved/") + name, FileMode.Append, FileAccess.Write, FileShare.Read))
            //                    stream.Write(fill, 0, size - temp_range * (Convert.ToInt32(piece_size) / 2));
            //        }

            //        Response.ContentType = extn;
            //         Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
            //        Response.TransmitFile(Server.MapPath(@"~/files/retrieved/") + name);
            //         Response.Flush();

            //        string fileName = (Server.MapPath(@"~/files/retrieved/") + name);
            //        string Path = fileName;
            //        FileInfo file = new FileInfo(Path);
            //        if (file.Exists)
            //        {
            //            file.Delete();
            //        }
            //        this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
            //        Response.End();
            //        Label2.Text = "Your File is Downloading.....";

                }
                else
                    Label2.Text = "Your code is incorrect !!!";
            }
            catch (Exception ex)
            { }
            finally { this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true); }

        }


        //if (TextBox1.Text == Session["random_digit"].ToString())
        //{
        //    Label2.Text = "Your File is Downloading.....";
        //    Session["get_file"] = "yes";
        //    this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        //}
        //else
        //    Label2.Text = "Your code is incorrect !!!";

    }

