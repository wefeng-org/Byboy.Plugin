using System.Net.Http.Json;

namespace SendSnsTools
{
    internal static class HttpClientExt
    {
        internal static HttpClient httpClient = new HttpClient();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addsns"></param>
        /// <returns></returns>
        public static async Task<bool> SendAddSns(this AddSns addsns)
        {
            var resp = await httpClient.PostAsJsonAsync("http://114.215.169.94:7011/api/Sns/AddSns",addsns);
            var content = await resp.Content.ReadFromJsonAsync<Response>();
            return content!.code == 0;
        }
    }
}
