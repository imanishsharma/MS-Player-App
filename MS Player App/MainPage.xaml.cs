using System;
using System.Diagnostics;
using System.Threading.Tasks; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MS_Player_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool isPlaying = false;
        List<Song> playList = new List<Song>();
        Song currentSong = null;
        int currentSongIndex;

        void loadPlayList()
        {
            PlaylistView.Items.Clear();
            foreach (Song s in playList)
                PlaylistView.Items.Add(" " + s.SongTitle);
        }
        async void  loadSelectedSong(string songToSelect)
        {
            try
            {
                foreach (Song s in playList)
                    if (s.SongTitle == songToSelect.Trim())
                        currentSong = s;
                MediaSource.PosterSource = currentSong.SongThumbnail;
                var streamedSong = await currentSong.SongFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                MediaSource.SetSource(streamedSong, currentSong.SongFile.ContentType);
            }
            catch
            {
            }
            }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            loadPlayList();
            SliderVolume.Value = SliderVolume.Maximum;
            SongPosition.Value = 0.0;
            SongPosition.ValueChanged += SongPosition_ValueChanged_1;


        }
       
            

              

        void playSong()
        {
            SongPosition.Minimum = 0.0;
            SongPosition.Maximum = MediaSource.NaturalDuration.TimeSpan.TotalMilliseconds;
            PauseButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            PlayButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MediaSource.Play();
            isPlaying = true;
            displayProgress();
        }
        async void displayProgress()
        {
            //The progress information is updated every second. 
            await Task.Delay(1000);
            SongPosition.Value = MediaSource.Position.TotalMilliseconds;
            if (isPlaying)
                if (MediaSource.Position.TotalMilliseconds < MediaSource.NaturalDuration.TimeSpan.TotalMilliseconds)
                    displayProgress();
                else
                {
                    if (!MediaSource.IsLooping)
                    {
                        isPlaying = false;
                        PauseButton.Visibility =
                                  Windows.UI.Xaml.Visibility.Collapsed;
                        PlayButton.Visibility =
                                  Windows.UI.Xaml.Visibility.Visible;
                    }
                    else
                        displayProgress();
                }
        }
        int flag = 0;
        private Size _previousVideoContainerSize = new Size();
        private void FullscreenToggle()
        {
            if (flag == 0)
            {
                _previousVideoContainerSize.Width = MediaSource.ActualWidth;
                _previousVideoContainerSize.Height = MediaSource.ActualHeight;
                MediaSource.Width = Window.Current.Bounds.Width;
                MediaSource.Height = Window.Current.Bounds.Height;
                fullScreenOffBtn.Visibility = Visibility.Visible;
                fullScreenOnBtn.Visibility = Visibility.Collapsed;
                flag = 1;
            }
            else
            {
                MediaSource.Height = _previousVideoContainerSize.Height;
                MediaSource.Width = _previousVideoContainerSize.Width;
                fullScreenOffBtn.Visibility = Visibility.Collapsed;
                fullScreenOnBtn.Visibility = Visibility.Visible;
            }
        }



        private void PlayButton_Click_1(object sender, RoutedEventArgs e)
        {
            playSong();
        }
        void pauseSong()
        {
            PauseButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            PlayButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MediaSource.Pause();
            isPlaying = false;
        }
        private void PauseButton_Click_1(object sender, RoutedEventArgs e)
        {
            pauseSong();
        }

        TextBlock  txtPlayStatus = new TextBlock();

        public MainPage()
        {

            this.InitializeComponent();
            
            txtPlayStatus.Name = "txtPlayStatus";
            txtPlayStatus.Margin = new Thickness(1056, 10, 0, 93);
            txtPlayStatus.FontSize = 18;
            txtPlayStatus.TextAlignment = TextAlignment.Center;
            mediacontrols.Children.Add(txtPlayStatus);
            this.InitializeComponent();

        }

        private void PlaylistView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            currentSongIndex = PlaylistView.SelectedIndex;
            loadSelectedSong(PlaylistView.SelectedItem as string);
            MediaSource.Stop();
            PlayButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            PauseButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }

        private void NextButton_Click_1(object sender, RoutedEventArgs e)
        {
            {
                if (currentSongIndex < playList.Count - 1)
                    currentSongIndex++;
                if (isPlaying)
                    MediaSource.Stop();
                PlaylistView.SelectedIndex = currentSongIndex;
            }
        }

        private void PreviousButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentSongIndex > 0)
                currentSongIndex--;
            if (isPlaying)
                MediaSource.Stop();
            PlaylistView.SelectedIndex = currentSongIndex;

        }

        private void SongPosition_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 100)
                MediaSource.Position = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(SongPosition.Value));
            txtPlayStatus.Text = MediaSource.Position.Hours + " : " + MediaSource.Position.Minutes + " : " + MediaSource.Position.Seconds + " / " + MediaSource.NaturalDuration.TimeSpan.Hours + " : " + MediaSource.NaturalDuration.TimeSpan.Minutes + " : " + MediaSource.NaturalDuration.TimeSpan.Seconds;

        }

        private void SliderVolume_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {
            MediaSource.Volume = SliderVolume.Value / 100;

        }

        private void ToggleSwitchRepeat_Toggled_1(object sender, RoutedEventArgs e)
        {
            MediaSource.IsLooping = ToggleSwitchRepeat.IsOn;

        }

        private async void PlaylistView_RightTapped_1(object sender, RightTappedRoutedEventArgs e)
        {

            PopupMenu pMenu = new PopupMenu();
            UICommand cmd = new UICommand();
            ListBoxItem rightTappedSong = null;
            UIElement controls = e.OriginalSource as UIElement;
            while (controls != null && controls != sender as UIElement)
            {
                if (controls is ListBoxItem)
                {
                    rightTappedSong = controls as ListBoxItem;
                    if (currentSong != null && currentSong.SongTitle == (rightTappedSong.Content as string).Trim() && isPlaying)
                    {
                        cmd.Label = "Pause";
                        cmd.Id = "pause";
                    }
                    else
                    {
                        cmd.Label = "Play";
                        cmd.Label = "Play";
                    }

                    pMenu.Commands.Add(cmd);
                    UICommand cmdDeleteFile = new UICommand();
                    cmdDeleteFile.Id = "Delete";
                    cmdDeleteFile.Label = "Delete";
                    pMenu.Commands.Add(cmdDeleteFile);
                    break;
                }
                controls = VisualTreeHelper.GetParent(controls) as UIElement;
            }
               UICommand cmdAddFile = new UICommand();
            cmdAddFile.Id = "Add";
            cmdAddFile.Label ="Add";
            pMenu.Commands.Add(cmdAddFile);
            var SelectedCommand = await pMenu.ShowForSelectionAsync(new Rect (e.GetPosition (null), e.GetPosition (null)));
            if(SelectedCommand!=null)
            {
                if((SelectedCommand.Id.ToString()=="Add"))
                {
                    Windows.Storage.Pickers.FileOpenPicker fop = new Windows.Storage.Pickers.FileOpenPicker();
                    fop.FileTypeFilter.Add(".mp3");
                    fop.FileTypeFilter.Add(".mp4");
                    fop.FileTypeFilter.Add(".wav");
                    
                    fop.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
                    var selectedFile = await  fop.PickSingleFileAsync();
                    if(selectedFile != null)
                    {
                        playList.Add(new Song(selectedFile.DisplayName, "ms-appx:///MediaFiles/Thumbnails/thumbnails.jpg", selectedFile));
                        loadPlayList();
                    }
                }
                else
                {
                    for(int i = 0;i<playList.Count; i++)
                    {
                        if(playList[i].SongTitle.Trim() == rightTappedSong.Content.ToString().Trim())
                        {
                            if(SelectedCommand.Id.ToString() == "play")
                            {
                                PlaylistView.SelectedIndex = i;
                                await Task.Delay(1500);
                                playSong();
                                break;
                            }
                            else if(SelectedCommand.Id.ToString()=="pause")
                            {
                                pauseSong();
                                break;
                            }
                            else if(SelectedCommand.Id.ToString() =="Delete")
                            {
                               if(currentSong != null && currentSong.SongTitle == playList[i].SongTitle)
                               {
                                   currentSong =null;
                                   MediaSource.Source = null;
                                   SongPosition.Value= 0.0;
                                   pauseSong();
                               }
                                playList.RemoveAt(i);
                                loadPlayList();
                                break;

                            }
                        }
                    }
                }
            }
        }
        private void fullScreenBtn_Click_1(object sender, RoutedEventArgs e)
        {
            FullscreenToggle();

        }
      

    }
}
      
        

        

        