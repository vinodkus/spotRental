﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMT.SpotRental.API.Controllers
{
    public class BaseController : ApiController
    {
        public bool VerifyUser()
        {
            return true;
        }
    }
}
