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
        List<Persoon> parsedPersonen = new List<Persoon>();
        int x = 0;

        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Please add / upload your content! (ex: FirstName;LastName;DateOfBirth).");
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
                + "Tools.Parse: Parses the content of the TextBox to the DataGrid" + Environment.NewLine
                + "Tools.Show-List: Shows a messagebox with the content of the DataGrid" + Environment.NewLine
                + "Greetz, Matske", "About NotePad");
        }

        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            if (fileContents.Text == "") { MessageBox.Show("Please add some content first!"); }
            else
            {
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
                        p.Geboortedatum = DateTime.Parse(fields[2]);
                        parsedPersonen.Add(p);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
                grid.ItemsSource = parsedPersonen;
                grid.Items.Refresh();
                x += 1;
            }
        }

        private void ShowPersonenList__Click(object sender, RoutedEventArgs e)
        {
            if (x >= 1)
            {
                string s = "";

                foreach (var p in parsedPersonen)
                {
                    s += p.ToString();
                    s += Environment.NewLine;
                }

                MessageBox.Show(s);
            }
            else { MessageBox.Show("The content isn't yet added to the DataGrid. Please click 'Tools.Parse' first.", "ERROR"); }
        }
    }
}
