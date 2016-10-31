<%@ WebHandler Language="C#" Class="GetFile" %>

using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;

public class GetFile : IHttpHandler
{
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;
        var server = context.Server;

        var itemID = DataConverter.ToNullableGuid(request["ID"]);
        if (itemID == null)
            return;

        var fileBytes = (byte[])null;
        var fileName = (String)null;
        var mimeType = (String)null;

        var itemType = request["Type"];

        using (var db = new PortalDataContext())
        {
            switch (itemType)
            {
                case "Legislation":
                    {
                        var item = db.LP_Legislations.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.FileData != null && item.FileData.Length > 0)
                        {
                            if (String.IsNullOrWhiteSpace(item.FileName))
                                fileName = String.Format("{0}.pdf", item.Title);
                            else
                                fileName = item.FileName;

                            fileBytes = item.FileData.ToArray();
                            mimeType = MimeTypeUtil.GetMimeType(fileName);
                        }
                    }
                    break;
                case "Project":
                    {
                        var item = db.LP_Projects.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.FileData != null && item.FileData.Length > 0)
                        {
                            if (String.IsNullOrWhiteSpace(item.FileName))
                                fileName = String.Format("{0}.pdf", item.Title);
                            else
                                fileName = item.FileName;

                            fileBytes = item.FileData.ToArray();
                            mimeType = MimeTypeUtil.GetMimeType(fileName);
                        }
                    }
                    break;
                case "Survey":
                    {
                        var item = db.LP_Surveys.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.FileData != null && item.FileData.Length > 0)
                        {
                            if (String.IsNullOrWhiteSpace(item.FileName))
                                fileName = String.Format("{0}.pdf", item.Title);
                            else
                                fileName = item.FileName;

                            fileBytes = item.FileData.ToArray();
                            mimeType = MimeTypeUtil.GetMimeType(fileName);
                        }
                    }
                    break;
            }
        }

        if (fileBytes != null)
        {
            var disposition = new ContentDisposition
            {
                Inline = true,
                FileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8)
            };

            response.ContentType = mimeType;
            response.Headers["Content-Disposition"] = disposition.ToString();
            response.BinaryWrite(fileBytes);
        }
    }
}