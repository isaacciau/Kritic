using Kritik.Backend.Services;
using Kritik.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kritik.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly UserService _userService; // Injected

    public SeedController(ProjectService projectService, UserService userService)
    {
        _projectService = projectService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Seed()
    {
        var existing = await _projectService.GetAsync();
        if (existing.Count > 0)
        {
            return Ok(new { message = "Database already has data." });
        }

        var projects = new List<Project>
        {
            new()
            {
                TeamName = "Equipo Alpha",
                Category = "IoT",
                Technologies = new() { "Arduino", "C#", "MQTT" },
                Description = "Sistema de riego automatizado con monitoreo remoto.",
                Members = new() { "Juan Pérez", "Maria Lopez" },
                FairLocation = "Stand A-01",
                Videos = new() 
                { 
                    new Video { Title = "Demo Funcionamiento", Url = "https://youtube.com/watch?v=demo1", Description = "Demostración en vivo del sistema." } 
                },
                Documents = new()
                {
                    new Document { Title = "Manual de Usuario", Url = "https://kritik.com/docs/manual.pdf", Type = "PDF" }
                }
            },
            new()
            {
                TeamName = "CodeCrafters",
                Category = "Web",
                Technologies = new() { "React", "Node.js", "MongoDB" },
                Description = "Plataforma de gestión escolar integral.",
                Members = new() { "Carlos Ruiz", "Sofia Dia" },
                FairLocation = "Stand B-05",
                 Videos = new() 
                { 
                    new Video { Title = "Pitch Deck", Url = "https://youtube.com/watch?v=pitch", Description = "Presentación de negocio." } 
                },
                Documents = new()
                {
                    new Document { Title = "Arquitectura", Url = "https://kritik.com/docs/arch.pdf", Type = "PDF" }
                }
            },
             new()
            {
                TeamName = "Innovators",
                Category = "AI",
                Technologies = new() { "Python", "TensorFlow", "FastAPI" },
                Description = "Detección de objetos en tiempo real para seguridad.",
                Members = new() { "Luis Torres", "Ana Silva" },
                FairLocation = "Stand C-10"
            }
        };

        foreach (var p in projects)
        {
            await _projectService.CreateAsync(p);
        }

        // Seed Default User
        var defaultUser = new User
        {
            Username = "evaluador",
            Password = "password123",
            FullName = "Evaluador Demo",
            Role = "Evaluator"
        };
        
        var existingUser = await _userService.GetByUsernameAsync(defaultUser.Username);
        if (existingUser == null)
        {
            await _userService.CreateAsync(defaultUser);
        }
        
        return Ok(new { message = "Seeded projects and default user." });
    }
}
