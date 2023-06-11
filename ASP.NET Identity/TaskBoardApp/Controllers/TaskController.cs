using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers
{
	[Authorize]
	public class TaskController : Controller
	{
		private readonly TaskBoardAppDbContext _data;

		public TaskController(TaskBoardAppDbContext data)
		{
			_data = data;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			TaskFormModel taskmodel = new TaskFormModel()
			{
				Boards = GetBoards()
			};

			return View(taskmodel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(TaskFormModel taskModel)
		{
			if (!GetBoards().Any(b => b.Id == taskModel.BoardId))
			{
				ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
			}

			string currentUserId = GetUserId();


			Task task = new Task()
			{
				Title = taskModel.Title,
				Description = taskModel.Description,
				CreatedOn = DateTime.Now,
				BoardId = taskModel.BoardId,
				OwnerId = currentUserId
			};

			await _data.Tasks.AddAsync(task);
			await _data.SaveChangesAsync();

			var boards = _data.Boards;

			return RedirectToAction("All", "Board");
		}

		public async Task<IActionResult> Details()
		{
			var task = await _data.Tasks
				.Select(t => new TaskDetailsViewModel()
				{
					Id = t.Id,
					Title = t.Title,
					Description = t.Description,
					Owner = t.Owner.UserName,
					Board = t.Board.Name,
					CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm")
				})
				.FirstOrDefaultAsync();

			if (task == null)
			{
				return BadRequest();
			}

			return View(task);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var task = await _data.Tasks.FindAsync(id);

			if (task == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();

			if (currentUserId != task.OwnerId)
			{
				return Unauthorized();
			}

			TaskFormModel model = new TaskFormModel
			{
				Title = task.Title,
				Description = task.Description,
				BoardId = task.BoardId,
				Boards = GetBoards()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(TaskFormModel model, int id)
		{
			var task = await _data.Tasks.FindAsync(id);

			if (task == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();

			if (currentUserId != task.OwnerId)
			{
				return Unauthorized();
			}

			if (!GetBoards().Any(b => b.Id == model.BoardId))
			{
				ModelState.AddModelError(nameof(model.BoardId), "Board does not exist.");
			}


			task.Title = model.Title;
			task.Description = model.Description;
			task.BoardId = model.BoardId;

			await _data.SaveChangesAsync();

			return RedirectToAction("All", "Board");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var task = await _data.Tasks.FindAsync(id);

			if (task == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();

			if (currentUserId != task.OwnerId)
			{
				return Unauthorized();
			}

			TaskViewModel model = new TaskViewModel()
			{
				Id = task.Id,
				Title = task.Title,
				Description = task.Description,
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(TaskViewModel model)
		{
			var task = await _data.Tasks.FindAsync(model.Id);

			if (task == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();

			if (currentUserId != task.OwnerId)
			{
				return Unauthorized();
			}

			_data.Tasks.Remove(task);
			await _data.SaveChangesAsync();

			return RedirectToAction("All", "Board");
		}

		private IEnumerable<TaskBoardModel> GetBoards()
		=> _data.Boards.Select(b => new TaskBoardModel()
		{
			Id = b.Id,
			Name = b.Name
		});

		private string GetUserId()
			=> User.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}
