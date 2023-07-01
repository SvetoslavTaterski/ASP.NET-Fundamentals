using Homies.Contracts;
using Homies.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService service)
        {
            _eventService = service;
        }

        public async Task<IActionResult> All()
        {
            var eventModels = await _eventService.GetAllEvents();

            return View(eventModels);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel eventViewModel = await _eventService.GetNewAddEventViewModelTypes();

            return View(eventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventService.AddNewEvent(model, userId);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Joined()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var models = await _eventService.GetAllJoinedEvents(userId);

            return View(models);
        }

        public async Task<IActionResult> Join(int id)
        {
            var currEvent = await _eventService.GetEventByIdAsync(id);

            if (currEvent == null)
            {
                return RedirectToAction("All");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventService.AddToJoined(userId, currEvent);

            return RedirectToAction("Joined");
        }

        public async Task<IActionResult> Leave(int id)
        {
            var currEvent = await _eventService.GetEventParticipantIdAsync(id);

            if (currEvent == null)
            {
                return RedirectToAction("All");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventService.RemoveEvent(userId, currEvent);

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currEvent = await _eventService.GetEventByIdAsync(id);

            return View(currEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _eventService.EditEventAsync(id, model);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Details(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await _eventService.GetDetails(id, userId);

            return View(model);
        }
    }
}
