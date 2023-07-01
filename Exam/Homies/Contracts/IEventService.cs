using Homies.Data.Entities;
using Homies.Models.Events;

namespace Homies.Contracts
{
	public interface IEventService
	{
		Task<IEnumerable<AllEventsViewModel>> GetAllEvents();

		Task AddNewEvent(AddEventViewModel model,string organiserId);

		Task<AddEventViewModel> GetNewAddEventViewModelTypes();

        Task<IEnumerable<JoinedEventViewModel>> GetAllJoinedEvents(string userId);

		Task AddToJoined(string userId,AddEventViewModel model);

		Task<AddEventViewModel?> GetEventByIdAsync(int id);

		Task RemoveEvent(string userId, EventParticipant model);

		Task<EventParticipant?> GetEventParticipantIdAsync(int id);

		Task EditEventAsync(int id, AddEventViewModel model);

		Task<DetailsViewModel> GetDetails(int id,string userId);
    }
}
