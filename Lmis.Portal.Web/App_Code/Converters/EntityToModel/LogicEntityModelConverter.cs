using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Models.Common;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class LogicEntityModelConverter : SingleModelConverterBase<LP_Logic, LogicModel>
	{
		public LogicEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LogicModel Convert(LP_Logic source)
		{
			var target = new LogicModel();

			FillObject(target, source);

			return target;
		}

		public override void FillObject(LogicModel target, LP_Logic source)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Type = source.Type;
			target.SourceType = source.SourceType;

			if (source.SourceType == "Table")
				target.SourceID = source.TableID;

			if (source.SourceType == "Logic")
				target.SourceID = source.LogicID;

			var logicXElem = source.RawData;
			if (logicXElem == null)
				return;

			target.Query = GetQuery(logicXElem);
			target.ExpressionsLogic = GetExpressionsLogic(logicXElem);

		}

		private String GetQuery(XElement logicXElem)
		{
			if (logicXElem.Name == "Query")
				return logicXElem.Value;

			return null;
		}

		private ExpressionsLogicModel GetExpressionsLogic(XElement logicXElem)
		{
			var model = new ExpressionsLogicModel();

			model.FilterBy = GetExpressionsList("FilterBy", logicXElem);
			model.GroupBy = GetNamedExpressionsList("GroupBy", logicXElem);
			model.OrderBy = GetExpressionsList("OrderBy", logicXElem);
			model.Select = GetNamedExpressionsList("Select", logicXElem);

			return model;
		}

		private ExpressionsListModel GetExpressionsList(String name, XElement logicXElem)
		{
			var expressionsByXElem = logicXElem.Element(name);
			if (expressionsByXElem == null)
				return null;

			var expressionsModel = new ExpressionsListModel
			{
				Expressions = new List<ExpressionModel>()
			};

			foreach (var itemXElem in expressionsByXElem.Elements("Item"))
			{
				var model = new ExpressionModel
				{
					Expression = (String)itemXElem.Element("Expression"),
					OutputType = (String)itemXElem.Element("OutputType")
				};

				expressionsModel.Expressions.Add(model);
			}

			return expressionsModel;
		}

		private NamedExpressionsListModel GetNamedExpressionsList(String name, XElement logicXElem)
		{
			var expressionsByXElem = logicXElem.Element(name);
			if (expressionsByXElem == null)
				return null;

			var expressionsModel = new NamedExpressionsListModel
			{
				Expressions = new List<NamedExpressionModel>()
			};

			foreach (var itemXElem in expressionsByXElem.Elements("Item"))
			{
				var model = new NamedExpressionModel
				{
					Name = (String)itemXElem.Element("Name"),
					Expression = (String)itemXElem.Element("Expression"),
					OutputType = (String)itemXElem.Element("OutputType")
				};

				expressionsModel.Expressions.Add(model);
			}

			return expressionsModel;
		}
	}
}