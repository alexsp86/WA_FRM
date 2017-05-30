using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WA_FRM.Models;
using Ninject;
using WA_FRM.Util;
using System.IO;

namespace WA_FRM.Controllers
{
    public class HomeController : Controller
    {        
        IMObject MObject;

        public HomeController()
        {
            IKernel ninjectKernel = new StandardKernel(new MONinjectModule());
            MObject = ninjectKernel.Get<IMObject>();
        }

        void FormMatrixResize(FormCollection f, ref IMObject m)
        {
            int n;
            if (Int32.TryParse(f["MatrixSize"], out n) && n > 0)
                m.Resize(n);
        }

        void FormToMatrix(FormCollection f, ref IMObject m)
        {
            FormMatrixResize(f,ref m);

            for (int i = 0; i < m.size; i++)
            {
                for (int j = 0; j < m.size; j++)
                {
                    m[i, j] = f[String.Format("M[{0}][{1}]", i, j)];
                }
            }            
        }        

        [HttpPost]
        public ActionResult LoadFromFile(HttpPostedFileBase file)
        {            
            if (file!=null && file.ContentLength > 0)
            {
                MObject.FromStream(file.InputStream);
                ViewBag.Matrix = MObject;
                return View(@"~\Views\Home\Index.cshtml");
            }
            else
            {
                ViewBag.Message = "Файл не указан!";                
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(FormCollection f, string action)
        {
            ViewBag.Matrix = MObject;

            if (action == "Resize")
            {                
                FormMatrixResize(f, ref MObject);
            }
            else if (action == "Rotate")
            {
                FormToMatrix(f,ref MObject);
                MObject.Rotate();               
            }
            else if (action == "SaveToFile")
            {                
                FormToMatrix(f, ref MObject);
                MemoryStream ms = MObject.ToStream();
                return File(ms, "xml", "matrix.xml");                
            }
            
            return View();
        }
        
        public ActionResult Index()
        {
            ViewBag.Matrix = MObject;
            return View();
        }
    }
}