using JobTracker.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JobTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private readonly JobContext _context;

    public JobsController(JobContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var jobs = await _context.Jobs.ToListAsync();

        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if(job == null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Job job)
    {
        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = job.Id }, job);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Job job)
    {
        if(id != job.Id)
        {
            return BadRequest();
        }

        _context.Entry(job).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if(job == null)
        {
            return NotFound();
        }

        _context.Jobs.Remove(job);

        await _context.SaveChangesAsync();

        return NoContent();


    }
    





}