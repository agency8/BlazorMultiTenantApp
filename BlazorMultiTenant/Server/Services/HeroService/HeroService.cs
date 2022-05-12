namespace BlazorMultiTenant.Server.Services.HeroService
{
    public class HeroService : IHeroService
    {
        private readonly DataContext _context;

        public HeroService(DataContext context)
        {
            _context = context;
        }


        private async Task<Hero> GetHeroById(int id)
        {
            return await _context.Heros.FindAsync(id);
        } //GetHeroById


        public async Task<ServiceResponse<List<Comic>>> GetComics()
        {
            var response = new ServiceResponse<List<Comic>>
            {
                Data = await _context.Comics.ToListAsync()
            };
            return response;
        } //GetHeros






        public async Task<ServiceResponse<List<Hero>>> GetHeros()
        {
            var response = new ServiceResponse<List<Hero>>
            {
                Data = await _context.Heros
                            .Include(sh => sh.Comic)
                            .ToListAsync()
            };
            return response;
        } //GetHeros


        public async Task<ServiceResponse<Hero>> GetHero(int id)
        {
            var response = new ServiceResponse<Hero>();
            var hero = await GetHeroById(id);
            if (hero == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this hero does not exist.";
            }
            else
            {
                response.Data = hero;
            }
            return response;
        } //GetHero


        public async Task<ServiceResponse<List<Hero>>> CreateHero(Hero hero)
        {
            hero.TenantId = _context.TenantId;
            _context.Heros.Add(hero);
            await _context.SaveChangesAsync();
            return await GetHeros();
        } //CreateHero

        public async Task<ServiceResponse<List<Hero>>> UpdateHero(Hero hero)
        {
            var dbHero = await GetHeroById(hero.Id);
            if (dbHero == null)
            {
                return new ServiceResponse<List<Hero>>
                {
                    Success = false,
                    Message = "Hero not found."
                };
            }

            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.HeroName = hero.HeroName;
            dbHero.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();


            return await GetHeros();
        } //UpdateHero

        public async Task<ServiceResponse<List<Hero>>> DeleteHero(int id)
        {
            var hero = await GetHero(id);
            if (hero.Data == null)
            {
                return new ServiceResponse<List<Hero>>
                {
                    Success = false,
                    Message = "Hero not found."
                };
            }
            else
            {
                _context.Heros.Remove(hero.Data);
                await _context.SaveChangesAsync();
            }

            return await GetHeros();
        }


    }
}
