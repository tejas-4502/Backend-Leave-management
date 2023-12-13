using Backend_Leave.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Leave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplyController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;
        public LeaveApplyController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<LeaveApply>>> GetEmployee()
        {
            if (_employeeContext.LeaveApplys == null)
            {
                return NotFound();
            }
            return await _employeeContext.LeaveApplys.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<LeaveApply>> GetEmployee(int id)
        {
            if (_employeeContext.LeaveApplys == null)
            {
                return NotFound();
            }
            var leave = await _employeeContext.LeaveApplys.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            return leave;
        }

        [HttpPost]
        public async Task<ActionResult<LeaveApply>> PostEmployee(LeaveApply leave)
        {
            _employeeContext.LeaveApplys.Add(leave);
            await _employeeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = leave.ID }, leave);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeaveApply>> PutEmployee(int id, LeaveApply leave)
        {
            if (id != leave.ID)
            {
                return BadRequest();
            }
            _employeeContext.Entry(leave).State = EntityState.Modified;

            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (_employeeContext.LeaveApplys == null)
            {
                return NotFound();
            }

            var leave = await _employeeContext.LeaveApplys.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            _employeeContext.LeaveApplys.Remove(leave);
            await _employeeContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("ByUserName")]
        public async Task<ActionResult<IEnumerable<LeaveApply>>> GetLeaveApplicationsByUserName([FromQuery] string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Username cannot be empty");
            }

            var leaveApplications = await _employeeContext.LeaveApplys
                .Where(leave => leave.UserName == userName)
                .ToListAsync();

            if (leaveApplications == null || !leaveApplications.Any())
            {
                return NotFound("No leave applications found for the given username");
            }

            return Ok(leaveApplications);
        }

        [HttpPut("{id}/Accept")]
        public async Task<ActionResult<LeaveApply>> AcceptLeave(int id)
        {
            var leave = await _employeeContext.LeaveApplys.FindAsync(id);

            if (leave == null)
            {
                return NotFound();
            }

            leave.Status = "Accepted"; // Update status to 'Accepted' (or your desired status for acceptance)

            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpPut("{id}/Decline")]
        public async Task<ActionResult<LeaveApply>> DeclineLeave(int id)
        {
            var leave = await _employeeContext.LeaveApplys.FindAsync(id);

            if (leave == null)
            {
                return NotFound();
            }

            leave.Status = "Declined"; // Update status to 'Declined' (or your desired status for rejection)

            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }


    }
}
