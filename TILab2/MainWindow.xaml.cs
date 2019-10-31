using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Encoders;
using Microsoft.Win32;
using TILab2.DataConverters;

namespace TILab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RadioButton activeRadioButton;
        private Dictionary<Control, EncoderParams> ciphers;
        private byte[] dataText = new byte[0], 
                       dataCiphertext = new byte[0];


        public MainWindow()
        {
            InitializeComponent();

            LFSREncoder lfsrEncoder = new LFSREncoder(new byte[] { 0, 14, 15, 29 });
            RC4Encoder rc4Encoder = new RC4Encoder();

            ciphers = new Dictionary<Control, EncoderParams> {
                { lfsrRB, new EncoderParams{
                    encoder = lfsrEncoder,
                    converter = new LFSRDataConverter(),
                    keyGetter = lfsrEncoder.GetKey} },
                { rc4RB, new EncoderParams{
                    encoder = rc4Encoder,
                    converter = new RC4DataConverter(),
                    keyGetter = rc4Encoder.GetKey} }
            };
        }

        /// <summary>
        /// Зашифровывает dataText в dataCiphertext и выводит ключ
        /// </summary>
        /// <param name="eparams">Параметры шифратора</param>
        public void Encrypt(EncoderParams eparams)
        {
            byte[] keyBt = eparams.converter.ToByteArray(key.Text);
            dataCiphertext = eparams.encoder.Encrypt(dataText, keyBt);
            ciphertext.Text = eparams.converter.ToText(dataCiphertext, 50);

            MessageBox.Show(eparams.converter.ToText(eparams.keyGetter(keyBt, 50)), "Ключ (до 50 байт)");
        }

        /// <summary>
        /// Расшифровывает dataCiphertext в dataText и выводит ключ
        /// </summary>
        /// <param name="eparams">Параметры шифратора</param>
        public void Decrypt(EncoderParams eparams)
        {
            byte[] keyBt = eparams.converter.ToByteArray(key.Text);
            dataText = eparams.encoder.Decrypt(dataCiphertext, keyBt);
            text.Text = eparams.converter.ToText(dataText, 50);

            MessageBox.Show(eparams.converter.ToText(eparams.keyGetter(keyBt, 50)), "Ключ (до 50 байт)");
        }

        /// <summary>
        /// Устанавливает последовательность байтов data в качестве текста в textBox
        /// </summary>
        /// <param name="textBox">Компонент, куда будет заноситься текст</param>
        /// <param name="data">Последовательность байтов</param>
        /// <param name="converter">Конвертер в string</param>
        public void UpdateText(TextBox textBox, byte[] data, IDataConverter converter)
        {
            if (textBox?.IsLoaded ?? false)
                textBox.Text = converter.ToText(data, 50);
        }

        /// <summary>
        /// Изменяет элементы массива data на соответсвующие числа в textBox
        /// </summary>
        /// <param name="textBox">Компонент, откуда берется текст</param>
        /// <param name="data">Последовательность байтов для изменения</param>
        /// <param name="converter">Конвертер в byte[]</param>
        public void UpdateData(TextBox textBox, ref byte[] data, IDataConverter converter)
        {
            byte[] ndata = converter.ToByteArray(textBox.Text);

            if (ndata.Length > data.Length)
                Array.Resize(ref data, ndata.Length);

            for (int i = 0; i < ndata.Length; i++)
                data[i] = ndata[i];
        }





        private void EncryptClick(object sender, RoutedEventArgs e)
        {
            Encrypt(ciphers[activeRadioButton]);   
        }

        private void DecryptClick(object sender, RoutedEventArgs e)
        {

            Decrypt(ciphers[activeRadioButton]);
        }

        private void RB_Checked(object sender, RoutedEventArgs e)
        {
            activeRadioButton = sender as RadioButton;

            UpdateText(ciphertext, dataCiphertext, ciphers?[activeRadioButton].converter);
            UpdateText(text, dataText, ciphers?[activeRadioButton].converter);
        }

        private void Ciphertext_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData(ciphertext, ref dataCiphertext, ciphers[activeRadioButton].converter);
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData(text, ref dataText, ciphers[activeRadioButton].converter);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();

            if (OF.ShowDialog() == true)
            {
                textSrcBox.Text = OF.FileName;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();

            if (OF.ShowDialog() == true)
            {
                ciphertextSrcBox.Text = OF.FileName;
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            UpdateData(text, ref dataText, ciphers[activeRadioButton].converter);
            UpdateData(ciphertext, ref dataCiphertext, ciphers[activeRadioButton].converter);

            File.WriteAllBytes(textSrcBox.Text, dataText);
            File.WriteAllBytes(ciphertextSrcBox.Text, dataCiphertext);
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            if (File.Exists(textSrcBox.Text))
            {
                dataText = File.ReadAllBytes(textSrcBox.Text);

                UpdateText(text, dataText, ciphers[activeRadioButton].converter);
            }

            if (File.Exists(ciphertextSrcBox.Text))
            {
                dataCiphertext = File.ReadAllBytes(ciphertextSrcBox.Text);

                UpdateText(ciphertext, dataCiphertext, ciphers[activeRadioButton].converter);
            }
        }
    }
}
