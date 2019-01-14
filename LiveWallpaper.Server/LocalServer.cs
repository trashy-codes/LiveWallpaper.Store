using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Server
{
    public class LocalServer : IWallpaperServer
    {
        string _host;
        bool _fuckMS = false;

        public async Task<List<TagServerObj>> GetTags()
        {
            //var result = await HttpGet<List<TagServerObj>>($"{_host}/tags");
            try
            {
                var temp = await HttpGet<dynamic>($"http://wallpaper.upupoo.com/async/getTags.htm?callback=");
                string json = JsonConvert.SerializeObject(temp.data);
                List<TagServerObj> result = Convert(json, (d) =>
                {
                    return new TagServerObj()
                    {
                        ID = d.tagId,
                        Name = d.tagName
                    };
                });
                if (!_fuckMS)
                {
                    // "鬼畜", "美の風景, "科幻世界","游戏风象"
                    string[] names = new string[] { "美の風景" };
                    result = result.Where(m => names.Contains(m.Name)).ToList();
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        private List<T> Convert<T>(string json, Func<dynamic, T> transform)
        {
            var tempList = JsonConvert.DeserializeObject<dynamic>(json, jsonSerializerSettings);
            var result = new List<T>();
            foreach (var item in tempList)
            {
                result.Add(transform(item));
            }
            return result;
        }

        public Task<List<SortServerObj>> GetSorts()
        {
            //var result = await HttpGet<List<SortServerObj>>($"{_host}/sorts");
            List<SortServerObj> result = new List<SortServerObj>
            {
                new SortServerObj() { ID = 0, Name = "最热" },
            };

            if (_fuckMS)
            {
                result.Add(new SortServerObj() { ID = 1, Name = "最新" });
                result.Add(new SortServerObj() { ID = 2, Name = "近期热门" });
            }


            return Task.FromResult(result);
        }

        public async Task<List<WallpaperServerObj>> GetWallpapers(int tag, int sort, int page)
        {
            //var result = await HttpGet<List<WallpaperServerObj>>($"{_host}/wallpapers?tag={tag}&sort={sort}&page={page}");
            try
            {
                var temp = await HttpGet<dynamic>($"http://wallpaper.upupoo.com/async/asyncSearch--1-{tag}-{sort}-{page}.htm?callback=");
                string json = JsonConvert.SerializeObject(temp.data.rows);
                List<WallpaperServerObj> result = Convert(json, (d) =>
                {
                    return new WallpaperServerObj()
                    {
                        URL = d.paperUrl,
                        Img = d.paperImg,
                        Name = d.paperName,
                        DownStr = d.downStr,
                    };
                });
                return result;
            }
            catch
            {
                return null;
            }
        }

        public void UnLock(string pwd)
        {
            _fuckMS = "whosyourdady" == pwd;
        }

        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        private async Task<T> HttpGet<T>(string url)
        {
            try
            {
                var handle = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                };
                using (var httpClient = new HttpClient(handle))
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                        return default(T);

                    var json = await response.Content.ReadAsStringAsync();

                    T result = default(T);
                    try
                    {
                        //多了两个括号需要移除掉
                        json = json.Substring(1, json.Length - 2);
                        result = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
                    }
                    catch (JsonReaderException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return default(T);
            }
        }
    }
}
