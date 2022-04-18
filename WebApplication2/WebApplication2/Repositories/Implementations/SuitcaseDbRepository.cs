using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApplication2.DTO;
using WebApplication2.Models;
using WebApplication2.Repositories.Interfaces;

namespace WebApplication2.Repositories.Implementations
{
    public class SuitcaseDbRepository : ISuitcaseDbRepository
    {
        private readonly VacationContext _context;
        public SuitcaseDbRepository(VacationContext context)
        {
            _context = context;
        }

        public async Task<ICollection<SuitcaseDto>> GetSuitcaseFromDbAsync(string name)
        {
            var list = new List<SuitcaseDto>();
            var cityFromDb = await _context.Cities.SingleOrDefaultAsync(x => x.Name == name);
            if (cityFromDb == null)
            {
                return null;
            }
            var suitcaseFromDb = await _context.Suitcases
                .Select(x => new Suitcase
                {
                    IdSuitcase = x.IdSuitcase,
                    Name = x.Name,
                    IdCity = x.IdCity,
                    IdUser = x.IdUser
                }).Where(x=> x.IdCity==cityFromDb.IdCity).ToArrayAsync();
            if (suitcaseFromDb == null)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < suitcaseFromDb.Length; i++)
                {
                    var items = await _context.Items
                        .Include(suitcaseItem => suitcaseItem.SuitcaseItems)
                        .Where(x => x.SuitcaseItems.Any(c =>
                            c.IdItem == x.IdItem && c.IdSuitcase == suitcaseFromDb[0].IdSuitcase))
                        .Select(x => new ItemDto
                        {
                            IdItem = x.IdItem,
                            Name = x.Name,
                            IsPacked = x.IsPacked,
                            Quantity = x.Quantity

                        }).ToArrayAsync();
                    var suitcase = new SuitcaseDto
                    {
                        CityName = cityFromDb.Name,
                        IdSuitcase = suitcaseFromDb[0].IdSuitcase,
                        Name = suitcaseFromDb[0].Name,
                        items = items
                    };
                    list.Add(suitcase);
                }
            }
            return list;
        }
        public async Task<string> PostItemFromDbAsync(ItemRequestDto itemRequestDto, int idSuitcase)
        {
            var suitcase = await GetSuitcase(idSuitcase);
            
            if (suitcase==null)
            {
                throw new Exception("No such suitcase");
            }

            var item = await _context.Items.AddAsync(new Item
            {
                IsPacked = itemRequestDto.IsPacked,
                Quantity = itemRequestDto.Quantity,
                Name = itemRequestDto.Name
            });
            await _context.SaveChangesAsync();
            var newItem= await _context.Items.OrderBy(x=>x.IdItem).LastAsync();
            Console.WriteLine(newItem.IdItem);
            var newSuitcaseItem = await _context.SuitcaseItems.AddAsync(new SuitcaseItem
            {
                IdSuitcase = idSuitcase,
                IdItem = newItem.IdItem
            });
            await _context.SaveChangesAsync();
            return "A new item has been added to the suitcase";
        }

        public async Task<string> DeleteItem(int idItem)
        {
            var item = await GetItem(idItem);
           
            if (item == null)
            {
                return "not found item";
            }
            
            var suitcaseItem = await  _context.SuitcaseItems.Where(x=>x.IdItem==idItem).SingleOrDefaultAsync();
            if (suitcaseItem != null)
            {
                _context.Remove(suitcaseItem);
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();
            return "Item removed correctly";
        }

        public async Task<string> DeleteSuitcase(int idSuitcase)
        {
            var suitcase = await GetSuitcase(idSuitcase);
            if (suitcase == null)
            {
                return "not found suitcase";
            }
            var suitcaseItem = await _context.SuitcaseItems.Where(x => x.IdSuitcase == idSuitcase).ToArrayAsync();
            for (int i = 0; i < suitcaseItem.Length; i++)
            {
                _context.Remove(suitcaseItem[i]);
            }
            _context.Remove(suitcase);
            
            await _context.SaveChangesAsync();
            return "Suitcase removed correctly";
        }

        public async Task<string> UpdateItem(int idItem)
        {
            var item = await GetItem(idItem);
            if (item == null)
            {
                return "not found item";
            }

            if (item.IsPacked == 1)
            {
                return "The entered item is already packed";
            }

            item.IsPacked = 1;
            await _context.SaveChangesAsync();
            return "The entered item was marked as packed";
        }

        public async Task<ICollection<Suitcase>> GetSuitcaseByTextDbAsync(string text)
        {
            Suitcase[] suitcase;
            suitcase = await _context.Suitcases.Where(x => x.Name.Contains(text)).Select(x => new Suitcase
            {
                IdSuitcase = x.IdSuitcase,
                IdCity = x.IdCity,
                IdUser = x.IdUser,
                Name = x.Name
            }).ToArrayAsync();
            Console.WriteLine(suitcase.Length);
            if (suitcase.Length==0)
            {
                return null;
            }

            return suitcase;
        }
        public async Task<Suitcase> GetSuitcase(int idSuitcase)
        {
            var suitcase = await _context.Suitcases.Where(x => x.IdSuitcase == idSuitcase).SingleOrDefaultAsync();
            return suitcase;
        }
        public async Task<Item> GetItem(int idItem)
        {
            var item = await _context.Items.Where(x=>x.IdItem==idItem).SingleOrDefaultAsync();
            return item;
        }
    }
}