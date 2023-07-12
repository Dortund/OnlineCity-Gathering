using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCUnion.Transfer.Model.Trading
{
    public struct StockpileTarget
    {
        public IEnumerable<SerializedThingFilter> Targets => Enumerable.Empty<SerializedThingFilter>();

        public bool ShouldSumTargets;

        public uint? BuyUpToCount;
        public uint? SellUpToCount;
        public decimal BuyPrice;
        public decimal SellPrice;
    }
}
