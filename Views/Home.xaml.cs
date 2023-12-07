using Microsoft.Win32;
using PhoneBook.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace PhoneBook.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : RadWindow
    {
        private readonly ApplicationDb db;
        private long id;

        public Home()
        {
            InitializeComponent();
            db = new ApplicationDb();
            db.Database.EnsureCreated();
            UpdateGridView();
        }


        void UpdateGridView()
        {
            Datagridview.ItemsSource = db.People.ToList();



        }
        void FormClear()
        {
            TextboxName.Text = string.Empty;
            TextboxLastName.Text = string.Empty;
            TextboxMobile.Text = string.Empty;
            TextboxEmail.Text = string.Empty;
        }

        private void CreateContact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            db.People.AddAsync(new Person { FirstName = TextboxName.Text, LastName = TextboxLastName.Text, Mobile = int.Parse(TextboxMobile.Text), Email = TextboxEmail.Text });
            db.SaveChangesAsync();
            UpdateGridView();
            FormClear();
            
            }
            catch (Exception ex)
            {

                RadWindow.Alert("Contact Without Data Cant Be Added!");
            }

        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var contact = db.People.Find(id);
                if (contact == null) { return; }
                contact.FirstName = TextboxName.Text;
                contact.LastName = TextboxLastName.Text;
                contact.Email = TextboxEmail.Text;
                contact.Mobile = int.Parse(TextboxMobile.Text);
                db.SaveChanges();
                FormClear();
                UpdateGridView();
            }
            catch (Exception ex)
            {

                RadWindow.Alert(ex.Message);
            }



        }


        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {

            var contact = db.People.Where(p => p.Id == id);
            db.People.RemoveRange(contact);
            db.SaveChanges();
            UpdateGridView();
            FormClear();
            id= 0;
        }


        private void Datagridview_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Datagridview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (id == 0)
            //{
            //    RadWindow.Alert("No Contact Found!");
            //    return;
            //}
            try
            {

                if ((Datagridview.SelectedItem) == null)
                {
                    return;
                }
                id = ((Person)Datagridview.SelectedItem).Id;
                TextboxName.Text = ((Person)Datagridview.SelectedItem).FirstName;
                TextboxLastName.Text = ((Person)Datagridview.SelectedItem).LastName;
                TextboxMobile.Text = ((Person)Datagridview.SelectedItem).Mobile.ToString();
                TextboxEmail.Text = ((Person)Datagridview.SelectedItem).Email;

            }
            catch (NullReferenceException ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }

        private void ExportContact_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xlsx";

            RadSaveFileDialog dialog = new RadSaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel"),
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    Datagridview.ExportToXlsx(stream,
                        new GridViewDocumentExportOptions()
                        {
                            ShowColumnFooters = true,
                            ShowColumnHeaders = true,
                            ShowGroupFooters = true
                        });
                }
                RadWindow.Alert("Export Was Succeeded");
            }
        }
    }

}
