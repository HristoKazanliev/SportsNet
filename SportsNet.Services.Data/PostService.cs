namespace SportsNet.Services.Data
{
    using Interfaces;
    using SportsNet.Data;
    using SportsNet.Data.Models.Enums;
    using System.Collections.Generic;

    public class PostService : IPostService
    {

        public PostService()
        {

        }

        public IEnumerable<PostType> GetPostTypes() 
            => Enum.GetValues(typeof(PostType))
            .Cast<PostType>()
            .ToList();

        
    }
}
