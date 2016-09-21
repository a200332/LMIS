<%@ WebHandler Language="C#" Class="CategoryImage" %>

using System;
using System.IO;
using System.Linq;
using System.Web;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;

public class CategoryImage : IHttpHandler
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

		response.ContentType = "image/png";

		var catetoryID = DataConverter.ToNullableGuid(request["CategoryID"]);
		var imagePath = server.MapPath("~/App_Themes/Default/Images/analitycactive.png");

		using (var db = new PortalDataContext())
		{
			var category = db.LP_Categories.FirstOrDefault(n => n.ID == catetoryID);
			if (category != null)
			{
				var fileVirtPath = String.Format("~/Files/{0:n}.png", category.ID);
				var filePhysPath = server.MapPath(fileVirtPath);

				if (File.Exists(filePhysPath))
					imagePath = filePhysPath;
			}
		}

		var bytes = File.ReadAllBytes(imagePath);
		response.BinaryWrite(bytes);
	}
}