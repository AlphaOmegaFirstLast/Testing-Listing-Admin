using System;
using System.Collections.Concurrent;
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

    }
}