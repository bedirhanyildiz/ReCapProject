using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetailsById(int id)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result =
                    from r in context.Rentals.Where(c => c.CarId == id)
                    join c in context.Cars on r.CarId equals c.Id
                    join cu in context.Customers on r.CustomerId equals cu.Id
                    join b in context.Brands on c.BrandId equals b.BrandId
                    join u in context.Users on cu.UserId equals u.Id
                    select new RentalDetailDto
                    {
                        Id = r.Id,
                        CarId = c.Id,
                        BrandName = b.BrandName,
                        CustomerName = cu.CompanyName,
                        UserName = $"{u.FirstName} {u.LastName}",
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }

        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result =
                    from r in context.Rentals
                    join c in context.Cars on r.CarId equals c.Id
                    join cu in context.Customers on r.CustomerId equals cu.Id
                    join b in context.Brands on c.BrandId equals b.BrandId
                    join u in context.Users on cu.UserId equals u.Id
                    select new RentalDetailDto
                    {
                        Id = r.Id,
                        CarId = c.Id,
                        BrandName = b.BrandName,
                        CustomerName = cu.CompanyName,
                        UserName = $"{u.FirstName} {u.LastName}",
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }
    }
}
