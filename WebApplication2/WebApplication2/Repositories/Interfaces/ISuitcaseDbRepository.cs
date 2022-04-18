using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.DTO;
using WebApplication2.Models;

namespace WebApplication2.Repositories.Interfaces
{
    public interface ISuitcaseDbRepository
    {
        Task<ICollection<SuitcaseDto>> GetSuitcaseFromDbAsync(string name);

        Task<string> PostItemFromDbAsync(ItemRequestDto itemRequestDto, int idSuitcase);
        Task<string> DeleteItem(int idItem);
        Task<string> DeleteSuitcase(int idSuitcase);
        Task<string> UpdateItem(int idItem);
        Task<ICollection<Suitcase>> GetSuitcaseByTextDbAsync(string text);
    }
}