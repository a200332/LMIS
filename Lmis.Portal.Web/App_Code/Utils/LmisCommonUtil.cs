﻿using System;
using System.Text;

namespace Lmis.Portal.Web.Utils
{
    public static class LmisCommonUtil
    {
        public static String ConvertToBase64(String text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static String ConvertFromBase64(String text)
        {
            var bytes = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}