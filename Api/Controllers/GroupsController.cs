using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Api.Models;
using Api.ViewModel;

namespace Api.Controllers
{
    public class GroupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Groups
        [HttpGet]
        [Route("Api/GetGroups")]
        public List<Group> GetGroups()
        {
            return db.Groups.ToList();
        }

        // GET: api/Groups/5
        [HttpGet]
        [Route("Api/GetGroupById/{id}")]
        public IHttpActionResult GetGroupById(int id)
        {
            var group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/Groups/5
        [HttpPut]
        [Route("Api/UpdateGroup/{id}")]
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutGroup(int id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.GroupId)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [HttpPost]
        [Route("Api/AddGroup")]
        public IHttpActionResult PostGroup(GroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = new Group();
            model.GroupName = group.GroupName;
            model.Educationallevel = group.Educationallevel;
            model.Term = group.Term;
            model.FirstDate = group.FirstDate;
            model.SecondDate = group.SecondDate;
            model.ThirdDate = group.ThirdDate;
            model.FourthDate = group.FourthDate;
            model.UserId = group.UserId;
            db.Groups.Add(model);
            db.SaveChanges();

            return Ok(model);
        }

        // DELETE: api/Groups/5
        [HttpDelete]
        [Route("Api/DeleteGroup/{id}")]
        public IHttpActionResult DeleteGroup(int id)
        {
            var group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}