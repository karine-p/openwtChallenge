using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactApi.Models;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactAndSkillController : ControllerBase
    {
        private readonly ContactAndSkillContext _context;

        public ContactAndSkillController(ContactAndSkillContext context)
        {
            _context = context;
        }

        // GET: api/ContactAndSkill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactAndSkill>>> GetContactAnsSkillTable()
        {
            return await _context.ContactAnsSkillTable.ToListAsync();
        }

        // GET: api/ContactAndSkill/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactAndSkill>> GetContactAndSkill(long id)
        {
            var contactAndSkill = await _context.ContactAnsSkillTable.FindAsync(id);

            if (contactAndSkill == null)
            {
                return NotFound();
            }

            return contactAndSkill;
        }


        // GET: search in the database for all lines where contact id = id
        [HttpGet("fromContact/{id}")]
        public async Task<ActionResult<ContactAndSkill>> GetContactAndSkillFromContact(long id)
        {
            var result =  await _context.ContactAnsSkillTable.Where(p => p.contactId == id).ToListAsync();

            if (!result.Any())
            return NotFound();

            return Ok(result);
        }

        // GET: search in the database for all lines where skill id = id
        [HttpGet("fromSkill/{id}")]
        public async Task<ActionResult<ContactAndSkill>> GetContactAndSkillFromSkill(long id)
        {
            var result =  await _context.ContactAnsSkillTable.Where(p => p.skillId == id).ToListAsync();

            if (!result.Any())
            return NotFound();

            return Ok(result);
        }

        // PUT: api/ContactAndSkill/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactAndSkill(long id, ContactAndSkill contactAndSkill)
        {
            if (id != contactAndSkill.id)
            {
                return BadRequest();
            }

            _context.Entry(contactAndSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactAndSkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContactAndSkill
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ContactAndSkill>> PostContactAndSkill(ContactAndSkill contactAndSkill)
        {
            _context.ContactAnsSkillTable.Add(contactAndSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactAndSkill", new { id = contactAndSkill.id }, contactAndSkill);
        }

        // DELETE: api/ContactAndSkill/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactAndSkill>> DeleteContactAndSkill(long id)
        {
            var contactAndSkill = await _context.ContactAnsSkillTable.FindAsync(id);
            if (contactAndSkill == null)
            {
                return NotFound();
            }

            _context.ContactAnsSkillTable.Remove(contactAndSkill);
            await _context.SaveChangesAsync();

            return contactAndSkill;
        }

        private bool ContactAndSkillExists(long id)
        {
            return _context.ContactAnsSkillTable.Any(e => e.id == id);
        }
    }
}
