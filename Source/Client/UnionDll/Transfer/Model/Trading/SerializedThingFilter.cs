using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCUnion.Transfer.Model.Trading
{
    public class SerializedThingFilter
    {
        public IEnumerable<IThingConstraint> constraints => _constraints;
        protected List<IThingConstraint> _constraints = new List<IThingConstraint>();

        public bool IsApplicable(SerializedThingFilter other)
        {
            //var myGroups = _constraints
        }
    }
}
