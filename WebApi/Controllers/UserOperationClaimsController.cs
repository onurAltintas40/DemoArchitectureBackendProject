using Business.Repositories.UserOperationClaimRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationService)
        {
            _userOperationService = userOperationService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserOperationClaim userOperationClaim)
        {
            var result =await _userOperationService.Add(userOperationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UserOperationClaim userOperationClaim)
        {
            var result = await _userOperationService.Update(userOperationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(UserOperationClaim userOperationClaim)
        {
            var result = await _userOperationService.Delete(userOperationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            var result = await _userOperationService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userOperationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
