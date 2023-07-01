using Homies.Contracts;
using Homies.Data;
using Homies.Data.Entities;
using Homies.Models.Events;
using Homies.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext _data;

        public EventService(HomiesDbContext data)
        {
            _data = data;
        }

        public async Task AddNewEvent(AddEventViewModel model, string organiserId)
        {
            var newEvent = new Event()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId,
                OrganiserId = model.OrganiserId,
                Organiser = model.Organiser
            };

            await _data.Events.AddAsync(newEvent);
            await _data.SaveChangesAsync();
        }

        public async Task AddToJoined(string userId, AddEventViewModel model)
        {
            
                var eventParticipant = new EventParticipant()
                {
                    EventId = model.Id,
                    HelperId = userId
                };

                await _data.EventsParticipants.AddAsync(eventParticipant);
                await _data.SaveChangesAsync();
            
        }

        public async Task EditEventAsync(int id, AddEventViewModel model)
        {
            var entity = await _data.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            entity.Id=model.Id;
            entity.Name=model.Name;
            entity.Description=model.Description;
            entity.Organiser=model.Organiser;
            entity.Start = model.Start;
            entity.End = model.End;
            entity.TypeId = model.TypeId;
            entity.Organiser = model.Organiser;

            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetAllEvents()
        {
            var eventEntities = await _data
                .Events
                .Select(e => new AllEventsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    Type = e.Type.Name,
                    TypeId = e.TypeId
                }).ToArrayAsync();

            return eventEntities;
        }

        public async Task<IEnumerable<JoinedEventViewModel>> GetAllJoinedEvents(string userId)
        {
            var entities = await _data.Events.Where(u => u.OrganiserId == userId)
                .Select(e => new JoinedEventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    Type = e.Type.Name,
                    TypeId = e.TypeId
                }).ToListAsync();

            return entities;
        }

        public async Task<DetailsViewModel> GetDetails(int id,string userId)
        {
            var entity = await _data.Events.FirstOrDefaultAsync(e => e.Id == id);

            var model = new DetailsViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedOn = entity.CreatedOn,
                Description = entity.Description,
                End = entity.End,
                Start = entity.Start,
                Type = entity.Type,
                TypeId = entity.TypeId
            };

            return model;
        }

        public async Task<AddEventViewModel?> GetEventByIdAsync(int id)
        {
            return await _data.Events
                .Where(e => e.Id == id)
                .Select(e => new AddEventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    Description = e.Description,
                    End = e.End,
                    TypeId = e.TypeId
                }).FirstOrDefaultAsync();
        }

        public async Task<EventParticipant?> GetEventParticipantIdAsync(int id)
        {
            var eventParticipant = await _data.EventsParticipants
                .Where(e => e.EventId == id)
                .Select(e => new EventParticipant()
                {
                    HelperId = e.HelperId,
                    EventId = e.EventId,
                    Event = e.Event,
                    Helper = e.Helper,
                }).FirstOrDefaultAsync();

            return eventParticipant;
        }

        public async Task<AddEventViewModel> GetNewAddEventViewModelTypes()
        {
            var types = await _data.Types.Select(t => new TypeViewModel()
            {
                Id = t.Id,
                Name = t.Name,
            }).ToArrayAsync();

            var model = new AddEventViewModel()
            {
                Types = types
            };

            return model;
        }

        public async Task RemoveEvent(string userId, EventParticipant model)
        {
            bool isEventExisting =
                await _data.EventsParticipants.AnyAsync(e => e.EventId == model.EventId && e.HelperId == userId);

            if (isEventExisting)
            {
                _data.EventsParticipants.Remove(model);
                await _data.SaveChangesAsync();
            }
        }
    }
}
