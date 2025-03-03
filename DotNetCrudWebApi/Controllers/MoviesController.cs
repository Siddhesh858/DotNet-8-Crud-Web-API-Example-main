﻿using DotNetCrudWebApi.Data;
using DotNetCrudWebApi.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public MoviesController(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        // Get : api/Movies
        /// <summary>
        /// Get Moveis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovies()
        {
            try
            {
                if (_appDbContext.Movies == null)
                {
                    return NotFound();
                }
                return await _appDbContext.Movies.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Get : api/Movies/2
        /// <summary>
        /// GetMovie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieModel>> GetMovie(int id)
        {
            try
            {
                if (_appDbContext.Movies is null)
                {
                    return NotFound();
                }
                var movie = await _appDbContext.Movies.FindAsync(id);
                if (movie is null)
                {
                    return NotFound();
                }
                return movie;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Post : api/Movies
        /// <summary>
        /// PostMovie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieModel>> PostMovie(MovieModel movie)
        {
            try
            {
                _appDbContext.Movies.Add(movie);
                await _appDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Put : api/Movies/2
        /// <summary>
        /// PutMovie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<MovieModel>> PutMovie(int id, MovieModel movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            _appDbContext.Entry(movie).State = EntityState.Modified;
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        private bool MovieExists(long id)
        {
            return (_appDbContext.Movies?.Any(movie => movie.Id == id)).GetValueOrDefault();
        }

        // Delete : api/Movies/2
        /// <summary>
        /// DeleteMovie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieModel>> DeleteMovie(int id)
        {
            try
            {
                if (_appDbContext.Movies is null)
                {
                    return NotFound();
                }
                var movie = await _appDbContext.Movies.FindAsync(id);
                if (movie is null)
                {
                    return NotFound();
                }
                _appDbContext.Movies.Remove(movie);
                await _appDbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
