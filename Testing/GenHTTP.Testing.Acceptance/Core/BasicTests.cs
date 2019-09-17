﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Xunit;

using GenHTTP.Testing.Acceptance.Domain;

namespace GenHTTP.Testing.Acceptance.Core
{

    public class BasicTests
    {

        [Fact]
        public void TestBuilder()
        {
            using var runner = new TestRunner();

            runner.Builder.RequestMemoryLimit(128)
                          .TransferBufferSize(128)
                          .RequestReadTimeout(TimeSpan.FromSeconds(2))
                          .Backlog(1);
            
            using var _ = runner.Builder.Build();

            using var response = runner.GetResponse();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void TestLegacyHttp()
        {
            using var runner = TestRunner.Run();

            var request = runner.GetRequest();
            request.ProtocolVersion = new Version(1, 0);

            using var response = request.GetSafeResponse();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void TestEmptyQuery()
        {
            using var response = TestRunner.Run().GetResponse("/?");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }

}