﻿using System.ComponentModel;
using System.ServiceModel;
using System.Text.RegularExpressions;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Login
{
	public class RegisterViewModel : ViewModelBase
	{
		private string _email = "";
		private string _password1 = "";
		private string _password2 = "";
		private string _username = "";

		public RegisterViewModel()
		{
			RegisterCommand = new Command(Register, CanRegister);
		}

		public Command RegisterCommand { get; }

		public string Username
		{
			get { return _username; }
			set
			{
				if (Equals(_username, value)) return;
				_username = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		public string Email
		{
			get { return _email; }
			set
			{
				if (Equals(_email, value)) return;
				_email = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		public string Password1
		{
			get { return _password1; }
			set
			{
				if (Equals(_password1, value)) return;
				_password1 = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		public string Password2
		{
			get { return _password2; }
			set
			{
				if (Equals(_password2, value)) return;
				_password2 = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		private void Register()
		{
			Service.RegisterCompleted += RegisterCompleted;

			Workers++;

			Service.RegisterAsync(Username, Email, Password1);
		}

		private void RegisterCompleted(object sender, AsyncCompletedEventArgs args)
		{
			Workers--;
			Service.RegisterCompleted -= RegisterCompleted;

			if (args.Error != null)
			{
				try
				{
					throw args.Error;
				}
				catch (FaultException fault)
				{
					DisplayAlert(this, new MessageData("Failure", fault.Message, "Ok"));
				}
			}
		}

		private bool CanRegister()
		{
			return IsInputValid();
		}

		private bool IsInputValid()
		{
			const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

			var isUsernameValid = Username.Length > 3 && Username.Length < 20;
			var regex = new Regex(emailRegex);
			var isEmailValid = regex.IsMatch(Email);
			var isPasswordValid = Equals(Password1, Password2) && Password1.Length > 8;
			return isPasswordValid && isUsernameValid && isEmailValid;
		}
	}
}