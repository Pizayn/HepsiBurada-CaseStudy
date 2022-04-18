using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TimeService.API.Data;
using TimeService.API.Entities;

namespace TimeService.API.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly ITimeContext _context;

        public TimeRepository(ITimeContext context)
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

     
    }
}
