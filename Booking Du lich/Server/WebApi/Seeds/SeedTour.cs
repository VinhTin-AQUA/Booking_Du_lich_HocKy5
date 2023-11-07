using Bogus;
using System;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Data;

namespace WebApi.Seeds
{
    public static class SeedTour
    {
        public static async Task SeedTours(ApplicationDbContext context, 
            string[] agentTourIds, 
            string[] employeeIds)
        {
            LinkedList<Tour> tours = new LinkedList<Tour>();

            Faker<Tour> fk = new Faker<Tour>();

            fk.RuleFor(t => t.TourName, f => f.Lorem.Sentence(5, 6));
            fk.RuleFor(t => t.TourAddress, f => f.Address.FullAddress());

            fk.RuleFor(t => t.Overview, f => f.Lorem.Sentences(4));
            fk.RuleFor(t => t.Schedule, f => f.Lorem.Sentence(10, 20));

            fk.RuleFor(t => t.DepartureLocation, f => f.Address.SecondaryAddress());
            fk.RuleFor(t => t.DropOffLocation, f => f.Address.Locale);

            fk.RuleFor(t => t.PostingDate, f => f.Date.Soon());
            fk.RuleFor(t => t.ApprovalDate, f => f.Date.Future());

            fk.RuleFor(t => t.CityId, f => f.Random.Int(1, 63));
            fk.RuleFor(t => t.TourTypeId, f => f.Random.Int(1, 15));
            fk.RuleFor(t => t.PosterID, f => agentTourIds[f.Random.Int(0, 4)]);
            fk.RuleFor(t => t.ApproverID, f => employeeIds[f.Random.Int(0, 4)]);

            for (int i = 1; i <= 30; i++)
            {
                Tour tour = fk.Generate();

                tour.PhotoPath = $"/tours/{i}";
                tours.AddLast(tour);
            }
            await context.Tour.AddRangeAsync(tours);
            await context.SaveChangesAsync();
        }
    }
}
