using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [Authorize]
public class UsersController : BaseApiController
{
    private readonly DataContext dataContext;
    //snipper: ctor
    //inject DataContext
    public UsersController(DataContext dataContext)
    {
        //putting cursor inside dataContext (ctor parameter) `ctrl+.` then select `create and assign feild`
        this.dataContext = dataContext;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return await dataContext.Users.ToListAsync();
    }

    [HttpGet("{id}")] //endpoint: /api/users/25         ,when id = 25
    public async Task<ActionResult<AppUser?>> GetUser(int id)
    {
        return await dataContext.Users.FindAsync(id);
    }

}