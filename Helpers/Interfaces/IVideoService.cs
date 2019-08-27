using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorldMVC.Models;

namespace HelloWorldMVC.Helpers.Interfaces
{
    public interface IVideoService
    {
        Task<Video> Update(Video video);
        Task<Video> Insert(Video video);
        Video GetVideo(int id);
        List<Video> GetAll();
    }
}