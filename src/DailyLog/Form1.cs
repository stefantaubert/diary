using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diary
{
    public partial class Form1 : Form
    {
        DiaryLog log;

        Entry currentEntry;

        public Form1()
        {
            InitializeComponent();

            this.log = DiaryLog.OpenOrCreate();
            this.LoadFromPicker();
            this.richTextBox2.Text = this.log.LogReport;
        }

        private void LoadFromPicker()
        {
            this.currentEntry = this.log.GetForDay(this.dateTimePicker1.Value);
            this.richTextBox1.Text = this.currentEntry.Log;
            this.textBox1.Text = this.currentEntry.Title;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.LoadFromPicker();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.currentEntry.Log = this.richTextBox1.Text;
            this.richTextBox2.Text = this.log.LogReport;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.currentEntry.Title = this.textBox1.Text;
            this.richTextBox2.Text = this.log.LogReport;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.log.Save();
        }
    }
}
