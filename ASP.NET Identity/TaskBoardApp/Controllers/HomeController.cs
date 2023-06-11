using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;
using TaskBoardApp.Models.Home;

namespace TaskBoardApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly TaskBoardAppDbContext _data;

		public HomeController(TaskBoardAppDbContext data)
		{
			_data = data;
		}

		public async Task<IActionResult> Index()
		{
			var taskBoards = _data.Boards
				.Select(b => b.Name)
				.Distinct();

			var taskCount = new List<HomeBoardModel>();

			foreach (var boardName in taskBoards)
			{
				var tasksInBoard = _data.Tasks.Where(t => t.Board.Name == boardName).Count();

				taskCount.Add(new HomeBoardModel
				{
					BoardName = boardName,
					TasksCount = tasksInBoard
				});
			}

			var userTasksCount = -1;

			if (User.Identity.IsAuthenticated)
			{
				var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				userTasksCount = _data.Tasks.Where(t => t.OwnerId == currentUserId).Count();
			}

			var homemodel = new HomeViewModel()
			{
				AllTasksCount = _data.Tasks.Count(),
				BoardsWithTasksCount = taskCount,
				UserTasksCount = userTasksCount
			};

			return View(homemodel);
		}
	}
}