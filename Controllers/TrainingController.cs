using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bangazon_inc.Data;
using bangazon_inc.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bangazon_inc.Controllers
{
    [Route("[controller]")]
    [EnableCors("BangazonAllowed")]


    public class TrainingController : Controller
    {
        private BangazonContext _context;
        public TrainingController(BangazonContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> training = from TrainingName in _context.Training select TrainingName;

            if (training == null)
            {
                return NotFound();
            }
            
            return Ok(training);
        }

        [HttpGet("{id}", Name = "GetSingleTraining")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Training training = _context.Training.Single(m => m.TrainingId == id);

                if (training == null)
                {
                    return NotFound();
                }
                
                return Ok(training);
            }

            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] Training newTraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Training.Add(newTraining);
            
            try
            {
                _context.SaveChanges();
            }

            catch (DbUpdateException)
            {
                if (TrainingExists(newTraining.TrainingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }

                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleTraining", new { id = newTraining.TrainingId }, newTraining);
        }

        private bool TrainingExists(int TrainingId)
        {
            return _context.Training.Any(o => o.TrainingId == TrainingId);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Training modifiedTraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedTraining.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(modifiedTraining).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Training singleTraining = _context.Training.Single(m => m.TrainingId == id);
            if (singleTraining == null)
            {
                return NotFound();
            }

            DateTime thisDay = DateTime.Today;
            var compare = DateTime.Compare(thisDay, (DateTime)singleTraining.StartDate);
            if (compare < 0)
            {
                _context.Training.Remove(singleTraining);
                _context.SaveChanges();

                return Ok(singleTraining);
            } 

            else 
            {
                return BadRequest();
            }

        }

    }
}