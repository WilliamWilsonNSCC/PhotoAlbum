using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhotoAlbum.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PhotoAlbum.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(ILogger<PhotosController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {

            List<Photo> photos = new List<Photo>();

            Photo photo1 = new Photo();
            photo1.PhotoId = 1;
            photo1.Title = "Kitt";
            photo1.Description = "My orange fluffy cat";
            photo1.Filename = "Kitt.jpg";
            photo1.CreateDate = new DateTime(2025, 09, 18, 13, 51, 00);

            Photo photo2 = new Photo();
            photo2.PhotoId = 1;
            photo2.Title = "Kosie";
            photo2.Description = "crazy cat";
            photo2.Filename = "Kosie.jpg";
            photo2.CreateDate = new DateTime(2025, 09, 18, 13, 51, 15);

            Photo photo3 = new Photo();
            photo3.PhotoId = 1;
            photo3.Title = "Jeff";
            photo3.Description = "a donkey";
            photo3.Filename = "MyNameAJeff.jpg";
            photo3.CreateDate = new DateTime(2025, 09, 18, 13, 51, 20);

            photos.Add(photo1);
            photos.Add(photo2);
            photos.Add(photo3);

            _logger.Log(LogLevel.Information, "Number of photos: " + photos.Count);

            return View(photos);
        }
    }
}
