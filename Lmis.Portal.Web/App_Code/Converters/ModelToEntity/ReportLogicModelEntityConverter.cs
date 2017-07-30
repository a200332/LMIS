using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
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
                    new XElement("ID", bindingModel.ID),
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