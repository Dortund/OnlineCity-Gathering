using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCUnion.Transfer.Model.Trading
{
    public struct Order
    {
        public struct Ratio
        {
            public SerializedThingFilter ThingFilter;
            public decimal Amount;
        }

        public IEnumerable<ThingStack> Stockpile => _stockpiles.SelectMany(kv => kv.Value);
        public IEnumerable<Ratio> SellOffer { get; private set; }
        public IEnumerable<Ratio> BuyOffer { get; private set; }

        private Dictionary<SerializedThingFilter, Stack<ThingStack>> _stockpiles;
        public Order(IEnumerable<Ratio> sellOffer, IEnumerable<Ratio> buyOffer, Dictionary<SerializedThingFilter, IEnumerable<ThingStack>> stockpile, int tradeCount)
        {
            SellOffer = sellOffer.ToArray();
            BuyOffer = buyOffer.ToArray();

            _stockpiles = stockpile.ToDictionary(kv => kv.Key, kv => new Stack<ThingStack>(kv.Value));
;
            foreach (var ratio in SellOffer)
            {
                if (!_stockpiles.ContainsKey(ratio.ThingFilter))
                    throw new ArgumentException("Stockpile is missing items", nameof(stockpile));

                long availableCount = _stockpiles[ratio.ThingFilter].Sum(s => s.Count);
            }
        }
    }
}
