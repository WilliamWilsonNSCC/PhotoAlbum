using Microsoft.AspNetCore.Mvc;
using PhotoAlbum.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PhotoAlbum.Controllers
{
    public class PhotosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
