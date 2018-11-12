using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Inspark.Services
{
    class NumberBehavior : Behavior<Entry>
    {
        // This class is used for number validation. 
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
            var pattern = @"^[0-9]{10}$";
            var number = textChangedEventArgs.NewTextValue;
            var numberEntry = sender as Entry;
            if(Regex.IsMatch(number, pattern))
            {
                numberEntry.TextColor = Color.Green;
            }
            else
            {
                numberEntry.TextColor = Color.Red;
            }
        }

        public static bool IsNumbers(string number)
        {
            if(number != null && number != "")
            {
                var pattern = @"^[0-9]{10}$";
                if (Regex.IsMatch(number, pattern))
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
