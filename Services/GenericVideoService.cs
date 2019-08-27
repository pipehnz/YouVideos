using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HelloWorldMVC.Helpers;
using HelloWorldMVC.Helpers.Interfaces;
using HelloWorldMVC.Models;

namespace HelloWorldMVC.Services
{
    public class GenericVideoService : IVideoService
    {
        private List<Video> _context { get; set; }

        public GenericVideoService()
        {
            _context = new List<Video> {
                new Video {
                    Id = 1,
                    Titulo = "Primer video de ejemplo",
                    Descripcion = "Breve descripción del contenido",
                    Url = "https://www.youtube.com/embed/UsNj7fcckcY"
                },
                new Video {
                    Id = 2,
                    Titulo = "Segundo video de ejemplo",
                    Descripcion = "Breve descripción del segundo video y su contenido",
                    Url = "https://www.youtube.com/embed/lOg2IuQIp-s"
                }
            };
        }
        public List<Video> GetAll()
        {
            return _context.OrderByDescending(v => v.Id).ToList();
        }

        public Video GetVideo(int id)
        {
            return _context.Find(v => v.Id == id);
        }

        public Task<Video> Update(Video video)
        {
            return Task.Run(() => {
                _context.ForEach(v =>
                {
                    if (v.Id == video.Id)
                    {
                        video = VideoHelper.getVideoWithIdParsed(video);
                        v.Titulo = video.Titulo;
                        v.Descripcion = video.Descripcion;
                        v.Url = video.Url;
                    }
                });

                return video;
            });
        }

        public Task<Video> Insert(Video video)
        {
            return Task.Run(() =>
            {
                video.Id = _context.Count + 1;
                video = VideoHelper.getVideoWithIdParsed(video);
                _context.Add(video);

                return video;
            });
        }
    }
}