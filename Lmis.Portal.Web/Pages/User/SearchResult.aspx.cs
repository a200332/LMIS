using System;
using System.Collections.Generic;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Entites;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class SearchResult : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var keyword = Request["Keyword"];
            if (String.IsNullOrWhiteSpace(keyword))
                return;

            var results = Search(keyword).ToList();

            if (results.Count == 0)
            {
                pnlResults.Visible = false;
                pnlEmpty.Visible = true;
            }
            else
            {
                pnlResults.Visible = true;
                pnlEmpty.Visible = false;

                rptItems.DataSource = results;
                rptItems.DataBind();
            }
        }

        protected IEnumerable<SearchEntry> Search(String keyword)
        {
            var newsQuery = (from n in DataContext.LP_News
                             where n.DateDeleted == null &&
                                   (
                                       n.Title.Contains(keyword) ||
                                       n.Description.Contains(keyword)
                                   )
                             select n);

            foreach (var entity in newsQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "News",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var legislationsQuery = (from n in DataContext.LP_Legislations
                                     where n.DateDeleted == null &&
                                           (
                                               n.Title.Contains(keyword) ||
                                               n.Description.Contains(keyword)
                                           )
                                     select n);

            foreach (var entity in legislationsQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "Legislation",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var eBooksQuery = (from n in DataContext.LP_EBooks
                               where n.DateDeleted == null &&
                                     (
                                         n.Title.Contains(keyword) ||
                                         n.Description.Contains(keyword)
                                     )
                               select n);

            foreach (var entity in eBooksQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "EBook",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var linksQuery = (from n in DataContext.LP_Links
                              where n.DateDeleted == null &&
                                    (
                                        n.Title.Contains(keyword) ||
                                        n.Description.Contains(keyword)
                                    )
                              select n);

            foreach (var entity in linksQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "Link",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var projectsQuery = (from n in DataContext.LP_Projects
                                 where n.DateDeleted == null &&
                                       (
                                            n.Title.Contains(keyword) ||
                                            n.Description.Contains(keyword)
                                       )
                                 select n);

            foreach (var entity in projectsQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "Project",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var reportsQuery = (from n in DataContext.LP_Reports
                                where n.DateDeleted == null &&
                                      (
                                        n.Name.Contains(keyword) ||
                                        n.Description.Contains(keyword) ||
                                        n.Interpretation.Contains(keyword) ||
                                        n.InformationSource.Contains(keyword)
                                      )
                                select n);

            foreach (var entity in reportsQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "Report",
                    Title = entity.Name,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var surveysQuery = (from n in DataContext.LP_Surveys
                                where n.DateDeleted == null &&
                                      (
                                        n.Title.Contains(keyword) ||
                                        n.Description.Contains(keyword)
                                      )
                                select n);

            foreach (var entity in surveysQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "Survey",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var careersQuery = (from n in DataContext.LP_Careers
                                where n.DateDeleted == null && (n.Title.Contains(keyword) || n.Description.Contains(keyword))
                                select n);

            foreach (var entity in careersQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = GetTargetUrl(entity),
                    Type = "Career",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }

            var videosQuery = (from n in DataContext.LP_Videos
                               where n.DateDeleted == null && (n.Title.Contains(keyword) || n.Description.Contains(keyword))
                               select n);

            foreach (var entity in videosQuery)
            {
                var entry = new SearchEntry
                {
                    ID = entity.ID,
                    Url = entity.Url,
                    Type = "Video",
                    Title = entity.Title,
                    Description = entity.Description,
                };

                yield return entry;
            }
        }

        private String GetTargetUrl(LP_Career entity)
        {
            if (entity == null)
                return "#";

            if (String.IsNullOrWhiteSpace(entity.Url) && entity.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Careers.aspx?ID={0}", entity.ID);
                return url;
            }
            else
            {
                var url = String.Format("~/Handlers/GetFile.ashx?Type=Project&ID={0}", entity.ID);
                return url;
            }
        }

        private String GetTargetUrl(LP_Link entity)
        {
            if (entity == null)
                return "#";

            if (String.IsNullOrWhiteSpace(entity.Url) && entity.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Links.aspx?ID={0}", entity.ID);
                return url;
            }
            else
            {
                return entity.Url;
            }
        }

        private String GetTargetUrl(LP_EBook entity)
        {
            return entity.Url;
        }

        protected String GetTargetUrl(LP_News entity)
        {
            if (entity == null)
                return "#";

            var url = String.Format("~/Pages/User/News.aspx?ID={0}", entity.ID);
            return url;
        }

        protected String GetTargetUrl(LP_Report entity)
        {
            if (entity == null)
                return "#";

            var url = String.Format("~/Pages/User/Dashboard.aspx?CategoryID={0}", entity.CategoryID);
            return url;
        }

        protected String GetTargetUrl(LP_Legislation entity)
        {
            if (entity == null)
                return "#";

            if (entity.FileData == null && entity.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Legislation.aspx?ID={0}", entity.ID);
                return url;
            }
            else
            {
                var url = String.Format("~/Handlers/GetFile.ashx?Type=Legislation&ID={0}", entity.ID);
                return url;
            }
        }

        protected String GetTargetUrl(LP_Survey entity)
        {
            if (entity == null)
                return "#";

            var url = String.Format("~/Handlers/GetFile.ashx?Type=Survey&ID={0}", entity.ID);
            return url;
        }

        protected String GetTargetUrl(LP_Project entity)
        {
            if (entity == null)
                return "#";

            if (entity.FileData == null && entity.ParentID == null)
            {
                var url = String.Format("~/Pages/User/Projects.aspx?ID={0}", entity.ID);
                return url;
            }
            else
            {
                var url = String.Format("~/Handlers/GetFile.ashx?Type=Project&ID={0}", entity.ID);
                return url;
            }
        }
    }
}