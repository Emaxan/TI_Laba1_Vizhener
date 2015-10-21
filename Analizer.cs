using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Laba1
{
    public class Analizer
    {
        public static void AnalizeCrypto()
        {
            var analizer = new BackgroundWorker { WorkerReportsProgress = true };
            analizer.DoWork += analizer_DoWork;
            analizer.ProgressChanged += analizer_ProgressChanged;
            analizer.RunWorkerCompleted += analizer_RunWorkerCompleted;
            analizer.RunWorkerAsync();
            Processor.DeactivateElements();
        }

        private static void analizer_DoWork(object sender, DoWorkEventArgs e)
        {
            Processor.AnalizeResult = Processor.AnalizeLog = "";
            var sum = 0;
            var ans = new List<Pair>();
            var generalDistanses = new List<int>();
            while (sum == 0)
            {
                Processor.AnalizeResult = "";
                Processor.AnalizeLog = "";
                generalDistanses.Clear();
                if (Processor.DigramLength > 20)
                {
                    Processor.AnalizeResult = "невозможно определить длинну ключа";
                    (sender as BackgroundWorker).ReportProgress(Processor.AnalizeText.Length - Processor.DigramLength + 1001);
                    return;
                }
                Processor.DigramLength++;

                var generalNod = new int[10000];
                var usedDigrams = new HashSet<string>();
                var privateDistanses = new List<int>();
                for (var i = 0; i < Processor.AnalizeText.Length - Processor.DigramLength + 1; i++)
                {
                    privateDistanses.Clear();
                    (sender as BackgroundWorker).ReportProgress(i + 1);

                    if (Processor.Task != Keys.Progressive)
                    {
                        var myMatch = Processor.Find(Processor.AnalizeText, Processor.AnalizeText.Substring(i, Processor.DigramLength).ToLower());
                        if (myMatch.Count < 2 || usedDigrams.Contains(myMatch[0].Value)) continue;
                        Processor.AnalizeLog += myMatch[0].Value + ' ';
                        for (var j = 0; j < myMatch.Count - 1; j++)
                        {
                            Processor.AnalizeLog += j != myMatch.Count - 2
                                ? (myMatch[j + 1].Index - myMatch[j].Index) + ", "
                                : (myMatch[j + 1].Index - myMatch[j].Index).ToString();
                            privateDistanses.Add(myMatch[j + 1].Index - myMatch[j].Index);
                        }
                    }
                    else
                    {
                        var match = Processor.FindProgressive(Processor.AnalizeText, Processor.AnalizeText.Substring(i, Processor.DigramLength));
                        if (match.Count() < 2 || usedDigrams.Contains(Processor.AnalizeText.Substring(i, Processor.DigramLength).ToLower()))
                            continue;
                        var temp = Processor.AnalizeText.Substring(i, Processor.DigramLength);
                        for (var j = 0; j < (Processor.Lang == Languages.Ru ? Processor.SmlRus.Length : Processor.SmlEng.Length) - 1; j++)
                        {
                            temp = Processor.IncString(temp);
                            usedDigrams.Add(temp);
                        }
                        Processor.AnalizeLog += Processor.AnalizeText.Substring(i, Processor.DigramLength) + ' ';
                        for (var j = 0; j < match.Count() - 1; j++)
                        {
                            Processor.AnalizeLog += j != match.Count() - 2
                                ? (match[j + 1] - match[j]) + ", "
                                : (match[j + 1] - match[j]).ToString();
                            privateDistanses.Add(match[j + 1] - match[j]);
                        }
                    }

                    var privateNod = privateDistanses[0];
                    for (var j = 1; j < privateDistanses.Count; j++)
                    {
                        privateNod = Processor.Nod(privateNod, privateDistanses[j]);
                    }
                    generalDistanses.Add(privateNod);
                    if (privateDistanses.Count != 1)
                        Processor.AnalizeLog += "\nНОД: " + privateNod + "\n\n";
                    else
                    {
                        Processor.AnalizeLog += "\nРезультат: " + privateNod + "\n\n";
                    }
                    usedDigrams.Add(Processor.AnalizeText.Substring(i, Processor.DigramLength).ToLower());
                }


                if (generalDistanses.Count > 1)
                    foreach (var t1 in generalDistanses)
                        foreach (var t in generalDistanses.Where(x => x != t1))
                            generalNod[Processor.Nod(t, t1)]++;
                else if (generalDistanses.Count > 0)
                    generalNod[generalDistanses[0]]++;

                (sender as BackgroundWorker).ReportProgress(Processor.AnalizeText.Length - Processor.DigramLength + 5);

                for (var i = 3; i < 30; i++)
                {
                    if (generalNod[i] == 0) continue;
                    ans.Add(new Pair
                    {
                        Index = i,
                        Value = generalNod[i]
                    });
                    sum += generalNod[i];
                    (sender as BackgroundWorker).ReportProgress(Processor.AnalizeText.Length - Processor.DigramLength + 5 + i);
                }
            }

            var anss = ans.OrderByDescending(p => p.Value).Take(1);
            Processor.AnalizeResult += anss.Where(s => s.Value > 0)
                .Aggregate("Возможный размер ключа:",
                    (current, s) => current + (string.Format("\n{0,-5} ({1,6:P})", s.Index, (float)s.Value / sum)));
            (sender as BackgroundWorker).ReportProgress(Processor.AnalizeText.Length - Processor.DigramLength + 501);
        }

        private static void analizer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Processor.Bar.Value = e.ProgressPercentage;
        }

        private static void analizer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Processor.KasiskeKey.Text = Processor.AnalizeResult;
            Processor.KasiskeDigrams.Text = Processor.AnalizeLog;
            Processor.DigramLength = Processor.MinDigramLength;
            Processor.DeactivateElements();
        }
    }
}