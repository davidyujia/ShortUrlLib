using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ShortUrlLib
{
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
}
