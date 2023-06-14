using Contacts.Models.Contact;

namespace Contacts.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactViewModel>> GetAllContactsAsync();

        Task AddNewContactAsync(ContactViewModel model);

        Task EditContactAsync(ContactViewModel model,int id);

        Task<ContactViewModel?> FindContactByIdAsync(int id);

        Task<IEnumerable<ContactViewModel>> GetAllMineContactsAsync(string userId);

        Task AddContactToUserCollectionAsync(string userId, int contactId);

        Task DeleteContactAsync(int id);
    }
}
