namespace SportsNet.Web.ViewModels.Image
{
    using SportsNet.Data.Models;
    using SportsNet.Services.Mapping;

    public class ImageAllViewModel : IMapFrom<Image>
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public ApplicationUser? Author { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
