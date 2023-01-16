using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Entities
{
    [Serializable]
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
        }

        public override string ToString()
        {
            return "[ENTITY: " + GetType().Name + "] Keys = " + GetKeys();
        }

        public abstract object[] GetKeys();

    }

    [Serializable]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>, IEntity
    {
        public virtual TKey Id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[1] { Id };
        }

        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}
