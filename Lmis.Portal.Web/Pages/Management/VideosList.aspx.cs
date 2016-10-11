using System;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Common;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.Management
{
	public partial class VideosList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FillDataGrid();
		}

		protected void btnAddNew_OnClick(object sender, EventArgs e)
		{
			videoControl.Model = new VideoModel();
			mpeAddEdit.Show();
		}

		protected void videosControl_OnEditItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Videos.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			var converter = new VideoEntityModelConverter(DataContext);
			var model = converter.Convert(entity);

			videoControl.Model = model;
			mpeAddEdit.Show();
		}

		protected void videosControl_OnDeleteItem(object sender, GenericEventArgs<Guid> e)
		{
			var entity = DataContext.LP_Videos.FirstOrDefault(n => n.ID == e.Value);
			if (entity == null)
				return;

			entity.DateDeleted = DateTime.Now;
			DataContext.SubmitChanges();

			FillDataGrid();
		}

		protected void btnSave_OnClick(object sender, EventArgs e)
		{
			var converter = new VideoModelEntityConverter(DataContext);

			var model = videoControl.Model;
			if (model.ID != null)
			{
				var entity = DataContext.LP_Videos.FirstOrDefault(n => n.ID == model.ID);
				if (entity == null)
					return;

				converter.FillObject(entity, model);
			}
			else
			{
				var entity = converter.Convert(model);
				DataContext.LP_Videos.InsertOnSubmit(entity);
			}

			DataContext.SubmitChanges();

			mpeAddEdit.Hide();

			FillDataGrid();
		}

		private void FillDataGrid()
		{
			var entities = (from n in DataContext.LP_Videos
							where n.DateDeleted == null
							orderby n.DateCreated descending
							select n).ToList();

			var converter = new VideoEntityModelConverter(DataContext);

			var models = (from n in entities
						  let m = converter.Convert(n)
						  select m).ToList();

			var model = new VideosModel();
			model.List = models;

			videosControl.Model = model;
		}
	}
}