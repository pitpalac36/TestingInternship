﻿using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Madison.Pages
{
    public class MyCartPage
    {
        #region Selectors
        private readonly By _shoppingCartHeader = By.CssSelector("h1");
        private readonly By _updateShoppingCartButton = By.CssSelector("button.btn-update[title='Update Shopping Cart']:not([style*='hidden'])");
        private readonly By _emptyShoppingCartButton = By.CssSelector("#empty_cart_button");
        private readonly By _shoppingCartTable = By.CssSelector("#shopping-cart-table");
        private readonly By _continueShoppingLinkEmptyCart = By.CssSelector(".cart-empty > p > a");
        private readonly By _checkoutForm = By.CssSelector(".cart-forms");
        private readonly By _productPriceList = By.CssSelector(".product-cart-total");
        private readonly By _subtotalPriceLabel = By.CssSelector("td.a-right>span.price ");
        private readonly By _quantityField = By.CssSelector(".product-cart-actions > .input-text.qty");
        private readonly By _confirmationMessageSelector = By.ClassName("success-msg");
        private readonly By _firstItemColorSelector = By.CssSelector("td.product-cart-info dd:nth-child(2)");
        private readonly By _firstItemSizeSelector = By.CssSelector("td.product-cart-info dd:nth-child(4)");
        #endregion

        public string GetHeaderMessage()
        {
            return _shoppingCartHeader.GetText();
        }

        public bool ContinueShoppingLinkEmptyIsVisible()
        {
            return _continueShoppingLinkEmptyCart.IsElementPresent();
        }

        public void ClickOnContinueShoppingLinkEmptyCart()
        {
            _continueShoppingLinkEmptyCart.ActionClick();
            WaitHelpers.WaitForDocumentReadyState();
        }

        public void InsertQuantity(IWebElement inputField,string quantity)
        {
            inputField.SendKeys(quantity);
        }

        public void ClickUpdateCart()
        {
            _updateShoppingCartButton.ActionClick();
        }

        public IList<IWebElement> GetQuantityInputFields()
        {
            return _quantityField.GetElements();
        }

        public List<string> GetQuantities()
        {
            return _quantityField.GetElements().Select(el => el.GetAttribute("value").ToString()).ToList();
        }


        public bool CheckoutFormVisibility()
        {
            return _checkoutForm.IsElementPresent();
        }

        public bool ItemTableVisibility()
        {
            return _shoppingCartTable.IsElementPresent();
        }

        public float GetSubtotalItemsPrice()
        {
            var priceList = _productPriceList.GetElements();
            return priceList.Select(pr => float.Parse(pr.Text.Trim('$'), CultureInfo.InvariantCulture)).Sum();
        }

        public float GetSubtotalLabelPrice()
        {
            return float.Parse(_subtotalPriceLabel.GetText().Trim('$'), CultureInfo.InvariantCulture);
        }


        public void ClickOnEmptyCartButton()
        {
            _emptyShoppingCartButton.ActionClick();
            WaitHelpers.WaitForDocumentReadyState();
        }

        public void UpdateQuantityList(List<string> updateQuantity)
        {
            IList<IWebElement> quantityInputFields = GetQuantityInputFields();
            for(int i=0;i<quantityInputFields.Count;i++)
            {
                quantityInputFields[i].Clear();
                InsertQuantity(quantityInputFields[i], updateQuantity[i]);
            }
            ClickUpdateCart();
        }

        public bool IsSuccessMessageDisplayed()
        {
            return _confirmationMessageSelector.IsElementPresent();
        }

        public string FirstItemColor()
        {
            return _firstItemColorSelector.GetText();
        }

    }
}
