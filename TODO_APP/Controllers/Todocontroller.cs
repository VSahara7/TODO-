using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using TODO_APP.Data;
using TODO_APP.Models;

namespace TODO_APP.Controllers
{
    
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TodoController(ApplicationDbContext db)
        {
            _db = db;

        }
       [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Todo> objList = _db.Todos;
         
            return View(objList);

        }
        [HttpGet]    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Todo obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Todos.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
      

            return View(obj);
        }
     
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var TaskFromDb = _db.Todos.Find(id);
            if (TaskFromDb == null)
            {
                return NotFound();
            }
            return View(TaskFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(Todo obj)
        {
            /* var obj = _db.Books.Find(obj);
             if (obj == null)
             {
                 return NotFound();
             }*/
            _db.Todos.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var TaskFromDb = _db.Todos.Find(id);
            if (TaskFromDb == null)
            {
                return NotFound();
            }
            return View(TaskFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Todo obj)
        {
            /* var obj = _db.Books.Find(obj);
             if (obj == null)
             {
                 return NotFound();
             }*/
            _db.Todos.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET Login
        [HttpGet("login")]
        public ActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //POST-Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(User obj, string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = "/";
            }
            var row = _db.Users.Where(model => model.UserMail == obj.UserMail && model.UserPassword == obj.UserPassword).FirstOrDefault();
            if (row != null)
            {




                string userName = obj.UserMail;
                var claims = new List<Claim>();
                claims.Add(new Claim("username", userName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
               
                return Redirect(returnUrl);
        


            }
            TempData["Error"] = "Error. Username or Password is invalid";
            return View("Login");
        }



        /* [Authorize]*/
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");



        }
        
    }
}
