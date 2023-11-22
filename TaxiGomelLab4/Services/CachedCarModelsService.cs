using TaxiGomelLab4.Data;
using TaxiGomelLab4.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelLab4.Services
{
    public class CachedCarModelsService : ICachedService<CarModel>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCarModelsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }
        public IEnumerable<CarModel> GetData(int rowsNumber = 20)
        {
            return _dbContext.CarModels.Take(rowsNumber).ToList();
        }

        public void AddData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<CarModel> car_models = _dbContext.CarModels.Take(rowsNumber).ToList();
            if (car_models != null)
            {
                _memoryCache.Set(cacheKey, car_models, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(270)
                });

            }

        }
        public IEnumerable<CarModel> GetData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<CarModel> car_models;
            if (!_memoryCache.TryGetValue(cacheKey, out car_models))
            {
                car_models = _dbContext.CarModels.Take(rowsNumber).ToList();
                if (car_models != null)
                {
                    _memoryCache.Set(cacheKey, car_models,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(270)));
                }
            }
            return car_models;
        }
    }
}

