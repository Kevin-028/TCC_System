using System;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Arduino
{
    public class Module : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Type Type { get; private set; }
        public string Value { get;private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        protected Module() { }
        public Module(Guid id, Type type)
        {
            SetId(id);
            SetType(type);
        }
        public void SetType(Type type) => this.Type = type;


        public void SetId(Guid guid)
        {
            this.Id = EntityValidation.SetGuidProperty(guid, "Id ");
        }
        public void SetValue(string value)
        { 
            this.Value = value;
        }
    }
}
