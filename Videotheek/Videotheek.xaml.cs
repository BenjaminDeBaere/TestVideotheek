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
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using VideotheekCommunity;

namespace Videotheek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Video> VideoOb = new List<Video>();
        public List<Video> GewijzigdeVideo = new List<Video>();
        public List<Video> NietGewijzigd = new List<Video>();
        public List<Genre> Genres = new List<Genre>();
        private bool toevoegen = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulListBoxIn();
            VulGenrecomboBoxIn();
            VideoListBox.SelectedIndex = 0;
            

        }

        private void VulListBoxIn()
        {
            var manager = new VideotheekManager();
            VideoOb = manager.selectVideos();

            VideoListBox.SelectedIndex = 0;
            VideoListBox.ItemsSource = VideoOb;
            VideoListBox.DisplayMemberPath = "Titel";
            VideoListBox.SelectedValuePath = "GenreNr";
            
            
            
            




        }
        private void VulGenrecomboBoxIn()
        {
            var manager = new VideotheekManager();
            Genres = manager.GetGenres(); 
            GenreComboBox.ItemsSource = Genres;
            GenreComboBox.SelectedValuePath = "GenreNr";
            GenreComboBox.DisplayMemberPath = "GenreNaam";
            GenreComboBox.SelectedValue = VideoListBox.SelectedValue;
            
            
            

        }


        private void ButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (toevoegen == false)
            {

                ButtonVerwijderen.IsEnabled = false;
                toevoegen = true;
                VideoListBox.IsEnabled = false; 
                foreach (Object child in infoGrid.Children)
                {
                    if (child is TextBox)
                    {
                        TextBox textbox = (TextBox)child;
                        textbox.Clear();
                        
                    }
                }
                GenreComboBox.SelectedIndex = 0;
                ButtonToevoegen.Content = "Toevoegen annuleren";
                ButtonWijzigingenOpslaan.Content = "Nieuwe Film Opslaan";
            }
            else
            {
                toevoegen = false;
                ButtonVerwijderen.IsEnabled = true;
                VideoListBox.IsEnabled = true;
                VideoListBox.SelectedIndex = 0;               
                VulListBoxIn();
                ButtonWijzigingenOpslaan.Content = "Wijzigingen opslaan";
                ButtonToevoegen.Content = "Toevoegen";

            }

            
        }

   
        private void ButtonWijzigingenOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if(toevoegen == true)
            {
                
                var manager = new VideotheekManager();          
                try
                {
                    manager.VideoToevoegen(
                        Convert.ToString(TextBoxTitel.Text),
                        GenreComboBox.SelectedIndex + 1,
                        Convert.ToInt32(TextBoxInVoorraad.Text.ToString()),
                        Convert.ToInt32(TextBoxUitgeleend.Text.ToString()),
                        Convert.ToDecimal(TextBoxPrijs.Text.ToString()),
                        Convert.ToInt32(TextBoxTotaalVerhuurd.Text.ToString()));
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                VulListBoxIn();
                VideoListBox.SelectedItem = Convert.ToString(TextBoxTitel.Text);
                ButtonVerwijderen.IsEnabled = true;
                VideoListBox.IsEnabled = true;
                ButtonWijzigingenOpslaan.Content = "Wijzigingen opslaan";
                ButtonToevoegen.Content = "Toevoegen";
                toevoegen = false;
         
            }
            else
            {
                var manager = new VideotheekManager();
                foreach (Video v in VideoOb)
                {
                    if (v.Changed == true) GewijzigdeVideo.Add(v);
                    v.Changed = false;
                }
                NietGewijzigd = manager.VeranderdeVideosOpslaan(GewijzigdeVideo);
                if(NietGewijzigd.Count > 0)
                {
                    MessageBox.Show(NietGewijzigd.Count + " films niet verwijderd", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error );
                }

            }
        }
        

        private void ButtonVerwijderen_Click(object sender, RoutedEventArgs e)
        {           
            var manager = new VideotheekManager();
            if (MessageBox.Show("Weet u zeker dat u deze film wit verwijderen", "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                manager.VideoVerwijderen(Convert.ToInt32(TextBoxBandNr.Text.ToString()));
            }
            VideoListBox.SelectedIndex = 0;
            VulListBoxIn();


        }

        private void VideoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VulGenrecomboBoxIn();
        }
    }
}
