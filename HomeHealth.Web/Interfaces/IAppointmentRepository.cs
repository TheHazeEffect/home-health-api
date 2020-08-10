using HomeHealth.Web.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHealth.Web.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<ICollection<Appointments>> getAllAppointments();
        
    }
    
}