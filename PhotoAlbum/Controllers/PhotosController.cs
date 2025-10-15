using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoAlbum.Data;
using PhotoAlbum.Models;

namespace PhotoAlbum.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly PhotoAlbumContext _context;

        public PhotosController(PhotoAlbumContext context)
        {
            _context = context;
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Title");

            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoId,Title,Description,Camera,CategoryId, FormFile")] Photo photo)
        {
            //Initalize values
            photo.CreateDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                //
                // Step 1: save the file (optionally)
                //
                if(photo.FormFile != null)
                {
                    //Create a unique filename
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(photo.FormFile.FileName);

                    //Initialize the filename in photo record
                    photo.Filename = filename;

                    //Get the file path to save the file. Use Path.Combine to handle different os
                    string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", filename);

                    //Save file
                    using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
                    {
                        await photo.FormFile.CopyToAsync(fileStream);
                    }
                }
                
                //
                //Step 2: save record in database
                //

                _context.Add(photo);

                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index), "Home");
            }

            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", photo.CategoryID);

            return View(photo);
        }

        // GET: Photos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryId", "Title", photo.CategoryID);

            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoId,Title,Description,Filename,CreateDate,CategoryId")] Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                //
                // Step 1: save the file (optionally)
                //
                //if (photo.FormFile != null)
                //{


                    //Create a unique filename
                    //string newfilename = Guid.NewGuid().ToString() + Path.GetExtension(photo.FormFile.FileName);

                    ////Initialize the new filename in db record
                    //photo.Filename = newfilename;

                    ////upload the new file
                    //string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", newfilename);

                    ////Delete the old file
                    //if(saveFilePath != null)
                    //{
                    //    using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
                    //    {
                    //        fileStream.Dispose();
                    //        System.IO.File.Delete(saveFilePath);
                    //    }
                    //}
                    
                //}

                //
                //Step 2: save record in database
                //

                try
                {
                    _context.Update(photo);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.PhotoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), "Home");
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", photo.CategoryID);

            return View(photo);
        }

        // GET: Photos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PhotoId == id);

            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photo = await _context.Photo.FindAsync(id);

            if (photo != null)
            {
                _context.Photo.Remove(photo);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Home");
        }

        private bool PhotoExists(int id)
        {
            return _context.Photo.Any(e => e.PhotoId == id);
        }
    }
}
