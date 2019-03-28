﻿using System;
using System.Collections.Generic;
using System.Text;

using GenHTTP.Api.Protocol;

namespace GenHTTP.Api.Modules.Templating
{
    
    public interface IPageProvider<T> where T : PageModel
    {

        T GetModel(IHttpRequest request, IHttpResponse response);

    }

}