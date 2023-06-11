using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Models
{
	public class Board
	{
		public Board()
		{
			Tasks = new HashSet<Task>();
		}

		[Key]
		public int Id { get; init; }

		[Required]
		[MaxLength(DataConstants.Board.BoardMaxName)]
		[MinLength(DataConstants.Board.BoardMinName)]
		public string Name { get; init; } = null!;

		public IEnumerable<Task> Tasks { get; set; }
	}
}
