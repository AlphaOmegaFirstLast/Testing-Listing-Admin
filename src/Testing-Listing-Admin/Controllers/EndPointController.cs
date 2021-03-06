using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Caching.Memory;

namespace Testing_Listing_Admin.Controllers
{
    [Route("api/[controller]")]
    public class EndPointController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private ConcurrentDictionary<string, string> myList;
        public EndPointController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // GET: api/values
        [HttpGet]
        public ConcurrentDictionary<string, string> Get()
        {
            _memoryCache.TryGetValue("list",out myList);
            return myList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            return DateTime.Now.ToString("HHmmssfff");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]SubscriberNotification value)
        {
            _memoryCache.TryGetValue("list", out myList);
            if (myList == null)
            {
                myList = new ConcurrentDictionary<string, string>();
            }
            myList.GetOrAdd(value.ProviderAdId, value.Timestamp);
            _memoryCache.Set("list", myList);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete()
        {
            _memoryCache.TryGetValue("list", out myList);
            if (myList == null)
            {
                myList = new ConcurrentDictionary<string, string>();
            }
            myList.Clear();
            _memoryCache.Set("list", myList);
        }

        public class SubscriberNotification
        {
            public string Timestamp { get; set; }
            public int DomainAgencyId { get; set; }
            public string ProviderAdId { get; set; }
            public long DomainAdId { get; set; }
            public string ProcessStatus { get; set; }
            public IEnumerable<string> Errors { get; set; }
        }

    }
}