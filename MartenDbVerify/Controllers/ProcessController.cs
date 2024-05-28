using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace MartenDbVerify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController(ProcessCommandHandler processCommandHandler) : ControllerBase
    {
        private readonly ProcessCommandHandler _handler = processCommandHandler;

        [HttpPost("step1")]
        public async Task<IActionResult> CompleteStep1([FromBody] CompleteStep1Command command)
        {
            await _handler.Handle(command);
            return Ok();
        }

        [HttpPost("step2")]
        public async Task<IActionResult> CompleteStep2([FromBody] CompleteStep2Command command)
        {
            await _handler.Handle(command);
            return Ok();
        }

        [HttpPost("step3")]
        public async Task<IActionResult> CompleteStep3([FromBody] CompleteStep3Command command)
        {
            await _handler.Handle(command);
            return Ok();
        }

        [HttpPost("step4")]
        public async Task<IActionResult> CompleteStep4([FromBody] CompleteStep4Command command)
        {
            await _handler.Handle(command);
            return Ok();
        }
    }
}
