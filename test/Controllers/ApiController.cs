using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using test.Exceptions;
using test.Facades;
using test.Models;

namespace test.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IOverallFacade _overallFacade;

        public ApiController(IOverallFacade overallFacade)
        {
            _overallFacade = overallFacade ?? throw new ArgumentNullException(nameof(overallFacade));
        }

        [HttpPost("Account")]
        public IActionResult CreateAccount(AddAccountModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var errors = _overallFacade.ValidateAccount(model);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.TryAddModelError(error.Key, error.Value);
                    }

                    return BadRequest(ModelState);
                }

                _overallFacade.AddAccount(model);
                return Ok();
            }
            catch (AccountExist)
            {
                return BadRequest("Already exist");
            }
            catch (ContactNotFound)
            {
                return BadRequest("Contact with such mail not found");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost("Contact")]
        public IActionResult CreateContact(AddContactModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var errors = _overallFacade.ValidateContact(model);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.TryAddModelError(error.Key, error.Value);
                    }

                    return BadRequest(ModelState);
                }

                _overallFacade.AddContact(model);
                return Ok();
            }
            catch (ContactExist)
            {
                return BadRequest("Already exist");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost("Incident")]
        public IActionResult CreateIncident(AddIncidentModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var errors = _overallFacade.ValidateIncident(model);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.TryAddModelError(error.Key, error.Value);
                    }

                    return BadRequest(ModelState);
                }

                _overallFacade.AddIncident(model);
                return Ok();
            }
            catch (AccountNotFound)
            {
                return NotFound("Account with such name not found");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
    }
}
