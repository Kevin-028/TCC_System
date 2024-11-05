using System;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Blog
{
    public class Post : Entity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; private set; }

        protected Post() { }
        public Post(Guid id, string title, string body, int userid)
        {
            SetID(id);
            SetTitle(title);
            SetBody(body);
            SetUser(userid);
        }

        public void SetID(Guid id) => this.Id = id;
        public void SetTitle(string title) => this.Title = title;
        public void SetBody(string body) => this.Body = body;
        public void SetUser(int userId)
        {
            this.UserId = userId;
        }

    }
}
