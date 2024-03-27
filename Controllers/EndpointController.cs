using Microsoft.AspNetCore.Mvc;
using WebCheckerAPI.EntityFrameworkStuff;
using Microsoft.EntityFrameworkCore;
using WebCheckerAPI.Models;

namespace WebCheckerAPI.Controllers;

[ApiController]
[Route("WebChekerApi/[controller]")]
public class EndpointController : ControllerBase
{
	private readonly ILogger<EndpointController> _logger;
	private readonly RequestDbContext _db;

	public EndpointController(RequestDbContext db, ILogger<EndpointController> logger)
	{
		_db = db;
		_logger = logger;
	}

	// POST -> /endpoint
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] EndpointModel model)
	{
		if (model == null)
		{
			return BadRequest("The model is null");
		}

		if (string.IsNullOrEmpty(model.Url))
		{
			return BadRequest("The Url is null or empty");
		}

		var hasEndpoint = await _db.Endpoints.AnyAsync(x => x.Url == model.Url);
		if (hasEndpoint)
		{
			return BadRequest("The Url is already exists");
		}

		try
		{
			var newEndpint = new WebCheckerAPI.Models.Endpoint
			{
				EndpointId = Guid.NewGuid(),
				Url = model.Url,
				Time = model.Time,
				LastRequestDate = DateTime.UtcNow
			};

			await _db.Endpoints.AddAsync(newEndpint);

			var result = await _db.SaveChangesAsync();
			if (result == 0)
			{
				return BadRequest("An error occurred while adding the endpoint");
			}

			return Ok("The endpoint added successfully");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while adding the endpoint");
		}

		return BadRequest("An error occurred while adding the endpoint");
	}
	[HttpDelete("url")]
	public async Task<IActionResult> Delete(string url)
	{
		var CommandToDelete = await _db.Endpoints.FirstAsync(x => x.Url == url);
		_db.Endpoints.Remove(CommandToDelete);
		_db.SaveChanges();
		return Ok($"The command to check the {url} has stoped");
	}

	// GET -> /endpoint
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var endpoints = await _db.Endpoints.ToListAsync();
		return Ok(endpoints);
	}

}
