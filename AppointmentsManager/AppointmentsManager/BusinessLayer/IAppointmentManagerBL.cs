using AppointmentsManager.Models;
using AppointmentsManager.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppointmentsManager.Common.Enums;

namespace AppointmentsManager.BusinessLayer
{
    public interface IAppointmentManagerBL
    {
        public ResponseAppointmentDetails ValidateAppointment(AppointmentDetail appointment, AppointmentDetailDBContext _context);

        public AppointmentDetailDBContext GetLocalDateTime(AppointmentDetail appointment, AppointmentDetailDBContext _context);
    }

    public class AppointmentManagerBL : IAppointmentManagerBL
    {
        public AppointmentDetailDBContext GetLocalDateTime(AppointmentDetail appointment, AppointmentDetailDBContext _context)
        {
            try
            {
                var appointmentToUpdate = _context.AppointmentDetails.FirstOrDefault(item => item.AppointmentId == appointment.AppointmentId);
                if (appointmentToUpdate != null)
                {
                    appointmentToUpdate.StartTime = appointmentToUpdate.StartTime.ToLocalTime();
                    appointmentToUpdate.EndTime = appointmentToUpdate.EndTime.ToLocalTime();
                    _context.AppointmentDetails.Update(appointmentToUpdate);
                }
                else
                {
                    appointment.StartTime = appointment.StartTime.ToLocalTime();
                    appointment.EndTime = appointment.EndTime.ToLocalTime();
                    _context.AppointmentDetails.Update(appointment);
                }
            }
            catch
            {
                throw;
            }
            return _context;
        }

        public ResponseAppointmentDetails ValidateAppointment(AppointmentDetail appointment, AppointmentDetailDBContext _context)
        {


            ResponseAppointmentDetails responseAppointment = new ResponseAppointmentDetails();

            try
            {
                foreach (var order in _context.AppointmentDetails.Where(x => x.AppointmentId != appointment.AppointmentId))
                {
                    if (appointment.StartTime == order.StartTime && appointment.EndTime == order.EndTime)
                    {
                        responseAppointment.AppointmentStatus = enmAppointmentStatus.SamePeriod;
                        responseAppointment.Message = "Same period with another appointment with " + order.CustomerName;
                        return responseAppointment;
                    }
                    else if (appointment.StartTime >= order.StartTime && appointment.EndTime <= order.EndTime)
                    {
                        responseAppointment.AppointmentStatus = enmAppointmentStatus.Intersects;
                        responseAppointment.Message = "Intersects with another appointment with " + order.CustomerName;
                        return responseAppointment;
                    }
                    else if ((appointment.StartTime >= order.StartTime && appointment.StartTime <= order.EndTime) || (appointment.EndTime >= order.StartTime && appointment.EndTime <= order.EndTime) || (appointment.StartTime <= order.StartTime && appointment.EndTime >= order.EndTime))
                    {
                        responseAppointment.AppointmentStatus = enmAppointmentStatus.SamePeriod;
                        responseAppointment.Message = "Overlaps with another appointment with " + order.CustomerName;
                        return responseAppointment;
                    }


                }

                responseAppointment.AppointmentStatus = enmAppointmentStatus.Success;
                responseAppointment.Message = "Successful" ;

                return responseAppointment;
            }
            catch
            {
                throw;
            }
        }
    }
}
