using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
namespace MS_Player_App
{
    class Song
    {
        public string SongTitle { get; set; }
        public string SongThumbNailPath { get; set; }
        public BitmapImage SongThumbnail
        {
            get
            {
                if (String.IsNullOrEmpty(SongThumbNailPath))
                    return new BitmapImage();
                else
                    //Use the Uri class instance to return the Uniform Resource //Identifier (URI) from the SongThumbNailPath. 
                    return (new BitmapImage(new Uri(SongThumbNailPath)));
            }
        }
        public Windows.Storage.StorageFile SongFile { get; set; }
        public Song(string title, string thumbnailImagePath, Windows.Storage.StorageFile file)
        {
            SongTitle = title;
            SongThumbNailPath = thumbnailImagePath;
            SongFile = file;     
        } 
    }
}
