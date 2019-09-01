using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HelloWorldMVC.Models;
using HelloWorldMVC.Data;
using System.Threading.Tasks;
using System;
using HelloWorldMVC.Helpers;

namespace HelloWorldMVC.Controllers 
{
    [Authorize(Roles = "Administrador")]
    public class VideoController : Controller
    {
        private VideoContext _context;

        public VideoController(VideoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Video> Videos = await _context.Videos.Include("VideoTag.Tag").OrderByDescending(v => v.Id).ToListAsync();

            return View(Videos);
        }

        [Authorize(Roles = "Creador")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Tags = await _context.Tags.Where(t => t.EsVisible).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Creador")]
        public async Task<IActionResult> Create(Video video, List<int> Tags)
        {
            if(ModelState.IsValid) {            
                using(var transaction = await _context.Database.BeginTransactionAsync()) 
                {     
                    try
                    {
                        await _context.Videos.AddAsync(VideoHelper.getVideoWithIdParsed(video));
                        await _context.SaveChangesAsync();

                        foreach (var tag in Tags)
                        {
                            await _context.VideoTag.AddAsync(new VideoTag {
                                VideoId = video.Id,
                                TagId = tag
                            });

                            await _context.SaveChangesAsync();
                        }

                        transaction.Commit();
                    } catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.Tags = await _context.Tags.Where(t => t.EsVisible).ToListAsync();

            return View(video);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Video video = await _context.Videos.Include(v => v.VideoTag).FirstOrDefaultAsync(v => v.Id == id);

            ViewBag.Tags = await _context.Tags.Where(t => t.EsVisible).ToListAsync();

            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Video video, List<int> Tags)
        {
            if(ModelState.IsValid) {            
                using(var transaction = await _context.Database.BeginTransactionAsync()) 
                {     
                    try
                    {
                        _context.Entry(video).State = EntityState.Modified;
                        await _context.SaveChangesAsync(); 

                        _context.VideoTag.RemoveRange(_context.VideoTag.Where(v => v.VideoId == video.Id));
                        await _context.SaveChangesAsync();

                        foreach (var tag in Tags)
                        {
                            await _context.VideoTag.AddAsync(new VideoTag {
                                VideoId = video.Id,
                                TagId = tag
                            });

                            await _context.SaveChangesAsync();
                        }

                        transaction.Commit();
                    } catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.Tags = await _context.Tags.Where(t => t.EsVisible).ToListAsync();

            return View(video);
        }
    }
}