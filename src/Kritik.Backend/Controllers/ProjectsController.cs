using Kritik.Backend.Services;
using Kritik.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kritik.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectsController(ProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<List<Project>> Get([FromQuery] string? search, [FromQuery] string? category, [FromQuery] string? technology) =>
        await _projectService.GetAsync(search, category, technology);

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Project>> Get(string id)
    {
        var project = await _projectService.GetAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Project newProject)
    {
        await _projectService.CreateAsync(newProject);

        return CreatedAtAction(nameof(Get), new { id = newProject.Id }, newProject);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Project updatedProject)
    {
        var project = await _projectService.GetAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        updatedProject.Id = project.Id;

        await _projectService.UpdateAsync(id, updatedProject);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var project = await _projectService.GetAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        await _projectService.RemoveAsync(id);

        return NoContent();
    }
}
