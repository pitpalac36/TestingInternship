﻿using System.ComponentModel;

namespace Madison.Helpers
{
    public enum Menu
    {
        [Description("My Account")] MyAccount,
        [Description("My Wishlist")] MyWishlits,
        [Description("My Cart")] MyCart,
        [Description("Checkout")] Checkout,
        [Description("Register")] Register,
        [Description("Log In")] Login,

    }
    public static class ResourceFileHelper
    {
        public static string GetDescription (this Menu menu )
        {
            var fieldInfo = menu.GetType().GetField(menu.ToString());
            if (fieldInfo == null) return string.Empty;

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }
    }
}

