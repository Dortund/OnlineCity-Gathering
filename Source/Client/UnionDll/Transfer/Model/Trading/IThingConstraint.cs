using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace OCUnion.Transfer.Model.Trading
{
    public interface IThingConstraint
    {
        /// <summary>
        /// Constraints that should be checked against each other need to specify the same key.
        /// </summary>
        string GroupingKey { get; }

        /// <summary>
        /// Human readable label for this matching group.
        /// </summary>
        string GroupLabel { get; }

        /// <summary>
        /// Human readable text describing this constraint.
        /// </summary>
        string Description { get; }

        bool IsComparableTo(IThingConstraint other);
        bool IsCompatibleWith(IThingConstraint other);
    }
}