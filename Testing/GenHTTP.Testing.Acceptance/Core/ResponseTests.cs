﻿using System;
using System.IO;
using System.Net;
using System.Text;

using Xunit;

using GenHTTP.Api.Modules;
using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Core;
using GenHTTP.Testing.Acceptance.Domain;

namespace GenHTTP.Testing.Acceptance.Core
{

    public class ResponseTests
    {

        private class ResponseProvider : IContentProvider
        {

            public DateTime Modified { get; }

            public ResponseProvider()
            {
                Modified = DateTime.Now.AddDays(-10);
            }

            public IResponseBuilder Handle(IRequest request)
            {
                var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));

                return request.Respond()
                              .Content(content, 11, "text/x-custom")
                              .Expires(DateTime.Now.AddYears(1))
                              .Modified(Modified)
                              .Header("X-Powered-By", "Test Runner");
            }

        }

        /// <summary>
        /// As a developer, I'd like to use all of the response builders methods.
        /// </summary>
        [Fact]
        public void TestProperties()
        {
            var provider = new ResponseProvider();

            var router = Layout.Create().Add("_", provider, true);

            using var runner = TestRunner.Run(router);

            using var response = runner.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal("Hello World", response.GetContent());
            Assert.Equal("text/x-custom", response.ContentType);

            Assert.Equal(provider.Modified.WithoutMS(), response.LastModified.WithoutMS());
            Assert.NotNull(response.Headers["Expires"]);

            Assert.Equal("Test Runner", response.Headers["X-Powered-By"]);
        }

    }

}