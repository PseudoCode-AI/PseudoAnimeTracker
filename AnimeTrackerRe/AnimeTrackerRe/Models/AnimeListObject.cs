using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeTrackerRe.Models
{
    class AnimeListObject
    {
        public int AnimeId { get; set; }
        public string AnimeTitle { get; set; }
        public decimal OverallRating { get; set; }
        public string StoryRating { get; set; }
        public string AnimationRating { get; set; }
        public string MusicRating { get; set; }
        public string AnimeDescription { get; set; }
        public List<AnimeListObject> AnimeList { get; set; }
    }
}
