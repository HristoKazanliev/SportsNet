namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Services.Data.Models.Post;
    using SportsNet.Web.Infrastructure.Extensions;
    using SportsNet.Web.ViewModels.Post;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class PostController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;

        public PostController(ICategoryService categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllPostsQueryModel queryModel)
        {
            AllPostsQueryServiceModel serviceModel = await this.postService.AllAsync(queryModel);

            queryModel.Posts = serviceModel.Posts;
            queryModel.TotalPosts = serviceModel.TotalPostsCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                PostFormModel model = new PostFormModel()
                {
                    Categories = await this.categoryService.AllCategoriesAsync(),
                    Types = this.postService.GetPostTypes()
                };

                return View(model);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            bool categoryExists = this.categoryService.ExistsByIdAsync(model.CategoryId);
            if (!categoryExists) 
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Types = this.postService.GetPostTypes();

                return View(model);
            }

            try
            {
                string postId = await this.postService.CreateAsync(model, this.User.GetId()!);

                TempData[SuccessMessage] = "Post was added successfully!";
                return RedirectToAction("Details", "Post", new { postId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new post! Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Types = this.postService.GetPostTypes();

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool postExists = await this.postService
                .ExistsByIdAsync(id);
            if (!postExists)
            {
                TempData[ErrorMessage] = "Post with the provided id does not exist!";
                return RedirectToAction("All", "Post");
            }

            bool isUserOwner = await this.postService.IsUserOwner(id, this.User.GetId()!);
            if (!isUserOwner) 
            {
                TempData[ErrorMessage] = "You must be admin or owner of the post you want to edit!";
                return RedirectToAction("Details", "Post", new { id });
            }

            try
            {
                PostFormModel postModel = this.postService.GetPost<PostFormModel>(id);
                postModel.Categories = await this.categoryService.AllCategoriesAsync();
                postModel.Types = this.postService.GetPostTypes();

                return View(postModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostFormModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Types = this.postService.GetPostTypes();

                return View(model);
            }

            bool postExists = await this.postService
                .ExistsByIdAsync(id);
            if (!postExists)
            {
                TempData[ErrorMessage] = "Post with the provided id does not exist!";
                return RedirectToAction("All", "Post");
            }

            bool isUserOwner = await this.postService.IsUserOwner(id, this.User.GetId()!);
            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "You must be admin or owner of the post you want to edit!";
                return RedirectToAction("Details", "Post", new { id });
            }

            try
            {
                await this.postService.EditPostAsync(model, id);

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the post. Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Types = this.postService.GetPostTypes();

                return View(model);
            }


            TempData[SuccessMessage] = "Post was edited successfully!";
            return this.RedirectToAction("Details", "Post", new { id });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool postExists = await this.postService
                .ExistsByIdAsync(id);
            if (!postExists) 
            {
                TempData[ErrorMessage] = "Post with the provided id does not exist!";
				return RedirectToAction("All", "Post");
			}

            try
            {
                PostDetailsViewModel post = this.postService.GetPost<PostDetailsViewModel>(id);

                return View(post);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool postExists = await this.postService
                .ExistsByIdAsync(id);
            if (!postExists)
            {
                TempData[ErrorMessage] = "Post with the provided id does not exist!";
                return RedirectToAction("All", "Post");
            }

            bool isUserOwner = await this.postService.IsUserOwner(id, this.User.GetId()!);
            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "You must be admin or owner of the post you want to edit!";
                return RedirectToAction("Details", "Post", new { id });
            }

            try
            {
                PostDetailsViewModel post = this.postService.GetPost<PostDetailsViewModel>(id);

                return View(post);
            }
            catch (Exception)
            {
                return GeneralError();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            bool postExists = await this.postService
                .ExistsByIdAsync(id);
            if (!postExists)
            {
                TempData[ErrorMessage] = "Post with the provided id does not exist!";
                return RedirectToAction("All", "Post");
            }

            bool isUserOwner = await this.postService.IsUserOwner(id, this.User.GetId()!);
            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "You must be admin or owner of the post you want to edit!";
                return RedirectToAction("Details", "Post", new { id });
            }

            try
            {
                await this.postService.DeletePostAsync(id);

                TempData[WarningMessage] = "The post was successfully deleted!";
                return RedirectToAction("All", "Post");
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return RedirectToAction("All", "Post");
        }
    }
}
