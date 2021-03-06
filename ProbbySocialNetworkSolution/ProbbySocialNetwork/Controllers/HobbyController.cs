﻿using ProbbySocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProbbySocialNetwork.Models.ViewModels;

namespace ProbbySocialNetwork.Controllers
{
    public class HobbyController : Controller
    {
		public ServiceSingleton serviceManager = ServiceSingleton.GetInstance;
		public AccountService accountService = ServiceSingleton.GetAccountService;
		public StatusService statusService = ServiceSingleton.GetStatusService;
		public GroupService groupService = ServiceSingleton.GetGroupService;
		public HobbyService hobbyService = ServiceSingleton.GetHobbyService;
		
        
		// GET: Hobby
        [Authorize]
        public ActionResult Index(int? id)
        {
            HobbyViewModel model = new HobbyViewModel();
            if (id.HasValue)
            {
                int realid = id.Value;
                model.currentUser = accountService.getUserByName(User.Identity.Name);
                model.currentHobby = hobbyService.getHobbyByID(realid);
                model.currentHobbyGroups = hobbyService.getGroupsByHobby(model.currentHobby);
                model.currentHobbyStatusHistory = statusService.getStatusesByHobby(model.currentHobby);
                model.currentHobbyUsers = hobbyService.getUsersByHobby(model.currentHobby);

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult CreateHobby(FormCollection collection)
        {
            Hobby h = new Hobby();
			h.Name = collection["hobbyName"];

			ApplicationUser currentUser = accountService.getUserByName(User.Identity.Name);

			hobbyService.addHobby(h);
			hobbyService.addHobbyToUser(currentUser, h);

			string url = this.Request.UrlReferrer.AbsoluteUri;
			return Redirect(url);
        }

        [Authorize]
        public ActionResult AddHobby(int? id)
        {
            if (id.HasValue)
            {
                int realid = id.Value;
                Hobby toAdd = hobbyService.getHobbyByID(realid);
                ApplicationUser currentUser = accountService.getUserByName(User.Identity.Name);

                hobbyService.addHobbyToUser(currentUser, toAdd);

				string url = this.Request.UrlReferrer.AbsoluteUri;
				return Redirect(url);
            }
            else
            {
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult RemoveHobby(int? id)
        {
            if (id.HasValue)
            {
                int realid = id.Value;
                Hobby toDel = hobbyService.getHobbyByID(realid);
                ApplicationUser currentUser = accountService.getUserByName(User.Identity.Name);

                hobbyService.removeHobbyFromuser(currentUser, toDel);

				string url = this.Request.UrlReferrer.AbsoluteUri;
				return Redirect(url);
            }
            else
            {
                return View("Error");
            }
        }
    }
}