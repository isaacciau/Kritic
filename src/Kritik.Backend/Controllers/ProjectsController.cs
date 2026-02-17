using Kritik.Backend.Services;
using Kritik.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kritik.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly EvaluationService _evaluationService;

    public ProjectsController(ProjectService projectService, EvaluationService evaluationService)
    {
        _projectService = projectService;
        _evaluationService = evaluationService;
    }

    [HttpGet("ranking")]
    public async Task<List<ProjectRankingDTO>> GetRanking()
    {
        var projects = await _projectService.GetAsync();
        var evaluations = await _evaluationService.GetAsync();

        var ranking = new List<ProjectRankingDTO>();

        foreach (var project in projects)
        {
            var projectEvals = evaluations.Where(e => e.ProjectId == project.Id).ToList();
            if (projectEvals.Any())
            {
                var avgScore = projectEvals.Average(e => e.Scores.Average);
                var integrity = 1.0; // Placeholder for now

                ranking.Add(new ProjectRankingDTO
                {
                    ProjectId = project.Id!,
                    TeamName = project.TeamName,
                    Category = project.Category,
                    AverageScore = Math.Round(avgScore, 1),
                    TotalVotes = projectEvals.Count,
                    IntegrityRate = integrity
                });
            }
        }

        return ranking.OrderByDescending(r => r.AverageScore).ThenByDescending(r => r.TotalVotes).ToList();
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
