using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;

public partial class upload_files : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    static int range;
    static int new_range;
    static long imagesize;
    static string filename1;
    static string fileext;
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //byte[] a;
        //byte[] pic;//=new byte[204800];
        //byte[] c = new byte[ 524343323 ];
        //pic.Initialize();
        if (!FileUpload1.HasFile || !FileUpload2.HasFile)
        {
            Label1.Text = "No File/Photo Selected...";
        }
        else
        {
            // //FileUpload1.SaveAs(Server.MapPath(@"~/temp/") + FileUpload1.FileName);
            // //string image_ext = System.IO.Path.GetExtension(FileUpload1.FileName);
            fileext = System.IO.Path.GetExtension(FileUpload2.FileName);
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
                //saving_files(a, pic, file_path, filename, fileext);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
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
                    image1.Save(Server.MapPath("EncryptedFiles/") + filename1+count + ".bmp", ImageFormat.MemoryBmp);
                    image1.Dispose();
                   
                    //Response.Flush();
                   //string  aaaa= image1.Size.ToString();
                    System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp");
                   ImageConverter imageConverter = new ImageConverter();
                    byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
                    save_into_table(imageByte,count);
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
        
       image1.Save(Server.MapPath("EncryptedFiles/") +  filename1+count + ".bmp", ImageFormat.MemoryBmp);
       image1.Dispose();
       System.Drawing.Image image11 = System.Drawing.Image.FromFile(Server.MapPath("EncryptedFiles/") + filename1 + count + ".bmp");
       ImageConverter imageConverter1 = new ImageConverter();
       byte[] imageByte1 = (byte[])imageConverter1.ConvertTo(image11, typeof(byte[]));
       save_into_table(imageByte1, count);
       image1.Dispose();
       
    }
    //public void saving_files(byte[] b, byte[] mix, string path, string file_name, string file_ext)
    //{
    //    //byte[] c = new byte[204800];
    //    int pic_size = mix.Length;
    //    byte[] c = new byte[pic_size * 2];////C's length is equal to the size of the picture
    //    //[] mix = pic;        
    //    //mix.Initialize();
    //    range = b.Length;
    //    new_range = range + (range / 204800) * 102400;//not needed right now
    //    //Response.Write("length b= " + b.Length.ToString()+ "<br>");
    //    int count = 0;
    //    int y = 0;//w=0;
    //    //if(range > 204800)

    //    try
    //    {
    //        for (y = 0; y < range - pic_size; y += pic_size)
    //        {
    //            c.Initialize();
    //            //Response.Write("length C= " + c.Length.ToString());
    //            //////////////////picture is on the odd places and the original file is on the even///////////////////////
    //            for (int u = 0, w = 0, l = 0; u < c.Length; u++, l++)
    //            {
    //                if (l == mix.Length)
    //                    l = 0;
    //                if (u % 2 == 0)
    //                    c[u] = b[y + w++];
    //                else
    //                    c[u] = mix[l];

    //            }
    //            ////using (FileStream stream = new FileStream(Server.MapPath(@"~/files/") + file_name + "_" + count + file_ext, FileMode.Create, FileAccess.Write, FileShare.Read))
    //            ////    stream.Write(c, 0, 204800);
    //            save_into_table(c, count);
    //            //Response.Write(y.ToString()+(range-102400).ToString());
    //            count += 1;
    //        }
    //        c.Initialize();
    //        for (int u = 0, w = 0, l = 0; u < 2 * (range - y); u++, l++)
    //        {
    //            if (l == mix.Length)
    //                l = 0;
    //            if (u % 2 == 0)
    //                c[u] = b[y + w++];
    //            else
    //                c[u] = mix[l];
    //        }
    //        ////using (FileStream stream = new FileStream(Server.MapPath(@"~/files/") + file_name + "_" + count + file_ext, FileMode.Create, FileAccess.Write, FileShare.Read))
    //        ////    stream.Write(c, 0, range - y);
    //        save_into_table(c, count);
    //        //Response.Write(y.ToString() + (range - 102400).ToString() + "<br>");
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message + y.ToString());
    //    }

    //}

    public void save_into_table(byte[] c , int count)
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cloudConnectionString"].ConnectionString);
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