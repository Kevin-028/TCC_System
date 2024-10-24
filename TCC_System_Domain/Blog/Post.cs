using System;
using System.Collections.Generic;
using System.Text;
using TCC_System_Domain.Core;
using TCC_System_Domain.Management;

namespace TCC_System_Domain.Blog
{
    public class Post : Entity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }

        private readonly List<CategoryPost> _categoryPosts;
        public IReadOnlyCollection<CategoryPost> CategoryPosts => _categoryPosts;

        public Post(Guid id, string title, string body, User user)
        {
            _categoryPosts = new List<CategoryPost>();
            SetID(id);
            SetTitle(title);
            SetBody(body);
            SetUser(user);
        }

        public void SetID(Guid id) => this.Id = id;
        public void SetTitle(string title) => this.Title = title;
        public void SetBody(string body) => this.Body = body;
        public void SetUser(User user) => this.User = user;


    }
}
