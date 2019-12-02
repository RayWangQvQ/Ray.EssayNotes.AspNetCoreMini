﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMini.Features;

namespace AspNetCoreMini
{
    /// <summary>
    /// Http上下文
    /// </summary>
    public class HttpContext
    {
        public HttpRequest Request { get; }
        public HttpResponse Response { get; }
        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }

    /// <summary>
    /// Http请求
    /// </summary>
    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;
        public Uri Url => _feature.Url;
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();
    }

    /// <summary>
    /// Http响应
    /// </summary>
    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;
        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }
    }

    public static partial class Extensions
    {
        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
