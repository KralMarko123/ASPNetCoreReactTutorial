﻿using FluentAssertions;
using Microsoft.Playwright;
using PostsByMarko.FrontendTests.Tests;
using PostsByMarko.Shared.Constants;
using PostsTesting.UI_Models.Pages;
using Xunit;

namespace PostsByMarko.FrontendTests.Frontend
{
    [Collection("Frontend Collection")]
    public class AuthTests
    {
        private readonly IPage page;
        private LoginPage loginPage => new LoginPage(page);
        private HomePage homePage => new HomePage(page);


        public AuthTests(PostsByMarkoFactory postsByMarkoHostFactory)
        {
            page = postsByMarkoHostFactory.page!;
        }

        [Fact]
        public async Task should_show_home_page_after_login()
        {
            await loginPage.Visit();
            await loginPage.Login(TestingConstants.TEST_USER.Email, TestingConstants.TEST_PASSWORD);
            await homePage.home.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            var homePageTitleText = await homePage.containerTitle.TextContentAsync();
            homePageTitleText.Should().Be("Today's Posts");
        }
    }
}
