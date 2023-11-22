using TaxiGomelLab4.Data;
using TaxiGomelLab4.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelLab4.Services
{
    public class CachedRatesService : ICachedService<Rate>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedRatesService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }
        public IEnumerable<Rate> GetData(int rowsNumber = 20)
        {
            return _dbContext.Rates.Take(rowsNumber).ToList();
        }

        public void AddData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Rate> rates = _dbContext.Rates.Take(rowsNumber).ToList();
            if (rates != null)
            {
                _memoryCache.Set(cacheKey, rates, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(270)
                });

            }

        }
        public IEnumerable<Rate> GetData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Rate> rates;
            if (!_memoryCache.TryGetValue(cacheKey, out rates))
            {
                rates = _dbContext.Rates.Take(rowsNumber).ToList();
                if (rates != null)
                {
                    _memoryCache.Set(cacheKey, rates,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(270)));
                }
            }
            return rates;
        }
    }
}

