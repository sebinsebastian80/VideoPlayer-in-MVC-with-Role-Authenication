
namespace SampleProject.Controllers
{
    using SampleProject.Models;
    using System.Configuration;
    using System.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Net.Http;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Web.UI.WebControls;
    using System.Data;

    [Authorize]
    public class HomeController : Controller
    {
       // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase Files)
        //{
        //    if (Files != null)
        //    {
        //        string filename = Path.GetFileName(Files.FileName);
        //        Files.SaveAs(Server.MapPath("/VideoFiles/" + filename));
        //        string maincon = ConfigurationManager.ConnectionStrings["FilesEntities"].ConnectionString;
        //        SqlConnection sqlconn = new SqlConnection(maincon);
        //        string sqlquery = "insert into Files values(@Name,@Path)";
        //        SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
        //        sqlconn.Open();
        //        sqlcomm.Parameters.AddWithValue("@Name", filename);
        //        sqlcomm.Parameters.AddWithValue("@Path", "/VideoFiles/" + filename);
        //        sqlcomm.ExecuteNonQuery();
        //        sqlconn.Close();
        //        ViewData["Message"] = "Record Saved Sucessfully";
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public ActionResult Index(int? fileId)
        {
            List<UploadClass> videolist = new List<UploadClass>();
            string CS = ConfigurationManager.ConnectionStrings["FilesEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select Id,Name,Path from Files", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UploadClass video = new UploadClass();
                   video.Id =int.Parse(rdr["Id"].ToString());
                    video.Name = rdr["Name"].ToString();
                    video.Path = rdr["Path"].ToString();

                    videolist.Add(video);
                }
            }
            return View(videolist);
        }
        public ActionResult Download()
        {
            string path = Server.MapPath("~/VideoFiles/");
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            FileInfo[] files = dirinfo.GetFiles("*.*");
            List<string> list = new List<string>(files.Length);
            foreach(var item in files)
            {
                list.Add(item.Name);
            }
            return View(list);
        }
      
        public ActionResult DownloadFile(string filename)
        {
            if (Path.GetExtension(filename) == ".mp4")
            {
                string fullpath = Path.Combine(Server.MapPath("~/VideoFiles/"),filename);
                // return File(fullpath, "mp4");
                string contentType = "VideoFiles/mp4";
                return File(fullpath, contentType, filename);
            }
            else
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

         public ActionResult DeleteFile(int deleteId)
        {        
            string maincon = ConfigurationManager.ConnectionStrings["FilesEntities"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(maincon);
            SqlCommand sqlcomm = new SqlCommand("delete from Files where Id = " + deleteId + " ", sqlconn);
            sqlconn.Open();
           
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            return  RedirectToAction("Index");
        }


        [Authorize(Roles = "1")]
        public ActionResult AdminOnlyLink()
        {
            return View();
        }
  
        }
}