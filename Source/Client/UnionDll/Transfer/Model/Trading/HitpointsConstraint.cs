using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCUnion.Transfer.Model.Trading
{
    public struct HitpointsConstraint : IThingConstraint
    {
        public string GroupingKey => "hitpoints";

        public string GroupLabel => "Hitpoints";

        public float HitpointsMin { get; private set; }
        public float HitpointsMax { get; private set; }

        public HitpointsConstraint(uint min, uint max)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min), "Min parameter cannot be less than 0.");
            if (max > 1)
                throw new ArgumentOutOfRangeException(nameof(max), "Max parameter cannot be more than 1.");

            if (max > min)
                throw new ArgumentOutOfRangeException(nameof(max), "Max parameter must be more than min parameter.");

            HitpointsMin = min;
            HitpointsMax = max;
        }

        public string Description
        {
            get
            {
                if (HitpointsMax - HitpointsMin < 0.001)
                    return string.Format("Exactly {0}% of hitpoints", Math.Round(HitpointsMin * 100));

                if (HitpointsMin == 0 && HitpointsMax == 0)
                    return "Any amount of hitpoints";

                if (HitpointsMin == 0)
                    return string.Format("Any hitpoints up to {0}%", Math.Floor(HitpointsMax * 100));

                if (HitpointsMax == 0)
                    return string.Format("Any hitpoints starting from {0}%", Math.Ceiling(HitpointsMin * 100));

                return string.Format("Between {0}% and {1}% hitpoints", Math.Ceiling(HitpointsMin * 100), Math.Floor(HitpointsMax * 100));
            }
        }

        public bool IsComparableTo(IThingConstraint other)
        {
            return other is HitpointsConstraint;
        }

        public bool IsCompatibleWith(IThingConstraint other)
        {
            var casted = other as HitpointsConstraint?;
            if (casted == null)
                return true;

            return HitpointsMin <= casted.Value.HitpointsMax && HitpointsMax >= casted.Value.HitpointsMin;
        }
    }
}
