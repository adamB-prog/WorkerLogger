using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerLogger.Application.Data;
using WorkerLogger.Application.WorkInformation.Create;
using WorkerLogger.Application.WorkInformation.Get;
using WorkerLogger.Application.WorkInformation.Update;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddWorkInformation([FromBody] CreateWorkInformationCommand command, ISender sender)
        {
            try
            {
                await sender.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetUserWorkInformations(string id, ISender sender)
        {

            var query = new GetWorkInformationsQuery(id);
            var result = await sender.Send(query);

            

            return Ok(result);
        }

        [HttpPatch]
        //[Authorize]
        public async Task<IActionResult> UpdateWorkInformation([FromBody] UpdateWorkInformationCommand command, ISender sender)
        {
            try
            {
                await sender.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
