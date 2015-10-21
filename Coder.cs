using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laba1
{
    public class Coder
    {
        public static void Coding()
        {
            var coder = new BackgroundWorker { WorkerReportsProgress = true };
            coder.DoWork += coder_DoWork;
            coder.ProgressChanged += coder_ProgressChanged;
            coder.RunWorkerCompleted += coder_RunWorkerCompleted;
            coder.RunWorkerAsync();
        }

        private static void coder_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            switch (Processor.Lang)
            {
                case Languages.Ru:
                    foreach (var c in Processor.Text.Where(char.IsLetter))
                    {
                        var reg = new Regex(Processor.Key[i].ToString());
                        var reg1 = new Regex(c.ToString());
                        Processor.Cipertext += char.IsLower(c)
                            ? Processor.SmlRus[((reg1.Match(Processor.SmlRus).Index) + reg.Match(Processor.SmlRus).Index) % 33]
                            : Processor.BigRus[((reg1.Match(Processor.BigRus).Index) + reg.Match(Processor.SmlRus).Index) % 33];
                        i++;
                        (sender as BackgroundWorker).ReportProgress(i);
                    }
                    break;
                case Languages.Eng:
                    foreach (var c in Processor.Text.Where(char.IsLetter))
                    {
                        Processor.Cipertext += char.IsLower(c)
                            ? Processor.SmlEng[((c - 'a') + (Processor.Key[i] - 'a')) % 26]
                            : Processor.BigEng[((c - 'A') + (Processor.Key[i] - 'a')) % 26];
                        i++;
                        (sender as BackgroundWorker).ReportProgress(i);
                    }
                    break;
            }
        }

        private static void coder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Processor.Bar.Value = e.ProgressPercentage;
        }

        private static void coder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Processor.Goal.Text = Processor.Cipertext;
        } 
    }
}