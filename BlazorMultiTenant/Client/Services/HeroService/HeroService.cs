namespace BlazorMultiTenant.Client.Services.HeroService
{
    public class HeroService : IHeroService
    {
        private readonly HttpClient _http;
        public HeroService(HttpClient http)
        {
            _http = http;
        }


        public event Action OnChange;
        public List<Hero> HeroList { get; set; } = new List<Hero>();
        public List<Comic> ComicList { get; set; } = new List<Comic>();




        public async Task GetComics()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Comic>>>("api/Hero/Get-comics");

            if (response != null && response.Data != null)
                ComicList = response.Data;
        } //GetComics


        public async Task GetHeros()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Hero>>>("api/Hero/GetHeros");

            if (response != null && response.Data != null)
                HeroList = response.Data;
        } //GetHeros

        public async Task<Hero> GetHero(int id)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Hero>>($"api/Hero/Get-hero/{id}");
            return response.Data;
        } //GetHero


        public async Task CreateHero(Hero hero)
        {
            var response = await _http.PostAsJsonAsync("api/Hero/Create-hero", hero);
            await GetHeros();
            OnChange.Invoke();
        } //CreateHero


        public async Task UpdateHero(Hero hero)
        {
            var response = await _http.PutAsJsonAsync("api/Hero/Update-hero", hero);
            await GetHeros();
            OnChange.Invoke();
        } //UpdateHero


        public async Task DeleteHero(int categoryId)
        {
            var response = await _http.DeleteAsync($"api/Hero/Delete-hero/{categoryId}");
            await GetHeros();
            OnChange.Invoke();
        } //DeleteHero


    }
}
