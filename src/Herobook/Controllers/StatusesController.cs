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
    public class StatusesController
    {
        private readonly IDatabase db;

        public StatusesController(IDatabase db)
        {
            this.db = db;
        }

        [Route("api/profiles/{username}/statuses")]
        [HttpGet]
        public object GetProfileStatuses(string username)
        {
            return db.LoadStatuses(username).Select(s => s.ToResource());
        }

        [Route("api/profiles/{username}/statuses")]
        [HttpPost]
        public object PostProfileStatus(string username, [FromBody]Status status)
        {
            status.Username = username;
            status.PostedAt = DateTimeOffset.Now;
            return db.CreateStatus(status).ToResource();
        }

        [Route("api/profiles/{username}/statuses/{statusId}")]
        [HttpGet]
        public object GetProfileStatus(string username, Guid statusGuid)
        {
            return db.LoadStatus(statusGuid).ToResource();
        }

        [Route("api/profiles/{username}/statuses/{statusId}")]
        [HttpPut]
        public object UpdateProfileStatus(string username, Guid statusId, [FromBody] Status status)
        {
            return db.UpdateStatus(statusId, status);
        }
    }
}