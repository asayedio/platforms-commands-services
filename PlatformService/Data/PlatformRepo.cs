using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        protected readonly AppDbContext _dbContext;

        public PlatformRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _dbContext.Platforms.Add(plat);
            this.SaveChanges();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _dbContext.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _dbContext.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}
