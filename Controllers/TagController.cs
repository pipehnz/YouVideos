using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldMVC.Data;
using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldMVC.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TagController : Controller
    {
        private VideoContext _context { get; set; }

        public TagController(VideoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Tag> Tags = _context.Tags.ToList();

            return View(Tags);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if(ModelState.IsValid) {
                try
                {
                    Tag tagExist = _context.Tags.Where(t => t.Descripcion.Equals(tag.Descripcion)).FirstOrDefault();

                    if(tagExist == null) {
                        await _context.Tags.AddAsync(tag);
                        await _context.SaveChangesAsync(); 
                    }

                    return RedirectToAction("Index", "Tag");
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }

            return View(tag);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Tag Tag = await _context.Tags.FindAsync(id);

            return View(Tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Edit(Tag tag)
        {
            if(ModelState.IsValid) {
                try
                {
                    _context.Entry(tag).State = EntityState.Modified;
                    await _context.SaveChangesAsync(); 

                    return RedirectToAction("Index", "Tag");
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }

            return View(tag);
        }
    }
}