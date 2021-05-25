using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body cannot be empty");
            }

            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        //GetAll
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //GetById
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        //Update(PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant(int id, Restaurant updatedRestaurant)
        {
            if (ModelState.IsValid)
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if(restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Address = updatedRestaurant.Address;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //Delete
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was successfully deleted.");
            }

            return InternalServerError();
        }

    }
}
