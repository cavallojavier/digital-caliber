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
    public static class MoyersTable
    {
        public static CustomMemoryCache CacheInstance { get; set; }

        /// <summary>
        /// Finds the moyer superior value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static async Task<decimal?> FindMoyerSuperiorValue(decimal referenceValue)
        {
            try
            {
                var table = await GetMoyersTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item1)).First();

                if (Math.Abs(referenceValue - closest.Item1) > 1)
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
        /// Finds the moyer inferior value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static async Task<decimal?> FindMoyerInferiorValue(decimal referenceValue)
        {
            try
            {
                var table = await GetMoyersTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item3)).First();

                if (Math.Abs(referenceValue - closest.Item3) > 1)
                {
                    return null;
                }

                return closest.Item4;
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
        private static async Task<IEnumerable<Tuple<decimal, decimal, decimal, decimal>>> GetMoyersTable()
        {
            List<Tuple<decimal, decimal, decimal, decimal>> result;
            CacheInstance = CacheInstance ?? new CustomMemoryCache();
            var cachedItem = await CacheInstance.GetAsync<List<Tuple<decimal, decimal, decimal, decimal>>>(CacheKey.MoyersTableKey);

            if (cachedItem != null)
            {
                return cachedItem;
            }

            result = new List<Tuple<decimal, decimal, decimal, decimal>>();
            
            using (var reader = new StreamReader(new MemoryStream(ResourceFiles.MoyersReference)))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
                {
                    var items = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var item1 = decimal.Parse(items[0].Trim(), CultureInfo.InvariantCulture);
                    var item2 = decimal.Parse(items[1].Trim(), CultureInfo.InvariantCulture);
                    var item3 = decimal.Parse(items[2].Trim(), CultureInfo.InvariantCulture);
                    var item4 = decimal.Parse(items[3].Trim(), CultureInfo.InvariantCulture);

                    result.Add(new Tuple<decimal, decimal, decimal, decimal>(item1, item2, item3, item4));
                }
            }

            await CacheInstance.AddAsync(result, CacheKey.MoyersTableKey, 120);

            return result;
        }
    }
}
