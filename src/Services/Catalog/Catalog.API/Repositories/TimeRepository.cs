using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly ICatalogContext _context;

        public TimeRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Time> GetTime()
        {
            return await _context
                           .Times.Find(p => true)
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateTime(Time time)
        {
            var updateResult = await _context
                                        .Times
                                        .ReplaceOneAsync(filter: g => g.Id == time.Id, replacement: time);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Time>> GetTimes()
        {
            return await _context
                            .Times
                            .Find(p => true)
                            .ToListAsync();
        }
    }
}
