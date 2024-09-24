﻿using System;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Arduino
{
    public class MessageAction : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Type Type { get; private set; }
        public Code Action { get; private set; }

        protected MessageAction() { }
        public MessageAction(Guid id, Type type, Code action)
        {
            SetId(id);
            SetType(type);
            SetAction(action);
        }


        public void SetId(Guid guid)
        {
            this.Id = EntityValidation.SetGuidProperty(guid, "Id ");
        }
        public void SetType(Type type) => this.Type = type;
        public void SetAction(Code action) => this.Action = action;        

    }
}