using ConnectApp.Views;

namespace ConnectApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register detail routes (no tab bar)
        Routing.RegisterRoute("chat",      typeof(ChatPage));
        Routing.RegisterRoute("videocall", typeof(VideoCallPage));
    }
}
