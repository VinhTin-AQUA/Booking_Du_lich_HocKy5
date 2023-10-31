﻿using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Tour;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi1.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourRepository tourRepository;
        private readonly IImageService imageService;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IAuthenRepository authenRepository;
        private readonly ICityRepository cityRepository;
        private readonly ITourTypeRepository tourTypeRepository;

        public TourController(ITourRepository tourRepository,
            IWebHostEnvironment hostEnvironment,
            IImageService imageService,
            IAuthenRepository authenRepository,
            ICityRepository cityRepository,
            ITourTypeRepository tourTypeRepository)
        {
            this.tourRepository = tourRepository;
            this.hostEnvironment = hostEnvironment;
            this.imageService = imageService;
            this.authenRepository = authenRepository;
            this.cityRepository = cityRepository;
            this.tourTypeRepository = tourTypeRepository;
        }

        [HttpPost("add-tour")]
        public async Task<IActionResult> AddTour([FromForm] List<IFormFile> files, [FromForm] AddTourDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var Poster = await authenRepository.GetUserById(model.PosterID);
            var tour = new Tour
            {
                TourName = model.TourName,
                PosterID = model.PosterID,
                ApproverID = null,
                Poster = Poster,
                Approver = null,
                CityId = model.CityId,
                TourTypeId = model.TourTypeId,
                DepartureLocation = model.DepartureLocation,
                DropOffLocation = model.DropOffLocation,
                TourAddress = model.TourAddress,
                Overview = model.Overview,
                Schedule = model.Schedule,
                PostingDate = DateTime.Now,
                PhotoPath = ""
            };
            var result = await tourRepository.AddTour(tour);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add tour" }));
            }

            // lưu hình ảnh
            string urlImgFolder = "";
            if (files != null)
            {
                urlImgFolder = await imageService.UploadTourImages(files, tour);
                tour.PhotoPath = urlImgFolder;
            }
            await tourRepository.UpdateTour(tour);
            return Ok(new JsonResult(new { title = "Success", message = "Add Tour successfully", newTour = tour }));
        }

        [HttpPut("update-tour")]
        public async Task<IActionResult> UpdateTour(List<IFormFile> files, [FromForm] UpdateTourDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var tour = await tourRepository.GetTourById(model.TourId);

            if (tour == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Tour was not found" }));
            }

            var city = await cityRepository.GetCityById(model.CityId);
            var tourType = await tourTypeRepository.GetTourTypeById(model.TourTypeId);

            tour.TourName = model.TourName;
            tour.TourAddress = model.TourAddress;
            tour.DepartureLocation = model.DepartureLocation;
            tour.DropOffLocation = model.DropOffLocation;
            tour.Schedule = model.Schedule;
            tour.Overview = model.Overview;
            tour.CityId = model.CityId;
            tour.TourTypeId = model.TourTypeId;
            tour.City = city;
            tour.TourType = tourType;
            tour.PosterID = model.PosterID;
            tour.ApproverID = model.ApproverID;
            

            // lưu hình ảnh
            string urlImgFolder = "";
            if (files != null)
            {
                urlImgFolder = await imageService.UploadTourImages(files, tour);
                tour.PhotoPath = urlImgFolder;
            }

            var r = await tourRepository.UpdateTour(tour);
            if (r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Update tour successfully" }));
        }


        [HttpGet("get-all-tours")]
        public async Task<IActionResult> GetAllTours()
        {
            var tours = await tourRepository.GetAllTours();
            return Ok(tours);
        }

        [HttpGet("get-tours-of-poster")]
        public async Task<IActionResult> GetToursOfPoster(string posterId)
        {
            var list = await tourRepository.GetToursOfPoster(posterId);
            return Ok(list);
        }

        [HttpGet("get-tour-by-id")]
        public async Task<IActionResult> GetTourById([FromQuery] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var tour = await tourRepository.GetTourById(id);
            if (tour == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", mesage = "Tour not found" }));
            }

            return Ok(tour);
        }

        [HttpDelete("delete-tour")]
        public async Task<IActionResult> DeleteTour([FromQuery] int? tourId)
        {
            if (tourId == null)
            {
                return BadRequest();
            }
            var tour = await tourRepository.GetTourById(tourId);

            if (tour == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Tour was not found" }));
            }

            var r = await tourRepository.DeleteTour(tour);
            if (r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Tour delete successfully" }));
        }

        [HttpPut("add-type-to-tour")]
        public async Task<IActionResult> AddTypeToTour([FromQuery] int? tourTypeId, [FromQuery] int? tourId)
        {
            if(tourTypeId == null || tourId == null)
            {
                return BadRequest();
            }

            var tour = await tourRepository.GetTourById(tourId);
            var tourType = await tourTypeRepository.GetTourTypeById(tourTypeId);
            if(tour == null || tourType == null)
            {
                return BadRequest();
            }

            var r = await tourRepository.AddTypeToTour(tourType, tour);
            if(r == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
