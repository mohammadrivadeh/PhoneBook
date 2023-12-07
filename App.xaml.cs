using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace PhoneBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            StyleManager.ApplicationTheme=new FluentTheme();
            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Light);


            new Views.Home().Show();
            
            base.OnStartup(e);
        }
    }
}
