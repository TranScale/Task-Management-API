using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTOs.ProjectObj;
using TaskManagementAPI.Services.Implement;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponseDTO>>> Get()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailDTO>> Get(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponseDTO>> Post([FromBody] ProjectCreateDTO projectCreateDTO)
        {
            var createdProject = await _projectService.CreateProjectAsync(projectCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectResponseDTO>> Put(int id, [FromBody] ProjectUpdateDTO projectUpdateDTO)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(id, projectUpdateDTO);
            if (updatedProject == null)
                return NotFound();
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result) 
                return NotFound();
            return NoContent();
        }

    }
}
