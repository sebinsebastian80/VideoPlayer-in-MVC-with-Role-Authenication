using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string FileName = "";
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;

                //iterating through multiple file collection   
                foreach (string str in files)
                {
                    HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                   
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        FileName = file.FileName;
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/VideoFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        string maincon = ConfigurationManager.ConnectionStrings["FilesEntities"].ConnectionString;
                        SqlConnection sqlconn = new SqlConnection(maincon);
                        string sqlquery = "insert into Files values(@Name,@Path)";
                        SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                        sqlconn.Open();
                        sqlcomm.Parameters.AddWithValue("@Name", FileName);
                        sqlcomm.Parameters.AddWithValue("@Path", "/VideoFiles/" + FileName);
                        sqlcomm.ExecuteNonQuery();
                        sqlconn.Close();
                    }

                }
                return RedirectToAction("Index","Home");
            }
            else
            {
                return Json("No files to upload");
            }
        }


        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase Files)
        //{
        //    if(Files!=null)
        //    {
        //        System.Threading.Thread.Sleep(1000);
        //        string filename = Path.GetFileName(Files.FileName);
        //        Files.SaveAs(Server.MapPath("/VideoFiles/"+filename));
        //        string maincon = ConfigurationManager.ConnectionStrings["FilesEntities"].ConnectionString;
        //        SqlConnection sqlconn = new SqlConnection(maincon);
        //        string sqlquery = "insert into Files values(@Name,@Path)";
        //        SqlCommand sqlcomm = new SqlCommand(sqlquery,sqlconn);
        //        sqlconn.Open();
        //        sqlcomm.Parameters.AddWithValue("@Name", filename);
        //        sqlcomm.Parameters.AddWithValue("@Path", "/VideoFiles/"+filename );
        //        sqlcomm.ExecuteNonQuery();
        //        sqlconn.Close();
        //        ViewData["Message"] = "Record Saved Sucessfully";
        //    }
        //    //return View();
        //    return RedirectToAction("Index","Home");
        //}


        [Authorize(Roles = "1")]
        public ActionResult AdminOnlyLink()
        {
            return View();
        }
    
    }
}