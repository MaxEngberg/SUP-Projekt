using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Inspark.Services
{
    public class EmptyFieldValidation : Behavior<Entry>
    {
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
            var textPattern = @"^$";

            var text = textChangedEventArgs.NewTextValue;

            var textEntry = sender as Entry;

            if (Regex.IsMatch(text, textPattern))
            {
                textEntry.BackgroundColor = Color.Red;
            }
            else
            {
                textEntry.BackgroundColor = Color.Green;
            }
        }

        public static bool IsTextOnly(string text)
        {
            if (text != "" && text != null)
            {
                var textPattern = @"^$";
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

