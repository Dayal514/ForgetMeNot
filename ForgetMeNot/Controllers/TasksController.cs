using ForgetMeNot.Data;
using ForgetMeNot.Models;
using ForgetMeNot.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForgetMeNot.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id!);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks([FromQuery] bool onlyOpen = false)
        {
            var userId = GetUserId();

            var query = _context.Tasks.AsNoTracking().Where(t => t.UserId == userId);

            if (onlyOpen)
            {
                query = query.Where(t => !t.IsDone);
            }

            var tasks = await query.OrderBy(t => t.IsDone)
                    .ThenBy(t => t.DueDateUtc ?? DateTime.MaxValue)
                    .Select(t => new TaskResponseDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Notes = t.Notes,
                        DueDateUtc = t.DueDateUtc,
                        IsDone = t.IsDone,
                        CreatedUtc = t.CreatedUtc,
                        CompletedUtc = t.CompletedUtc
                    }).ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> GetTask(int id)
        {
            var userId = GetUserId();

            var task = await _context.Tasks.AsNoTracking()
                    .Where(t => t.UserId == userId && t.Id == id)
                    .Select(t => new TaskResponseDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Notes = t.Notes,
                        DueDateUtc = t.DueDateUtc,
                        IsDone = t.IsDone,
                        CreatedUtc = t.CreatedUtc,
                        CompletedUtc = t.CompletedUtc
                    }).FirstOrDefaultAsync();

            if (task is null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto dto)
        {
            var userId = GetUserId();

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest("Task Title is requiered.");
            }

            var dbTask = new TaskItem
            {
                Title = dto.Title,
                Notes = dto.Notes,
                DueDateUtc = dto.DueDateUtc,
                IsDone = false,
                UserId = userId,
                CreatedUtc = DateTime.UtcNow
            };

            _context.Tasks.Add(dbTask);
            await _context.SaveChangesAsync();

            var response = new TaskResponseDto
            {
                Id = dbTask.Id,
                Title = dbTask.Title,
                Notes = dbTask.Notes,
                DueDateUtc = dbTask.DueDateUtc,
                IsDone = dbTask.IsDone,
                CreatedUtc = dbTask.CreatedUtc,
                CompletedUtc = dbTask.CompletedUtc
            };

            return CreatedAtAction(nameof(GetTask), new { id = dbTask.Id }, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var userId = GetUserId();

            var dbTask = await _context.Tasks.SingleOrDefaultAsync(t => t.UserId == userId && t.Id == id);
            if (dbTask is null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest("Task Title is requiered.");
            }

            dbTask.Title = dto.Title;
            dbTask.Notes = dto.Notes;
            dbTask.DueDateUtc = dto.DueDateUtc;

            if (!dbTask.IsDone && dto.IsDone)
            {
                dbTask.CompletedUtc = DateTime.UtcNow;
            } 
            else
            {
                dbTask.CompletedUtc = null;
            }

            dbTask.IsDone = dto.IsDone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = GetUserId();

            var dbTask = await _context.Tasks.SingleOrDefaultAsync(t => t.UserId == userId && t.Id == id);
            if (dbTask is null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(dbTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
