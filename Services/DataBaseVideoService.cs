using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldMVC.Data;
using HelloWorldMVC.Helpers;
using HelloWorldMVC.Helpers.Interfaces;
using HelloWorldMVC.Models;

namespace HelloWorldMVC.Services
{
    public class DataBaseVideoService : IVideoService
    {
        private VideoContext _context { get; set; }

        public DataBaseVideoService(VideoContext context)
        {
            _context = context;
        }

        public List<Video> GetAll()
        {
            return _context.Videos.OrderByDescending(v => v.Id).ToList();
        }

        public Video GetVideo(int id)
        {
            return _context.Videos.Find(id);
        }

        public async Task<Video> Update(Video video)
        {
            _context.Videos.Update(VideoHelper.getVideoWithIdParsed(video));
            _context.Entry<Video>(video).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<Video> Insert(Video video)
        {
            _context.Videos.Add(VideoHelper.getVideoWithIdParsed(video));
            await _context.SaveChangesAsync();
            return video;
        }
    }
}