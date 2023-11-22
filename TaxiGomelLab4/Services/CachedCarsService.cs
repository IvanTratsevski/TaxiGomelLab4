using TaxiGomelLab4.Data;
using TaxiGomelLab4.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelLab4.Services
{
    public class CachedCarsService : ICachedService<Car>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCarsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }
        public IEnumerable<Car> GetData(int rowsNumber = 20)
        {
            return _dbContext.Cars.Include(c => c.CarModel).Take(rowsNumber).ToList();
        }

        public void AddData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Car> Cars = _dbContext.Cars.Include(c => c.CarModel).Take(rowsNumber).ToList();
            if (Cars != null)
            {
                _memoryCache.Set(cacheKey, Cars, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<Car> GetData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Car> cars;
            if (!_memoryCache.TryGetValue(cacheKey, out cars))
            {
                cars = _dbContext.Cars.Include(c => c.CarModel).Take(rowsNumber).ToList();
                if (cars != null)
                {
                    _memoryCache.Set(cacheKey, cars,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(270)));
                }
            }
            return cars;
        }
    }
}

