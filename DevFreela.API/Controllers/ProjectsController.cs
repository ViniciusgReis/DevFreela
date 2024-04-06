using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //api/projects?query= net core    
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetAll(string query)
        {
            var projectsQuery = new GetAllProjectsQuery(query);
            var projects = await _mediator.Send(projectsQuery);
            return Ok(projects);
        }

        //api/projects/1
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var command = new GetProjectByIdQuery(id);
            if (command == null)
            {
                return NotFound();
            }
            var projects = await _mediator.Send(command);
            return Ok(projects);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post(CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);
            //var id = _projectService.Create(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);
            
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, FinishProjectCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("O pagamento não pôde ser processado.");
            }

            return Accepted();
        }
    }
}
