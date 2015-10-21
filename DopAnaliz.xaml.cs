using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Laba1
{
    /// <summary>
    /// Логика взаимодействия для DopAnaliz.xaml
    /// </summary>
    public partial class DopAnaliz
    {
        private readonly CheckBox[] _keyType;
        private readonly List<ComboBox> _keyLetters = new List<ComboBox>();
        public string Text, Key;
        public int Length;
        public readonly Languages Lang;
        private readonly FileInfo 
            _eng = new FileInfo(@"E:\Programs\VS\_TI\Laba1\TI_Laba1\Eng.txt"), 
            _rus = new FileInfo(@"E:\Programs\VS\_TI\Laba1\TI_Laba1\Rus.txt");

        public DopAnaliz(string text, int length, Languages lang)
        {
            Text = text;
            Length = length;
            Lang = lang;
            InitializeComponent();
            ReadDoubles();
            var key = Processor.GenerateKey(Text, Length);
            InitializeKey(key);
            _keyType = new[] {CbProgressive, CbStraight};
        }

        private void ReadDoubles()
        {
            var stream = _rus.OpenRead();
            var read = new StreamReader(stream);
            var i = 0;
            foreach (var number in read.ReadToEnd().Split('\n').ToList())
            {
                Processor.RusDoubles.Add(new Doubles(){Frequency = double.Parse(number),Letter = Processor.SmlRus[i++]});
            }
            Processor.RusDoubles = Processor.RusDoubles.OrderByDescending(p => p.Frequency).ToList();
            stream = _eng.OpenRead();
            read = new StreamReader(stream);
            i = 0;
            foreach (var number in read.ReadToEnd().Split('\n').ToList())
            {
                Processor.EngDoubles.Add(new Doubles(){Frequency = double.Parse(number),Letter = Processor.SmlEng[i++]});
            }
            Processor.EngDoubles = Processor.EngDoubles.OrderByDescending(p => p.Frequency).ToList();
        }

        private void InitializeKey(string key)
        {
            var count = 0;
            foreach (var c in key)
            {
                var letter = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 40
                };
                GKey.Children.Add(letter);
                Grid.SetColumn(letter, count%10);
                Grid.SetRow(letter, count/10);
                _keyLetters.Add(letter);
                var number = -1;
                for (var i = 0; i < (Lang == Languages.Ru ? Processor.SmlRus.Length : Processor.SmlEng.Length); i++)
                {
                    var temp = Lang == Languages.Ru ? Processor.SmlRus[i] : Processor.SmlEng[i];
                    letter.Items.Add(temp);
                    if (c == temp)
                        number = i;
                }
                letter.SelectedIndex = number;
                count++;
            }
        }

        private string BuildKey()
        {
            return _keyLetters.Aggregate("", (current, letter) => current + letter.Text);
        }

        private void Decoding(object sender, RoutedEventArgs e)
        {
            if (!(CbProgressive.IsChecked == true || CbStraight.IsChecked == true))
            {
                MessageBox.Show("Не выбран тип ключа", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Processor.Text = Text;
            Processor.Lang = Lang;
            Processor.Key = Processor.KeyPreparation(BuildKey(),
                CbProgressive.IsChecked == true ? Keys.Progressive : Keys.Straight, Text, Lang);
            Processor.Goal = TbSourceText;
            Processor.Cipertext = "";
            Decoder.Decoding();
        }

        private void Cb_Checked(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 2; i++)
            {
                _keyType[i].IsChecked = Equals(_keyType[i], e.Source);
            }
        }
    }
}
