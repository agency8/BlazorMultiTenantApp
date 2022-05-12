namespace BlazorMultiTenant.Server.Services.HeroService
{
    public interface IHeroService
    {
        Task<ServiceResponse<List<Hero>>> GetHeros();
        Task<ServiceResponse<List<Comic>>> GetComics();
        Task<ServiceResponse<Hero>> GetHero(int id);
        Task<ServiceResponse<List<Hero>>> CreateHero(Hero hero);
        Task<ServiceResponse<List<Hero>>> UpdateHero(Hero hero);
        Task<ServiceResponse<List<Hero>>> DeleteHero(int id);
    }
}
