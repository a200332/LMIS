using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using CITI.EVO.Tools.Utils;
using CITI.EVO.UserManagement.DAL.Context;
using CITI.EVO.UserManagement.Svc.Contracts;
using CITI.EVO.UserManagement.Svc.Enums;
using CITI.EVO.UserManagement.Web.Common;
using CITI.EVO.UserManagement.Web.Enums;
using CITI.EVO.UserManagement.Web.Extensions;
using CITI.EVO.UserManagement.Web.Manages;
using log4net;
using CITI.EVO.Tools.Extensions;

namespace CITI.EVO.UserManagement.Web.Services.Managers
{
	public static class UserManagementManager
	{
		private static ILog logger;
		public static ILog Logger
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				logger = (logger ?? LogUtil.GetLogger("ServiceLoginLogger"));
				return logger;
			}
		}

		private static IAccessController accessController;
		public static IAccessController AccessController
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				accessController = (accessController ?? AccessControlFactory.GetAccessController());
				return accessController;
			}
		}

		public static Guid? Login(String loginName, String password, bool encryptedPassword)
		{
			if (String.IsNullOrWhiteSpace(loginName) || String.IsNullOrEmpty(password))
			{
				return null;
			}

			var user = UsersManager.GetUser(loginName);
			if (user == null)
			{
				return null;
			}

			if (!encryptedPassword)
			{
				if (user.Password != password)
				{
					return null;
				}
			}
			else
			{
				var userPasswordHash = CryptographyUtil.ComputeMD5(user.Password);
				if (userPasswordHash != password)
				{
					return null;
				}
			}

			var token = AccessController.CreateUserToken(user.ID);

			if (Logger != null)
			{
				var clientIP = String.Empty;

				if (HttpContext.Current != null)
				{
					var request = HttpContext.Current.Request;
					clientIP = String.Format("{0}, {1}", request.UserHostAddress, request.UserHostName);
				}

				Logger.Info(String.Format("Login - LoginName: {0}, Password: {1}, ClientIP: {2}, Token: {3}", loginName, password, clientIP, token));
			}

			return token;
		}

		public static void Logout(Guid token)
		{
			if (Logger != null)
			{
				var clientIP = String.Empty;

				if (HttpContext.Current != null)
				{
					var request = HttpContext.Current.Request;

					clientIP = String.Format("{0}, {1}", request.UserHostAddress, request.UserHostName);
				}

				var loginName = String.Empty;
				var password = String.Empty;

				var user = GetCurrentUser(token);
				if (user != null)
				{
					loginName = user.LoginName;
					password = user.Password;
				}

				Logger.Info(String.Format("Logout - LoginName: {0}, Password: {1}, ClientIP: {2}, Token: {3}", loginName, password, clientIP, token));
			}

			AccessController.ReleaseUserToken(token);
		}

		public static bool IsTokenActual(Guid token)
		{
			return AccessController.ValidateToken(token);
		}

		public static UserContract GetCurrentUser(Guid token)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			var userID = AccessController.GetTokenOwnerID(token);
			if (userID == null)
			{
				return null;
			}

			var user = UsersManager.GetUser(userID.Value);
			return user.ToContract();
		}

		public static PasswordChangeResultEnum ChangePassword(Guid token, String newPassword, String oldPassword)
		{
			if (!AccessController.ValidateToken(token))
			{
				return PasswordChangeResultEnum.TokenNotFound;
			}

			var userID = AccessController.GetTokenOwnerID(token);
			if (userID == null)
			{
				return PasswordChangeResultEnum.UserNotFound;
			}

			var user = UsersManager.GetUser(userID.Value);
			lock (user)
			{
				if (user.Password != oldPassword)
				{
					return PasswordChangeResultEnum.PasswordMismatch;
				}

				if (user.Password == newPassword)
				{
					return PasswordChangeResultEnum.NewAndOldPasswordMatch;
				}

				if (String.IsNullOrWhiteSpace(newPassword))
				{
					return PasswordChangeResultEnum.InvalidPattern;
				}

				if (newPassword == user.LoginName)
				{
					return PasswordChangeResultEnum.InvalidPattern;
				}

				var monthNumber = DataConverter.ToInt32(ConfigurationManager.AppSettings["MonthNumber"]);

				user.Password = newPassword;
				user.PasswordExpirationDate = DateTime.Now.AddMonths(monthNumber);

				user.DateChanged = DateTime.Now;

				UsersManager.UpdateUser(user);
			}

			return PasswordChangeResultEnum.Success;
		}

		public static List<PermissionContract> GetAllResourcesPermissions(Guid token, Guid? projectID)
		{
			var tokenUser = GetCurrentUser(token);
			if (tokenUser == null)
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var query = from user in db.UM_Users
							where user.ID == tokenUser.ID
							from userGroup in user.GroupUsers
							where userGroup.DateDeleted == null
							let @group = userGroup.Group
							where @group.DateDeleted == null
							from perm in @group.Permissions
							let res = perm.Resource
							where perm.DateDeleted == null &&
								  res.DateDeleted == null
							select new
							{
								//GroupID = @group.ID,
								//GroupName = @group.Name,
								ProjectID = res.ProjectID,
								Permission = perm
							};

				if (projectID != null)
				{
					query = from item in query
							where item.ProjectID == projectID || item.ProjectID == null
							select item;
				}

				var entities = query.ToList();
				var contracts = entities.Select(n => n.Permission.ToContract(n.ProjectID)).ToList();

				return contracts;
			}
		}

		public static List<ProjectContract> GetProjects()
		{
			using (var db = new UserManagementDataContext())
			{
				var items = db.UM_Projects.Where(n => n.DateDeleted == null).ToList();
				return items.ToContracts();
			}
		}

		public static List<GroupContract> GetProjectGroups(Guid token, Guid projectID)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var items = (from n in db.UM_Groups
							 where n.ProjectID == projectID &&
								   n.DateDeleted == null
							 select n).ToList();

				return items.ToContracts();
			}
		}


		public static List<UserContract> GetAllUsers(Guid token, bool deleteds)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			var currentUser = GetCurrentUser(token);

			if (currentUser == null)
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var query = from n in db.UM_Users
							select n;

				if (!deleteds)
				{
					query = from n in query
							where n.DateDeleted == null
							select n;
				}

				var items = query.ToList();

				if (!currentUser.IsSuperAdmin)
				{
					items.ForEach(p => p.Password = null);
				}

				return items.ToContracts();
			}
		}

		public static List<UserContract> GetGroupUsers(Guid token, Guid groupID)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var items = (from gu in db.UM_GroupUsers
							 where gu.GroupID == groupID && gu.DateDeleted == null
							 let user = gu.User
							 where user != null &&
								   user.ID == gu.UserID &&
								   user.DateDeleted == null
							 select user).ToList();

				return items.ToContracts();
			}
		}

		public static List<GroupContract> GetUserGroups(Guid token, Guid userID, Guid projectID)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var items = (from gu in db.UM_GroupUsers
							 where gu.UserID == userID &&
									gu.DateDeleted == null
							 let @group = gu.Group
							 where @group != null &&
									@group.DateDeleted == null &&
									@group.ProjectID == projectID
							 select @group).ToList();

				return items.ToContracts();
			}
		}

		public static List<GroupAttributeContract> GetGroupAttributes(Guid token, Guid groupID)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}
			using (var db = new UserManagementDataContext())
			{
				var items = (from groupAttr in db.UM_GroupAttributes
							 where groupAttr.GroupID == groupID &&
								   groupAttr.DateDeleted == null
							 let node = groupAttr.AttributesSchemaNode
							 where node != null &&
								   node.DateDeleted == null
							 let schema = node.AttributesSchema
							 where schema != null &&
								   schema.DateDeleted == null
							 select groupAttr).ToList();

				return items.ToContracts();
			}
		}

		public static List<UserAttributeContract> GetUserAttributes(Guid token, Guid userID, Guid projectID)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var globalItems = (from userAttr in db.UM_UserAttributes
								   where userAttr.DateDeleted == null &&
										 userAttr.UserID == userID
								   let node = userAttr.AttributesSchemaNode
								   where node != null &&
										 node.DateDeleted == null
								   let schema = node.AttributesSchema
								   where schema != null &&
										 schema.DateDeleted == null
								   where schema.ProjectID == null
								   select userAttr).ToList();

				var projectItems = (from userAttr in db.UM_UserAttributes
									where userAttr.DateDeleted == null &&
										  userAttr.UserID == userID
									let node = userAttr.AttributesSchemaNode
									where node != null &&
										  node.DateDeleted == null
									let schema = node.AttributesSchema
									where schema != null &&
										  schema.DateDeleted == null
									let project = schema.Project
									where project != null &&
										  project.DateDeleted == null &&
										  project.ID == projectID
									select userAttr).ToList();

				var allItems = globalItems.Union(projectItems);
				return allItems.ToContracts();
			}
		}

		public static Dictionary<String, String> GetUserAttributesDictionary(Guid token, Guid userID, Guid projectID)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var globalItems = (from userAttr in db.UM_UserAttributes
								   where userAttr.DateDeleted == null &&
										 userAttr.UserID == userID
								   let node = userAttr.AttributesSchemaNode
								   where node != null &&
										 node.DateDeleted == null
								   let schema = node.AttributesSchema
								   where schema != null &&
										 schema.DateDeleted == null
								   where schema.ProjectID == null
								   select new { node.Name, userAttr.Value }).ToList();

				var projectItems = (from userAttr in db.UM_UserAttributes
									where userAttr.DateDeleted == null &&
										  userAttr.UserID == userID
									let node = userAttr.AttributesSchemaNode
									where node != null &&
										  node.DateDeleted == null
									let schema = node.AttributesSchema
									where schema != null &&
										  schema.DateDeleted == null
									let project = schema.Project
									where project != null &&
										  project.DateDeleted == null &&
										  project.ID == projectID
									select new { node.Name, userAttr.Value }).ToList();

				var allItems = globalItems.Union(projectItems);

				var comparer = StringComparer.OrdinalIgnoreCase;

				var allItemsLp = allItems.ToLookup(n => n.Name, comparer);

				var dict = new Dictionary<String, String>(comparer);
				foreach (var itemsGrp in allItemsLp)
				{
					var valuesQuery = (from n in itemsGrp
									   where !String.IsNullOrWhiteSpace(n.Value)
									   select n.Value);

					var @set = valuesQuery.ToHashSet(comparer);

					var values = String.Join(",", @set);
					dict[itemsGrp.Key] = values;
				}

				return dict;
			}
		}

		public static Dictionary<Guid, Dictionary<String, String>> GetAllUsersGlobalAttribetes(Guid token, bool deleteds)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var usersQuery = from n in db.UM_Users
								 select n;

				if (!deleteds)
				{
					usersQuery = from n in usersQuery
								 where n.DateDeleted == null
								 select n;
				}

				var globalAttrQuery = from user in usersQuery
									  from attr in user.UserAttributes
									  where attr.DateDeleted == null
									  let node = attr.AttributesSchemaNode
									  where node != null &&
											node.DateDeleted == null
									  let schema = node.AttributesSchema
									  where schema != null &&
											schema.DateDeleted == null
									  where schema.ProjectID == null
									  select new
									  {
										  UserID = attr.UserID,
										  Name = node.Name,
										  Value = attr.Value
									  };

				var globaAttrLp = globalAttrQuery.ToLookup(n => n.UserID);

				var globalAttrDict = new Dictionary<Guid, Dictionary<String, String>>();
				foreach (var globalAttrGrp in globaAttrLp)
				{
					var dict = new Dictionary<String, String>();

					var attrLp = globalAttrGrp.ToLookup(n => n.Name);
					foreach (var attrGrp in attrLp)
					{
						var valuesQuery = attrGrp.Select(n => n.Value).ToHashSet(StringComparer.OrdinalIgnoreCase);
						var values = String.Join(";", valuesQuery);

						dict.Add(attrGrp.Key, values);
					}

					globalAttrDict.Add(globalAttrGrp.Key, dict);
				}

				return globalAttrDict;
			}
		}

		public static PermissionContract GetResourcePermission(Guid token, String resourcePath)
		{
			using (var db = new UserManagementDataContext())
			{
				var comparer = StringComparer.OrdinalIgnoreCase;

				var pathArray = resourcePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
				if (pathArray.Length == 0)
					return null;

				var projectID = DataConverter.ToNullableGuid(pathArray[0]);
				if (projectID == null)
					return null;

				var exists = db.UM_Projects.Any(n => n.ID == projectID && n.IsActive == true);
				if (!exists)
					return null;

				var user = GetCurrentUser(token);
				if (user == null)
					return null;

				var requestGlobal = comparer.Equals(pathArray[1], "Global");

				var actualProject = (requestGlobal ? null : projectID);
				var index = (requestGlobal ? 1 : 2);

				if (pathArray.Length == index)
					return new PermissionContract();

				var groupsID = (from n in db.UM_GroupUsers
								where n.UserID == user.ID &&
									   n.DateDeleted == null
								let m = n.Group
								where m.DateDeleted == null &&
									  m.ProjectID == projectID
								select m.ID).ToList();

				if (groupsID.Count == 0)
					return null;

				var resQuery = from n in db.UM_Resources
							   where n.DateDeleted == null
							   select n;

				if (requestGlobal)
				{
					resQuery = from n in resQuery
							   where n.ProjectID == null
							   select n;
				}
				else
				{
					resQuery = from n in resQuery
							   where n.ProjectID == projectID
							   select n;
				}

				UM_Resource parentRes = null;
				UM_Resource currentRes = null;

				for (int i = index; i < pathArray.Length; i++)
				{
					var resourceName = pathArray[i];

					var finalQuery = from n in resQuery
									 where n.Value == resourceName
									 select n;

					if (parentRes == null)
					{
						finalQuery = from n in finalQuery
									 where n.ParentID == null
									 select n;
					}
					else
					{
						finalQuery = from n in finalQuery
									 where n.ParentID == parentRes.ID
									 select n;
					}

					currentRes = finalQuery.FirstOrDefault();

					if (currentRes == null)
					{
						currentRes = new UM_Resource
						{
							ID = Guid.NewGuid(),
							DateCreated = DateTime.Now,
							Name = resourceName,
							ProjectID = actualProject,
							Description = "Automatic Record",
							Value = resourceName,
							Type = 0
						};

						if (parentRes == null)
							db.UM_Resources.InsertOnSubmit(currentRes);
						else
							parentRes.Children.Add(currentRes);
					}

					parentRes = currentRes;
				}

				db.SubmitChanges();

				return GetPermissionContract(currentRes, groupsID, projectID.Value);
			}
		}

		private static PermissionContract GetPermissionContract(UM_Resource resource, IList<Guid> groupsID, Guid projectID)
		{
			var result = new PermissionContract
			{
				ProjectID = projectID,
				ResourceID = resource.ID,
				ResourcePath = resource.FullPath()
			};

			foreach (var groupID in groupsID)
			{
				var groupPerm = GetPermissionContract(resource, groupID, projectID);
				result.RuleValue |= groupPerm.RuleValue;

				if (groupPerm.PermissionParameter != null)
				{
					foreach (var pair in groupPerm.PermissionParameter)
					{
						if (result.PermissionParameter.ContainsKey(pair.Key))
						{
							var key = String.Format("{0}_{1}", pair.Key, groupID);
							result.PermissionParameter[key] = pair.Value;
						}
						else
						{
							result.PermissionParameter[pair.Key] = pair.Value;
						}
					}
				}
			}

			return result;
		}

		private static PermissionContract GetPermissionContract(UM_Resource resource, Guid groupID, Guid projectID)
		{
			using (var db = new UserManagementDataContext())
			{
				var permission = (from n in db.UM_Permissions
								  where n.DateDeleted == null &&
										n.GroupID == groupID &&
										n.ResourceID == resource.ID
								  orderby n.RuleValue descending
								  select n).FirstOrDefault();

				if (permission != null)
				{
					return permission.ToContract(projectID);
				}

				if (resource.Parent == null)
				{
					var newPemission = new UM_Permission();

					newPemission.ID = Guid.NewGuid();
					newPemission.GroupID = groupID;
					newPemission.ResourceID = resource.ID;
					newPemission.RuleValue = (int)RulePermissionsEnum.View;
					newPemission.DateCreated = DateTime.Now;

					db.UM_Permissions.InsertOnSubmit(newPemission);
					db.SubmitChanges();

					return newPemission.ToContract(projectID);
				}

				return GetPermissionContract(resource.Parent, groupID, projectID);
			}
		}

		public static List<MessageContract> GetMessages(Guid token, Guid? moduleId)
		{
			var user = GetCurrentUser(token);
			var userGroups = GetUserGroups(token, user.ID, moduleId.Value);

			using (var db = new UserManagementDataContext())
			{
				var objectsList = userGroups.Select(p => p.ID).ToSortedSet();
				objectsList.Add(moduleId.Value);
				objectsList.Add(user.ID);

				var objectsArrays = objectsList.ToArray();

				var allTypeMessagesQuery = from n in db.UM_Messages
										   where n.DateDeleted == null &&
												 n.Type == (int)MessageTypeEnum.All &&
												 objectsArrays.Contains(n.ObjectID)
										   select n;

				var stdMessagesQuery = from n in db.UM_Messages
									   let count = n.MessageViewers.Count(t => t.UserID != user.ID)
									   where n.DateDeleted == null &&
											 n.Type == (int)MessageTypeEnum.Standard &&
											 objectsArrays.Contains(n.ObjectID) &&
											 count == 0
									   select n;

				var messagesQuery = stdMessagesQuery.Union(allTypeMessagesQuery);

				var messagesList = messagesQuery.ToList();

				foreach (var message in messagesList)
				{
					if (message.Type == (int)MessageTypeEnum.All)
					{
						var count = (from n in db.UM_MessageViewers
									 where n.MessageID == message.ID &&
										   n.UserID == user.ID
									 select n.ID).Count();

						if (count == 0)
						{
							var messageViewer = new UM_MessageViewer();
							messageViewer.ID = Guid.NewGuid();
							messageViewer.UserID = user.ID;
							messageViewer.MessageID = message.ID;

							db.UM_MessageViewers.InsertOnSubmit(messageViewer);
						}
					}
					else
					{
						var messageViewer = new UM_MessageViewer();
						messageViewer.ID = Guid.NewGuid();
						messageViewer.UserID = user.ID;
						messageViewer.MessageID = message.ID;

						db.UM_MessageViewers.InsertOnSubmit(messageViewer);
					}
				}

				db.SubmitChanges();

				return messagesList.ToContracts();
			}
		}

		public static List<ProjectContract> GetProjectByUserToken(Guid token)
		{
			if (!AccessController.ValidateToken(token))
			{
				return null;
			}

			var currentUser = GetCurrentUser(token);

			if (currentUser == null)
			{
				return null;
			}

			using (var db = new UserManagementDataContext())
			{
				var projects = (from gu in db.UM_GroupUsers
								where gu.UserID == currentUser.ID &&
									  gu.DateDeleted == null
								let @group = gu.Group
								where @group != null && @group.DateDeleted == null
								let project = @group.Project
								where project != null && project.DateDeleted == null
								select project).Distinct().ToList();

				return projects.ToContracts();
			}
		}

		public static bool HasMessages(Guid token, Guid? moduleId)
		{
			var user = GetCurrentUser(token);
			if (user == null)
			{
				throw new Exception("Unable to get user by token");
			}

			var userGroups = GetUserGroups(token, user.ID, moduleId.Value);
			if (userGroups == null)
			{
				return false;
			}

			using (var db = new UserManagementDataContext())
			{
				var objectsList = userGroups.Select(p => p.ID).ToSortedSet();
				objectsList.Add(moduleId.Value);
				objectsList.Add(user.ID);

				var objectsArrays = objectsList.ToArray();

				var allTypeMessagesQuery = from n in db.UM_Messages
										   where n.DateDeleted == null &&
												 n.Type == (int)MessageTypeEnum.All &&
												 objectsArrays.Contains(n.ObjectID)
										   select n;

				var stdMessagesQuery = from n in db.UM_Messages
									   let count = n.MessageViewers.Count(t => t.UserID != user.ID)
									   where n.DateDeleted == null &&
											 objectsArrays.Contains(n.ObjectID) &&
											 (n.Type == (int)MessageTypeEnum.Standard && count == 0)
									   select n;

				var messagesQuery = stdMessagesQuery.Union(allTypeMessagesQuery);
				var messagesCount = messagesQuery.Count();

				return (messagesCount > 0);

			}

		}
	}
}