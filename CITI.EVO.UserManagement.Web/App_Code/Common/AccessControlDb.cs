using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using CITI.EVO.Tools.Utils;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.Tools.Extensions;

namespace CITI.EVO.UserManagement.Web.Common
{
    public class AccessControlDb : IAccessController
    {
        private readonly IDictionary<Guid, Object> lockersDict = new Dictionary<Guid, Object>();

        private TimeSpan? expireTime;
        public TimeSpan ExpireTime
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (expireTime == null)
                {
                    var loginExpireTime = DataConverter.ToNullableInt(ConfigurationManager.AppSettings["LoginExpireTime"]);
                    if (loginExpireTime != null)
                    {
                        expireTime = TimeSpan.FromMinutes(loginExpireTime.Value);
                    }
                    else
                    {
                        expireTime = TimeSpan.FromMinutes(30);
                    }
                }

                return expireTime.Value;
            }
        }

        public Guid CreateUserToken(Guid userID)
        {
            lock (lockersDict)
            {
                using (var db = DcFactory.Create<UserManagementDataContext>())
                {
                    //db.EnableChangesLog = false;

                    var loginToken = (from n in db.UM_LoginTokens
                                      where n.UserID == userID &&
                                            n.DateDeleted == null
                                      orderby n.DateCreated descending
                                      select n).FirstOrDefault();

                    if (loginToken == null || CheckExpiration(loginToken))
                    {
                        loginToken = new UM_LoginToken
                        {
                            ID = Guid.NewGuid(),
                            DateCreated = DateTime.Now,
                            UserID = userID,
                            LoginToken = Guid.NewGuid(),
                        };

                        SetExpiration(loginToken);
                        GetLocker(loginToken.LoginToken);

                        db.UM_LoginTokens.InsertOnSubmit(loginToken);
                    }

                    db.SubmitChanges();

                    return loginToken.LoginToken;
                }
            }
        }

        public void ReleaseUserToken(Guid token)
        {
            var locker = GetLocker(token);
            lock (locker)
            {
                using (var db = DcFactory.Create<UserManagementDataContext>())
                {
                    //db.EnableChangesLog = false;

                    var loginToken = (from n in db.UM_LoginTokens
                                      where n.DateDeleted == null &&
                                            n.LoginToken == token
                                      select n).FirstOrDefault();

                    if (loginToken != null)
                    {
                        loginToken.DateDeleted = DateTime.Now;
                        loginToken.DeleteReason = 1;

                        db.SubmitChanges();

                        RemoveLocker(token);
                    }
                }
            }
        }

        public Guid? GetTokenOwnerID(Guid token)
        {
            var locker = GetLocker(token);
            lock (locker)
            {
                using (var db = DcFactory.Create<UserManagementDataContext>())
                {
                    //db.EnableChangesLog = false;

                    var loginToken = (from n in db.UM_LoginTokens
                                      where n.DateDeleted == null &&
                                            n.LoginToken == token
                                      select n).FirstOrDefault();

                    var user = (Guid?)null;

                    if (!CheckExpiration(loginToken))
                    {
                        if (loginToken != null)
                        {
                            user = loginToken.UserID;
                        }
                    }

                    db.SubmitChanges();

                    return user;
                }
            }
        }

        public IDictionary<Guid, Guid?> GetTokensOwners()
        {
            lock (lockersDict)
            {
                using (var db = DcFactory.Create<UserManagementDataContext>())
                {
                    //db.EnableChangesLog = false;

                    var loginTokens = (from n in db.UM_LoginTokens
                                       where n.DateDeleted == null
                                       select n).ToList();

                    var dict = new Dictionary<Guid, Guid?>(loginTokens.Count);

                    foreach (var loginToken in loginTokens)
                    {
                        if (!CheckExpiration(loginToken))
                        {
                            dict.Add(loginToken.ID, loginToken.UserID);
                        }
                    }

                    db.SubmitChanges();

                    return dict;
                }
            }
        }

        public bool ValidateToken(Guid token)
        {
            var locker = GetLocker(token);
            lock (locker)
            {
                using (var db = DcFactory.Create<UserManagementDataContext>())
                {
                    //db.EnableChangesLog = false;

                    var loginToken = (from n in db.UM_LoginTokens
                                      where n.DateDeleted == null &&
                                            n.LoginToken == token
                                      select n).FirstOrDefault();

                    var result = !CheckExpiration(loginToken);
                    if (result)
                    {
                        result = (loginToken != null);
                    }

                    db.SubmitChanges();

                    return result;
                }
            }
        }

        private bool CheckExpiration(UM_LoginToken loginToken)
        {
            if (loginToken == null)
            {
                return false;
            }

            if (loginToken.ExpireDate != null)
            {
                if (loginToken.ExpireDate < DateTime.Now)
                {
                    loginToken.DateDeleted = DateTime.Now;
                    loginToken.DeleteReason = 2;

                    RemoveLocker(loginToken.LoginToken);

                    return true;
                }
            }

            SetExpiration(loginToken);

            return false;
        }

        private void SetExpiration(UM_LoginToken loginToken)
        {
            if (loginToken == null)
            {
                return;
            }

            var currentDate = DateTime.Now;
            loginToken.LastAccessDate = currentDate;
            loginToken.ExpireDate = currentDate.Add(ExpireTime);
        }

        private Object GetLocker(Guid token)
        {
            lock (lockersDict)
            {
                var locker = lockersDict.GetValueOrDefault(token);
                if (locker == null)
                {
                    locker = new Object();
                    lockersDict.Add(token, locker);
                }

                return locker;
            }
        }

        private void RemoveLocker(Guid token)
        {
            lock (lockersDict)
            {
                lockersDict.Remove(token);
            }
        }
    }
}