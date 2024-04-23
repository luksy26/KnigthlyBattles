using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PlayerController : AuthorizedController
{
    private readonly IPlayerService _playerService;
    public PlayerController(IUserService userService, IPlayerService playerService) : base(userService)
    {
        _playerService = playerService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<int>>> GetCount()
    {
        return this.FromServiceResponse(await _playerService.GetPlayerCount());
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<PlayerDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _playerService.GetPlayer(id)) :
            this.ErrorMessageResult<PlayerDTO>(currentUser.Error);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<PlayerDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _playerService.GetPlayers(pagination)) :
            this.ErrorMessageResult<PagedResponse<PlayerDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] PlayerAddDTO player)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse(await _playerService.AddPlayer(player, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] PlayerUpdateDTO player)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _playerService.UpdatePlayer(player, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _playerService.DeletePlayer(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
