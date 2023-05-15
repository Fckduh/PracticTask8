using BarcodeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Aspose.BarCode;
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;
using System.Drawing;
using System.Drawing.Imaging;
using Color = System.Drawing.Color;
using Aspose.BarCode.Generation;
using System.Runtime.InteropServices.ComTypes;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Data.Entity;
using PracticTask8.Ado;
using System.ComponentModel;
using System.Windows.Controls;

namespace PracticTask8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        public class Barcode
    {
           
            
        public string text { get; set; }
        public BaseEncodeType BarcodeType { get; set; }
        public BarCodeImageFormat ImageType { get; set; }
           
    }
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        public MainWindow()
        {
            InitializeComponent();
        }
        private string GenerateBarcode(Barcode barcode)
        {
            List<Byte> Bcodes = new List<Byte>();
            using (BarcodesContext db = new BarcodesContext())
            {
                int num = 0;
                Random rnd = new Random();
                b.IncludeLabel = true;
                string imagePath = "";
                for (int i = 0; i < 20; i++)//По сути сам генератор баркодов
                { 
                    num = num + 1;
                    imagePath = num + "." + barcode.ImageType;
                    System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.CODE128, Convert.ToString(rnd.Next(1000000, 10000000)), Color.Black, Color.White, 250, 110);
                    img.Save(imagePath);
                    
                    
                    //Вставка баркодов в бд
                    byte[] image_bytes = File.ReadAllBytes(imagePath);

                    Bcodes.AddRange(image_bytes);
                }

                    db.DataBaseB.AddRange((IEnumerable<Images3>)Bcodes);
                    db.SaveChanges();   
               return imagePath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//Кнопка для вызова генерации баркодов
        {
                Random rnd = new Random();
                var imageType = "Png";
                var imageFormat = (BarCodeImageFormat)Enum.Parse(typeof(BarCodeImageFormat), imageType.ToString());
                var encodeType = EncodeTypes.Code128;
                Barcode barcode = new Barcode();
                barcode.BarcodeType = encodeType;
                barcode.ImageType = imageFormat;
                try
                {
                    string imagePath = "";
                    imagePath = GenerateBarcode(barcode);// Сгенерировать штрих-код
                    
                    // Показать изображение
                    /* Uri fileUri = new Uri(Path.GetFullPath(imagePath));
                    imgdynamic.Source = new BitmapImage(fileUri);*/
                   /* byte[] image_bytes = File.ReadAllBytes(imagePath);
                    string filename = imagePath;
                    string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1); // cats.jpg
                    byte[] imageData;
                    using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//выгрузка баркода
        {

        }
    }
}
