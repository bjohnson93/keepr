using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly AccountService _accountService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;
  public ProfilesController(Auth0Provider auth0Provider, AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
  {
    _auth0Provider = auth0Provider;
    _accountService = accountService;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetUsersProfile(string profileId)
  {
    try
    {
      Profile profile = _accountService.GetUsersProfile(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetUsersKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetUsersVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetUsersVaults(profileId, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> Edit([FromBody] Account accountData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Account account = _accountService.Edit(accountData, userInfo.Email);
      return Ok(account);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
