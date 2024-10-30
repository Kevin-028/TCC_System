using System;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Arduino
{
    public class Module : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public TypeModule Type { get; private set; }
        public string Value { get;private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public bool Active { get; private set; }
        public byte[] Image { get; set; }


        protected Module() { }
        public Module(TypeModule type,string value,Guid productId)
        {
            SetType(type);
            SetValue(value);
            SetProductID(productId);
            SetActive();
        }
        public void SetType(TypeModule type) => this.Type = type;
        public void SetId()=>  this.Id = Guid.NewGuid();
        public void SetValue(string value)=> this.Value = value;
        
        public void SetProductID(Guid id) => this.ProductId = id;
        public void SetStatus(bool a) => this.Active = a;
        public void SetActive() => this.Active = true;
        public void SetActiveFalse() => this.Active = false;
        public void SetImage(byte[] image)=> this.Image = image;
    }
}
