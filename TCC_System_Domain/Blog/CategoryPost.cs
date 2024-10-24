using System;

namespace TCC_System_Domain.Blog
{
    public class CategoryPost
    {
        public Guid CategoryId { get;private set; }
        public Guid PostId { get; private set; }

        public Category Category { get; set; }
        public Post Post { get; set; }


        public CategoryPost(Guid postId) 
        {
            PostId = postId;
        }
        public void SetCategoryId(Guid categoryId)=> CategoryId = categoryId;
        public void SetPostId(Guid postId)=> PostId = postId;



    }
}
