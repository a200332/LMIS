using System;
using System.Linq;
using CITI.EVO.UserManagement.DAL.Context;

namespace CITI.EVO.UserManagement.Web.Manages
{
    public static class UsersManager
    {
        public static UM_User GetUser(Guid userID)
        {
            using (var db = new UserManagementDataContext())
            {
                return db.UM_Users.FirstOrDefault(n => n.ID == userID);
            }
        }

        public static UM_User GetUser(String loginName)
        {
            loginName = (loginName ?? String.Empty);

            using (var db = new UserManagementDataContext())
            {
                var user = (from n in db.UM_Users
                            where n.DateDeleted == null &&
                                  n.LoginName.Trim().ToLower() == loginName.Trim().ToLower()
                            select n).FirstOrDefault();

                return user;
            }
        }

        public static void UpdateUser(UM_User user)
        {
            if (user == null)
                return;

            using (var db = new UserManagementDataContext())
            {
                var exUser = db.UM_Users.FirstOrDefault(n => n.ID == user.ID);
                if (exUser == null)
                    return;

                exUser.ID = user.ID;
                exUser.LoginName = user.LoginName;
                exUser.Password = user.Password;
                exUser.PasswordExpirationDate = user.PasswordExpirationDate;
                exUser.FirstName = user.FirstName;
                exUser.LastName = user.LastName;
                exUser.IsActive = user.IsActive;
                exUser.IsSuperAdmin = user.IsSuperAdmin;
                exUser.Email = user.Email;
                exUser.Address = user.Address;
                exUser.DateChanged = user.DateChanged;
                exUser.DateCreated = user.DateCreated;
                exUser.DateDeleted = user.DateDeleted;

                db.SubmitChanges();
            }
        }
    }
}