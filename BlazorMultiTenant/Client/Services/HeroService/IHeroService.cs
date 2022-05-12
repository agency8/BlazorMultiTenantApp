namespace BlazorMultiTenant.Client.Services.HeroService
{
    public interface IHeroService
    {
        event Action OnChange;
        List<Hero> HeroList { get; set; }
        List<Comic> ComicList { get; set; }

        Task GetComics();
        Task GetHeros();
        Task<Hero> GetHero(int id);
        Task CreateHero(Hero hero);
        Task UpdateHero(Hero hero);
        Task DeleteHero(int categoryId);
    }
}
