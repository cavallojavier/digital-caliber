using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using digital.caliber.services.Cache;
using digital.caliber.services.Resources;

namespace digital.caliber.services.CalculationTables
{
    public static class PontTable
    {
        public static CustomMemoryCache CacheInstance { get; set; }

        /// <summary>
        /// Finds the moyer superior value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static async Task<decimal?> FindPontValue(decimal referenceValue)
        {
            try
            {
                var table = await GetPontTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item1)).First();

                if(Math.Abs(referenceValue - closest.Item1) > 1)
                {
                    return null;
                }

                return closest.Item2;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the moyers superior.
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<Tuple<decimal, decimal>>> GetPontTable()
        {
            List<Tuple<decimal, decimal>> result;
            CacheInstance = CacheInstance ?? new CustomMemoryCache();
            var cachedItem = await CacheInstance.GetAsync<List<Tuple<decimal, decimal>>>(CacheKey.PontTableKey);

            if (cachedItem != null)
            {
                return cachedItem;
            }

            result = new List<Tuple<decimal, decimal>>();

            using (var reader = new StreamReader(new MemoryStream(ResourceFiles.PontReferences)))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
                {
                    var items = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var item1 = decimal.Parse(items[0].Trim(), CultureInfo.InvariantCulture);
                    var item2 = decimal.Parse(items[1].Trim(), CultureInfo.InvariantCulture);

                    result.Add(new Tuple<decimal, decimal>(item1, item2));
                }
            }

            await CacheInstance.AddAsync(result, CacheKey.PontTableKey, 120);

            return result;
        }
    }
}
