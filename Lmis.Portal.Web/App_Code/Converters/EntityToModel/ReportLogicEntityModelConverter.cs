using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class ReportLogicEntityModelConverter : SingleModelConverterBase<LP_ReportLogic, ReportLogicModel>
	{
		private readonly LogicEntityModelConverter logicConverter;

		public ReportLogicEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
			logicConverter = new LogicEntityModelConverter(dbContext);
		}

		public override ReportLogicModel Convert(LP_ReportLogic source)
		{
			var target = new ReportLogicModel();
			FillObject(target, source);

			return target;
		}

		public override void FillObject(ReportLogicModel target, LP_ReportLogic source)
		{
			target.ID = source.ID;
			target.Logic = logicConverter.Convert(source.Logic);

			var bindings = new BindingInfosModel
			{
				List = ConvertBindings(source.ConfigXml).ToList()
			};

			target.Bindings = bindings;
		}

		private IEnumerable<BindingInfoModel> ConvertBindings(XElement xElem)
		{
			foreach (var bindingXElem in xElem.Elements("Binding"))
			{
				var model = new BindingInfoModel
				{
					ID = (Guid?)bindingXElem.Element("ID"),
					Target = (String)bindingXElem.Element("Target"),
					Source = (String)bindingXElem.Element("Source"),
					Caption = (String)bindingXElem.Element("Caption"),
				};

				yield return model;
			}
		}
	}
}