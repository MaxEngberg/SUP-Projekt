using System;
using System.Windows.Input;
using Inspark.Helpers;
using Inspark.Views;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
	public class IntroCodeViewModel : BaseViewModel
    {
        public IntroCodeViewModel()
        {
        }

		private string _message;

        public string Message
        {
			get { return _message; }
            set
            {
				if (_message != value)
                {
					_message = value;
                    OnPropertyChanged();
                }
            }
        }

		private string _introCode;

        public string IntroCode
        {
            get { return _introCode; }
            set
            {
                if (_introCode != value)
                {
                    _introCode = value;
                    OnPropertyChanged();
                }
            }
        }

		public ICommand IntroCodeCommand => new Command(async () =>
        {
			var response = await _api.introCode(IntroCode);
			if (response)
			{
				Settings.UserRole = "Intro";
				Application.Current.MainPage = new MainPage(new HomePage());
			}
			else
			{
				Message = "Något Gick Fel försök senare";
			}
		});
    }
}
