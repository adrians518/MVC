using System;

namespace MVC.Models
{
    public class TagViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public float Popularity { get; set; }

        public TagViewModel()
        {

        }

        public TagViewModel(string name, int count, float popularity)
        {
            Name = name;
            Count = count;
            Popularity = popularity;
        }
    }
}
