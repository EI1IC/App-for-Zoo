using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace RazorPagesZoo
{
    public class VaccinationService
    {
        private readonly ZoodbContext _context;

        public VaccinationService(ZoodbContext context)
        {
            _context = context;
        }

        public async Task<List<UpcomingVaccination>> GetUpcomingVaccinationsAsync()
        {
            var result = new List<UpcomingVaccination>();

            // Вызов функции PostgreSQL
            var query = @"
                SELECT animal_name, vaccination_name, next_vaccination_date
                FROM zoo_keepers.upcoming_vaccinations();";

            await using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                await _context.Database.OpenConnectionAsync();

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new UpcomingVaccination
                        {
                            AnimalName = reader.GetString(0),
                            VaccinationName = reader.GetString(1),
                            NextVaccinationDate = reader.GetDateTime(2)
                        });
                    }
                }
            }

            return result;
        }
    }

    public class UpcomingVaccination
    {
        public string AnimalName { get; set; }
        public string VaccinationName { get; set; }
        public DateTime NextVaccinationDate { get; set; }
    }
}