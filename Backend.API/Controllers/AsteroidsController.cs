﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsteroidsController : ControllerBase
    {
        private readonly ILogger<AsteroidsController> _logger;

        public AsteroidsController(ILogger<AsteroidsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(string planet)
        {
            string ret = string.Empty;

            try
            {

            }
            catch (Exception exc)
            {
                ret = $"Internal Server error: {exc.Message}.";
                _logger.LogCritical($"Internal Server error: {exc.Message}.", planet);

                if (exc.InnerException != null)
                {
                    _logger.LogCritical($"Internal Server error: {exc.InnerException.Message}.", planet);
                }
            }

            //The result is returned
            return ret;
        }
    }
}
