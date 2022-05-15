using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PhotosLibrary.v1;
using Google.Apis.PhotosLibrary.v1.Data;
using Google.Apis.Services;
using Newtonsoft.Json;


namespace DriveTrimBackend
{
    public class GoogleAPI
    {
        private const String api = "https://photoslibrary.googleapis.com/v1";
        
        public enum Request
        {
            CreateAlbum,
            AddToAlbum,
        }

        private GoogleClientSecrets client;

        public GoogleAPI()
        {
            string file_name = "clientsecret.json";
            using (StreamReader r = new StreamReader(file_name))
            {
                try
                {
                    client = GoogleClientSecrets.FromFile("client_secrets.json");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        
        public void create_album(String name)
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                //credential = ;
            }
            
            PhotosLibraryService service = new PhotosLibraryService(new BaseClientService.Initializer
            {
                //HttpClientInitializer = credential,
                ApplicationName = "DriveTrim",
            });

            CreateAlbumRequest albumRequest = new CreateAlbumRequest()
            {
                Album = new Album{
                    Title = name
                }
            };

            Album req = service.Albums.Create(albumRequest).Execute();
        }

        public IList<MediaItem> get_range(TrimDate start, TrimDate end)
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                //credential = ;
            }
            
            PhotosLibraryService service = new PhotosLibraryService(new BaseClientService.Initializer
            {
                //HttpClientInitializer = credential,
                ApplicationName = "DriveTrim",
            });

            List<DateRange> dateRanges = new List<DateRange>();
            dateRanges.Append(new DateRange()
            {
                StartDate = start.getDate(),
                EndDate = end.getDate(),
            });

            SearchMediaItemsRequest searchRequest = new SearchMediaItemsRequest()
            {
                Filters = new Filters
                {
                    DateFilter = new DateFilter
                    {
                        Ranges = dateRanges
                    }
                }
            };
            
            SearchMediaItemsResponse req = service.MediaItems.Search(searchRequest).Execute();
            return req.MediaItems;
        }
    }
}