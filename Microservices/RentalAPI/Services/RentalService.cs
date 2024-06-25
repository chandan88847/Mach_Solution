using Microsoft.EntityFrameworkCore;
using RentalAPI.Data;
using RentalAPI.Dto;
using RentalAPI.Models;

namespace RentalAPI.Services
{
    public class RentalService
    {
        private readonly RentalDbContext _rentalDbContext;

        public RentalService(RentalDbContext rentalDbContext)
        {
            _rentalDbContext = rentalDbContext;
        }

        public async Task CreateRentalAsync(RentalDetails rentalDetails)
        {
            rentalDetails.RentedDate = DateTime.Now;
            _rentalDbContext.Add(rentalDetails);
            await _rentalDbContext.SaveChangesAsync();

        }
    }
}
