using AnimeTrackerRe.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnimeTrackerRe.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AnimeListTab.xaml
    /// </summary>
    public partial class AnimeListTab : UserControl
    {
        public AnimeListTab()
        {
            InitializeComponent();
            JobGrid.ItemsSource = _presenter.LoadJobSchedule();
            //AnimeIDTxt.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, StopPaste));
            AnimeTitleTxt.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, StopPaste));
            JobGrid.SelectedIndex = 0;
        }   

            private Presenter _presenter = new Presenter();


        private void StopPaste(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddAnimeBtn_Click(object sender, RoutedEventArgs e)

        {
            var addJob = new AddAnimeWindow();
            //var addJob = new AddJob();
            addJob.ShowDialog();

            var AnimeTitle = AnimeTitleTxt.Text;


            JobGrid.ItemsSource = _presenter.FilterJobGrid(AnimeTitle);
        }

      
        private void AnimeTitleTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                filterBtn_Click(this, new RoutedEventArgs());
            }

            if (e.Key == Key.Escape)
            {
                ClearFilterBtn_Click(this, new RoutedEventArgs());
            }
           
        }

        private void filterBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string AnimeTitle = "";
         

            AnimeTitle = AnimeTitleTxt.Text;


            JobGrid.ItemsSource = _presenter.FilterJobGrid(AnimeTitle);
            JobGrid.SelectedIndex = 0;
        }

        private void ClearFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            AnimeTitleTxt.Clear();
        

            JobGrid.ItemsSource = _presenter.LoadJobSchedule();
            JobPlatesGrid.ItemsSource = "";
        }

        private void changeStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (JobGrid.SelectedItem == null) return;

           
        }

        private void ValidateNoLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void JobGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "AnimeDescription")
            {
                e.Cancel = true;
            }
        }

        private void JobPlatesGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Color" || e.Column.Header.ToString() == "Quantity")
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void JobGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedJob = (AnimeListObject)JobGrid.SelectedItem;
            if (selectedJob != null)
            {
                JobPlatesGrid.ItemsSource = _presenter.LoadPlatesForSelectedJob(Convert.ToInt32(selectedJob.AnimeId));
                JobPlatesGrid.Items.Refresh();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
           
            var AnimeTitle = AnimeTitleTxt.Text;

            JobGrid.ItemsSource = _presenter.FilterJobGrid(AnimeTitle);
            JobGrid.SelectedIndex = 0;
            var selectedJob = (AnimeListObject)JobGrid.SelectedItem;
            if (selectedJob != null)
            {
                JobPlatesGrid.ItemsSource = _presenter.LoadPlatesForSelectedJob(Convert.ToInt32(selectedJob.AnimeId));
                JobPlatesGrid.Items.Refresh();
            }
        }

    
    }
    
}
