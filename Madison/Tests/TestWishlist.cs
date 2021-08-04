﻿using FluentAssertions;
using Madison.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Madison.Tests
{
    [TestClass]
    public class TestWishlist: BaseTest
    {

        private static IEnumerable<object[]> GetQuantity()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new object[] { Pages.MyWishlistPage.RandomQuantity() };
            }
        }

        private static IEnumerable<object[]> GetComment()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new object[] { Pages.MyWishlistPage.RandomComment() };
            }
        }

        private static IEnumerable<object[]> GetInvalidEmails()
        {
            yield return new object[] { "", "This is a required field" };
            yield return new object[] { Pages.MyWishlistPage.RandomWord(), "Please enter a valid email addresses" };
        }

        [TestMethod]
        public void MyWishlistButtonIsDisplayed()
        {
            Pages.HomePage.ClickOnAccount();
            Pages.MyWishlistPage.IsWishlistButtonDisplayed().Should().BeTrue();
        }

        [TestMethod]
        public void ClickOnMyWishlistButtonRedirects()
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.IsRedirectedToWishlist().Should().BeTrue();
        }

        [DataTestMethod]
        [DynamicData(nameof(GetQuantity), DynamicDataSourceType.Method)]
        public void WishlistItemQuantityUpdatesCorrectly(string quantity)
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.ChangeQuantity(quantity);
            Pages.MyWishlistPage.UpdateItem();
            Pages.MyWishlistPage.ItemQuantity().Should().Be(quantity);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetComment), DynamicDataSourceType.Method)]
        public void WishlistItemCommentUpdatesCorrectly(string comment)
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.InsertComment(comment);
            Pages.MyWishlistPage.UpdateItem();
            Pages.MyWishlistPage.ItemComment().Should().Be(comment);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetQuantity), DynamicDataSourceType.Method)]
        public void WishlistUpdatesCorrectly(string quantity)
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.ChangeQuantity(quantity);
            Pages.MyWishlistPage.UpdateWishlist();
            Pages.MyWishlistPage.ItemQuantity().Should().Be(quantity);
        }

        [TestMethod]
        public void ClickOnShareWishlistButtonRedirects()
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.ShareWishlist();
            string expectedUrl = WebLinks.ShareWishlist;
            Pages.MyWishlistPage.GetUrl().Should().Contain(expectedUrl);
            Pages.MyWishlistPage.IsShareWishlistFormDisplayed().Should().BeTrue();
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidEmails), DynamicDataSourceType.Method)]
        public void ShareWishlistFormValidatesBadEmail(string email, string expectedMessage)
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.ShareWishlist();
            Pages.MyWishlistPage.FillEmail(email);
            Pages.MyWishlistPage.ShareWishlistFinal();
            Pages.MyWishlistPage.IsRequiredValidationAdviceDisplayed().Should().BeTrue();
            Pages.MyWishlistPage.GetRequiredValidationAdvice().Should().Contain(expectedMessage);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetQuantity), DynamicDataSourceType.Method)]
        public void UpdateWishlistFromShowroom(string quantity)
        {
            Pages.HomePage.SelectMyAccountMenu(Menu.Login.GetDescription());
            Pages.LoginPage.Login(Constants.Usernames[0], Constants.Passwords[0]);
            Pages.NavigationPage.GoToWishlist();
            Pages.MyWishlistPage.ClickOnEdit();
            Pages.MyWishlistPage.EditQuantityFromShowroom(quantity);
            Pages.MyWishlistPage.UpdateWishlistFromShowroom();
            Pages.MyWishlistPage.ItemQuantity().Should().Be(quantity);
            Pages.MyWishlistPage.IsErrorMessageDisplayed().Should().BeFalse();
        }

    }
}
