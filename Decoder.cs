using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laba1
{
    public class Decoder
    {
        public static void Decoding()
        {
            var decoder = new BackgroundWorker { WorkerReportsProgress = true };
            decoder.DoWork += decoder_DoWork;
            decoder.ProgressChanged += decoder_ProgressChanged;
            decoder.RunWorkerCompleted += decoder_RunWorkerCompleted;
            decoder.RunWorkerAsync();
        }

        private static void decoder_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            switch (Processor.Lang)
            {
                case Languages.Ru:
                    switch (Processor.Task)
                    {
                        case Keys.Straight:
                        case Keys.Progressive:
                            foreach (var c in Processor.Text.Where(char.IsLetter))
                            {
                                var reg = new Regex(Processor.Key[i].ToString());
                                var reg1 = new Regex(c.ToString());
                                Processor.Cipertext += char.IsLower(c)
                                    ? Processor.SmlRus[((reg1.Match(Processor.SmlRus).Index) + 33 - reg.Match(Processor.SmlRus).Index) % 33]
                                    : Processor.BigRus[((reg1.Match(Processor.BigRus).Index) + 33 - reg.Match(Processor.SmlRus).Index) % 33];
                                i++;
                                (sender as BackgroundWorker).ReportProgress(i);
                            }
                            break;
                        case Keys.Auto:
                            for (i = 0; i < Processor.KeyLen; i++)
                            {
                                var c = Processor.Text[i];
                                var reg = new Regex(Processor.Key[i].ToString());
                                var reg1 = new Regex(c.ToString());
                                Processor.Cipertext += char.IsLower(c)
                                    ? Processor.SmlRus[(reg1.Match(Processor.SmlRus).Index + 33 - reg.Match(Processor.SmlRus).Index) % 33]
                                    : Processor.BigRus[(reg1.Match(Processor.BigRus).Index + 33 - reg.Match(Processor.SmlRus).Index) % 33];
                                (sender as BackgroundWorker).ReportProgress(i);
                            }
                            for (; i < Processor.Text.Length; i++)
                            {
                                var c = Processor.Text[i];
                                var reg = new Regex(Processor.Cipertext.ToLower()[i - Processor.KeyLen].ToString());
                                var reg1 = new Regex(c.ToString());
                                Processor.Cipertext += char.IsLower(c)
                                    ? Processor.SmlRus[(reg1.Match(Processor.SmlRus).Index + 33 - reg.Match(Processor.SmlRus).Index) % 33]
                                    : Processor.BigRus[(reg1.Match(Processor.BigRus).Index + 33 - reg.Match(Processor.SmlRus).Index) % 33];
                                (sender as BackgroundWorker).ReportProgress(i);
                            }
                            break;
                    }
                    break;
                case Languages.Eng:
                    switch (Processor.Task)
                    {
                        case Keys.Straight:
                        case Keys.Progressive:
                            foreach (var c in Processor.Text.Where(char.IsLetter))
                            {
                                Processor.Cipertext += char.IsLower(c)
                                    ? Processor.SmlEng[((c - 'a') + 104 - (Processor.Key[i] - 'a')) % 26]
                                    : Processor.BigEng[((c - 'A') + 104 - (Processor.Key[i] - 'a')) % 26];
                                i++;
                                (sender as BackgroundWorker).ReportProgress(i);
                            }
                            break;
                        case Keys.Auto:
                            for (i = 0; i < Processor.KeyLen; i++)
                            {
                                var c = Processor.Text[i];
                                Processor.Cipertext += char.IsLower(c)
                                    ? Processor.SmlEng[((c - 'a') + 104 - (Processor.Key[i] - 'a')) % 26]
                                    : Processor.BigEng[((c - 'A') + 104 - (Processor.Key[i] - 'a')) % 26];
                                (sender as BackgroundWorker).ReportProgress(i);
                            }
                            for (; i < Processor.Text.Length; i++)
                            {
                                var c = Processor.Text[i];
                                Processor.Cipertext += char.IsLower(c)
                                    ? Processor.SmlEng[((c - 'a') + 104 - (Processor.Cipertext.ToLower()[i - Processor.KeyLen] - 'a')) % 26]
                                    : Processor.BigEng[((c - 'A') + 104 - (Processor.Cipertext.ToLower()[i - Processor.KeyLen] - 'a')) % 26];
                                (sender as BackgroundWorker).ReportProgress(i);
                            }
                            break;
                    }
                    break;
            }
        }

        private static void decoder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Processor.Bar.Value = e.ProgressPercentage;
        }

        private static void decoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Processor.Goal.Text = Processor.Cipertext;
        } 
    }
}