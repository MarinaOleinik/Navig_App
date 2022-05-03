using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navig_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class File_Page : ContentPage
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public File_Page()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateFileList();
        }

        private void UpdateFileList()
        {
            // получаем все файлы
            filesList.ItemsSource = Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f));
            // снимаем выделение
            filesList.SelectedItem = null;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            // получаем имя файла
            string filename = (string)((MenuItem)sender).BindingContext;
            // удаляем файл из списка
            File.Delete(Path.Combine(folderPath, filename));
            // обновляем список файлов
            UpdateFileList();
        }

        private void filesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            // получаем выделенный элемент
            string filename = (string)e.SelectedItem;
            // загружем текст в текстовое поле
            textEditor.Text = File.ReadAllText(Path.Combine(folderPath, (string)e.SelectedItem));
            // устанавливаем название файла
            fileNameEntry.Text = filename;
            // снимаем выделение
            filesList.SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string filename = fileNameEntry.Text;
            if (String.IsNullOrEmpty(filename)) return;
            // если файл существует
            if (File.Exists(Path.Combine(folderPath, filename)))
            {
                // запрашиваем разрешение на перезапись
                bool isRewrited = await DisplayAlert("Kinnitus", "Fail on juba olemas, Kas salvestada ümber?", "Jah", "Ei");
                if (isRewrited == false) return;
            }
            // перезаписываем файл
            File.WriteAllText(Path.Combine(folderPath, filename), textEditor.Text);
            // обновляем список файлов
            UpdateFileList();
        }
    }
}