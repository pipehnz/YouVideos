using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HelloWorldMVC.Models;
using HelloWorldMVC.Helpers.Interfaces;

namespace HelloWorldMVC.Controllers 
{
    [Authorize]
    public class VideoController : Controller
    {
        private IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public IActionResult Index()
        {
            List<Video> Videos = _videoService.GetAll();

            return View(Videos);
        }

        [Authorize(Roles = "Creador, Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Creador, Administrador")]
        public IActionResult Create(Video video)
        {
            if(ModelState.IsValid) {
                _videoService.Insert(video);
                return RedirectToAction("Index");
            }

            return View(video);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            Video video = _videoService.GetVideo(id);
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(Video video)
        {
            if(ModelState.IsValid) {
                _videoService.Update(video);
                return RedirectToAction("Index");
            }

            return View(video);
        }
    }
}