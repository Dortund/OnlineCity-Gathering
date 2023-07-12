using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace OCUnion.Transfer.Model.Trading
{
    public struct QualityConstraint : IThingConstraint
    {
        HashSet<RimWorld.QualityCategory> _allowedCategories;
        public IEnumerable<RimWorld.QualityCategory> AllowedCategories => _allowedCategories;

        public QualityConstraint(IEnumerable<RimWorld.QualityCategory> categories)
        {
            _allowedCategories = categories.ToHashSet();
        }

        public string GroupingKey => "quality";
        public string GroupLabel => "Quality";

        public string Description
        {
            get
            {
                if (All.ToHashSet() == _allowedCategories)
                    return "Any";

                if (_allowedCategories.Count == 0)
                    return "Only quality-less items";

                if (_allowedCategories.Count == 1)
                    return string.Format("Only {0} or no quality", _allowedCategories.First().GetLabelShort());

                var result = new List<string>() { "No quality or one of: " };
                foreach (var quality in _allowedCategories.OrderBy(q => q))
                {
                    result.Append(quality.GetLabelShort());
                }
                return string.Join(", ", result);
            }
        }

        public bool IsComparableTo(IThingConstraint other)
        {
            return other is QualityConstraint;
        }

        public bool IsCompatibleWith(IThingConstraint other)
        {
            var casted = other as QualityConstraint?;
            if (casted == null)
                return true;

            return casted.Value._allowedCategories.Intersect(_allowedCategories).Any();
        }

        private static IEnumerable<RimWorld.QualityCategory> All => new RimWorld.QualityCategory[]
        {
            RimWorld.QualityCategory.Awful,
            RimWorld.QualityCategory.Poor,
            RimWorld.QualityCategory.Normal,
            RimWorld.QualityCategory.Good,
            RimWorld.QualityCategory.Excellent,
            RimWorld.QualityCategory.Masterwork,
            RimWorld.QualityCategory.Legendary
        };

        public static QualityConstraint Single(RimWorld.QualityCategory category)
        {
            return new QualityConstraint(new RimWorld.QualityCategory[] { category });
        }

        public static QualityConstraint Where(Func<RimWorld.QualityCategory, bool> predicate)
        {
            return new QualityConstraint(All.Where(predicate));
        }

        public static QualityConstraint? FromThing(Thing thing)
        {
            var comp = thing.TryGetComp<CompQuality>();
            if (comp == null)
                return null;

            return Single(comp.Quality);
        }
    }
}
