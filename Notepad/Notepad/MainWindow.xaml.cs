using System;
using System.IO;
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
using Microsoft.Win32;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Persoon> personen = new List<Persoon>();
        
        public MainWindow()
        {
            InitializeComponent();
            personen.Add(
                new Persoon() { Voornaam = "Willy", Achternaam = "Janssens", GeboorteDatum = new DateTime(1990, 1, 2) }
            );
            grid.ItemsSource = personen;
        }

        private void exitItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void OpenDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fileContents.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void SaveDialog_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, fileContents.Text);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            fileContents.Clear();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("File.Open: Allows you to open a file." + Environment.NewLine 
                + "File.Save: Allows you to save the file." + Environment.NewLine 
                + "File.Clear: Clears the window." + Environment.NewLine
                + "File.Exit: Closes the window." + Environment.NewLine
                + "Help.About: Opens this useless window." + Environment.NewLine
                + "Greetz, Matske", "About NotePad");
        }

        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            List<Persoon> parsedPersonen = new List<Persoon>();
            parsedPersonen.Clear();
            string[] filedata = fileContents.Text.Split('\n');
            try
            {
                foreach (var row in filedata)
                {
                    string[] fields = row.Split(';');
                    var p = new Persoon();
                    p.Voornaam = fields[0];
                    p.Achternaam = fields[1];
                    p.GeboorteDatum = DateTime.Parse(fields[2]);
                    parsedPersonen.Add(p);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            grid.ItemsSource = parsedPersonen;
        }

        private void ShowPersonenList__Click(object sender, RoutedEventArgs e)
        {
            string s = "";

            foreach (var p in parsedPersonen)
            {
                s += p.ToString();
                s += Environment.NewLine;
            }

            MessageBox.Show(s);
        }
    }
}
