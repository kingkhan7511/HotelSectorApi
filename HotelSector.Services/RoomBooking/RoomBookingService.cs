using HostelSector.Models.Base;
using HotelSector.ApplicationServices.Session;
using HotelSector.Models.RoomBooking;
using HotelSector.Shared;
using HotelSectorDataAccess.Base;
using System;
using System.Linq;

namespace HotelSector.ApplicationServices.RoomBooking
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelSectorSession _hotelSectorSession;
        private readonly long? currentUserId;
        public RoomBookingService(IUnitOfWork unitOfWork,
            IHotelSectorSession hotelSectorSession)
        {
            _unitOfWork = unitOfWork;
            _hotelSectorSession = hotelSectorSession;
            currentUserId = _hotelSectorSession.GetUserId();
        }
        public ApiResponseDto AddUpdateBooking(RoomBookingInputDto inputDto)
        {
            ApiResponseDto responseDto = new();

            if (currentUserId == null)
            {
                return responseDto.DynamicResponse(HotelSectorStatusCode.UnAthorized,
                     false,
                     new ErrorModel()
                     {
                         ErrorMessage = HotelSectorMessages.UnAuthorize,
                     });
            }
            if (inputDto.Id == null || inputDto.Id < 1)
            {
                return BookRoom(inputDto);
            }
            else
            {
                return UpdateBookedRoom(inputDto);
            }
        }

        public ApiResponseDto DeleteBooking(RoomBookingDeleteInputDto inputDto)
        {
            ApiResponseDto responseDto = new();

            try
            {
                var entity = _unitOfWork.RoomBooking.Find(x => x.Id == inputDto.Id && x.UserId == currentUserId).FirstOrDefault();
                if (entity == null)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.InValidRoomBookingId,
                           false,
                           new ErrorModel()
                           {
                               ErrorMessage = HotelSectorMessages.InvalidRoomBooking,
                           });
                }
                else
                {
                    _unitOfWork.RoomBooking.Remove(entity);
                    _unitOfWork.Complete();
                    return responseDto.SucessResponse(HotelSectorMessages.RoomBookingDelete);
                }
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }
        }

        public ApiResponseDto GetMyBookedRooms(GetMyBookedRoomsInputDto inputDto)
        {
            ApiResponseDto responseDto = new();

            try
            {
                long? currentUserId = _hotelSectorSession.GetUserId();
                if (currentUserId == null)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.UnAthorized,
                         false,
                         new ErrorModel()
                         {
                             ErrorMessage = HotelSectorMessages.UnAuthorize,
                         });
                }
                var resultListDto = _unitOfWork.RoomBooking.GetMyBookedRoom(currentUserId ?? 0, inputDto);

                return responseDto.SucessResponse(string.Empty, resultListDto);
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }
        }
        private ApiResponseDto BookRoom(RoomBookingInputDto inputDto)
        {
            ApiResponseDto responseDto = new();
            try
            {
                var room = _unitOfWork.Room.GetById(inputDto.RoomId);
                if (room == null)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.InValidRoomId,
                                 false,
                                 new ErrorModel()
                                 {
                                     ErrorMessage = HotelSectorMessages.InValidRoomId,
                                 });
                }
                bool isRoomAvailable = _unitOfWork.RoomBooking.CheckRoomAvailabilty(inputDto, false);
                if (isRoomAvailable == false)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.RoomIsNotAvailable,
                                       false,
                                       new ErrorModel()
                                       {
                                           ErrorMessage = HotelSectorMessages.RoomIsNotAvailable,
                                       });
                }
                int howManyHours = (int)(inputDto.EndDate.Subtract(inputDto.StartDate).TotalHours);


                inputDto.TotalCharges = howManyHours * room.RentPerHour;
                inputDto.HowManyHours = howManyHours;

                _unitOfWork.RoomBooking.BookRoom(currentUserId ?? 0, inputDto);
                room.IsAvailable = false;
                _unitOfWork.Room.Update(room);
                _unitOfWork.Complete();
                return responseDto.SucessResponse(HotelSectorMessages.RoomHasBeenBooked);
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }
        }
        private ApiResponseDto UpdateBookedRoom(RoomBookingInputDto inputDto)
        {
            ApiResponseDto responseDto = new();
            try
            {
                var bookedRoom = _unitOfWork.RoomBooking.Find(x => x.Id == inputDto.Id && x.UserId == currentUserId).FirstOrDefault();
                if (bookedRoom == null)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.RoomIsNotBookedByYou,
                                      false,
                                      new ErrorModel()
                                      {
                                          ErrorMessage = HotelSectorMessages.RoomIsNotBookedByYou,
                                      });
                }
                var room = _unitOfWork.Room.GetById(inputDto.RoomId);
                if (room == null)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.InValidRoomId,
                                 false,
                                 new ErrorModel()
                                 {
                                     ErrorMessage = HotelSectorMessages.InValidRoomId,
                                 });
                }
                bool isRoomAvailable = _unitOfWork.RoomBooking.CheckRoomAvailabilty(inputDto, true);
                if (isRoomAvailable == false)
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.RoomIsNotAvailable,
                                       false,
                                       new ErrorModel()
                                       {
                                           ErrorMessage = HotelSectorMessages.RoomIsNotAvailable,
                                       });
                }
                int howManyHours = (int)(inputDto.EndDate.Subtract(inputDto.StartDate).TotalHours);


                bookedRoom.TotalCharges = howManyHours * room.RentPerHour;
                bookedRoom.HowManyHours = howManyHours;
                bookedRoom.StartDate = inputDto.StartDate;
                bookedRoom.EndDate = inputDto.EndDate;
                bookedRoom.EditBy = currentUserId;
                bookedRoom.EditDateTime = DateTime.Now;

                room.IsAvailable = false;
                _unitOfWork.Room.Update(room);
                _unitOfWork.Complete();
                return responseDto.SucessResponse(HotelSectorMessages.BookingHasBeenUpdate);
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }
        }
    }
}
