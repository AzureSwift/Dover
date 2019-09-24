using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip; // инициализация системных объектов и объекта с библиотекой для архивации

namespace DoverSharp //объявляем неймспейс что бы не было конфликтов с дефолтными классами .NET (а их и не должно быть, наверное)
{
    public partial class ZipGUI : Form //стандартный класс для дизайнера форм
    {
        public ZipGUI()
        {
            InitializeComponent();
        }
        
        FolderBrowserDialog ZFDialog = new FolderBrowserDialog(); //создание окна выбора папки для архивации
        

        private void button1_Click(object sender, EventArgs e) //кнопка "Choose Folder" которая помещает выбранный путь в текстовое окно
        {
            if (ZFDialog.ShowDialog() == DialogResult.OK) //если диалог успешно завершен
            {
                textBox1.Text = ZFDialog.SelectedPath; //текстовое поле принимает путь к файлу
            }
        }

        private void button2_Click(object sender, EventArgs e) //кнопка "Zip!" 
        {
            SaveFileDialog SFDialog = new SaveFileDialog(); // диалоговое окно сохранения архива 
            SFDialog.Filter = "Zip files (*.zip)|*.zip"; //тип файла для сохранения, если не указан пользователем вручную (и хорошо что не указан)
                if (textBox1.Text!="" && SFDialog.ShowDialog() == DialogResult.OK) //если текстовое поле не пустое и диалог успешно завершен
                 {
                    ZipFile file = new ZipFile(SFDialog.FileName); //создается новый архив с названием взятым из окна SFDialog 
                    file.AddDirectory(ZFDialog.SelectedPath); //в него добавляется содержимое выбранной папки ZFDialog
                    file.Save(); //архив сохраняется
                    MessageBox.Show("Successful!"); //выводится окно о завершении
                 }

        }

        OpenFileDialog ZODialog = new OpenFileDialog(); //диалоговое окно открытия файла
        private void button4_Click(object sender, EventArgs e) //кнопка "Choose File" 
        {
            ZODialog.Title = "Choose ZIP to unpack"; //заголовок окна выбора файла
            ZODialog.Filter = "Zip files (*.zip)|*.zip"; // -> строка 35
            
            if (ZODialog.ShowDialog() == DialogResult.OK) //если окно успешно завершилось
            {
                textBox2.Text = ZODialog.FileName; //текстовое поле принимает путь к файлу
            }
        }
        private void button3_Click(object sender, EventArgs e) // кнопка "Unzip!"
        { 
            FolderBrowserDialog UZDialog = new FolderBrowserDialog(); //окно выбора папки для разархивации
            

            if (textBox2.Text != "" && UZDialog.ShowDialog() == DialogResult.OK) //если текстовое поле не пустое и диалог успешно завершен
            {
                using (Ionic.Zip.ZipFile file = Ionic.Zip.ZipFile.Read(ZODialog.FileName)) //using автоматически избавляется от метода после его завершения
                {                                                                          //метод прочитает путь к файлу из окна ZODialog
                    file.ExtractAll(UZDialog.SelectedPath, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite); //и разархивирует его по пути в окне UZDialog
                    MessageBox.Show("Successful!"); // при этом не перезаписывая уже существующие файлы. Так же выведет окошко о успешном завершении.
                    
                }
            }
        }
        
     

       
    }
}
