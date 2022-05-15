using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DriveTrimBackend.Utilities;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PhotosLibrary.v1;
using Google.Apis.PhotosLibrary.v1.Data;
using Google.Apis.Services;


namespace DriveTrimBackend
{
    public class GoogleAPI
    {
        public enum Request
        {
            CreateAlbum,
            AddToAlbum,
        }

        private string api = "https://photoslibrary.googleapis.com/v1";

        private GoogleClientSecrets client;

        public DBUtils DB;

        public GoogleAPI()
        {
            DB = new DBUtils();
            DB.initDB();
            
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
        
        public Album create_album(string token, string name)
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
            return req;
        }

        public IList<MediaItem> get_range(string token, TrimDate start, TrimDate end)
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

        public MediaItem get_media(string token, string id)
        {
            UserCredential credential; //= new UserCredential(float, userId, token, "DriveTrim"); // import token from request

            PhotosLibraryService service = new PhotosLibraryService(new BaseClientService.Initializer
            {
                //HttpClientInitializer = credential,
                ApplicationName = "DriveTrim",
            });


            return service.MediaItems.Get(id).Execute();
        }

        public void submit_albums(string token, string job)
        {
            List<string> albums = DB.getAlbums(job);
            foreach (string al in albums)
            {
                List<String> ids = DB.getIds(job, al);
                string drive_name = al.Substring(al.Length - 5, al.Length - 1);
                Album new_alb = create_album(token, drive_name);
                add_to_albumn(token, new_alb.Id, ids.ToArray());
            }
        }
        
        public void add_to_albumn(string token, string albumnid, string[] mid)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(new AlbumRequest
            {
                mediaItemIds = mid,
            });
            
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var result = httpClient.PostAsync(this.api + "/albums/" + albumnid + ":batchAddMediaItems", data);
            
            httpClient.Dispose();
        }
    }
}