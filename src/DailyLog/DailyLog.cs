namespace Diary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class DiaryLog
    {
        public List<Entry> Logs
        {
            get; set;
        }

        const string fileName = "log.xml";

        private static string Path
        {
            get
            {
                return Application.StartupPath + "\\" + fileName;
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(DiaryLog));

            using (var stream = new StreamWriter(Path))
            {
                serializer.Serialize(stream, this);
            }
        }

        public string LogReport
        {
            get
            {
                var res = string.Empty;

                foreach (var item in this.Logs.Where(s => s.Log != string.Empty).OrderBy(s => s.Day).Reverse())
                {
                    res += item.Day.ToShortDateString();

                    if (item.Title != string.Empty)
                    {
                        res += " - " + item.Title;
                    }

                    res += "\n";
                    foreach (var line in item.Log.Split('\n'))
                    {
                        res += "- " + line + "\n";
                    }

                    res += "\n";
                }

                return res;
            }
        }

        public static DiaryLog OpenOrCreate()
        {
            if (File.Exists(Path))
            {
                var deserializer = new XmlSerializer(typeof(DiaryLog));

                using (var stream = new StreamReader(Path))
                {
                    var res = (DiaryLog)deserializer.Deserialize(stream);

                    return res;
                }
            }
            else
            {
                return new DiaryLog();
            }
        }

        public DiaryLog()
        {
            this.Logs = new List<Entry>();
        }

        public Entry GetForDay(DateTime day)
        {
            var dayStr = day.ToShortDateString();

            if (!this.Logs.Any(s => s.Day.ToShortDateString() == dayStr))
            {
                this.Logs.Add(new Entry() { Day = day });
            }

            return this.Logs.Where(s => s.Day.ToShortDateString() == dayStr).Single();
        }
    }
}
