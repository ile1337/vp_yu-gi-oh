using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    public class YGOController
    {

        // TODO: Add synchronization between download & cache preload
        //      Do it using a ThreadPool for download ( wait for entire thread pool to finish before letting the preload start running)
        //      Another (possibly better) option is to add the image directly to the cache right after it gets downloaded, with this option we'd be avoiding both collision & unnecessary calls

        private readonly static string folder = Path.GetTempPath();

        private readonly static string[] ids = CardController.GetAllCardIds().GetAwaiter().GetResult().ToArray();

        private readonly static Random random = new Random();


        // Add static & final dictionary of images (card_id -> image) which is supposed to be used as a cache
        private readonly static Dictionary<string, byte[]> previewCache = new Dictionary<string, byte[]>(ids.Length);
        private readonly static Dictionary<string, byte[]> imageCache = new Dictionary<string, byte[]>(ids.Length);



        // Add preload method which loads the images into the dictionary
        public async static Task PreLoadCache()
        {
            Clear();
            await LoadCache(imageCache, Properties.FULL_IMAGES_PATH);
            await LoadCache(previewCache, Properties.PREVIEW_IMAGES_PATH);
        }

        public static string GetRandomId()
        {
            return ids[random.Next(0, ids.Length)];
        }

        private async static Task LoadCache(Dictionary<string, byte[]> cache, string mainPath)
        {
            string path = folder + mainPath;

            // TODO: Implement custom exceptions with tracebacks & explanations
            if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0) throw new Exception();

            // TODO: Check if Threads are the better approach here since we aren't awaiting a result
            _ = Task.Run(() =>
            {
                Directory.GetFiles(path).ToList().ForEach(img =>
                            {
                                using (var ms = new MemoryStream())
                                {
                                    var image = Image.FromFile(img);
                                    image.Save(ms, image.RawFormat);
                                    cache.Add(Path.GetFileNameWithoutExtension(img), ms.ToArray());
                                }
                            });

                GC.Collect();
            });
        }


        // Add download method for downloading an image onto disc
        private async static Task DownloadImage(string id, string url, string mainPath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(2);
                try
                {
                    Image.FromStream(await client.GetStreamAsync(url + id + ".jpg")).Save($"{folder}{mainPath}\\{id}.jpg");
                } catch(HttpRequestException ex)
                {
                    Thread.Sleep(100);
                    await DownloadImage(id, url, mainPath);
                }
            }
        }


        // Add orchestration method for downloading all the card images in parallel
        private async static Task DownloadImages(string[] ids, string url, string mainPath)
        {
            string path = folder + mainPath;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (Directory.GetFiles(path).Length != 0) return;

            ids.ToList().ForEach(id => DownloadImage(id, url, mainPath));
        }


        // Add orchestration method which calls the downloading orchestration for both preview & full/main images
        public async static void DownloadAllImages()
        {
            //await DownloadImages(ids, Properties.YGO_FULL_IMAGES_URL, Properties.FULL_IMAGES_PATH);
           // await DownloadImages(ids, Properties.YGO_PREVIEW_IMAGES_URL, Properties.PREVIEW_IMAGES_PATH);
        }


        // Add getters for cards in dictionary
        public static Image GetPreview(string id)
        {
            return GetImageFromBytes(id, previewCache);
        }

        public static Image GetImage(string id)
        {
            return GetImageFromBytes(id, imageCache);
        }

        public static Image GetImageFromBytes(string id, Dictionary<string, byte[]> cache) 
        {
            using(var ms = new MemoryStream(cache.GetValueOrDefault(id)))
            {
                return Image.FromStream(ms);
            }
        }

        public static void Clear()
        {
            previewCache.Clear();
            imageCache.Clear();
        }
    }
}
