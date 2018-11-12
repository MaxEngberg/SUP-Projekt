using Xamarin.Forms;
namespace Inspark.Services
{
    public class ConfirmPasswordBehaviour : Behavior<Entry>
    {
        private Entry _thisEntry;

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ConfirmPasswordBehaviour), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(Entry), typeof(ConfirmPasswordBehaviour), null);

        public Entry CompareToEntry
        {
            get { return (Entry)base.GetValue(CompareToEntryProperty); }
            set
            {
                base.SetValue(CompareToEntryProperty, value);
                if (CompareToEntry != null)
                    CompareToEntry.TextChanged -= baseValue_changed;
                value.TextChanged += baseValue_changed;
            }
        }

        void baseValue_changed(object sender, TextChangedEventArgs e)
        {
            IsValid = ((Entry)sender).Text.Equals(_thisEntry.Text);
            _thisEntry.TextColor = IsValid ? Color.Green : Color.Red;
        }

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            _thisEntry = bindable;

            if (CompareToEntry != null)
                CompareToEntry.TextChanged += baseValue_changed;

            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            if (CompareToEntry != null)
                CompareToEntry.TextChanged -= baseValue_changed;
            base.OnDetachingFrom(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            string theBase = CompareToEntry.Text;
            string confirmation = e.NewTextValue;
            IsValid = (bool)theBase?.Equals(confirmation);

            ((Entry)sender).TextColor = IsValid ? Color.Green : Color.Red;
        }
    }

}

