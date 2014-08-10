using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using Xunit;

namespace MicroCms.Tests
{
    public abstract class CmsRenderServiceTests<TRenderer> : CmsUnityTests
        where TRenderer : ICmsRenderService
    {
        protected abstract string ContentType { get; }
        protected abstract TRenderer CreateRenderer();

        [Fact]
        public void SupportsNullReturnsFalse()
        {
            Assert.False(CreateRenderer().Supports(null));
        }

        [Fact]
        public void SupportsCorrectTypeReturnsTrue()
        {
            Assert.True(CreateRenderer().Supports(ContentType));
        }

        [Fact]
        public void SupportsIncorrectTypeReturnsFalse()
        {
            Assert.False(CreateRenderer().Supports(Guid.NewGuid().ToString()));
        }
    }
}