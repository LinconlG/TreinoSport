using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Xml.Linq;
using TreinoSport.Contexts;
using TreinoSport.Contexts.Base;
using TreinoSport.Services;
using TreinoSport.ViewModels;
using TreinoSport.Views;

namespace TreinoSport;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TreinoSport.appsettings.json");
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Configuration.AddConfiguration(config);

        BaseContext.StartContext(config);

        return builder.Build();
    }
}
