using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
   public class PageLinkTagHelperTests 
    {
        [Fact]

        public void Can_Generate_Link_Tests() {

            //Arrange 
            var helperUrl = new Mock <IUrlHelper>();
            helperUrl.SetupSequence(h => h.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f=>f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(helperUrl.Object);

            var context = new Mock<ViewContext>();
            PageLinkTagHelper tagHelper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage =2,
                    TotalItems = 12,
                    ItemsPerPage =4,
                },
                ViewContext = context.Object,
                PageAction = "Test"

            };

            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(), new Dictionary<object, object>(), ""
                );

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cache, encode) => Task.FromResult(content.Object)
                );

            //Act 
            tagHelper.Process(ctx, output);


            //Assert 
            Assert.Equal(
                @"<a href=""Test/Page1"">1</a><a href=""Test/Page2"">2</a><a href=""Test/Page3"">3</a>",
                output.Content.GetContent());









        }




    }

    
 }
