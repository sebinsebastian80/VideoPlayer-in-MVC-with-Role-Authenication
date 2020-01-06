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
        public ActionResult Index(HttpPostedFileBase Files)
        {
            if(Files!=null)
            {
                string filename = Path.GetFileName(Files.FileName);
                Files.SaveAs(Server.MapPath("/VideoFiles/"+filename));
                string maincon = ConfigurationManager.ConnectionStrings["FilesEntities"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(maincon);
                string sqlquery = "insert into Files values(@Name,@Path)";
                SqlCommand sqlcomm = new SqlCommand(sqlquery,sqlconn);
                sqlconn.Open();
                sqlcomm.Parameters.AddWithValue("@Name", filename);
                sqlcomm.Parameters.AddWithValue("@Path", "/VideoFiles/"+filename );
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
                ViewData["Message"] = "Record Saved Sucessfully";
            }
            return RedirectToAction("Index","Home");
        }
    }
}