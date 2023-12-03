using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Data;
using Booking.Models;
using Microsoft.AspNetCore.Identity;
using Booking.Interfaces;
using Booking.Repositories;
using WebApi.Repositories;
using System.Security.Claims;
using Booking.Services;


namespace Booking.Areas.AgentTour.Controllers
{
    [Area("AgentTour")]
    [Route("agent-tour")]
    public class ToursController : Controller
    {

        private readonly ITourRepository _tourRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IImageService _imageService;

        public ToursController(ITourRepository tourRepository, UserManager<AppUser> userManager, IImageService imageService)
        {
            _tourRepository = tourRepository;
            _userManager = userManager;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourRepository.GetAllTours();
            ViewBag.Tours = tours.ToList();
            return View();
        }

        [Route("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var tour = await _tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        [Route("create")]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;

            return View();
        }

       
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Tour tour, List<int> tp , List<IFormFile> fileInputs)
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (tour == null)
            {
                return NotFound();
            }
            Tour newTour = new Tour()
            {
                TourName = tour.TourName,
                TourAddress = tour.TourAddress,
                Overview = tour.Overview,
                Schedule = tour.Schedule,
                DepartureLocation = tour.DepartureLocation,
                DropOffLocation = tour.DropOffLocation,
                ApproverID = null,
                PosterID = userId,
                ApprovalDate = null,
                PostingDate = null,
            };
           
            if (ModelState.IsValid)
            {
                if (fileInputs == null)
                {
                    newTour.PhotoPath = "/no-image.jpg";
                }
                else
                {
                    var rImage = await _imageService.UploadTourImages(fileInputs, newTour);
                    if(rImage == null)
                    {
                        return View(); 
                    }
                    string[] paths = new string[fileInputs.Count];

                    for(int i = 0; i < fileInputs.Count; i++)
                    {
                        paths[i] = fileInputs[i].FileName;
                    }

                    newTour.PhotoPath = paths.ToString();
                }

               

                var r = await _tourRepository.AddTour(newTour);
                if(r == false)
                {
                    return RedirectToAction("Error");

                }
                return RedirectToAction("Index");

            }
            return RedirectToAction("Error");

        }

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {


            var tour = await _tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }
            
            return View(tour);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id, Tour tour)
        {
            if(id ==null || tour == null)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    var r = await _tourRepository.UpdateTour(tour);
                    if(r == false)
                    {
                        return RedirectToAction("Error");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error");
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");

        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            var tour = await _tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

       
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var tour = await _tourRepository.GetTourById(id);

            if (tour != null)
            {
                var r = await _tourRepository.UpdateTour(tour);
                if(r == false)
                {
                    return RedirectToAction("Error");
                }
                return RedirectToAction("Index");

            }
            return RedirectToAction("Error");
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
