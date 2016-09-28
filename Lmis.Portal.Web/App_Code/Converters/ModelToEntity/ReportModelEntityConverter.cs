using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class ReportModelEntityConverter : SingleModelConverterBase<ReportModel, LP_Report>
	{
		public ReportModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_Report Convert(ReportModel source)
		{
			var entity = new LP_Report
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
			};

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_Report target, ReportModel source)
		{
			target.Name = source.Name;
			target.CategoryID = source.CategoryID.Value;
			target.Type = source.Type;

			foreach (var entity in target.ReportLogics)
				entity.DateDeleted = DateTime.Now;

			var converter = new ReportLogicModelEntityConverter(DbContext);

			if (source.ReportLogics != null && source.ReportLogics.List != null)
			{
				var query = source.ReportLogics.List.Select(n => converter.Convert(n));
				target.ReportLogics.AddRange(query);
			}
		}
	}

	public class ReportLogicModelEntityConverter : SingleModelConverterBase<ReportLogicModel, LP_ReportLogic>
	{
		public ReportLogicModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_ReportLogic Convert(ReportLogicModel source)
		{
			var entity = new LP_ReportLogic
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
			};

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_ReportLogic target, ReportLogicModel source)
		{
			target.Type = source.Type;
			target.LogicID = source.Logic.ID;
			target.ConfigXml = ConvertBindings(source.Bindings);
		}

		private XElement ConvertBindings(BindingInfosModel model)
		{
			if (model == null || model.List == null)
				return null;

			return ConvertBindings(model.List);
		}

		private XElement ConvertBindings(IEnumerable<BindingInfoModel> collection)
		{
			var rootXElem = new XElement("Bindings");

			foreach (var bindingModel in collection)
			{
				var bindingXElem = new XElement
					(
						"Binding",
						new XElement("Target", bindingModel.Target),
						new XElement("Source", bindingModel.Source),
						new XElement("Caption", bindingModel.Caption)
					);

				rootXElem.Add(bindingXElem);
			}

			return rootXElem;
		}
	}
}