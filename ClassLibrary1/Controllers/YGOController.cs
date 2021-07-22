using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    public class YGOController
    {
        private readonly static string folder = Path.GetTempPath();


        // Add static & final dictionary of images (card_id -> image) which is supposed to be used as a cache
        private readonly static Dictionary<string, Image> previewCache = new Dictionary<string, Image>();
        private readonly static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();


        // Add preload method which loads the images into the dictionary
        public async static Task PreLoadCache()
        {
            await LoadCache(imageCache, Properties.FULL_IMAGES_PATH);
            await LoadCache(previewCache, Properties.PREVIEW_IMAGES_PATH);
        }

        public static void Clear()
        {
            previewCache.Clear();
            imageCache.Clear();
        }

        private async static Task LoadCache(Dictionary<string, Image> cache, string mainPath)
        {
            cache.Clear();
            string path = folder + mainPath;

            // TODO: Implement custom exceptions with tracebacks & explanations
            if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0) throw new Exception();

            // TODO: Check if Threads are the better approach here since we aren't awaiting a result
            LoadCacheDebug(path, cache);
        }

        private static void LoadCacheDebug(string path, Dictionary<string, Image> cache)
        {
            Directory.GetFiles(path).ToList().ForEach(img =>
            {
                cache.Add(Path.GetFileNameWithoutExtension(img), (Image)Image.FromFile(img).Clone());
            });
        }


        // Add download method for downloading an image onto disc
        private async static Task DownloadImage(string id, string url, string mainPath)
        {
            using (HttpClient client = HttpClientBuilder.GetHttpClient())
            {
                Image.FromStream(await client.GetStreamAsync(url + id + ".jpg")).Save($"{folder}{mainPath}\\{id}.jpg");
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
        public async static Task DownloadAllImages(string[] ids)
        {
            await DownloadImages(ids, Properties.YGO_FULL_IMAGES_URL, Properties.FULL_IMAGES_PATH);
            await DownloadImages(ids, Properties.YGO_PREVIEW_IMAGES_URL, Properties.PREVIEW_IMAGES_PATH);
        }


        // Add getters for cards in dictionary
        public static Image GetPreview(string id)
        {
            return previewCache.GetValueOrDefault(id);
        }

        public static Image GetImage(string id)
        {
            return imageCache.GetValueOrDefault(id);
        }
    }
}
