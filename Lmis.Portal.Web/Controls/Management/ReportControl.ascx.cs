﻿using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Models;
using Lmis.Portal.Web.Utils;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class ReportControl : BaseExtendedControl<ReportModel>
    {
        public ReportLogicsModel ReportLogics
        {
            get { return ViewState["ReportLogics"] as ReportLogicsModel; }
            set { ViewState["ReportLogics"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            FillComboBoxes();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            reportLogicsControl.Model = ReportLogics;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            reportLogicsControl.Model = ReportLogics;
        }

        protected void btnSaveReportLogic_OnClick(object sender, EventArgs e)
        {
            var reportLogicModel = reportLogicControl.Model;

            ReportLogics = (ReportLogics ?? new ReportLogicsModel());
            ReportLogics.List = (ReportLogics.List ?? new List<ReportLogicModel>());

            var reportLogic = ReportLogics.List.FirstOrDefault(n => n.ID == reportLogicModel.ID);
            if (reportLogic != null)
            {
                reportLogic.Bindings = reportLogicModel.Bindings;
                reportLogic.Logic = reportLogicModel.Logic;
                reportLogic.Type = reportLogicModel.Type;
            }
            else
            {
                reportLogicModel.ID = Guid.NewGuid();
                ReportLogics.List.Add(reportLogicModel);
            }
        }

        protected void btnNewReportLogic_OnClick(object sender, EventArgs e)
        {
            reportLogicControl.Model = new ReportLogicModel();
            mpeAddEditReportLogic.Show();
        }

        protected void reportLogicControl_OnDataChanged(object sender, EventArgs e)
        {
            mpeAddEditReportLogic.Show();
        }

        protected void reportLogicsControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
        {
            if (ReportLogics == null || ReportLogics.List == null)
                return;

            var reportLogic = ReportLogics.List.FirstOrDefault(n => n.ID == e.Value);
            if (reportLogic == null)
                return;

            ReportLogics.List.Remove(reportLogic);
        }

        protected void reportLogicsControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
        {
            if (ReportLogics == null || ReportLogics.List == null)
                return;

            var reportLogic = ReportLogics.List.FirstOrDefault(n => n.ID == e.Value);
            if (reportLogic == null)
                return;

            reportLogicControl.Model = reportLogic;
            mpeAddEditReportLogic.Show();
        }

        protected override void OnGetModel(object model, Type type)
        {
            var reportModel = model as ReportModel;
            if (reportModel == null)
                return;

            reportModel.ReportLogics = ReportLogics;
        }

        protected override void OnSetModel(object model, Type type)
        {
            var reportModel = model as ReportModel;
            if (reportModel == null)
                return;

            ReportLogics = reportModel.ReportLogics;
        }

        protected void FillComboBoxes()
        {
            var categories = (from n in DataContext.LP_Categories
                              where n.DateDeleted == null
                              orderby n.Name
                              select n).ToList();

            cbxCategory.DataSource = categories;
            cbxCategory.DataBind();

            var languages = LanguageUtil.GetLanguages();

            cbxLanguage.DataSource = languages;
            cbxLanguage.DataBind();
        }


    }
}
