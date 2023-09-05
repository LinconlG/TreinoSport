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
			.DependenciesInjection()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .Configuration.AddConfiguration(config);

        return builder.Build();
	}

    public static MauiAppBuilder DependenciesInjection(this MauiAppBuilder mauiAppBuilder) {
        #region Context
        mauiAppBuilder.Services.AddSingleton<BaseContext>();
        mauiAppBuilder.Services.AddSingleton<UsuarioContext>();
        mauiAppBuilder.Services.AddSingleton<TreinoContext>();
        #endregion

        #region Service
        mauiAppBuilder.Services.AddSingleton<UsuarioService>();
        #endregion

        #region View
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<CadastroPage>();
        mauiAppBuilder.Services.AddSingleton<CriacaoTreino>();
        mauiAppBuilder.Services.AddSingleton<PaginaInicialAluno>();
        mauiAppBuilder.Services.AddSingleton<PaginaInicialCT>();
        #endregion

        #region ViewModel
        mauiAppBuilder.Services.AddSingleton<TreinoViewModel>();
        mauiAppBuilder.Services.AddSingleton<CriacaoTreinoViewModel>();
        #endregion

        return mauiAppBuilder;
    }
}
