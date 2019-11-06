using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.ViewModel;
using WebApplication2.Models;
using WebApplication5.Data;
using WebApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using WebApplication5.ViewModel;
using WebApplication4.Services;
using WebApplication5.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication5.Controllers
{

    public class WorkerLoginController : Controller
    {
        private ApplicationDbContext _context;
        private IWorkerData _userdata;
        private ICustomerData _customerdata;
        private IServiceListData _servicelistdata;
        private IServiceInfoData _serviceInfodata;
       

        public WorkerLoginController(IWorkerData userdata, ICustomerData customerData,IServiceListData serviceListData,IServiceInfoData serviceinfodata)
        {
            _servicelistdata = serviceListData;
            _customerdata = customerData;
            _userdata = userdata;
            _serviceInfodata = serviceinfodata;
           
        }
        // GET: /<controller>/
        [HttpGet("Worker/Index")]
        public IActionResult Index()
        {

            var model = new WorkerServiceModel();
            //model.Workers = _userdata.GetAll();
            //model.Customers = _customerdata.GetAll();
            //model.Service = _servicelistdata.GetAll();
            

            return View(model);
        }
        public IActionResult Default()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(WorkerEditModel obj)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // your code 

                    var worker = new Worker();
                    worker.Email = obj.Email;
                    worker.Password = obj.Password;
                    worker = _userdata.Validate(worker);

                    if (worker != null)
                    {
                        return RedirectToAction("Index", "Operation");
                    }

                    else
                    {
                        return View();
                    }




                }
                catch (AggregateException e)
                {

                }
            }
            return View(obj);
        }
        [HttpGet("WorkerLogin/ServiceInformation/{id}")]
        [AllowAnonymous]
        public IActionResult ServiceInformation(int id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // your code 

                    var model = _userdata.Get(id);
                    if (model == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                   
                     else
                    {
                        var srvModel = _serviceInfodata.Get(id);
                        if (srvModel == null)
                        {
                            return RedirectToAction(nameof(Index));
                       }
                        else
                        {
                            /////////////////////////////////////
                            return View(srvModel);
                        }
                        
                    }


                }
                catch (AggregateException e)
                {

                }
            }
            return View();
        }
    }
}
