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

        protected Module() { }
        public Module(TypeModule type,string value,Guid productId)
        {
            SetType(type);
            SetValue(value);
            SetProductID(productId);
        }
        public void SetType(TypeModule type) => this.Type = type;


        public void SetId()
        {
            this.Id = Guid.NewGuid();
        }
        public void SetValue(string value)
        { 
            this.Value = value;
        }
        public void SetProductID(Guid id) => this.ProductId = id;
    }
}
