using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMultiTenant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }


        [HttpGet("Get-comics")]
        public async Task<ActionResult<ServiceResponse<List<Comic>>>> GetComics()
        {
            var result = await _heroService.GetComics();
            return Ok(result);
        } //GetHeros


        [HttpGet("GetHeros")]
        public async Task<ActionResult<ServiceResponse<List<Hero>>>> GetHeros()
        {
            var result = await _heroService.GetHeros();
            return Ok(result);
        } //GetHeros


        [HttpGet("Get-hero/{id}")]
        public async Task<ActionResult<ServiceResponse<Hero>>> GetHero(int id)
        {
            var result = await _heroService.GetHero(id);
            return Ok(result);
        } //GetHero


        [HttpPost("Create-hero")]
        public async Task<ActionResult<ServiceResponse<List<Hero>>>> CreateHero(Hero hero)
        {
            var result = await _heroService.CreateHero(hero);
            return Ok(result);
        } //CreateHero



        [HttpPut("Update-hero")]
        public async Task<ActionResult<ServiceResponse<List<Hero>>>> UpdateHero(Hero hero)
        {
            var result = await _heroService.UpdateHero(hero);
            return Ok(result);
        } //UpdateHero



        [HttpDelete("Delete-hero/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Hero>>>> DeleteHero(int id)
        {
            var result = await _heroService.DeleteHero(id);
            return Ok(result);
        } //DeleteHero




    }
}
