﻿namespace SportsNet.Web.ViewModels.Category
{
	public class CategoryAllViewModel
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public int PostsCount { get; set; }
	}
}
