using AutoMapper;
using CRUD_PRAC.Constants;
using CRUD_PRAC.Data;
using CRUD_PRAC.DTOs.AvailablityDTO;
using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace CRUD_PRAC.Services
{
    public class AvailablityService : IAvailablityService
    {
        private Slot CheckEnumSlot(string slotName)
        {
            switch (slotName)
            {
                case "EarlyMorning": return Slot.EarlyMorning;
                case "Morning": return Slot.Morning;
                case "AfterNoon": return Slot.AfterNoon;
                case "Evening": return Slot.Evening;
                default: return Slot.Morning;
            }

        }

        private IMapper _mapper;
        private DataContext _context;

        public AvailablityService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceResponse<AvailablityEntity>> AddAvailablity(AvailablityEntity newAvailablity)
        {
            var serviceResponse = new ServiceResponse<AvailablityEntity>();

            foreach (Slots? rec in newAvailablity.Availablity)
            {
                foreach (var item in rec.Slot)
                {
                    var mapped = _mapper.Map<Availablity>(newAvailablity);
                    _mapper.Map(rec, mapped);
                    mapped.Slot = item.ToString();


                    var availablitiesRec = await _context.TempAvailablities.Where(row => row.PlayerId == newAvailablity.PlayerId).ToArrayAsync();

                    if (availablitiesRec.Length > 0)
                    {
                        _context.TempAvailablities.RemoveRange(availablitiesRec);
                        serviceResponse.Message = "Availablity updated successfully";
                    }
                    else
                    {
                        serviceResponse.Message = "Availablity saved successfully";
                    }
                    _context.TempAvailablities.Add(mapped);
                }
            }

            await _context.SaveChangesAsync();
            serviceResponse.Data = newAvailablity;
            return serviceResponse;
        }


        public async Task<ServiceResponse<AvailablityDTO>> GetAvailablityByPlayerId(int playerId)
        {
            var serviceResponse = new ServiceResponse<AvailablityDTO>();

            try
            {
                var playerAvailablities = await _context.TempAvailablities.Where(x => x.PlayerId == playerId).ToListAsync();
                var playerDetail = await _context.TempPlayers.FirstOrDefaultAsync(x => x.Id == playerId);

                if (playerDetail != null) { 
                    // prepare player details with availablities object
                    AvailablityDTO availDto = new AvailablityDTO();
                    List<Slots> newplayerSLot = new List<Slots>();

                    availDto.PlayerId = playerId;
                    availDto.Name = playerDetail.Name;
                    availDto.Email = playerDetail.Email;

                    var newSlotList = playerAvailablities.Select(x => x.Day).Distinct().ToList();
                    foreach (var day in newSlotList)
                    {
                        Slots newSlot = new Slots();
                        newSlot.Day = day;
                        var allSlots = playerAvailablities.Where(x => x.Day == day).Select(x => x.Slot).ToList();
                        List<Slot> newSlotPlayerList = new List<Slot>();
                        foreach (var slot in allSlots)
                        {
                            var sloDetails = CheckEnumSlot(slot);
                            newSlotPlayerList.Add(sloDetails);
                        }
                        newSlot.Slot = newSlotPlayerList;
                        newplayerSLot.Add(newSlot);
                    }
                    availDto.Availablity = newplayerSLot;


                    if (availDto != null)
                    {
                        serviceResponse.Data = availDto;
                        serviceResponse.Message = "Data Found";

                    }
                    else
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "No Data Found !";
                    }

                }

                return serviceResponse;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServiceResponse<FetchedPlayersList>> GetCommonAvaildAsync(int[] playerIds, string type = "Singles" )
        {
            var numberOfPlayers = type == "Singles" ? 2 : 4;
            // will add middleware to validate event type and player ids params
            var serviceResponse = new ServiceResponse<FetchedPlayersList>();
            try
            {
                var playerAvailablities = await _context.TempAvailablities.Where(x => playerIds.Contains(x.PlayerId)).ToListAsync();
                var playerDetails = await _context.TempPlayers.Where(x => playerIds.Contains(x.Id)).ToListAsync();

                //var pData = await _context.Users.Join(
                //                _context.Availablities,
                //                pDetails => pDetails.Id,
                //                pAvail => pAvail.PlayerId,
                //                (pDetails, pAvail) => new
                //                {
                //                    PlayerId = pDetails.Id,
                //                    Day = pAvail.Day,
                //                    Slot = pAvail.Slot,
                //                    Name = pDetails.Name,
                //                    Email = pDetails.Email,
                //                }
                //            ).ToListAsync();


                var players_availablities = from player in playerAvailablities
                                            group player by new { player.Day, player.Slot } into slotGroup
                                            where slotGroup.Count() > numberOfPlayers - 1
                                            select new { day = slotGroup.Key.Day, slot = slotGroup.Key.Slot };

                var distinctPlayerIds = playerDetails.Select(x => x.Id).Distinct()
                     .ToList();

                List<AvailablityDTO> newPlayersAvailabities = new List<AvailablityDTO>();
                // prepare player details with availablities object
                foreach (var pId in distinctPlayerIds)
                {
                    AvailablityDTO availDto = new AvailablityDTO();
                    List<Slots> newplayerSLot = new List<Slots>();
                    availDto.PlayerId = pId;
                    availDto.Name = playerDetails?.FirstOrDefault(p => p.Id == pId)?.Name;
                    availDto.Email = playerDetails?.FirstOrDefault(p => p.Id == pId)?.Email; ;

                    var newSlotList = playerAvailablities.Where(x => x.PlayerId == pId).Select(x => x.Day).Distinct().ToList();
                    foreach (var day in newSlotList)
                    {
                        Slots newSlot = new Slots();
                        newSlot.Day = day;
                        var allSlots = playerAvailablities.Where(x => x.PlayerId == pId && x.Day == day).Select(x => x.Slot).ToList();
                        List<Slot> newSlotPlayerList = new List<Slot>();
                        foreach (var slot in allSlots)
                        {
                            var sloDetails = CheckEnumSlot(slot);
                            newSlotPlayerList.Add(sloDetails);
                        }
                        newSlot.Slot = newSlotPlayerList;
                        newplayerSLot.Add(newSlot);
                    }
                    availDto.Availablity = newplayerSLot;
                    newPlayersAvailabities.Add(availDto);
                }

                var availtest = players_availablities.Select(x => x.day).Distinct();

                // prepare common matched availablities object
                List<Slots> matchedList = new List<Slots>();
                foreach (var day in players_availablities.Select(x => x.day).Distinct())
                {
                    Slots newSlot = new Slots();
                    newSlot.Day = day;
                    var allSlots = players_availablities.Where(x => x.day == day).Select(x => x.slot).ToList();
                    List<Slot> newPlayerSlotList = new List<Slot>();
                    foreach (var slot in allSlots)
                    {
                        var sloDetails = CheckEnumSlot(slot);
                        newPlayerSlotList.Add(sloDetails);
                    }
                    newSlot.Slot = newPlayerSlotList;
                    matchedList.Add(newSlot);

                }


                if (newPlayersAvailabities != null)
                {
                    FetchedPlayersList fetchDto = new FetchedPlayersList();
                    fetchDto.MatchedAvailablity = newPlayersAvailabities.Count() == numberOfPlayers ? matchedList : new List<Slots>();
                    fetchDto.PlayersDetails = newPlayersAvailabities;
                    serviceResponse.Data = fetchDto;
                    serviceResponse.Message = fetchDto.PlayersDetails.Count() > 0 ? "Data Found" : "No Availablity Found";
                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No Data Found !";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return serviceResponse;
        }      

    }
}
