using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskBoardApp.Data.Models
{
	public class Task
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(DataConstants.Task.TaskMaxTitle)]
		[MinLength(DataConstants.Task.TaskMinTitle)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(DataConstants.Task.TaskMaxDescription)]
		[MinLength(DataConstants.Task.TaskMinDescription)]
		public string Description { get; set; } = null!;

		public DateTime CreatedOn { get; set; }

		public int BoardId { get; set; }

		public Board? Board { get; set; }

		[Required] 
		public string OwnerId { get; set; } = null!;

		public IdentityUser Owner { get; set; } = null!;
	}
}
