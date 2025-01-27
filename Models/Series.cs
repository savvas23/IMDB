using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MvcSeries.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster_path { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Seasons { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
        public string Cast {get; set;}
        public int Votes {get; set;}
        public List<string> Likes {get;set;}
        public List<string> Dislikes {get; set;}
    }

    public class SeriesComment
    {
        public int Id { get; set; }

        public int SId { get; set; }
        public string Creator { get; set; }

        [DataType(DataType.Date)]
        public DateTime PubDate { get; set; } = DateTime.Now;
        public string Text { get; set; }
    }
}