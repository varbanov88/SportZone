using SportZone.Data;

namespace SportZone.Services
{
    public abstract class BasicService
    {
        protected readonly SportZoneDbContext db;

        public BasicService(SportZoneDbContext db)
        {
            this.db = db;
        }
    }
}