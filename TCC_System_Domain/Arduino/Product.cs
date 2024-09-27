using System;
using System.Collections.Generic;
using System.Linq;
using TCC_System_Domain.Core;
using TCC_System_Domain.Management;

namespace TCC_System_Domain.Arduino
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int UserId { get; private set; }



        private readonly List<Module> _productModeles;
        public IReadOnlyCollection<Module> ProductModeles => _productModeles;

        protected Product() { }
        public Product(int userId, string name)
        {
            _productModeles = new List<Module>();

            SetUser(userId);
            SetName(name);
        }

        public void SetId()
        {
            this.Id = Guid.NewGuid();
        }
        public void SetUser(int userId)
        {
            this.UserId = userId;
        }
        public void SetName(string name)
        { 
            this.Name = EntityValidation.SetStringTitleCaseProperty(name, "Name ");
        }

        public void AddModule(Module module)
        {
            EntityValidation.VerifyPropertyIsValid(module, "Modulo");

            module.SetId();

            if (!_productModeles.Exists(x => x.Type == module.Type))
            {
                _productModeles.Add(module);
            }
            else
            {
                AssertionConcern.AssertNotification("Modulo ja existente!!");
            }

        }
        public void RemoveModule(Guid guid) 
        {
            Module module = _productModeles.Where(x => x.Id == guid).SingleOrDefault();

            if (module != null)
            {
                _productModeles.Remove(module);
            }
        }

    }
}
