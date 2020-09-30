using WebRole1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebRole1.Controllers
{
   
    public class TalentController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();

        [Authorize]
        [HttpGet]
        [Route("api/v1/GetAllTalents")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAllTalents();
        }
    }
}
