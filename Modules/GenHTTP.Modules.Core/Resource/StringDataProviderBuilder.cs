﻿using System;
using System.Collections.Generic;
using System.Text;

using GenHTTP.Api.Infrastructure;
using GenHTTP.Api.Modules;

namespace GenHTTP.Modules.Core.Resource
{

    public class StringDataProviderBuilder : IBuilder<IResourceProvider>
    {
        private string? _Content;

        #region Functionality

        public StringDataProviderBuilder Content(string content)
        {
            _Content = content;
            return this;
        }

        public IResourceProvider Build()
        {
            if (_Content == null)
            {
                throw new BuilderMissingPropertyException("Content");
            }

            return new StringDataProvider(_Content);
        }

        #endregion

    }

}