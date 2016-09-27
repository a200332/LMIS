using System;
using System.Xml.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Models.Common;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class LogicModelEntityConverter : SingleModelConverterBase<LogicModel, LP_Logic>
	{
		public LogicModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_Logic Convert(LogicModel source)
		{
			var entity = new LP_Logic
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now,
			};

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_Logic target, LogicModel source)
		{
			//target.ID = source.ID;
			target.Name = source.Name;
			target.Type = source.Type;

			if (source.SourceType == "Table")
				target.TableID = source.SourceID;

			if (source.SourceType == "Logic")
				target.LogicID = source.SourceID;

			target.RawData = GetRawData(source);
		}

		private XElement GetRawData(LogicModel model)
		{
			if (model.Type == "Query")
				return GetQueryXml(model);

			return GetLogicXml(model);
		}

		private XElement GetQueryXml(LogicModel model)
		{
			return new XElement("Query", model.Query);
		}
		private XElement GetLogicXml(LogicModel model)
		{
			var expressionsLogic = model.ExpressionsLogic;
			if (expressionsLogic != null)
				return GetLogicXml(expressionsLogic);

			return null;
		}

		private XElement GetLogicXml(ExpressionsLogicModel model)
		{
			var logicXElem = new XElement("Logic");

			var filterByXElem = GetExpressionsXElem("FilterBy", model.FilterBy);
			if (filterByXElem != null)
				logicXElem.Add(filterByXElem);

			var groupByXElem = GetExpressionsXElem("GroupBy", model.GroupBy);
			if (groupByXElem != null)
				logicXElem.Add(groupByXElem);

			var orderByXElem = GetExpressionsXElem("OrderBy", model.OrderBy);
			if (orderByXElem != null)
				logicXElem.Add(orderByXElem);

			var selectXElem = GetExpressionsXElem("Select", model.Select);
			if (selectXElem != null)
				logicXElem.Add(selectXElem);

			return logicXElem;
		}

		private XElement GetExpressionsXElem(String name, ExpressionsListModel model)
		{
			if (model == null || model.Expressions == null)
				return null;

			var expressionsByXElem = new XElement(name);

			foreach (var item in model.Expressions)
			{
				var itemXElem = new XElement("Item",
											new XElement("Expression", item.Expression),
											new XElement("OutputType", item.OutputType));

				expressionsByXElem.Add(itemXElem);
			}

			return expressionsByXElem;
		}

		private XElement GetExpressionsXElem(String name, NamedExpressionsListModel model)
		{
			if (model == null || model.Expressions == null)
				return null;

			var expressionsByXElem = new XElement(name);

			foreach (var item in model.Expressions)
			{
				var itemXElem = new XElement("Item",
											new XElement("Name", item.Name),
											new XElement("Expression", item.Expression),
											new XElement("OutputType", item.OutputType));

				expressionsByXElem.Add(itemXElem);
			}

			return expressionsByXElem;
		}
	}
}