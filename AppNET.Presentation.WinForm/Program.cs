using AppNET.App;

namespace AppNET.Presentation.WinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            ApplicationServiceSettings.RegisterAllService();
            LogService logService = new LogService();
            logService.Information("Program Başlatıldı");
            Application.Run(new Form1());
            
        }
    }
}