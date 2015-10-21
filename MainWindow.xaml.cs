using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.Win32;

namespace Laba1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly CheckBox[] _keyBoxs, _languageBoxs;

        public MainWindow()
        {
            InitializeComponent();
            _keyBoxs = new[] {Cb1, Cb2, Cb3};
            _languageBoxs = new[] {CbEn, CbRu};
            Processor.Bar = Bar;
            Processor.KasiskeDigrams = KasiskeNew;
            Processor.KasiskeKey = KasiskeText;
            Processor.CheckBoxs.AddRange(_keyBoxs);
            Processor.CheckBoxs.AddRange(_languageBoxs);
            Processor.Buttons.AddRange(new[] {BCoding, BDecoding, BOpen, BSave, Analize, DopAnaliz});
            Processor.TextBoxs.AddRange(new[] {TbMain, TbCrypto});
            TbMain.Focus();
        }

        private void Cb_Checked(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 3; i++)
            {
                _keyBoxs[i].IsChecked = Equals(_keyBoxs[i], e.Source);
            }
        }

        private void CbLang_Checked(object sender, RoutedEventArgs e)
        {
            if (Equals(e.OriginalSource, _languageBoxs[1]))
            {
                _languageBoxs[0].IsChecked = false;
            }
            else
            {
                _languageBoxs[1].IsChecked = false;
            }
        }
        
        private void BKasiske_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da, dw;
            switch (LKasiske.Text)
            {
                case ">":
                    LKasiske.Text = "<";
                    da = new DoubleAnimation()
                    {
                        From = Width,
                        To = Width + 200,
                        Duration = TimeSpan.FromSeconds(1),
                    };
                    dw = new DoubleAnimation()
                    {
                        From = Left,
                        To = Left - 100,
                        Duration = TimeSpan.FromSeconds(1),
                    };
                    WMain.BeginAnimation(WidthProperty, da);
                    WMain.BeginAnimation(LeftProperty, dw);
                    break;
                case "<":
                    LKasiske.Text = ">";
                    da = new DoubleAnimation()
                    {
                        From = Width,
                        To = Width - 200,
                        Duration = TimeSpan.FromSeconds(1),
                    };
                    dw = new DoubleAnimation()
                    {
                        From = Left,
                        To = Left + 100,
                        Duration = TimeSpan.FromSeconds(1),
                    };
                    WMain.BeginAnimation(WidthProperty, da);
                    WMain.BeginAnimation(LeftProperty, dw);
                    break;
            }
        }

        private void Analize_Click(object sender, RoutedEventArgs e)
        {
            Processor.DigramLength = Processor.MinDigramLength;
            if (!(CbRu.IsChecked == true || CbEn.IsChecked == true))
            {
                MessageBox.Show("Не выбран язык", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Processor.Lang = CbRu.IsChecked == true ? Languages.Ru : Languages.Eng;
            KasiskeText.Text = "";
            KasiskeNew.Text = "";
            Processor.AnalizeText = TbCrypto.Text;
            Bar.Maximum = Processor.AnalizeText.Length - Processor.DigramLength + 501;
            Analizer.AnalizeCrypto();
        }

        private void Coding_Click(object sender, RoutedEventArgs e)
        {
            if (!(Cb1.IsChecked == true || Cb2.IsChecked == true || Cb3.IsChecked == true))
            {
                MessageBox.Show("Не выбран тип ключа", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Processor.Task = Cb1.IsChecked == true
                ? Keys.Straight : Cb2.IsChecked == true ? Keys.Progressive : Keys.Auto;

            if (!(CbRu.IsChecked == true || CbEn.IsChecked == true))
            {
                MessageBox.Show("Не выбран язык", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Processor.Lang = CbRu.IsChecked == true ? Languages.Ru : Languages.Eng;

            switch (Processor.Lang)
            {
                case Languages.Ru:
                    Processor.Key =
                        TbKey.Text.Where(t => Processor.RussianText.Contains(t))
                            .Aggregate("", (current, t) => current + t).ToLower();
                    Processor.Text = TbMain.Text.Where(t => Processor.RussianText.Contains(t))
                        .Aggregate("", (current, t) => current + t);
                    break;
                case Languages.Eng:
                    Processor.Key =
                        TbKey.Text.Where(t => Processor.EnglishText.Contains(t))
                            .Aggregate("", (current, t) => current + t).ToLower();
                    Processor.Text = TbMain.Text.Where(t => Processor.EnglishText.Contains(t))
                        .Aggregate("", (current, t) => current + t);
                    break;
            }


            if (Processor.Text.Length == 0 || Processor.Key.Length == 0) return;

            ExtendedKeyText.Text =
                Processor.Key =
                    Processor.KeyPreparation(Processor.Key, Processor.Task, Processor.Text, Processor.Lang).ToLower();

            if (Processor.Text.Length == 0 || Processor.Key.Length == 0) return;

            Bar.Maximum = Processor.Text.Where(char.IsLetter).Aggregate("", (curent, t) => curent + t).Length;
            TbCrypto.Text = Processor.Cipertext = "";
            Processor.Goal = TbCrypto;
            
            Coder.Coding();
        }

        private void Decoding_Click(object sender, RoutedEventArgs e)
        {
            if (!(Cb1.IsChecked == true || Cb2.IsChecked == true || Cb3.IsChecked == true))
            {
                MessageBox.Show("Не выбран тип ключа", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Processor.Task = Cb1.IsChecked == true
                ? Keys.Straight : Cb2.IsChecked == true ? Keys.Progressive : Keys.Auto;

            if (!(CbRu.IsChecked == true || CbEn.IsChecked == true))
            {
                MessageBox.Show("Не выбран язык", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Processor.Lang = CbRu.IsChecked == true ? Languages.Ru : Languages.Eng;

            switch (Processor.Lang)
            {
                case Languages.Ru: //ru
                    Processor.Key =
                        TbKey.Text.Where(t => Processor.RussianText.Contains(t))
                            .Aggregate("", (current, t) => current + t).ToLower();
                    Processor.Text = TbCrypto.Text.Where(t => Processor.RussianText.Contains(t))
                        .Aggregate("", (current, t) => current + t);
                    break;
                case Languages.Eng: //en
                    Processor.Key =
                        TbKey.Text.Where(t => Processor.EnglishText.Contains(t))
                            .Aggregate("", (current, t) => current + t).ToLower();
                    Processor.Text = TbCrypto.Text.Where(t => Processor.EnglishText.Contains(t))
                        .Aggregate("", (current, t) => current + t);
                    break;
            }
            Processor.KeyLen = Processor.Key.Length;

            if (Processor.Text.Length == 0 || Processor.Key.Length == 0) return;

            Processor.Key =
                Processor.KeyPreparation(Processor.Key, Processor.Task, Processor.Text, Processor.Lang).ToLower();

            switch (Processor.Task)
            {
                case Keys.Straight:
                case Keys.Progressive:
                    ExtendedKeyText.Text = Processor.Key;
                    break;
                case Keys.Auto:
                    ExtendedKeyText.Text = "Нельзя определить до частичной расшифровки текста!";
                    break;
            }

            if (Processor.Text.Length == 0 || Processor.Key.Length == 0) return;

            Bar.Maximum = Processor.Text.Where(char.IsLetter).Aggregate("", (curent, t) => curent + t).Length;
            TbMain.Text = Processor.Cipertext = "";
            Processor.Goal = TbMain;

            Decoder.Decoding();
        }

        private void BOpen_Click(object sender, RoutedEventArgs e)
        {
            var mbr = MessageBox.Show("Вы открываете исходный текст(да) или шифротекст(нет)?", "Подтвердите действие!",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes,
                MessageBoxOptions.RightAlign);

            if (mbr == MessageBoxResult.Cancel) return;

            var ofd = new OpenFileDialog
            {
                Title = "Открыть файл",
                Multiselect = false,
                InitialDirectory = @"E:\Programs\VS\_TI\Laba1\",
                Filter = "Текстовые файлы (*.txt)|*.txt",
                FilterIndex = 1
            };

            if (ofd.ShowDialog() != true) return;

            var path = ofd.FileNames[0];

            var inStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 8,
                FileOptions.SequentialScan);

            var br = new StreamReader(inStream, Encoding.UTF8, false, 8, false);

            var s = br.ReadToEnd();

            br.Close();

            switch (mbr)
            {
                case MessageBoxResult.Yes:
                    TbMain.Text = s;
                    break;
                case MessageBoxResult.No:
                    TbCrypto.Text = s;
                    break;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            var mbr = MessageBox.Show("Вы хотите сохранить исходный текст(да) или шифротекст(нет)?",
                "Подтвердите действие!", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes,
                MessageBoxOptions.RightAlign);

            if (mbr == MessageBoxResult.Cancel) return;

            var ofd = new SaveFileDialog
            {
                Title = "Cохранить файл",
                InitialDirectory = @"E:\Programs\VS\_TI\Laba1\",
                Filter = "Текстовые файлы (*.txt)|*.txt",
                FilterIndex = 1
            };

            if (ofd.ShowDialog() != true) return;
            

            var path = ofd.FileNames[0];

            var outStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 8,
                FileOptions.WriteThrough);

            var bw = new StreamWriter(outStream, Encoding.UTF8, 8, false);

            switch (mbr)
            {
                case MessageBoxResult.Yes:
                    bw.Write(TbMain.Text);
                    break;
                case MessageBoxResult.No:
                    bw.Write(TbCrypto.Text);
                    break;
            }
            bw.Close();
        }

        private void DopAnaliz_Click(object sender, RoutedEventArgs e)
        { 
            if (KasiskeText.Text.Trim()=="") return;
            var length = KasiskeText.Text[24] - '0';
            if (char.IsDigit(KasiskeText.Text[25]))
            {
                length *= 10;
                length += KasiskeText.Text[25] - '0';
            }
            var dopAnaliz = new DopAnaliz(TbCrypto.Text, length, CbRu.IsChecked == true ? Languages.Ru : Languages.Eng);
            dopAnaliz.Show();
        }

        private void WMain_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
