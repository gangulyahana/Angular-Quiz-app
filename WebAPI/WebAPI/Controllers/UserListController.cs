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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserListController : ApiController
    {
        private DataModel db = new DataModel();

        // GET: api/UserList
        public IQueryable<UserList> GetUserLists()
        {
            return db.UserLists;
        }

        // GET: api/UserList/5
        [ResponseType(typeof(UserList))]
        public IHttpActionResult GetUserList(string id)
        {
            UserList userList = db.UserLists.Find(id);
            if (userList == null)
            {
                return NotFound();
            }

            return Ok(userList);
        }

        // PUT: api/UserList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserList(string id, UserList userList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userList.NT_ID)
            {
                return BadRequest();
            }

            db.Entry(userList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserListExists(id))
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

        // POST: api/UserList
        [ResponseType(typeof(UserList))]
        public IHttpActionResult PostUserList(UserList userList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserLists.Add(userList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserListExists(userList.NT_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userList.NT_ID }, userList);
        }

        // DELETE: api/UserList/5
        [ResponseType(typeof(UserList))]
        public IHttpActionResult DeleteUserList(string id)
        {
            UserList userList = db.UserLists.Find(id);
            if (userList == null)
            {
                return NotFound();
            }

            db.UserLists.Remove(userList);
            db.SaveChanges();

            return Ok(userList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserListExists(string id)
        {
            return db.UserLists.Count(e => e.NT_ID == id) > 0;
        }
    }
}