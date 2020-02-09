using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public abstract class Entity : IEntity
    {
        public abstract bool IsValid();

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            CompareLogic compareLogic = new CompareLogic();
            compareLogic.Config = new ComparisonConfig()
            {
                ComparePrivateFields = true,
                CompareFields = true,
                CompareReadOnly = true,
                CompareChildren = true,
                ComparePrivateProperties = true,
                CompareStaticProperties = true,
                CompareProperties = true
            };

            return compareLogic.Compare(this, other).AreEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
