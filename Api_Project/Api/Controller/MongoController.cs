using Api_Project.Models;
using Api_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MongoController : ControllerBase
{
    private readonly CheckService _checksService;

    public MongoController(CheckService checksService) =>
        _checksService = checksService;

    [HttpGet]
    public async Task<List<CheckCollection>> Get() =>
        await _checksService.GetAsync();

    [HttpGet("{checkId}")] 
    public async Task<ActionResult<CheckCollection>> Get(int checkId)
    {
        var check = await _checksService.GetAsync(checkId);

        if (check is null)
        {
            return NotFound();
        }

        return check;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CheckCollection newCheck)
    {
        await _checksService.CreateAsync(newCheck);

        return CreatedAtAction(nameof(Get), new { id = newCheck.checkId }, newCheck);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CheckCollection updatedCheck)
    {
        var check = await _checksService.GetAsync(id);

        if (check is null)
        {
            return NotFound();
        }

        updatedCheck.checkId = check.checkId;

        await _checksService.UpdateAsync(id, updatedCheck);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var check = await _checksService.GetAsync(id); 

        if (check is null)
        {
            return NotFound();
        }

        await _checksService.RemoveAsync(id);

        return NoContent();
    }
}