using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;
using WebApplication2.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using WebApplication4.Services;
using System.Text;
using Microsoft.AspNetCore.Http;
using WebApplication4.ViewModel;
using WebApplication4.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication5.Data;
using Microsoft.AspNetCore.Authorization;
using WebApplication5.Models;
using WebApplication5.Services;
using Microsoft.Extensions.Logging;
using WebApplication5.Controllers;
using System.Security.Claims;

namespace WebApplication4.Controllers
{
  [Authorize]
    public class OperationController : Controller
    {
        private IGreeter _greeter;
        private IWorkerData _workerData;
        private ICustomerData _customerData;
        private ApplicationDbContext _context;
        private GetServiceType _getServiceType;
        private IServiceListData _serviceListData;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        //private readonly UserManager<MyIdentityUser> _userManager;
        public OperationController(IWorkerData workerData, IGreeter greeter, ICustomerData customerData, IServiceListData serviceListData, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _workerData = workerData;
            _greeter = greeter;
            _customerData = customerData;
            _serviceListData = serviceListData;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        // GET: /<controller>/
        public  IActionResult Index()
        {
            //MyIdentityUser user = _userManager.GetUserAsync
            //             (HttpContext.User).Result;

            //ViewBag.Message = $"Welcome {user.FullName}!";
            //if (_userManager.IsInRoleAsync(user, "NormalUser").Result)
            //{
            //    ViewBag.RoleMessage = "You are a NormalUser.";
            //}
            int WorkerId = 0;
            string Email = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Email;
            var uId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string cn = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
           // SqlConnection cn = new SqlConnection("Data Source=(local);Integrated Security=False;Database=clnsyste_aspnet;User ID=clnsyste_Hesam;Connect Timeout=15;Encrypt=False;Packet Size=4096");
  
            cn.Open();
            SqlCommand cm2 = new SqlCommand("Select RoleId  from AspNetUserRoles  where UserId='" + uId + "'",cn);
            SqlDataReader reader2 = cm2.ExecuteReader();

            string RoleId = null;
            
            while (reader2.Read())
            {
                // get the results of each column
                RoleId = (string)reader2["RoleId"];

            }


            reader2.Dispose();

            cn.Close();

            var model = new HomeIndexViewModel();
            model.Workers = _workerData.GetAll();
            model.Customers = _customerData.GetAll();
            model.Service = _serviceListData.GetAll();
            model.CurrentMessage = _greeter.getTimeofday();
           
            if (RoleId == "1")
            {
                return View(model);

            }
            else
                if (RoleId == "2")
            {
                cn.Open();
                SqlCommand cm3 = new SqlCommand("Select Id  from Workers where Email='" + Email + "'", cn);
                SqlDataReader reader3 = cm3.ExecuteReader();
                while (reader3.Read())
                {
                    WorkerId = (Int32)reader3["Id"];
                }
                reader3.Dispose();
                cn.Close();
                cn.Open();
                int WId = 0;
                SqlCommand cm4 = new SqlCommand("Select WId  from ServiceInfo where WId=" + WorkerId, cn);
                SqlDataReader reader4 = cm4.ExecuteReader();
                while (reader4.Read())
                {
                    WId = (Int32)reader4["WId"];
                }
                reader4.Dispose();
                cn.Close();
                if (WId > 0)
                {
                    return RedirectToAction("ServiceInformation", "WorkerLogin", new { id = WorkerId });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public IActionResult Details_Worker(int id)
        {
            var model = _workerData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create_Worker()
        {
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Create_Worker(WorkerEditModel model)
        {
            //...
            if (ModelState.IsValid)
            {
                var newWorker = new Worker();

                newWorker.Name = model.Name;
                newWorker.Service = model.Service;
                newWorker.WorkerNo = model.WorkerNo;
                newWorker.Tel = model.Tel;
                newWorker.Mobile = model.Mobile;
                newWorker.RegisterDate = System.DateTime.Now;
                newWorker.Password = model.Password;
                newWorker.Email = model.Email;
                newWorker = _workerData.Add(newWorker);

                return RedirectToAction(nameof(Details_Worker), new { Id = newWorker.Id });

            }
            else
            {
                return View();
            }
        }

        public IActionResult Edit_Worker(int id)
        {

            var model = _workerData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);


        }
        [HttpPost]
        public IActionResult Edit_Worker(WorkerEditModel model, int id)
        {
            if (ModelState.IsValid)
            {



                var worker = new Worker();
                id = model.Id;
                worker.Id = id;

                worker.Service = model.Service;
                worker.Name = model.Name;
                worker.WorkerNo = model.WorkerNo;
                worker.Tel = model.Tel;
                worker.Mobile = model.Mobile;
                worker.RegisterDate = System.DateTime.Now;
                worker.Password = model.Password;
                //SqlConnection cn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ServiceApplicationDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
                //cn.Open();
                //SqlCommand cm = new SqlCommand("update Workers set Name='" + worker.Name.ToString() + "'" +
                //    ",Service=" + srvType.ToString() + "" +
                //    ",WorkerNo='" + worker.WorkerNo.ToString() + "'" +
                //    ",Tel='" + worker.Tel.ToString() + "'" +
                //    ",Mobile='" + worker.Mobile.ToString() + "'" +
                //    //",RegisterDate='" + worker.RegisterDate +"'"+
                //    ",Password='" + worker.Password.ToString() + "'" +
                //    " where Id=" + id
                //    , cn);
                //cm.ExecuteNonQuery();
                //cn.Close();


                _workerData.Update(id, worker);


                // _context.Model.FindEntityType(model).
                //return RedirectToAction(nameof(DetailsAfterEdit), new { Id = worker.Id });
                return RedirectToAction(nameof(Details_Worker), new { Id = id });

            }

            return View();

        }
        [HttpGet]
        public IActionResult Delete_Worker(int id)
        {
            var model = _workerData.Delete(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        public IActionResult Details_Customer(int id)
        {
            var model = _customerData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create_Customer()
        {
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Create_Customer(CustomerEditModel model)
        {
            //...
            if (ModelState.IsValid)
            {
                var newCustomer = new Customer();

                newCustomer.Name = model.Name;
                newCustomer.Address = model.Address;
                newCustomer.CustNo = model.CustNo;
                newCustomer.Tel = model.Tel;
                newCustomer.Mobile = model.Mobile;
                newCustomer.RegisterDate = System.DateTime.Now;
                newCustomer.Email = model.Email;
                newCustomer = _customerData.Add(newCustomer);

                return RedirectToAction(nameof(Details_Customer), new { Id = newCustomer.Id });

            }
            else
            {
                return View();
            }
        }

        public IActionResult Edit_Customer(int id)
        {

            var model = _customerData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);


        }
        [HttpPost]
        public IActionResult Edit_Customer(CustomerEditModel model, int id)
        {
            if (ModelState.IsValid)
            {

                var customer = new Customer();
                id = model.Id;
                customer.Id = id;

                customer.Address = model.Address;
                customer.Name = model.Name;
                customer.CustNo = model.CustNo;
                customer.Tel = model.Tel;
                customer.Mobile = model.Mobile;
                customer.RegisterDate = System.DateTime.Now;
                customer.Email = model.Email;
                //SqlConnection cn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ServiceApplicationDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
                //cn.Open();
                //SqlCommand cm = new SqlCommand("update Workers set Name='" + worker.Name.ToString() + "'" +
                //    ",Service=" + srvType.ToString() + "" +
                //    ",WorkerNo='" + worker.WorkerNo.ToString() + "'" +
                //    ",Tel='" + worker.Tel.ToString() + "'" +
                //    ",Mobile='" + worker.Mobile.ToString() + "'" +
                //    //",RegisterDate='" + worker.RegisterDate +"'"+
                //    ",Password='" + worker.Password.ToString() + "'" +
                //    " where Id=" + id
                //    , cn);
                //cm.ExecuteNonQuery();
                //cn.Close();


                _customerData.Update(id, customer);


                // _context.Model.FindEntityType(model).
                //return RedirectToAction(nameof(DetailsAfterEdit), new { Id = worker.Id });
                return RedirectToAction(nameof(Details_Customer), new { Id = id });

            }

            return View();

        }
        [HttpGet]
        public IActionResult Delete_Customer(int id)
        {
            var model = _customerData.Delete(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        public IActionResult Details_ServiceList(int id)
        {
            var model = _serviceListData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create_ServiceList()
        {
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Create_ServiceList(ServiceListEditModel model)
        {
            //...
            if (ModelState.IsValid)
            {
                var newServiceList = new ServiceList();

                newServiceList.CustID = model.CustID;
                newServiceList.WorkerID = model.WorkerID;
                newServiceList.ServiceTime = model.ServiceTime;
                newServiceList.Cost = model.Cost;
                // newServiceList.RegisterDate = model.RegisterDate;
                newServiceList.RegisterDate = System.DateTime.Now;

                newServiceList = _serviceListData.Add(newServiceList);

                return RedirectToAction(nameof(Details_ServiceList), new { Id = newServiceList.Id });

            }
            else
            {
                return View();
            }
        }

        public IActionResult Edit_ServiceList(int id)
        {

            var model = _serviceListData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);


        }
        [HttpPost]
        public IActionResult Edit_ServiceList(ServiceListEditModel model, int id)
        {
            if (ModelState.IsValid)
            {

                var servicelist = new ServiceList();
                id = model.Id;
                servicelist.Id = id;

                servicelist.CustID = model.CustID;
                servicelist.WorkerID = model.WorkerID;
                servicelist.ServiceTime = model.ServiceTime;
                servicelist.Cost = model.Cost;
                servicelist.RegisterDate = System.DateTime.Now;

                _serviceListData.Update(id, servicelist);


                // _context.Model.FindEntityType(model).
                //return RedirectToAction(nameof(DetailsAfterEdit), new { Id = worker.Id });
                return RedirectToAction(nameof(Details_ServiceList), new { Id = id });

            }

            return View();

        }
        [HttpGet]
        public IActionResult Delete_ServiceList(int id)
        {
            var model = _serviceListData.Delete(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }
    }
}
