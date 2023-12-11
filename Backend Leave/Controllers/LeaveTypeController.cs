using Backend_Leave.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Leave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {

        private readonly EmployeeContext _employeeContext;
        public LeaveTypeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<LeaveType>>> GetEmployee()
        {
            if (_employeeContext.LeaveTypes == null)
            {
                return NotFound();
            }
            return await _employeeContext.LeaveTypes.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<LeaveType>> GetEmployee(int id)
        {
            if (_employeeContext.LeaveTypes == null)
            {
                return NotFound();
            }
            var employee = await _employeeContext.LeaveTypes.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostEmployee(LeaveType employee)
        {
            _employeeContext.LeaveTypes.Add(employee);
            await _employeeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeaveType>> PutEmployee(int id, LeaveType employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }
            _employeeContext.Entry(employee).State = EntityState.Modified;

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
            if (_employeeContext.LeaveTypes == null)
            {
                return NotFound();
            }

            var employee = await _employeeContext.LeaveTypes.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeContext.LeaveTypes.Remove(employee);
            await _employeeContext.SaveChangesAsync();

            return Ok();
        }
    }
}
