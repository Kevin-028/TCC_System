using System;
using System.Collections.Generic;
using System.Text;
using TCC_System_Domain.Blog;

namespace TCC_System_Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(TCC_Context context) : base(context)
        {

        }
    }
}
