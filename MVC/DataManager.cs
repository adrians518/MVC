using System.Collections.Generic;
using System.Linq;
using MVC.Models;

namespace MVC
{
    public static class DataManager
    {
        private static StackExchangeClient client;
        public static IReadOnlyList<TagViewModel> Tags { get; private set; }

        public static void Initialize()
        {
            client = new StackExchangeClient("2.3", "stackoverflow");

            List<Tag> tags = new List<Tag>();
            for (uint page = 1; page <= 10; page++)
            {
                Tag[] pageTags = client.GetTags(page);
                tags.AddRange(pageTags);
            }
            int total = tags.Sum(tag => tag.Count);
            var fetchedTags = new TagViewModel[tags.Count];
            for (int i = 0; i < fetchedTags.Length; i++)
            {
                Tag tag = tags[i];
                float popularity = 100.0f * tag.Count / total;
                fetchedTags[i] = new TagViewModel { Name = tag.Name, Count = tag.Count, Popularity = popularity };
            }
            Tags = fetchedTags;
        }
    }
}
