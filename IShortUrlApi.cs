using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ShortUrlLib
{
    /// <summary>
    /// Short URL API Interface
    /// </summary>
    public interface IShortUrlApi
    {
        /// <summary>
        /// Get short url
        /// </summary>
        /// <param name="url">original url</param>
        /// <returns>short url</returns>
        string Generate(string url);

        /// <summary>
        /// Get short url
        /// </summary>
        /// <param name="url">original url</param>
        /// <returns>short url</returns>
        Task<string> GenerateAsync(string url);
    }

    /// <summary>
    /// Base short URL API class
    /// </summary>
    public abstract class ShortUrlApi : IShortUrlApi
    {
        /// <summary>
        /// Get short url
        /// </summary>
        /// <param name="url">original url</param>
        /// <returns>short url</returns>
        public abstract string Generate(string url);

        /// <summary>
        /// Get short url
        /// </summary>
        /// <param name="url">original url</param>
        /// <returns>short url</returns>
        public async Task<string> GenerateAsync(string url)
        {
            return await Task.Factory.StartNew(() => Generate(url));
        }
    }
}
