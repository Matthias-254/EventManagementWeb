﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagementMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ApiService>();
#if DEBUG
            builder.Logging.AddDebug();

#endif
            return builder.Build();
        }
    }
}
