using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using HomeHealth.Data.Tables;
using HomeHealth.Constants;
using HomeHealth.Data;

namespace HomeHealth.Data.Seeders
{
    public static class SeedServices
    {
        public static HomeHealthDbContext _context;


        public static async Task Init(HomeHealthDbContext context){
            _context = context;
            await CreateServices();
        }

        public static async Task CreateServices()
        {

            if(_context.Service.Any())
            {
                return;
            }

            await _context.Service.AddRangeAsync(
                ServiceData
            );

            await _context.SaveChangesAsync(); 

        }

        public static ICollection<Service> ServiceData = new HashSet<Service> {
            new Service {
                ServiceName = "Dentistry"
            },
            new Service {
                ServiceName = "Pediatrics"
            },
            new Service {
                ServiceName = "Radiology"
            },
            new Service {
                ServiceName = "General Practicioneer"
            },
            new Service {
                ServiceName = "Obstetrics/Gynaecolgy"
            },
            new Service {
                ServiceName = "Orthopedic"
            },
            new Service {
                ServiceName = "Ophthalmology"
            },
            new Service {
                ServiceName = "Ear Nose Throat(ENP)"
            },
            new Service {
                ServiceName = "Pharmacist"
            },
            new Service {
                ServiceName = "PhysioTherapy"
            },
            new Service {
                ServiceName = "Psychology"
            },
            new Service {
                ServiceName = "Psychiatrist"
            },
        };
        


    }
}