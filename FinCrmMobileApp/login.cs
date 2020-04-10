using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinCrmAPICommunication;



namespace FinCrmMobileApp
{
    [Activity(Label = "Login", MainLauncher = true)]
    public class Login : Activity
    {

       
        private Button _loginButton;
        private TextView _errMsgLabel;
        private EditText _tbEmail;
        private EditText _tbPass;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
            // Create your application here
            FindViews();
            LinkEventHandlers();
        }

        private void FindViews()
        {
            _loginButton = FindViewById<Button>(Resource.Id.btn_login);
            _errMsgLabel = FindViewById<TextView>(Resource.Id.error_msg);
            _tbEmail = FindViewById<EditText>(Resource.Id.input_email);
            _tbPass = FindViewById<EditText>(Resource.Id.input_password);

        }

        private void LinkEventHandlers()
        {
            _loginButton.Click += _loginButton_Click;
        }

        private async void _loginButton_Click(object sender, EventArgs e)
        {
            _errMsgLabel.Visibility = ViewStates.Invisible;

            if (!Android.Util.Patterns.EmailAddress.Matcher(_tbEmail.Text).Matches())
            {
                _errMsgLabel.Text = "Wprowadź email w prwidłowym formacie";
                _errMsgLabel.Visibility = ViewStates.Visible;
                return;
            }

            if (_tbPass.Text.Length < 3)
            {
                _errMsgLabel.Text = "Podaj hasło";
                _errMsgLabel.Visibility = ViewStates.Visible;
                return;
            }

            LoginAPI api = new LoginAPI();
            string isLoginSucces = await api.LoginProcess(_tbEmail.Text, _tbPass.Text);

            if (isLoginSucces.ToString() == "NOT")
            {
                _errMsgLabel.Text = "Błąd logowania";
                _errMsgLabel.Visibility = ViewStates.Visible;
                return;
            }

            else
            {
                _errMsgLabel.Text = "Zalogowanie prawidłowe jwt:" + isLoginSucces;
                _errMsgLabel.Visibility = ViewStates.Visible;
                
                return;
            }
        }





        
    }
}