using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Svc.Contracts;
using CITI.EVO.UserManagement.Web.Extensions;

namespace CITI.EVO.UserManagement.Web.Utils
{
    public static class CacheEntitiesUtil
    {
        private const String UmProjectsKey = "$[CacheEntitiesUtil_UmProjects]";

        private const String UmGroupsKey = "$[CacheEntitiesUtil_UmGroupsKey]";
        private const String UmGroupsByProjectKey = "$[CacheEntitiesUtil_UmGroupsByProjectKey]";
        private const String UmGroupsByParentKey = "$[CacheEntitiesUtil_UmGroupsByParentKey]";

        private const String UmGroupUsersKey = "$[CacheEntitiesUtil_UmGroupUsers]";
        private const String UmGroupUsersByGroupKey = "$[CacheEntitiesUtil_UmGroupUsersByGroupKey]";

        private const String UmUsersKey = "$[CacheEntitiesUtil_UmUsers]";

        #region Properties

        public static HttpSessionState Session
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    return context.Session;
                }

                return null;
            }
        }

        public static IDictionary<Guid, ProjectContract> UmProjects
        {
            get
            {
                var dict = Session[UmProjectsKey] as IDictionary<Guid, ProjectContract>;
                if (dict == null)
                {
                    using (var db = new UserManagementDataContext())
                    {
                        var projects = db.UM_Projects.Where(n => n.DateDeleted == null).ToList();
                        var contracts = projects.ToContracts();

                        dict = contracts.ToDictionary(n => n.ID);

                        Session[UmProjectsKey] = dict;
                    }
                }

                return dict;
            }
        }

        public static IDictionary<Guid, GroupContract> UmGroups
        {
            get
            {
                var dict = Session[UmGroupsKey] as IDictionary<Guid, GroupContract>;

                if (dict == null)
                {
                    using (var db = new UserManagementDataContext())
                    {
                        var list = (from g in db.UM_Groups
                                    where g.DateDeleted == null
                                    select g).ToList();

                        var contracts = list.ToContracts();
                        if (contracts == null)
                        {
                            dict = new Dictionary<Guid, GroupContract>();
                        }
                        else
                        {
                            dict = contracts.ToDictionary(n => n.ID);
                        }

                        Session[UmGroupsKey] = dict;
                    }
                }

                return dict;
            }
        }

        public static IDictionary<Guid, List<GroupContract>> UmGroupsByProject
        {
            get
            {
                var dict = Session[UmGroupsByProjectKey] as IDictionary<Guid, List<GroupContract>>;
                if (dict == null)
                {
                    var umGroupsLookup = UmGroups.Values.Where(n => n.DateDeleted == null).ToLookup(n => n.ProjectID);
                    dict = umGroupsLookup.ToDictionary(n => n.Key, n => n.ToList());

                    Session[UmGroupsByProjectKey] = dict;
                }

                return dict;
            }
        }

        public static IDictionary<Guid, List<GroupContract>> UmGroupsByParent
        {
            get
            {
                var dict = Session[UmGroupsByParentKey] as IDictionary<Guid, List<GroupContract>>;
                if (dict == null)
                {
                    var umGroupsLookup = UmGroups.Values.Where(n => n.DateDeleted == null).ToLookup(n => n.ParentID.GetValueOrDefault());
                    dict = umGroupsLookup.ToDictionary(n => n.Key, n => n.ToList());

                    Session[UmGroupsByParentKey] = dict;
                }

                return dict;
            }
        }

        public static IDictionary<Guid, GroupUserContract> UmGroupUsers
        {
            get
            {
                var dict = Session[UmGroupUsersKey] as IDictionary<Guid, GroupUserContract>;
                if (dict == null)
                {
                    using (var db = new UserManagementDataContext())
                    {
                        var list = (from g in db.UM_GroupUsers
                                    where g.DateDeleted == null
                                    select g).ToList();

                        var contracts = list.ToContracts();

                        dict = contracts.ToDictionary(n => n.ID);

                        Session[UmGroupUsersKey] = dict;
                    }
                }

                return dict;
            }

        }

        public static IDictionary<Guid, List<GroupUserContract>> UmGroupUsersByGroup
        {
            get
            {
                var dict = Session[UmGroupUsersByGroupKey] as IDictionary<Guid, List<GroupUserContract>>;
                if (dict == null)
                {
                    var umGroupsLookup = UmGroupUsers.Values.Where(n => n.DateDeleted == null).ToLookup(n => n.GroupID);
                    dict = umGroupsLookup.ToDictionary(n => n.Key, n => n.ToList());

                    Session[UmGroupUsersByGroupKey] = dict;
                }

                return dict;
            }
        }

        public static IDictionary<Guid, UserContract> UmUsers
        {
            get
            {
                var dict = Session[UmUsersKey] as IDictionary<Guid, UserContract>;

                if (dict == null)
                {
                    using (var db = new UserManagementDataContext())
                    {
                        var list = (from g in db.UM_Users
                                    where g.DateDeleted == null
                                    select g).ToList();

                        var contracts = list.ToContracts();

                        dict = contracts.ToDictionary(n => n.ID);

                        Session[UmUsersKey] = dict;
                    }
                }

                return dict;
            }
        }

        #endregion

        public static void ResetProjectsCache()
        {
            Session[UmProjectsKey] = null;
        }

        public static void ResetGroupsCache()
        {
            Session[UmGroupsKey] = null;
        }

        public static void ResetGroupUsersCache()
        {
            Session[UmGroupUsersKey] = null;
        }

        public static void ResetGroupUsersByGroupCache()
        {
            Session[UmGroupUsersByGroupKey] = null;
        }

        public static void ResetUsersCache()
        {
            Session[UmUsersKey] = null;
        }

        public static void ResetGroupsByParentCache()
        {
            Session[UmGroupsByParentKey] = null;
        }

        public static void ResetGroupsByProjectCache()
        {
            Session[UmGroupsByProjectKey] = null;
        }

        public static void ResetAll()
        {
            ResetProjectsCache();
            ResetGroupsCache();
            ResetGroupUsersCache();
            ResetGroupUsersByGroupCache();
            ResetUsersCache();
            ResetGroupsByParentCache();
            ResetGroupsByProjectCache();
        }
    }
}