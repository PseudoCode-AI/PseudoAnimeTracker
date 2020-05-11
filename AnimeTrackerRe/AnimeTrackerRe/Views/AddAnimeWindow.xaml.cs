using AnimeTrackerRe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnimeTrackerRe.Views
{
    /// <summary>
    /// Interaction logic for AddAnimeWindow.xaml
    /// </summary>
    public partial class AddAnimeWindow : Window
    {
        ObservableCollection<AnimeListObject> JobPlates = new ObservableCollection<AnimeListObject>();
        Presenter presenter = new Presenter();

        bool success = false;
       
        public AddAnimeWindow()
        {
            InitializeComponent();
            //JobGrid.ItemsSource = _presenter.LoadJobSchedule();

        }


        //PlateObject optimalPlate = new PlateObject();





        //private int UpdatePlateTotal()
        //{
        //    int sum = 0;
        //    for (int i = 0; i < plateColorGrid.Items.Count; i++)
        //    {
        //        var plate = (JobPlateObject)plateColorGrid.Items[i];
        //        sum += plate.Quantity;
        //    }
        //    return sum;
        //}



        private void SubmitJobBtn_Click(object sender, RoutedEventArgs e)
        {
            var jobObject = new AnimeListObject();
            //int summedPlates = 0;


            //foreach (var plate in JobPlates)
            //{
            //    summedPlates += plate.Quantity;
            //}

            if (string.IsNullOrEmpty(AnimeIDTxt.Text) || string.IsNullOrEmpty(AnimeTitleTxt.Text)
                || string.IsNullOrEmpty(OverallRatingTxt.Text) || string.IsNullOrEmpty(StoryRatingTxt.Text) 
                || string.IsNullOrEmpty(AnimationRatingTxt.Text) || string.IsNullOrEmpty(MusicRatingTxt.Text) 
                || string.IsNullOrEmpty(AnimeDescriptionTxt.Text))
            {
                MessageBox.Show("All fields must contain a value.");
            }
            //else if (Convert.ToInt32(plateTotalTxt.Text) != summedPlates)
            //{
            //    MessageBox.Show("Inputted plate total does not match the grid's summed plate total.");
            //}
            else
            {
                jobObject.AnimeId = Convert.ToInt32(AnimeIDTxt.Text);
                jobObject.AnimeTitle = AnimeTitleTxt.Text;
                jobObject.OverallRating = Convert.ToDecimal(OverallRatingTxt.Text);
                jobObject.StoryRating = StoryRatingTxt.Text;
                jobObject.AnimationRating = AnimationRatingTxt.Text;
                jobObject.MusicRating = MusicRatingTxt.Text;
                jobObject.AnimeDescription = AnimeDescriptionTxt.Text;




                if (presenter.InsertJobIntoSchedule(jobObject))
                {
                    //int jobNumber = Convert.ToInt32(jobObject.AnimeId);
                    success = true;
                }

                else
                {
                    MessageBox.Show("Something went wrong when adding your job to the schedule.");
                }
                this.Close();
            }
        }





        private void AddAnotherJobBtn_Click(object sender, RoutedEventArgs e)
        {
            SubmitJobBtn_Click(this, new RoutedEventArgs());
            if (success == true)
            {
                var addJob = new AddAnimeWindow();
                addJob.ShowDialog();
            }

            
            //ClearFilterBtn_Click(this, new RoutedEventArgs());
            //var AnimeTitle = AnimeTitleTxt.Text;


            //JobGrid.ItemSource = _presenter.FilterJobGrid(AnimeTitle);
        }







        private void ValidateNoLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ValidateDecimalNumber(object sender, TextCompositionEventArgs e)
        {

            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
                e.Handled = false;

            else
                e.Handled = true;

        }
    }
}
