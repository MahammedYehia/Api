using Api.Models;
using Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class UsersController : ApiController
    {
         private ApplicationDbContext db = new ApplicationDbContext();

        //GET: api/GetAllUsers
       [HttpGet]
       [Route("Api/GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        [HttpGet]
        [Route("Api/GetUsersByType")]
        public List<User> GetUsersByType(string type)
        {
            return db.Users.Where(x => x.UserType == type).ToList();
        }
        // GET: api/GetUserById
        [HttpGet]
        [Route("Api/GetUserById")]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPut]
        [Route("Api/UpdateUser/id")]

        public IHttpActionResult UpdateUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        // DELETE: api/Users1/5
        [HttpDelete]
        [Route("Api/DeleteUser/Id")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }


        // PUT: api/Users/5
        [Route("Api/Login")]
        public IHttpActionResult Login(UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (user == null)
            {
                return NotFound();
            }
            user.Password = Encrypt(user.Password);
            var ExistModel = db.Users.FirstOrDefault(x => x.Phone == user.Phone && x.Password == user.Password && x.UserType == user.UserType);
            if (ExistModel == null)

            {
                ModelState.AddModelError(nameof(user.Phone), "INCORRECT Phone Or Password");
                return BadRequest(ModelState);
            }
            ExistModel.Password = null;
            ExistModel.ConfirmPassword = null;
            ExistModel.Groups = null;
            return Ok(ExistModel);
        }

        [HttpPost]
        [Route("Api/Register")]
        public IHttpActionResult Register(RegisterViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Exist = db.Users.Any(x => x.Phone == userViewModel.Phone &&x.UserType==userViewModel.UserType);
         
            if (!Exist)
            {
                   var user = new User();
                    user.UserName = userViewModel.UserName;
                    user.Password = Encrypt(userViewModel.Password);
                    user.ConfirmPassword = Encrypt(userViewModel.ConfirmPassword);
                    user.Phone = userViewModel.Phone;
                    user.UserType = userViewModel.UserType;
                    if (userViewModel.UserType == "Student")
                    {
                        user.StudentCode = GetStudentCodeAutomatic();
                    }
                db.Users.Add(user);
                db.SaveChanges();
                user.ConfirmPassword = null;
                user.Password = null;
                return Ok(user);
            }
            else
            {
                ModelState.AddModelError(nameof(userViewModel.Phone), "The Phone Number Is Exit Before!");
                return BadRequest(ModelState);
            }
            
           

        }

        private string GetStudentCodeAutomatic()
        {
            var code = "100";
           var MaxCode= db.Users.Where(x => x.UserType == "Student").Max(x => x.StudentCode);
            if (!string.IsNullOrEmpty(MaxCode))
            {
                return (int.Parse(MaxCode)+1).ToString();
            }
            return code;
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }

        private string Encrypt(string confirmPassword)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(confirmPassword);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        private string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

       
    }
}
