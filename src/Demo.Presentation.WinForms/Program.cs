using System;
using WinForms = System.Windows.Forms;         
namespace Demo.Presentation.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            WinForms.ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new MigrationForm());
        }
    }
}
