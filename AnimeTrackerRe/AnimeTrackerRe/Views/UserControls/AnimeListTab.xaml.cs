using AnimeTrackerRe.Models;
using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Navigation;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

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
            TotalAnimeTracker();


            //webBrowser1_LoadCompleted(this, new NavigationEventArgs());
            //(WebBrowserGrid.Child as System.Windows.Forms.WebBrowser).Navigate("http://www.wikipedia.com");
            //(wfhSample.Child as System.Windows.Forms.WebBrowser).Navigate("https://www.wikipedia.com");
            //(Grid1.Children as System.Windows.Forms.WebBrowser).Navigate("http://www.wikipedia.com");





        }

        private Presenter _presenter = new Presenter();
        bool LoadComplete = false;


        private void TotalAnimeTracker()
        {
            List<AnimeListObject> AnimeList = _presenter.LoadJobSchedule().ToList();

            TotalAnimeTxt.Text = AnimeList.Count.ToString();
        }


        private void StopPaste(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

         




        }

        private void AddAnimeBtn_Click(object sender, RoutedEventArgs e)

        {
            var AnimeTitle = "";
            JobGrid.ItemsSource = _presenter.FilterJobGrid(AnimeTitle);
            ClearFilterBtn_Click(this, new RoutedEventArgs());

            var addJob = new AddAnimeWindow();
            //var addJob = new AddJob();
            addJob.ShowDialog();
            ClearFilterBtn_Click(this, new RoutedEventArgs());



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
           
            if (e.Key == Key.RightShift)
            {
                AddAnimeBtn_Click(this, new RoutedEventArgs());
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
            TotalAnimeTracker();
            AnimeTitleTxt.Clear();
        

            JobGrid.ItemsSource = _presenter.LoadJobSchedule();
            //JobPlatesGrid.ItemsSource = "";
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

        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //Uri webURL = new Uri("http://www.wikipedia.com", UriKind.RelativeOrAbsolute);
            //this.WebBrowserGrid.Navigate(webURL);

            //WebBrowserGrid.Source = "http://www.wikipedia.com";
            //WebBrowserGrid.Navigate("http://www.wikipedia.com");
            //dynamic doc = WebBrowserGrid.Document;

            //this.AnimeTitleTxt = GetPropertyValue<string>(doc, "Title");
        }

        //private T GetPropertyValue<T>(object obj, string propertyName)
        //{
        //    Type objectType = obj.GetType();
        //    StylusPointPropertyInfo propertyInfo = objectType.GetProperty(propertyName);
        //    Type propertyType = propertyInfo.PropertyType;
        //    if(propertyType == typeof(T))
        //    {
        //        object propertyValue = (T)propertyInfo.GetValue(obj, null);
        //        return Value;
        //    }
        //    else
        //    {
        //        throw new Exception("Property " + propertyName + " is not of type " + T);
        //    }
        //}


        private void JobGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedJob = (AnimeListObject)JobGrid.SelectedItem;
            if (selectedJob != null && LoadComplete == true)
            {
                var Title = selectedJob.AnimeTitle;

                //WebBrowserGrid.Navigate("http://en.wikipedia.org");
                //foreach (HtmlElement elem in (WebBrowserGrid.Document as mshtml.HTMLDocument).getElementsByTagName("input"))
                //{

                //}
                //(WebBrowserGrid as mshtml.HTMLDocument) = "http://en.wikipedia.org";
                var URL = _presenter.LoadPlatesForSelectedJob(Convert.ToString(Title));
                (WebBrowserGrid.Document as mshtml.HTMLDocument).getElementById("searchInput").innerText = "List of " + URL + " Episodes";
                //(WebBrowserGrid.Document as mshtml.HTMLDocument).getElementById("searchInput").setAttribute("Value",  "List of " + URL + "Episodes");
                (WebBrowserGrid.Document as mshtml.HTMLDocument).getElementById("searchButton").click();
                System.Threading.Thread.Sleep(1000);

                //JobPlatesGrid.Items.Refresh();
            }
        }

        private void WebPage_SearchClick(object sender, RoutedEventArgs e)
        {
            (WebBrowserGrid.Document as mshtml.HTMLDocument).getElementById("searchInput");

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
           
            var AnimeTitle = AnimeTitleTxt.Text;

            JobGrid.ItemsSource = _presenter.FilterJobGrid(AnimeTitle);
            JobGrid.SelectedIndex = 0;
            var selectedJob = (AnimeListObject)JobGrid.SelectedItem;
            //if (selectedJob != null)
            //{
            //    JobPlatesGrid.ItemsSource = _presenter.LoadPlatesForSelectedJob(Convert.ToInt32(selectedJob.AnimeId));
            //    JobPlatesGrid.Items.Refresh();
            //}
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var SelectedJob = (AnimeListObject)JobGrid.SelectedItem;
            var SelectedAnimeID = SelectedJob.AnimeId;

            if (MessageBox.Show($"Do you really want to remove Anime #{SelectedAnimeID}?!","Message", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                if (SelectedJob != null)
                {
                    JobGrid.ItemsSource = _presenter.DeleteRow(Convert.ToInt32(SelectedJob.AnimeId));
                    ClearFilterBtn_Click(this, new RoutedEventArgs());
                }

            }


            

        }


        //////////////////////////////////////////////////
        ///web browser commands testing
        //private void wbSample_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        //{
        //    //var selectedJob = (AnimeListObject)JobGrid.SelectedItem;

        //    AnimeTitleTxt.Text = e.Uri.OriginalString;
        //    WebBrowserGrid.Navigate(AnimeTitleTxt.Text);
        //}

        //private void GoToPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        //private void GoToPage_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    WebBrowserGrid.Navigate(AnimeTitleTxt.Text);
        //}



        //private void GridWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // Create the interop host control.
        //    System.Windows.Forms.WebBrowser host =
        //        new System.Windows.Forms.WebBrowser();

        //    // Create the MaskedTextBox control.
        //    MaskedTextBox mtbDate = new MaskedTextBox("00/00/0000");

        //    // Assign the MaskedTextBox control as the host control's child.
        //    host.Child = mtbDate;

        //    // Add the interop host control to the Grid
        //    // control's collection of child controls.
        //    this.grid1.Children.Add(host);
        //}

        private void GridWindow_Loaded(object sender, EventArgs e)
        {
            // Create the interop host control.
            //WindowsFormsHost host =
            //    new WindowsFormsHost();

            // Create the MaskedTextBox control.
            //System.Windows.Forms.WebBrowser WebBrowserTest = new System.Windows.Forms.WebBrowser();

            // Assign the MaskedTextBox control as the host control's child.
            //host.Child = WebBrowserTest;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            //this.Grid1.Children.Add(host);

            //(WBHost.Child as System.Windows.Forms.WebBrowser).Navigate("https://www.wikipedia.com");
            //(WBHost.Child as System.Windows.Forms.WebBrowser) = new System.Windows.Forms.WebBrowser();
            //WBHost.Child = new System.Windows.Forms.WebBrowser();

            //System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
            //WBHost.Child.Controls.Add(WBHost.Child);

            //(WBHost.Child as System.Windows.Forms.WebBrowser).AllowNavigation = true;

            //(WBHost.Child as System.Windows.Forms.WebBrowser).DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);

            //(WBHost.Child as System.Windows.Forms.WebBrowser).Navigate("http://www.wikipedia.com");
            //WebBrowserGrid.Navigate("http://en.wikipedia.org");


            //(WebBrowserGrid.Document as mshtml.HTMLDocument) += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);

            //HTMLDocument Doc = this.WebBrowserGrid.Document as mshtml.HTMLDocument;

            //Doc.open("http://en.wikipedia.org");
            WebBrowserGrid.Navigate("http://en.wikipedia.org");
            System.Threading.Thread.Sleep(5000);


            LoadComplete = true;

        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            System.Windows.Forms.WebBrowser wb = sender as System.Windows.Forms.WebBrowser;
            // wb.Document is not null at this point
        }

        private System.Windows.Forms.WebBrowser HtmlEditor = new System.Windows.Forms.WebBrowser();

        //public HtmlEditControl()
        //{

        //    InitializeComponent();
        //    HtmlEditor.DocumentText = "<html><body></body></html>";
        //    HtmlEditor.DocumentCompleted += (sender, e) =>
        //    {
        //        WebBrowserGrid = (IHTMLDocument2)HtmlEditor.Document.DomDocument;
        //        myDoc.designMode = "On";
        //        HtmlEditor.Refresh(WebBrowserRefreshOption.Completely);
        //        myContentsChanged = false;
        //    };
        //}



        //private void wbWinForms_DocumentTitleChanged(object sender, EventArgs e)
        //{
        //    //this.Title = (sender as System.Windows.Forms.WebBrowser).DocumentTitle;
        //}






    }
    
}
