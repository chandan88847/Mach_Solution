using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalServiceAPI.Data;
using RentalServiceAPI.Models;

namespace RentalServiceAPI.Services
{
    public class RentalService
    {
        private readonly RentalServiceDbContext _context;

        public RentalService(RentalServiceDbContext context)
        {
            _context = context;
        }

        public async Task<RentalDetails> CreateRentalDetailsAsync([FromBody] RentalDetails rentalDetails)
        {
            _context.RentalDetails.Add(rentalDetails);
            await _context.SaveChangesAsync();
            return rentalDetails;
        }
        public async Task<RentalDetails> GetAllRentalDetailsByIdAsync([FromBody] Guid id)
        {
            return await _context.RentalDetails.FindAsync(id);
        }

        public async Task<IEnumerable<RentalDetails>> GetAllRentalDetailsAsync()
        {
            return await _context.RentalDetails.ToListAsync();
        }

        public async Task<RentalDetails> UpdateRentalServiceAsync(RentalDetails rentalDetails)
        {
            var Id = rentalDetails.RentalId;
            var existingDetails = _context.RentalDetails.Find(Id);
            if (existingDetails == null)
            {
                return null;
            }

            _context.Entry(existingDetails).CurrentValues.SetValues(rentalDetails);
            await _context.SaveChangesAsync();
            return rentalDetails;
        }

        public async Task<bool> DeleteRentalDetailsAsync(Guid id)
        {
            var rentalDetails = await _context.RentalDetails.FindAsync(id);
            if(rentalDetails == null)
            {
                return false;
            }
            _context.RentalDetails.Remove(rentalDetails);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
