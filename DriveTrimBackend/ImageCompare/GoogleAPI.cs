using System;
using System.IO;
using Google.Apis.PhotosLibrary.v1;
using Google.Apis.PhotosLibrary.v1.Data;
using Google.Apis.Services;
using Newtonsoft.Json;


namespace ImageCompare
{
    public class GoogleAPI
    {
        private const String api = "https://photoslibrary.googleapis.com/v1";
        
        public enum Request
        {
            CreateAlbum,
            AddToAlbum,
        }

        private APIClient client;

        public GoogleAPI()
        {
            string file_name = "clientsecret.json";
            using (StreamReader r = new StreamReader(file_name))
            {
                try
                {
                    string json = r.ReadToEnd();
                    client = JsonConvert.DeserializeObject<APIClient>(json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        

        private void create_album(String name)
        {
            PhotosLibraryService service = new PhotosLibraryService(new BaseClientService.Initializer
            {
                ApplicationName = client.web.project_id,
            });

            CreateAlbumRequest albumRequest = new CreateAlbumRequest()
            {
                Album =
                {
                    Title = name
                }
            };

            AlbumsResource.CreateRequest req = service.Albums.Create(albumRequest);
        }
    }
}