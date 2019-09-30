using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ShortUrlLib
{
    /// <summary>
    /// https://pse.is/
    /// </summary>
    public class PicSeeApi : ShortUrlApi
    {
        private readonly string _url;

        /// <summary>
        /// use test access token to generates short url (the URL only alive one hour). 
        /// </summary>
        public PicSeeApi() : this("20f07f91f3303b2f66ab6f61698d977d69b83d64")
        {
        }

        /// <summary>
        /// use access token to init
        /// </summary>
        /// <param name="accessToken"></param>
        public PicSeeApi(string accessToken)
        {
            _url = "https://api.pics.ee/v1/links/?access_token=" + accessToken;
        }

        /// <summary>
        /// Get short url
        /// </summary>
        /// <param name="url">original url</param>
        /// <returns>short url</returns>
        public override string Generate(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.ContentType = "application/json";
            request.Method = "POST";

            var postParams = "{\"url\":\"" + url + "\"}";

            using (var stream = new StreamWriter(request.GetRequestStream()))
            {
                stream.Write(postParams);
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var result = GetResult<ApiResult>(response.GetResponseStream());
                    return result.data.picseeUrl;
                }
            }
            catch (WebException webEx)
            {
                using (var errorResponse = (HttpWebResponse)webEx.Response)
                {
                    var result = GetResult<ErrorResult>(errorResponse.GetResponseStream());
                    throw new Exception($"{result.error.status} {result.error.message}");
                }
            }
        }

        private static T GetResult<T>(Stream stream) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            return serializer.ReadObject(stream) as T;
        }

        [DataContract]
        private class ApiResult
        {
            [DataMember]
            internal DataResult data { get; set; }
        }

        [DataContract]
        private class DataResult
        {
            [DataMember]
            internal string picseeUrl { get; set; }
        }

        [DataContract]
        private class ErrorResult
        {
            [DataMember]
            internal ErrorResultDetail error { get; set; }
        }

        [DataContract]
        private class ErrorResultDetail
        {
            [DataMember]
            internal string message { get; set; }
            [DataMember]
            internal string status { get; set; }
        }
    }
}