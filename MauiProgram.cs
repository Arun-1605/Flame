using CommunityToolkit.Maui;
using ConnectApp.Services;
using ConnectApp.ViewModels;
using ConnectApp.Views;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Firebase.Auth;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Firestore;
using Plugin.Firebase.Storage;

namespace ConnectApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Poppins-Regular.ttf",       "PoppinsRegular");
                fonts.AddFont("Poppins-SemiBold.ttf",      "PoppinsSemiBold");
                fonts.AddFont("Poppins-Bold.ttf",          "PoppinsBold");
                fonts.AddFont("PlayfairDisplay-Bold.ttf",  "PlayfairDisplayBold");
            });

        // ── Firebase ──────────────────────────────────────────────────────────
        builder.Services.AddSingleton(CrossFirebaseAuth.Current);
        builder.Services.AddSingleton(CrossFirebaseFirestore.Current);
        builder.Services.AddSingleton(CrossFirebaseStorage.Current);
        builder.Services.AddSingleton(CrossFirebaseCloudMessaging.Current);

        // ── App Services ─────────────────────────────────────────────────────
        builder.Services.AddSingleton<FirebaseAuthService>();
        builder.Services.AddSingleton<FirestoreService>();
        builder.Services.AddSingleton<StorageService>();
        builder.Services.AddSingleton<MessagingService>();
        builder.Services.AddSingleton<LocationService>();
        builder.Services.AddSingleton<AgoraService>();
        builder.Services.AddSingleton<PlayBillingService>();

        // ── ViewModels ────────────────────────────────────────────────────────
        builder.Services.AddTransient<AuthViewModel>();
        builder.Services.AddTransient<DiscoverViewModel>();
        builder.Services.AddTransient<MatchesViewModel>();
        builder.Services.AddTransient<ChatViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<VideoCallViewModel>();

        // ── Views ─────────────────────────────────────────────────────────────
        builder.Services.AddTransient<SplashPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<DiscoverPage>();
        builder.Services.AddTransient<MatchesPage>();
        builder.Services.AddTransient<ChatPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<VideoCallPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
