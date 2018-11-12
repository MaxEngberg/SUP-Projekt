using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Inspark.Services
{
    class TextOnlyBehavior : Behavior<Entry>
    {
        // This class is used for text only validation. 
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindableonTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableonTextChanged;
        }

        private void BindableonTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var textPattern = @"^[a-öA-Ö]*$";

            var text = textChangedEventArgs.NewTextValue;

            var textEntry = sender as Entry;

            if (Regex.IsMatch(text, textPattern))
            {
                textEntry.TextColor = Color.Green;
            }
            else
            {
                textEntry.TextColor = Color.Red;
            }
        }

        public static bool IsTextOnly(string text)
        {
            if(text != "" && text != null)
            {
                var textPattern = @"^[a-öA-Ö]*$";
                if (Regex.IsMatch(text, textPattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
