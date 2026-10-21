namespace SecourRapide237.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // L'interface est dessinée pour un fond clair (comme la maquette).
            UserAppTheme = AppTheme.Light;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());
#if WINDOWS
            // Sur PC, la fenêtre a la forme d'un téléphone (elle reste redimensionnable).
            window.Width = 440;
            window.Height = 880;
            window.MinimumWidth = 360;
            window.MinimumHeight = 600;
#endif
            return window;
        }
    }
}
