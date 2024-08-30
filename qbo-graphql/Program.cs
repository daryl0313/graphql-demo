using Microsoft.Extensions.DependencyInjection;
using qbo_graphql.StarWarsClient;
using StrawberryShake;

var serviceCollection = new ServiceCollection();

serviceCollection
    .AddStarWarsClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://swapi-graphql.netlify.app/.netlify/functions/index"));

IServiceProvider services = serviceCollection.BuildServiceProvider();

IStarWarsClient client = services.GetRequiredService<IStarWarsClient>();

var result = await client.GetPlanets.ExecuteAsync();
result.EnsureNoErrors();

var planets = result.Data?.AllPlanets?.Planets ?? [];

foreach (var planet in planets)
{
    Console.WriteLine(planet?.Name);
}

var films = await client.GetFilms.ExecuteAsync();
films.EnsureNoErrors();

var filmsData = films.Data?.AllFilms?.Films ?? [];
foreach (var film in filmsData)
{
    Console.WriteLine(film?.Title);
}
