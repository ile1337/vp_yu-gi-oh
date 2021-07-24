using System;
using System.Collections.Concurrent;
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
        // TODO: Implement fallback functionality for files that might not have gotten downloaded

        private readonly static string folder = Path.GetTempPath();

        private readonly static string[] ids = CardController.GetAllCardIds().GetAwaiter().GetResult().ToArray();


        // Add static & final IDictionary of images (card_id -> image) which is supposed to be used as a cache
        //private readonly static ConcurrentDictionary<string, byte[]> previewCache = new ConcurrentDictionary<string, byte[]>(100, ids.Length);
        private readonly static ConcurrentDictionary<string, byte[]> imageCache = new ConcurrentDictionary<string, byte[]>(100, ids.Length);



        // Add preload method which loads the images into the IDictionary
        public async static Task PreLoadCache()
        {
            await LoadCache(imageCache, Properties.FULL_IMAGES_PATH);
            //await LoadCache(previewCache, Properties.PREVIEW_IMAGES_PATH);
        }

        private async static Task LoadCache(IDictionary<string, byte[]> cache, string mainPath)
        {
            string path = folder + mainPath;

            // TODO: Implement custom exceptions with tracebacks & explanations
            if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0)
            {
                GC.Collect(2, GCCollectionMode.Optimized);
                return;
            }

            // TODO: Check if Threads are the better approach here since we aren't awaiting a result
            _ = Task.Run(() =>
            {
                Directory.GetFiles(path).ToList().ForEach(img => AddImageToCache(cache, img));
                GC.Collect();
            });
        }

        private static void AddImageToCache(IDictionary<string, byte[]> cache, string img)
        {
            try
            {
                string key = Path.GetFileNameWithoutExtension(img);
                if (cache.ContainsKey(key)) return;

                using (var ms = new MemoryStream())
                {
                    var image = Image.FromFile(img);
                    image.Save(ms, image.RawFormat);
                    cache.TryAdd(key, ms.ToArray());
                    image.Dispose();
                }
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }


        // Add download method for downloading an image onto disc
        private async static Task DownloadImage(string id, string url, string mainPath, IDictionary<string, byte[]> cache)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(3);
                try
                {
                    string img = $"{folder}{mainPath}\\{id}.jpg";
                    Image image = Image.FromStream(await client.GetStreamAsync(url + id + ".jpg"));
                    image.Save(img);
                    image.Dispose();
                    AddImageToCache(cache, img);
                } catch (HttpRequestException)
                {
                    Thread.Sleep(100);
                    await DownloadImage(id, url, mainPath, cache);
                }
            }
        }


        // Add orchestration method for downloading all the card images in parallel
        private async static Task DownloadImages(string[] ids, string url, string mainPath, IDictionary<string, byte[]> cache)
        {
            string path = folder + mainPath;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (Directory.GetFiles(path).Length != 0) return;

            Task.Run(() => ids.ToList().ForEach(async id => await DownloadImage(id, url, mainPath, cache))).ContinueWith(_ => GC.Collect());
        }


        // Add orchestration method which calls the downloading orchestration for both preview & full/main images
        public async static void DownloadAllImages()
        {
            await DownloadImages(ids, Properties.YGO_FULL_IMAGES_URL, Properties.FULL_IMAGES_PATH, imageCache);
            //await DownloadImages(ids, Properties.YGO_PREVIEW_IMAGES_URL, Properties.PREVIEW_IMAGES_PATH, previewCache);
        }


        //public static Image GetPreview(string id)
        //{
        //    return GetImageFromBytes(id, previewCache);
        //}


        // Add getters for cards in IDictionary
        public static Image GetImage(string id)
        {
            return GetImageFromBytes(id, imageCache);
        }

        public static Image GetImageFromBytes(string id, IDictionary<string, byte[]> cache) 
        {
            byte[] res;
            cache.TryGetValue(id, out res);
            using (var ms = new MemoryStream(res))
            {
                return Image.FromStream(ms);
            }
        }

    }
}
