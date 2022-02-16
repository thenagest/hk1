using System;
using System.Linq;
using System.Collections.Generic;
using AwaraIt.Hackathon.Models;

namespace AwaraIt.Hackathon.WebSite.Models
{
	public class ImageVM
	{
		public ImageVM()
		{
		}

		public ImageVM(Image image, string userId)
        {
			Id = image?.Id ?? default;
			Liked = image?.Likes?.
				Any(x => x.UserId == userId && !x.Deleted) ?? false;
			Url = image?.Url;
			LikeNumber = image?.Likes?.Count ?? 0;
			Date = image?.DateOfCreation ?? default;

			Comments = image?.Comments?.
				Where(x => !x.Deleted)?.
				OrderBy(x => x.DateOfCreation)?.
				ToList()?.
				Select(x => new CommentVM(x))?.
				ToList()
				?? new List<CommentVM>();
			CommentNumber = Comments.Count;
        }

		public Guid Id { get; set; }

		public int CommentNumber { get; set; }

		public int LikeNumber { get; set; }

		public bool Liked { get; set; }

		public string Url { get; set; }

		public DateTime Date { get; set; }

		public List<CommentVM> Comments { get; set; }
	}
}

