namespace SportsNet.Services.Data.Interfaces
{
    using SportsNet.Data.Models.Enums;

    public interface IPostService
    {
        IEnumerable<PostType> GetPostTypes();
    }
}
