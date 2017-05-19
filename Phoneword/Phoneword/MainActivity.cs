using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumderText);
            Button translatedButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButon = FindViewById<Button>(Resource.Id.CallButton);

            callButon.Enabled = false;

            string translatedNumber = string.Empty;
            translatedButton.Click += (object sender, EventArgs e) =>
            {
                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButon.Text = "Call";
                    callButon.Enabled = true;
                }
                else
                {
                    callButon.Text = "Call" + translatedNumber;
                    callButon.Enabled = true;
                }
            };
            callButon.Click += (object sender, EventArgs e) =>
            {
                var callDiaolog = new AlertDialog.Builder(this);
                callDiaolog.SetMessage("Call" + translatedNumber + "?");
                callDiaolog.SetNeutralButton("Call", delegate
                {
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    StartActivity(callIntent);
                });
                callDiaolog.Show();
            };
        }
    }
}

