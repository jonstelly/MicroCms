#MicroCms

##Basics
MicroCms is intended to be a lightweight CMS engine that can be hosted inside of an existing ASP.NET MVC site.  It gives some basic classes around content types, parts, items, documents and templates, and provides several simple extensibility points for where you want to store your content, and an ability to create your own content types and renderers.

* [Project Page](https://github.com/jonstelly/MicroCms)
* [Project Wiki](https://github.com/jonstelly/MicroCms/wiki)

##Status
The code is just now coming together.  There are some early alpha build packages available on NuGet that are semi-usable.  Look at the MicroCmsWeb project for examples of using the framework until some more detailed documentation is available

##Packages
* [MicroCms](https://www.nuget.org/packages/MicroCms/) - Core library

### Storage
* [MicroCms.Azure](https://www.nuget.org/packages/MicroCms.Azure/) - Support for storing Cms content in Azure blob storage

### Renderers 
* [MicroCms.Markdown](https://www.nuget.org/packages/MicroCms.Markdown/) - Markdown support via [MarkdownDeep](http://www.toptensoftware.com/markdowndeep/) 
* [MicroCms.SourceCode](https://www.nuget.org/packages/MicroCms.SourceCode/) - Colored syntax highlighting support via [ColorCode](http://colorcode.codeplex.com/)

### Transports/Interfaces
* [MicroCms.Mvc](https://www.nuget.org/packages/MicroCms.Mvc/) - ASP.NET MVC Extension methods and helpers
* [MicroCms.WebApi](https://www.nuget.org/packages/MicroCms.WebApi/) - ASP.NET Web API controllers
* [MicroCms.Client](https://www.nuget.org/packages/MicroCms.Client/) - ASP.NET Web API client

### Search
* [MicroCms.Lucene](https://www.nuget.org/packages/MicroCms.Lucene/) - Lucene Search Support 
