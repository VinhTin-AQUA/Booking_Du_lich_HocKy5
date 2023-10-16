﻿using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.City;
using WebApi.DTOs.Hotel;
using WebApi.DTOs.Room;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        [HttpPost("add-room")]
        public async Task<IActionResult> AddRoom(AddRoomDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var room = new Room
            {
                RoomNumber = model.RoomNumber,
                Name = model.Name,
                Description = model.Description,

            };
            var result = await roomRepository.AddRoom(room);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add hotel" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add room successfully", newRoom = room }));
        }

        [HttpGet("get-all-room")]

        public async Task<IActionResult> GetAllRoom()
        {
            var rooms = await roomRepository.GetAllRoom();
            return Ok(rooms.ToList());
        }

        [HttpGet("get-room-by-id")]
        public async Task<IActionResult> GetRoomById([FromQuery] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var room = await roomRepository.GetRoomById(id);
            if (room == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", mesage = "Room not found" }));
            }

            return Ok(room);
        }

        [HttpDelete("delete-room")]
        public async Task<IActionResult> DeleteRoom([FromQuery] int? roomId)
        {
            if (roomId == null)
            {
                return BadRequest();
            }
            var room = await roomRepository.GetRoomById(roomId);

            if (room == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room was not found" }));
            }

            var r = await roomRepository.DeleteRoom(room);
            if (r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Room delete successfully" }));
        }

        [HttpGet("search-room")]

        public async Task<IActionResult> SearchRoom(string available)
        {
            var check = true;
            if(String.Compare(available.ToLower(), "co san") == 0) 
            {
                check = true;
                
            }
            else // logic chưa chặt
            {
                check = false;
            }

            var rooms = await roomRepository.SearchRoom(check);
            return Ok(new JsonResult(new { title = "Success", rooms }));
        }

        [HttpPut("update-room")]
        public async Task<IActionResult> UpdateRoom([FromForm] EditRoomDTO model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room" }));
            }

            var roomExisted = await roomRepository.GetRoomById(model.Id);
            if (roomExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room was not existed" }));
            }

            
            roomExisted.RoomNumber = model.RoomNumber;
            roomExisted.Name = model.Name;
            roomExisted.Description = model.Description;


            var resultUpdate = await roomRepository.UpdateRoom(roomExisted);
            if (resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room" }));
            }
            return Ok();
        }
    }
}