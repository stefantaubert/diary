namespace Diary
{
    using System;

    public class Entry
    {
        public Entry()
        {
            this.Log = string.Empty;
            this.Title = string.Empty;
        }

        public string Log { get; set; }

        public string Title { get; set; }

        public DateTime Day { get; set; }
    }
}
