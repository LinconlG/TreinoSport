using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using TreinoSport.Contexts;
using TreinoSport.Contexts.Base;

namespace TreinoSport;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.DependenciesInjection()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        return builder.Build();
	}

    public static MauiAppBuilder DependenciesInjection(this MauiAppBuilder mauiAppBuilder) {
        mauiAppBuilder.Services.AddSingleton<UsuarioContext>();

        return mauiAppBuilder;
    }
}
