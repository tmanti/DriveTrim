using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PhotosLibrary.v1;
using Google.Apis.PhotosLibrary.v1.Data;
using Google.Apis.Services;


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
        
        public void create_album(String Token, String name)
        {
            UserCredential credential; //= new UserCredential(float, userId, token, "DriveTrim"); // import token from request

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

        public IList<MediaItem> get_range(String Token, TrimDate start, TrimDate end)
        {
            UserCredential credential; //= new UserCredential(float, userId, token, "DriveTrim"); // import token from request

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

        public MediaItem get_media(String Token, String id)
        {
            UserCredential credential; //= new UserCredential(float, userId, token, "DriveTrim"); // import token from request

            PhotosLibraryService service = new PhotosLibraryService(new BaseClientService.Initializer
            {
                //HttpClientInitializer = credential,
                ApplicationName = "DriveTrim",
            });


            return service.MediaItems.Get(id).Execute();
        }
    }
}