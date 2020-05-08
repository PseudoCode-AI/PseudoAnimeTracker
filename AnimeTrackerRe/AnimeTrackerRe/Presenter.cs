using AnimeTrackerRe.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeTrackerRe
{
    class Presenter
    {
   
        SQL SQLCaller = new SQL();

        internal void UpdateJobStatus(string jobNum, string distNum, string status)
        {
            SQLCaller.UpdateJobStatus(jobNum, distNum, status);
        }

        public ObservableCollection<AnimeListObject> LoadJobSchedule()
        {
            return SQLCaller.GetJobs();
        }

        public ObservableCollection<AnimeListObject> LoadPlatesForSelectedJob(int AnimeID)
        {
            return SQLCaller.LoadPlatesForSelectedJob(AnimeID);
        }


        public ObservableCollection<AnimeListObject> FilterJobGrid(string AnimeTitle)
        {
            return SQLCaller.FilterJobs(AnimeTitle);
        }

        public bool InsertJobIntoSchedule(AnimeListObject job)
        {
            return SQLCaller.InsertJobIntoSchedule(job);
        }

       

        public int GetLastEnteredJobNumber()
        {
            return SQLCaller.GetLastEnteredJobNumber();
        }

        

       
    }
}
