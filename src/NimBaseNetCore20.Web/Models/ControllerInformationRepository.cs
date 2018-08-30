using PEIIS.Attributes;
using PEIIS.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PEIIS.Data;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DisplayNameAttribute = PEIIS.Attributes.DisplayNameAttribute;

namespace PEIIS.Models
{
    /// <summary>
    /// Dynamically get a list of the controllers and actions for the AdminLTE demo
    /// </summary>
    public class ControllerInformationRepository : IControllerInformationRepository
    {
        ApplicationDbContext _context;
        private List<ControllerInfo> _controllerInfo = new List<ControllerInfo>();

        public ControllerInformationRepository(ApplicationDbContext context)
        {
            _context = context;

            //TODO: Refactor to make more readable
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().AsEnumerable();
            var controllers = types.Where(type => typeof(Controller).IsAssignableFrom(type)).ToList();
            var orderedcontrollers = controllers.OrderBy(t =>
            {
                var orderby = (DisplayOrderAttribute)t.GetTypeInfo().GetCustomAttribute<DisplayOrderAttribute>();
                if (orderby != null)
                    return orderby.DisplayOrder;
                else
                    return 0;
            });
            var controllerAttrs = orderedcontrollers.Select( d => new {
                fullname = d.FullName,
                controllerName = CleanupControllerName(d.Name),
                displayImage = d.GetTypeInfo().GetCustomAttribute<DisplayImageAttribute>(),
                action_list = d.GetMethods().Where(method => method.IsPublic && method.IsDefined(typeof(DisplayActionMenuAttribute))),
                treeview = d.GetTypeInfo().GetCustomAttribute<TreeViewAttribute>(),
                treeviewsettings = d.GetTypeInfo().GetCustomAttribute<TreeViewSettingsAttribute>(),
                displayName = d.GetTypeInfo().GetCustomAttribute<DisplayNameAttribute>(),
                authorize = d.GetTypeInfo().GetCustomAttribute<AuthorizeAttribute>()
            });
            _controllerInfo = controllerAttrs.Where(a => a.action_list.Count() > 0).ToList()
                .Select(
                   a =>
                    new ControllerInfo()
                    {
                        ControllerName = a.controllerName,
                        FullName = a.fullname,
                        DisplayName = a.displayName != null ? a.displayName.DisplayName : a.controllerName.SeparatePascalCase(),
                        DisplayImage = a.displayImage.DisplayImage,
                        TreeViewSettings = a.treeview,
                        TreeViewSettings2 = a.treeviewsettings,
                        HasAuthorize = a.authorize != null,
                        AuthorizedRoles = a.authorize != null && a.authorize.Roles != null ? a.authorize.Roles.Split(",") : new string[0],
                        //Actions
                        ControllerActions = a.action_list.Select(act => new Models.ActionInfo()
                        {
                            ActionName = act.Name,
                            DisplayName = act.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault() != null ? act.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault().DisplayName : act.Name.SeparatePascalCase(),
                            DisplayImage = act.GetCustomAttributes<DisplayImageAttribute>().FirstOrDefault().DisplayImage, //Will generate an exception if the attribute is not defined.
                            //ScriptAfterPartialView = act.GetCustomAttributes<ScriptAfterPartialViewAttribute>().FirstOrDefault().ScriptToRun, //Will generate an exception if the attribute is not defined.
                            TreeViewSettings = act.GetCustomAttributes<TreeViewAttribute>().FirstOrDefault(),
                            TreeViewSettings2 = act.GetCustomAttributes<TreeViewSettingsAttribute>().FirstOrDefault(),
                            HasAuthorize = act.GetCustomAttribute<AuthorizeAttribute>() != null,
                            AuthorizedRoles = act.GetCustomAttribute<AuthorizeAttribute>() != null && act.GetCustomAttribute<AuthorizeAttribute>().Roles != null ? act.GetCustomAttribute<AuthorizeAttribute>().Roles.Split(",") : new string[0]
                        }).ToList()
                    }
            ).ToList();

        }

        private string CleanupControllerName(string controllerName)
        {
            return controllerName.Replace("Controller", "");
        }

        public List<ControllerInfo> GetAll()
        {
            return _controllerInfo;
        }
        
        public List<ControllerInfo> GetAllForUsername(string username)
        {
            var lstValidControllers = new List<ControllerInfo>();

            //Getting User
            var usr = (IdentityUser)_context.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (usr != null)
            {
                //Getting User-Role relationship
                var lstUserRoles = _context.UserRoles.Where(ur => ur.UserId == usr.Id).ToList();
                if (lstUserRoles != null)
                {
                    //Getting role names
                    var lstUserRoleNames = _context.Roles.Where(r => lstUserRoles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToList();
                    if (lstUserRoleNames.Count() > 0)
                    {
                        //_controllerInfo = _controllerInfo.Where(c => c.AuthorizedRoles.Any(cr => lstRoles.Contains(cr) || c.ControllerActions.Any(a => a.AuthorizedRoles.Any(ar => lstRoles.Contains(ar))))).ToList();
                        foreach(var controller in _controllerInfo)
                        {
                            if (controller.HasAuthorize)
                            {
                                var proof = controller.AuthorizedRoles.Any(cr => lstUserRoleNames.Contains(cr));
                                proof = controller.AuthorizedRoles.Any(cr => lstUserRoleNames[0]==cr);
                                //Protected controller
                                if (controller.AuthorizedRoles.Length == 0 || controller.AuthorizedRoles.Any(cr => lstUserRoleNames.Contains(cr)))
                                {
                                    //User has an authorized role. Remove all action that have authorize and does not include the autorized roles in controller
                                    controller.ControllerActions.RemoveAll(a => a.HasAuthorize && a.AuthorizedRoles.Length > 0 && !a.AuthorizedRoles.Any(ar => lstUserRoleNames.Contains(ar)));
                                    lstValidControllers.Add(controller);
                                }
                                //else: user is not in authorized role. not include controller or actions
                            }
                            else
                            {
                                //Its a public controller, remove only actions that are protected and that not include any user role
                                controller.ControllerActions.RemoveAll(a => a.HasAuthorize && a.AuthorizedRoles.Length > 0 && !a.AuthorizedRoles.Any(ar => lstUserRoleNames.Contains(ar)));
                                lstValidControllers.Add(controller);
                            }     
                        }
                    }
                }
            }
            
            return lstValidControllers;
        }
    }
}

