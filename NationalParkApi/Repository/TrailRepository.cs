using Microsoft.EntityFrameworkCore;
using NationalParkApi.Data;
using NationalParkApi.Models;
using NationalParkApi.Repository.IRepository;

namespace NationalParkApi.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _context;
        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateTrail(Trail trail)
        {
            _context.Trailers.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trailers.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _context.Trailers.Find(trailId);
        }

        public ICollection<Trail> GetTrails()
        {
           return _context.Trailers.Include(t=> t.NationalPark).ToList();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int nationalParkId)
        {
            return _context.Trailers.Include(t=>t.NationalPark).Where(t=>t.NationalParkId==nationalParkId).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges()==1?true:false;
        }

        public bool TrailExist(int trailId)
        {
            return _context.Trailers.Any(t=> t.Id == trailId);
        }

        public bool TrailExists(string trailName)
        {
            return _context.Trailers.Any(t=>t.Name == trailName);
        }

        public bool UpdateTrail(Trail trail)
        {
          _context.Trailers.Update(trail);
            return Save();  
        }
    }
}
