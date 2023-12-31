﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero {
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Power = "Spider Webs",
                    IsFly = true,
                },
                new SuperHero {
                    Id = 2,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Power = "Laser Jet",
                    IsFly = true,
                }
            };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            if (heroes.Count == 0)
                return BadRequest("Heroes doesn't exist.");
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) 
                return BadRequest($"Hero with id {id} not found.");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            var heroes = await _context.SuperHeroes.ToListAsync();
            if (heroes.Count == 0)
                return BadRequest("Heroes doesn't exist.");
            return Ok(heroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest($"Hero with id {request.Id} not found.");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Power = request.Power;
            hero.IsFly = request.IsFly;

            await _context.SaveChangesAsync();

            var heroes = await _context.SuperHeroes.ToListAsync();
            if (heroes.Count == 0)
                return BadRequest("Heroes doesn't exist.");
            return Ok(heroes);
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest($"Hero with id {id} not found.");

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            var heroes = await _context.SuperHeroes.ToListAsync();
            if (heroes.Count == 0)
                return BadRequest("Heroes doesn't exist.");
            return Ok(heroes);
        }
    }
}
