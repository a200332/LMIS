using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class SurveysList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            FillDataGrid();
        }

        protected void btnAddNew_OnClick(object sender, EventArgs e)
        {
            surveyControl.Model = new SurveyModel();
            mpeAddEdit.Show();
        }

        protected void surveysControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Surveys.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            var converter = new SurveyEntityModelConverter(DataContext);
            var model = converter.Convert(entity);

            surveyControl.Model = model;
            mpeAddEdit.Show();
        }

        protected void surveysControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            var entity = DataContext.LP_Surveys.FirstOrDefault(n => n.ID == e.Value);
            if (entity == null)
                return;

            entity.DateDeleted = DateTime.Now;
            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var converter = new SurveyModelEntityConverter(DataContext);

            var model = surveyControl.Model;
            if (model.ID != null)
            {
                var entity = DataContext.LP_Surveys.FirstOrDefault(n => n.ID == model.ID);
                if (entity == null)
                    return;

                converter.FillObject(entity, model);
            }
            else
            {
                var entity = converter.Convert(model);

                var maxOrder = DataContext.LP_Surveys.Max(n => n.OrderIndex);
                entity.OrderIndex = maxOrder.GetValueOrDefault() + 1;

                DataContext.LP_Surveys.InsertOnSubmit(entity);
            }

            DataContext.SubmitChanges();

            mpeAddEdit.Hide();

            FillDataGrid();
        }

        protected void surveysControl_OnUpItem(object sender, GenericEventArgs<Guid> e)
        {
            var surveys = (from n in DataContext.LP_Surveys
                where n.DateDeleted == null
                orderby n.OrderIndex, n.DateCreated
                select n).ToList();

            for (int i = 0; i < surveys.Count; i++)
                surveys[i].OrderIndex = i;

            var currentItem = surveys.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = surveys.IndexOf(currentItem);
            if (index < 0 || index == 0)
                return;

            surveys[index] = surveys[index - 1];
            surveys[index - 1] = currentItem;

            for (int i = 0; i < surveys.Count; i++)
                surveys[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void surveysControl_OnDownItem(object sender, GenericEventArgs<Guid> e)
        {
            var surveys = (from n in DataContext.LP_Surveys
                where n.DateDeleted == null
                orderby n.OrderIndex, n.DateCreated
                select n).ToList();

            for (int i = 0; i < surveys.Count; i++)
                surveys[i].OrderIndex = i;

            var currentItem = surveys.FirstOrDefault(n => n.ID == e.Value);
            if (currentItem == null)
                return;

            var index = surveys.IndexOf(currentItem);
            if (index < 0 || index == (surveys.Count - 1))
                return;

            surveys[index] = surveys[index + 1];
            surveys[index + 1] = currentItem;

            for (int i = 0; i < surveys.Count; i++)
                surveys[i].OrderIndex = i;

            DataContext.SubmitChanges();

            FillDataGrid();
        }

        protected void FillDataGrid()
        {
            var entities = (from n in DataContext.LP_Surveys
                where n.DateDeleted == null
                orderby n.OrderIndex, n.DateCreated
                select n).ToList();

            var converter = new SurveyEntityModelConverter(DataContext);

            var models = (from n in entities
                let m = converter.Convert(n)
                select m).ToList();

            var model = new SurveysModel();
            model.List = models;

            surveysControl.Model = model;
        }
    }
}
