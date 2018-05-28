using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Herobook.Workshop.Data;
using Herobook.Workshop.Data.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Herobook.Workshop.Hypermedia;
using System.Dynamic;

namespace Herobook.Controllers.Api
{

    public class ProfilesController : Controller
    {

        private IDatabase db;
        private PhotosController photosController;

        public ProfilesController(IDatabase database, PhotosController pc)
        {
            this.db = database;
            this.photosController = pc;
        }

        [Route("api/profiles/")]
        [HttpGet]
        public object GetProfiles(int index = 0, int count = 10)
        {
            var _links = Hal.Paginate(Request.Path, index, count, db.CountProfiles());
            var items = db.ListProfiles().Skip(index).Take(count).Select(profile => profile.ToResource()); ;
            var result = new
            {
                _links,
                items
            };
            return Ok(result);
        }

        [Route("api/profiles/{username}")]
        [HttpGet]
        public object GetProfile(string username, string expand = null)
        {
            var resource = db.FindProfile(username).ToResource();
            if (resource != null && !string.IsNullOrEmpty(expand)) {
                dynamic embedded = new ExpandoObject();
                if (expand.Contains("friends")) embedded.friends = GetProfileFriends(username);
                if (expand.Contains("photos")) embedded.photos = photosController.GetProfilePhotos(username);
                resource._embedded = embedded;
            }
            return (object)resource ?? NotFound();
        }

        [Route("api/profiles/{username}/friends")]
        [HttpGet]
        public object GetProfileFriends(string username)
        {
            return db.LoadFriends(username);
        }

        [Route("api/profiles/")]
        [HttpPost]
        public object Post([FromBody] Profile profile)
        {
            var existing = db.FindProfile(profile.Username);
            if (existing != null) return StatusCode(StatusCodes.Status409Conflict, "That username is not available");
            db.CreateProfile(profile);
            return Created(Url.Content($"~/api/profiles/{profile.Username}"), profile.ToResource());
        }

        [Route("api/profiles/{username}")]
        [HttpPut]
        public object Put(string username, [FromBody] Profile profile)
        {
            var result = db.UpdateProfile(username, profile);
            return result.ToResource();
        }

        [Route("api/profiles/{username}")]
        [HttpDelete]
        public object Delete(string username)
        {
            db.DeleteProfile(username);
            return Ok();
        }
    }
}