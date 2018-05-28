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

namespace Herobook.Controllers.Api
{
    public class PhotosController : Controller
    {
        private readonly IDatabase db;

        public PhotosController(IDatabase db)
        {
            this.db = db;
        }

        [Route("api/profiles/{username}/photos")]
        [HttpGet]
        public object GetProfilePhotos(string username)
        {
            var items = db.LoadPhotos(username).Select(s => s.ToResource());
            var result = new
            {
                items
            };
            return result;
        }


        [Route("api/profiles/{username}/photos")]
        [HttpPost]
        public object PostProfilePhoto(string username, [FromBody] Photo photo)
        {
            photo.Username = username;
            photo.PostedAt = DateTimeOffset.Now;
            return db.CreatePhoto(photo).ToResource();
        }

        [Route("api/profiles/{username}/photos/{photoId}")]
        [HttpGet]
        public object GetProfilePhoto(string username, Guid photoId)
        {
            var photo = db.LoadPhoto(photoId);
            return photo.ToResource();
        }

        [Route("api/profiles/{username}/photos/{photoId}")]
        [HttpPut]
        public object UpdateProfilePhoto(string username, Guid photoId, [FromBody] Photo photo)
        {
            return db.UpdatePhoto(photoId, photo);
        }

        [Route("api/profiles/{username}/photos/{photoId}")]
        [HttpDelete]
        public void DeleteProfilePhoto(string username, Guid photoId)
        {
            db.DeletePhoto(photoId);
        }
    }
}