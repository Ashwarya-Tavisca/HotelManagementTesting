using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagementSystem.Controllers
{
    public class HotelController : ApiController
    {
        private static int _count = 0;
        private static List<Hotel> _hotels = new List<Hotel>();

        public IHttpActionResult GetAllHotels()
        {
            try
            {
                if (_hotels.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Currently we have no hotel details");

                return Ok(_hotels);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        public IHttpActionResult GetHotelById(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, "Model state is invalid");

                Hotel hotel = _hotels.Find(list => list.Id == id);
                if (hotel == null)
                    return Content(HttpStatusCode.NotFound, "Requested Hotel ID is not Available");

                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        public IHttpActionResult PostNewHotel(Hotel hotel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, "Model state is invalid");

                var hotelPresent = (_hotels.Find(hotelList => hotelList.Id == hotel.Id) != null) ? true : false;
                if (hotelPresent)
                    return Content(HttpStatusCode.BadRequest, "DuplicateEntry");
                else
                    _hotels.Add(hotel);
                _hotels.Add(hotel);
                return Ok(_hotels);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        public IHttpActionResult PutBookHotel(int id, String roomType, int noOfRoomsBooking)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, "Model state is invalid");

                if (noOfRoomsBooking <= 0)
                    return Content(HttpStatusCode.BadRequest, "Number of rooms booked should be greater than 0");

                var hotelDetails = _hotels.SingleOrDefault(list => list.Id == id);
                if (hotelDetails == null)
                    return Content(HttpStatusCode.NotFound, "Invalid Hotel ID");

                var rooms = hotelDetails.Rooms;
                if (rooms == null)
                    return Content(HttpStatusCode.NotFound, "Requested Hotel didn't provided the room details");

                var requestedRoomType = rooms.SingleOrDefault(list => list.RoomType.Equals(roomType));
                if (requestedRoomType == null)
                    return Content(HttpStatusCode.NotFound, "Requested Room type is not available from requested hotel ID");

                if (requestedRoomType.NoOfRoomsAvailable >= noOfRoomsBooking)
                {
                    requestedRoomType.NoOfRoomsAvailable -= noOfRoomsBooking;
                    return Ok("Requested Room is Booked");
                }
                else
                    return Content(HttpStatusCode.RequestedRangeNotSatisfiable, "Requested number of rooms not available");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        public IHttpActionResult DeleteRemoveHotel(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, "Model state is invalid");

                var hotelToRemove = _hotels.SingleOrDefault(list => list.Id == id);
                if (hotelToRemove == null)
                    return Content(HttpStatusCode.NotFound, "Invalid Hotel ID");

                _hotels.Remove(hotelToRemove);
                return Ok("Requested Hotel ID is deleted");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }
}