using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace EjednevnicDZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool buttonsEnabled = false;
        private List<Zametka> zametka;
        public MainWindow()
        {
            InitializeComponent();
            zametka = new List<Zametka>();
            dataPicker.SelectedDate = DateTime.Today;
            
        }

        public static readonly DependencyProperty NotesOnSelectedDateProperty = DependencyProperty.Register("NotesOnSelectedDate", typeof(List<Zametka>), typeof(MainWindow));

        public List<Zametka> NotesOnSelectedDate
        {
            get { return (List<Zametka>)GetValue(NotesOnSelectedDateProperty); }
            set { SetValue(NotesOnSelectedDateProperty, value); }
        }



        private void DateSelection(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            DateTime selectedDate = (DateTime)dataPicker.SelectedDate;
            NotesOnSelectedDate = zametka.Where(x => x.Date.Date == selectedDate.Date).ToList();
            List.ItemsSource = null;
            List.ItemsSource = NotesOnSelectedDate;
        }

        public void NoteTitle(object sender, RoutedEventArgs e)
        {
            if (TitleIn != null)
            {
                buttonsEnabled = true;
            }
        }
        

        public void DescriptionOfTheNote(object sender, RoutedEventArgs e)
        {
           if (DescriptionIn != null)
           {
                buttonsEnabled = true;
           }

        }

        private void CreateANote()
        {
           
        }

        private void DeletingANote(object sender, RoutedEventArgs e)
        {
            if (buttonsEnabled == true && List.SelectedItem != null)
            {
                zametka.Remove((Zametka)List.SelectedItem);
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "notes.json");
                Serializer.Serialize(zametka, filePath); // перезаписываем файл без удаленной заметки
                Refresh();
            }
        }
       
        public void NewNote()
        {
            if (List.SelectedItems != null)
            {
                Zametka selectedZametka = (Zametka)List.SelectedItem;
                selectedZametka.Title = TitleIn.Text;
                selectedZametka.Description = DescriptionIn.Text;
                Refresh();
            }
            else
            {
                CreateANote();
            }
            
        }

        private void CreateANote(object sender, RoutedEventArgs e)
        {

            DateTime selectedDate;
            if (dataPicker.SelectedDate == null)
            {
                selectedDate = DateTime.Today; 
            }
            else
            {
                selectedDate = (DateTime)dataPicker.SelectedDate;
            }
            Zametka existingNote = (Zametka)List.SelectedItem;
            if (existingNote != null)
            {
                existingNote.Title = TitleIn.Text;
                existingNote.Description = DescriptionIn.Text;
                existingNote.Date = selectedDate;
            }
            else
            {
                var newNote = new Zametka() { Title = TitleIn.Text, Description = DescriptionIn.Text, Date = selectedDate };
                zametka.Add(newNote);
            }

            Refresh();
            TitleIn.Text = "";
            DescriptionIn.Text = "";
        }

        private void SaveANote(object sender, RoutedEventArgs e)
        {
            buttonsEnabled = true;
            if (List.SelectedItem != null)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "notes.json");
                Serializer.Serialize(zametka, filePath);
            }

        }

        public void ListOfNotes(object sender, SelectionChangedEventArgs e)
        {
            Zametka selectedNote = (Zametka)List.SelectedItem;
            if (selectedNote != null)
            {
                TitleIn.Text = selectedNote.Title;
                DescriptionIn.Text = selectedNote.Description;
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "notes.json");
            if (File.Exists(filePath))
            {
                zametka = Serializer.Deserialize<List<Zametka>>(filePath);
                Refresh();
            }
        }
    }   
}
