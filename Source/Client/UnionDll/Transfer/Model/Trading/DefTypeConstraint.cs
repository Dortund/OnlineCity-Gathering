using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace OCUnion.Transfer.Model.Trading
{
    public struct DefTypeConstraint : IThingConstraint
    {
        HashSet<string> _allowedDefTypes;
        public IEnumerable<string> AllowedDefTypes => _allowedDefTypes;

        public DefTypeConstraint(IEnumerable<string> defTypes)
        {
            if (!defTypes.Any())
                throw new ArgumentException("At least one defType must be specified", nameof(defTypes));
            _allowedDefTypes = defTypes.ToHashSet();
        }

        public string GroupingKey => "defType";
        public string GroupLabel => "Item Type";

        private string LookupName(string defType)
        {
            var def = (ThingDef)GenDefDatabase.GetDef(typeof(ThingDef), defType);
            return def.label.Translate();
        }

        public string Description
        {
            get
            {
                if (_allowedDefTypes.Count == 1)
                    return string.Format("Must be {0}", LookupName(_allowedDefTypes.First()));

                var result = new List<string>() { "Must be one of: " };
                foreach (var defType in _allowedDefTypes.OrderBy(q => q))
                {
                    result.Append(LookupName(defType));
                }
                return string.Join(", ", result);
            }
        }

        public bool IsComparableTo(IThingConstraint other)
        {
            return other is DefTypeConstraint;
        }

        public bool IsCompatibleWith(IThingConstraint other)
        {
            var casted = other as DefTypeConstraint?;
            if (casted == null)
                return true;

            return casted.Value._allowedDefTypes.Intersect(_allowedDefTypes).Any();
        }
    }
}
