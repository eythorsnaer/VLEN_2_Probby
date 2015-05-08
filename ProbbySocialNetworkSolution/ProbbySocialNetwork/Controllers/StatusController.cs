﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProbbySocialNetwork.Models;
using ProbbySocialNetwork.Models.ViewModels;
//using ProbbySocialNetwork.Services;

namespace ProbbySocialNetwork.Controllers
{
	public class StatusController : Controller
	{
		public ServiceSingleton ss = ServiceSingleton.GetInstance;
		public StatusService statusService = ServiceSingleton.GetStatusService;
		//public AccountService accountService = ServiceSingleton.GetAccountService;
		// GET: Status
		public ActionResult Index()
		{

			//TODO: Implement
			return View();
		}

		[HttpPost]
		public ActionResult CreateStatus(FormCollection collection)
		{
			Status s = new Status();
			s.Post = collection["statusText"];
			s.MediaURL = null;
			s.Date = DateTime.Now;
			s.UserID = User.Identity.GetUserId();
			//ApplicationUser a = accountService.getUserByName(User.Identity.Name);
			//s.UserID = a.Id;
			statusService.addStatus(s);
			return RedirectToAction("Index", "Home");
		}

		public ActionResult EditStatus()
		{
			//TODO: Implement
			return View();
		}

		public ActionResult RemoveStatus()
		{
			//TODO: Implement
			return View();
		}

		public ActionResult Search()
		{
			//TODO: Implement
			return View();
		}
	}
}