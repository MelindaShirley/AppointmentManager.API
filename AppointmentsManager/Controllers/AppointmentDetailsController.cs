using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentsManager.Models;
using AppointmentsManager.BusinessLayer;
using static AppointmentsManager.Common.Enums;
using AppointmentsManager.Response;
using Newtonsoft.Json;

namespace AppointmentsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailsController : ControllerBase
    {
        private readonly AppointmentDetailDBContext _context;
        
        public AppointmentDetailsController(AppointmentDetailDBContext context)
        {
            _context = context;
        }
        AppointmentManagerBL appointmentBL = new AppointmentManagerBL();
        ResponseAppointmentDetails response = new ResponseAppointmentDetails();
       

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult (_context.AppointmentDetails.OrderBy(x=>x.StartTime));
        }
        
        // PUT: api/AppointmentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public JsonResult PutAppointmentDetail(int id, AppointmentDetail appointmentDetail)
        {
            if (id != appointmentDetail.AppointmentId)
            {
                return new JsonResult (BadRequest());
            }

            _context.Entry(appointmentDetail).State = EntityState.Modified;
           
            try
            {
                appointmentBL.GetLocalDateTime(appointmentDetail, _context);
                response =appointmentBL.ValidateAppointment(appointmentDetail,_context);

                if(response.AppointmentStatus==enmAppointmentStatus.Success)
                {
                    
                 _context.SaveChangesAsync();
                                  
                }
                       
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentDetailExists(id))
                {
                    return new JsonResult(NotFound());
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(response.Message);
        }

        // POST: api/AppointmentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public JsonResult PostAppointmentDetail(AppointmentDetail appointmentDetail)
        {
            try
            {
                appointmentBL.GetLocalDateTime(appointmentDetail, _context);
                response = appointmentBL.ValidateAppointment(appointmentDetail, _context);

                if (response.AppointmentStatus == enmAppointmentStatus.Success)
                {
                    _context.AppointmentDetails.Add(appointmentDetail);
                    _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(response.Message);
        }

        // DELETE: api/AppointmentDetails/5
        [HttpDelete("{id}")]
        public JsonResult DeleteAppointmentDetail(int id)
        {
            var appointmentDetail =  _context.AppointmentDetails.Find(id);
            if (appointmentDetail == null)
            {
               return new JsonResult (NotFound());
            }
            try
            {
                _context.AppointmentDetails.Remove(appointmentDetail);
                _context.SaveChangesAsync();

                return new JsonResult("Deleted Successfully");
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        private bool AppointmentDetailExists(int id)
        {
            return _context.AppointmentDetails.Any(e => e.AppointmentId == id);
        }
    }
}
 