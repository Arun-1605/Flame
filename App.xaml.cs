using ConnectApp.Services;
using ConnectApp.Views;

namespace ConnectApp;

public partial class App : Application
{
    private readonly FirebaseAuthService _authService;
    private readonly MessagingService _messagingService;

    public App(FirebaseAuthService authService, MessagingService messagingService)
    {
        InitializeComponent();
        _authService = authService;
        _messagingService = messagingService;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    protected override async void OnStart()
    {
        base.OnStart();

        // Request push notification permission and save token
        await _messagingService.RequestPermissionAsync();
        var token = await _messagingService.GetTokenAsync();
        if (_authService.CurrentUser != null && !string.IsNullOrEmpty(token))
            await _authService.SaveFcmTokenAsync(_authService.CurrentUser.Uid, token);

        // Mark user online
        if (_authService.CurrentUser != null)
            await _authService.UpdateOnlineStatusAsync(_authService.CurrentUser.Uid, true);
    }

    protected override async void OnSleep()
    {
        base.OnSleep();
        if (_authService.CurrentUser != null)
            await _authService.UpdateOnlineStatusAsync(_authService.CurrentUser.Uid, false);
    }

    protected override async void OnResume()
    {
        base.OnResume();
        if (_authService.CurrentUser != null)
            await _authService.UpdateOnlineStatusAsync(_authService.CurrentUser.Uid, true);
    }
}
