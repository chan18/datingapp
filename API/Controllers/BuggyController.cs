using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly DataContext context;
    public BuggyController(DataContext context) 
    {
        this.context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "secret";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        if (context.Users?.Find(-1) is { } thing)
        {
            return thing;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        if(context.Users?.Find(-1) is { } user)
        {
            return "found";
        }
        else
        {
            throw new NullReferenceException();
        }
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
}
