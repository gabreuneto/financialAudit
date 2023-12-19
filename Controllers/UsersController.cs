using System.Data;
using System.Diagnostics;
using dbUsers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private DataTable _userDataTable;
    

    public UserController(IUserService userService)
    {
        _userService = userService;
        UserData userData = new UserData();
        _userDataTable = userData.CreateUserDataTable();
    }

    [HttpPost("create")]
    public ActionResult CreateUser([FromBody] User newUser)
    {
        try
        {
            _userService.CreateUser(newUser);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<Dictionary<string, object>>> GetTBLUserValues()
    {
        var tblUserValues = _userService.GetAllUsers();
        return Ok(tblUserValues);
    }

    [HttpGet("{userId}")]
    public ActionResult<User> GetUser(string userId)
    {
        var user = _userService.GetUserByIdOrName(userId);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId, [FromBody] User updatedUser)
    {
        try
        {
            _userService.UpdateUser(userId, updatedUser);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userId}")]
    public ActionResult DeleteUser(int userId)
    {
        try
        {
            _userService.DeleteUser(userId);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
