using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Models.Task
{
	public class TaskFormModel
	{
		[Required]
		[StringLength(Data.DataConstants.Task.TaskMaxTitle, MinimumLength = Data.DataConstants.Task.TaskMinTitle,
			ErrorMessage = "Title should be at least {2} characters long.")]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(Data.DataConstants.Task.TaskMaxDescription,MinimumLength = Data.DataConstants.Task.TaskMinDescription,
			ErrorMessage = "Description should be at least {2} characters long.")]
		public string Description { get; set; } = null!;

		[Display(Name = "Board")]
		public int BoardId { get; set; }

		public IEnumerable<TaskBoardModel> Boards { get; set; } = null!;
	}
}
