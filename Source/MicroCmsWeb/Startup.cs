﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicroCms.Startup))]
namespace MicroCms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
