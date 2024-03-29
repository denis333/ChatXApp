﻿using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Xaml;

namespace ChatXApp.CustomMarkupExtension
{
    public class NameOfExtension : IMarkupExtension
    {
        public Type Type { get; set; }
        public string Member { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));


            if (Type == null || string.IsNullOrEmpty(Member) || Member.Contains("."))
                throw new ArgumentException("Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]");

            var pinfo = Type.GetRuntimeProperties().FirstOrDefault(pi => pi.Name == Member);
            var finfo = Type.GetRuntimeFields().FirstOrDefault(fi => fi.Name == Member);
            if (pinfo == null && finfo == null)
                throw new ArgumentException($"No property or field found for {Member} in {Type}");

            return Member;
        }
    }
}
