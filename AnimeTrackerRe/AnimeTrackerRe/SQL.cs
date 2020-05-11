using AnimeTrackerRe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AnimeTrackerRe
{
    class SQL
    {
        
        
        
        string connectionString = "Data Source = aliasrain\\sqlexpress;Initial Catalog = AnimeTracker; Integrated Security = True";





        public void UpdateJobStatus(string jobNum, string distNum, string status)
        {
            string query = $"UPDATE Schedule SET Job_status = '{status}' WHERE Job_number = {jobNum} AND Dist_number = {distNum}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //using

                try
                {
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {

                }

                finally
                {
                    connection.Close();
                }
            }
        }


        public ObservableCollection<AnimeListObject> GetJobs()
        {
            ObservableCollection<AnimeListObject> jobsList = new ObservableCollection<AnimeListObject>();
            string query = "SELECT * FROM ListOfAnime" ;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //using
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var job = new AnimeListObject();

                    

                        job.AnimeId = Convert.ToInt32(reader["AnimeID"]);
                        job.AnimeTitle = Convert.ToString(reader["AnimeTitle"]);
                        job.OverallRating = Convert.ToInt32(reader["Overallrating"]);
                        job.StoryRating = Convert.ToString(reader["StoryRating"]);
                        job.AnimationRating = Convert.ToString(reader["AnimationRating"]);
                        job.MusicRating = Convert.ToString(reader["MusicRating"]);
                        job.AnimeDescription = Convert.ToString(reader["AnimeDescription"]);



                        jobsList.Add(job);
                    }
                    return jobsList;
                }

                catch (Exception ex)
                {
                    return new ObservableCollection<AnimeListObject>();
                }

                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
        }

        // I need to edit this to match my database
        public ObservableCollection<AnimeListObject> FilterJobs(string AnimeTitle)
        {
            ObservableCollection<AnimeListObject> jobsList = new ObservableCollection<AnimeListObject>();
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM ListOfAnime");

            //bool showCompleted = false;
            bool first = true;
            //if (status == "Complete") showCompleted = true;

         
            //////////////////////////////////////////////////////////////////
            if (!String.IsNullOrWhiteSpace(AnimeTitle) && first)
            {
                query.AppendLine($@" WHERE AnimeTitle LIKE '%{AnimeTitle}%'");
                first = false;
            }
            else if (!String.IsNullOrWhiteSpace(AnimeTitle))
            {
                query.AppendLine($@" AND AnimeTitle LIKE '%{AnimeTitle}%'");
            }
            //////////////////////////////////////////////////////////////////
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using
                SqlCommand command = new SqlCommand(query.ToString(), connection);
                connection.Open();

                //using
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var job = new AnimeListObject();

                        job.AnimeId = Convert.ToInt32(reader["AnimeID"]);
                        job.AnimeTitle = Convert.ToString(reader["AnimeTitle"]);
                        job.OverallRating = Convert.ToInt32(reader["Overallrating"]);
                        job.StoryRating = Convert.ToString(reader["StoryRating"]);
                        job.AnimationRating = Convert.ToString(reader["AnimationRating"]);
                        job.MusicRating = Convert.ToString(reader["MusicRating"]);
                        job.AnimeDescription = Convert.ToString(reader["AnimeDescription"]);

                        jobsList.Add(job);
                    }
                    return jobsList;
                }

                catch (Exception ex)
                {
                    return new ObservableCollection<AnimeListObject>();
                }

                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
        }

        public bool InsertJobIntoSchedule(AnimeListObject job)
        {
            string query = $"INSERT INTO ListOfAnime(AnimeId, AnimeTitle, OverallRating, StoryRating, AnimationRating, MusicRating, AnimeDescription)"+
                $" VALUES({job.AnimeId}, '{job.AnimeTitle}', {job.OverallRating}, '{job.StoryRating}', " +
                $"'{job.AnimationRating}', '{job.MusicRating}', '{job.AnimeDescription}' );";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //using


                try
                {
                    var success = command.ExecuteNonQuery();
                    if (success == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                catch (Exception ex)
                {
                    return false;
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        

        public int GetLastEnteredJobNumber()
        {
            string query = "SELECT MAX(Job_number) AS JobNumber FROM SCHEDULE";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //using
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    int jobNumber = 0;
                    while (reader.Read())
                    {
                        jobNumber = Convert.ToInt32(reader["JobNumber"]);
                    }
                    return jobNumber;
                }

                catch (Exception ex)
                {
                    return 0;
                }

                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
        }



        public ObservableCollection<AnimeListObject> LoadPlatesForSelectedJob(int AnimeID)
        {
            ObservableCollection<AnimeListObject> platesList = new ObservableCollection<AnimeListObject>();
            string query = $"SELECT AnimeDescription FROM ListOfAnime WHERE AnimeID = {AnimeID} ;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //using
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var job = new AnimeListObject();
                        job.AnimeDescription = Convert.ToString(reader["AnimeDescription"]);
                        
                        platesList.Add(job);
                    }
                    return platesList;
                }

                catch (Exception ex)
                {
                    return new ObservableCollection<AnimeListObject>();
                }

                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
        }

    }
}
