using System;
using AwaraIt.Hackathon.Models;

namespace AwaraIt.Hackathon.WebSite.Models
{
	public class CommentVM
	{
		public CommentVM()
		{
		}

		public CommentVM(Comment comment)
		{
			Id = comment?.Id ?? default;
			Text = comment?.Message;
			Date = comment?.DateOfCreation ?? default;
		}

		public Guid Id { get; set; }

		public string Text { get; set; }

		public DateTime Date { get; set; }
	}
}

