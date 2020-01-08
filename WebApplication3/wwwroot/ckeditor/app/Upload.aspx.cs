using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fcity.UI.Portal.Content.ckeditor.app
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            if (Request.Files.Count > 0)
            {
                string output = @"<script type=""text/javascript"">window.parent.CKEDITOR.tools.callFunction({0} ,'{1}');</script>";
                System.Web.HttpPostedFile f = Request.Files["upload"];
                string filename = "/UploadFiles/" + System.Guid.NewGuid().ToString() + f.FileName.Substring(f.FileName.LastIndexOf("."));
                f.SaveAs(Server.MapPath("~" + filename));
                string url = "http://" + Request.Url.Authority;
                output = string.Format(output, Request["CKEditorFuncNum"], url+filename);
                Response.Write(output);
                Response.End();
            }
            else
            {
                Response.Write("no file");
                Response.End();
            }
        }
    }
}