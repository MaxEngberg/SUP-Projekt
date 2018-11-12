using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace Inspark.Services
{
    public class EmailBehaviors : Behavior<Entry>
    {
        // This class is used for email validation. 
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
            var email = textChangedEventArgs.NewTextValue;

            var pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var emailEntry = sender as Entry;

            if (Regex.IsMatch(email, pattern))
            {
                emailEntry.TextColor = Color.Green;
            }
            else
            {
                emailEntry.TextColor = Color.Red;

            }
        }

        public static bool IsEmail(string email)
        {
            if(email != "" && email != null)
            {
                var pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                if (Regex.IsMatch(email, pattern))
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
