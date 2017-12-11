using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        private CheckBox cbVs, cbR1, cbR2, cbVo;
        private TextBlock message;
        private TextBox etVs, etR1, etR2, etVo;
        Boolean chkVs = false;
        Boolean chkR1 = false;
        Boolean chkR2 = false;
        Boolean chkVo = false;
        int numChecked = 0;
        double vsource = 0;
        double resist1 = 0;
        double resist2 = 0;
        double voutput = 0;    
        Boolean error = false;
        String stringUpdated = "";

        public MainPage()
        {
            this.InitializeComponent();          

            var av = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            var di = Windows.Graphics.Display.DisplayInformation.GetForCurrentView();
            var bounds = av.VisibleBounds;
            var factor = di.RawPixelsPerViewPixel;
            var size = new Windows.Foundation.Size(bounds.Width * factor, bounds.Height * factor);
            size.Height -= 100;
            size.Width -= 100;
            av.TryResizeView(size);

            cbVs = checkBoxVsource;
            cbR1 = checkBoxR1;
            cbR2 = checkBoxR2;
            cbVo = checkBoxVout;
            etVs = editTextVsource;
            etR1 = editTextR1;
            etR2 = editTextR2;
            etVo = editTextVout;
            message = textViewUpdated;            
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            message.Text = ("");

            error = false;
            if (numChecked != 3)
            {
                error = true;
                message.Text = ("Please check 3 boxes and input values");
            }
            else
            {

                if (chkVs == true)
                {
                    if (etVs.Text == (""))
                    {
                        message.Text = ("Please input Voltage Source");
                        error = true;
                    }
                    else
                    {
                        vsource = Double.Parse(etVs.Text);
                    }
                }
                if (chkR1 == true)
                {
                    if (etR1.Text == (""))
                    {
                        message.Text = ("Please input Resistance 1");
                        error = true;
                    }
                    else
                    {
                        resist1 = Double.Parse(etR1.Text);
                    }
                }
                if (chkR2 == true)
                {
                    if (etR2.Text == (""))
                    {
                        message.Text = ("Please input Resistance 2");
                        error = true;
                    }
                    else
                    {
                        resist2 = Double.Parse(etR2.Text);
                    }
                }
                if (chkVo == true)
                {
                    if (etVo.Text == (""))
                    {
                        message.Text = ("Please input Output Voltage");
                        error = true;
                    }
                    else
                    {
                        voutput = Double.Parse(etVo.Text);
                    }
                }

            }

            if (error == false)
            {
                if ((chkVs == true) && (chkR1 == true) && (chkR2 == true))
                {
                    voutput = (vsource * resist2) / (resist1 + resist2);
                    etVo.Text = (voutput).ToString();
                    message.Text = stringUpdated;
                }
                if ((chkVs == true) && (chkR1 == true) && (chkVo == true))
                {
                    resist2 = (voutput * resist1) / (vsource - voutput);
                    etR2.Text = (resist2).ToString();
                    message.Text = stringUpdated;
                }
                if ((chkVs == true) && (chkR2 == true) && (chkVo == true))
                {
                    resist1 = (resist2 * (vsource - voutput)) / voutput;
                    etR1.Text = (resist1).ToString();
                    message.Text = stringUpdated;
                }
                if ((chkR1 == true) && (chkR2 == true) && (chkVo == true))
                {
                    vsource = (voutput * (resist1 + resist2)) / resist2;
                    etVs.Text = (vsource).ToString();
                    message.Text = stringUpdated;
                }

            }            

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbVs.IsChecked = false;
            cbR1.IsChecked = false;
            cbR2.IsChecked = false;
            cbVo.IsChecked = false;
            etVs.Text = ("");
            etR1.Text = ("");
            etR2.Text = ("");
            etVo.Text = ("");
            cbVs.IsEnabled = true;
            cbR1.IsEnabled = true;
            cbR2.IsEnabled = true;
            cbVo.IsEnabled = true;
            etVs.IsEnabled = false;
            etR1.IsEnabled = false;
            etR2.IsEnabled = false;
            etVo.IsEnabled = false;
            message.Text = ("");
            chkVs = false;
            chkR1 = false;
            chkR2 = false;
            chkVo = false;

            numChecked = 0;
        }

        private void checkBoxVsource_CheckedChanged(object sender, RoutedEventArgs e)
        {            

            if (checkBoxVsource.IsChecked == true)
                {
                chkVs = true;
                numChecked++;
                etVs.IsEnabled = true;
            }
            else
            {
                ClearUnchecked();

                chkVs = false;
                numChecked--;
                etVs.IsEnabled = false;
            }

            if (numChecked == 3)
            {
                if (chkVs == false)
                {

                    cbVs.IsEnabled = false;
                    etVs.IsEnabled = false;
                }
                if (chkR1 == false)
                {
                    cbR1.IsEnabled = false;
                    etR1.IsEnabled = false;
                }
                if (chkR2 == false)
                {
                    cbR2.IsEnabled = false;
                    etR2.IsEnabled = false;
                }
                if (chkVo == false)
                {
                    cbVo.IsEnabled = false;
                    etVo.IsEnabled = false;
                }
            }
            else
            {
                cbVs.IsEnabled = true;
                cbR1.IsEnabled = true;
                cbR2.IsEnabled = true;
                cbVo.IsEnabled = true;
            }
        }

        private void checkBoxR1_CheckedChanged(object sender, RoutedEventArgs e)
        {

            if (checkBoxR1.IsChecked == true)
                {
                chkR1 = true;
                numChecked++;
                etR1.IsEnabled = true;
            }
            else
            {
                ClearUnchecked();

                chkR1 = false;
                numChecked--;
                etR1.IsEnabled = false;
            }

            if (numChecked == 3)
            {
                if (chkVs == false)
                {

                    cbVs.IsEnabled = false;
                    etVs.IsEnabled = false;
                }
                if (chkR1 == false)
                {
                    cbR1.IsEnabled = false;
                    etR1.IsEnabled = false;
                }
                if (chkR2 == false)
                {
                    cbR2.IsEnabled = false;
                    etR2.IsEnabled = false;
                }
                if (chkVo == false)
                {
                    cbVo.IsEnabled = false;
                    etVo.IsEnabled = false;
                }
            }
            else
            {
                cbVs.IsEnabled = true;
                cbR1.IsEnabled = true;
                cbR2.IsEnabled = true;
                cbVo.IsEnabled = true;
            }
        }

        private void checkBoxR2_CheckedChanged(object sender, RoutedEventArgs e)
        {

            if (checkBoxR2.IsChecked == true)
                {
                chkR2 = true;
                numChecked++;
                etR2.IsEnabled = true;
            }
            else
            {
                ClearUnchecked();

                chkR2 = false;
                numChecked--;
                etR2.IsEnabled = false;
            }

            if (numChecked == 3)
            {
                if (chkVs == false)
                {
                    cbVs.IsEnabled = false;
                    etVs.IsEnabled = false;
                }
                if (chkR1 == false)
                {
                    cbR1.IsEnabled = false;
                    etR1.IsEnabled = false;
                }
                if (chkR2 == false)
                {
                    cbR2.IsEnabled = false;
                    etR2.IsEnabled = false;
                }
                if (chkVo == false)
                {
                    cbVo.IsEnabled = false;
                    etVo.IsEnabled = false;
                }
            }
            else
            {
                cbVs.IsEnabled = true;
                cbR1.IsEnabled = true;
                cbR2.IsEnabled = true;
                cbVo.IsEnabled = true;
            }
        }

        private void checkBoxVout_CheckedChanged(object sender, RoutedEventArgs e)
        {

            if (checkBoxVout.IsChecked == true)
                {
                chkVo = true;
                numChecked++;
                etVo.IsEnabled = true;
            }
            else
            {
                ClearUnchecked();                

                chkVo = false;
                numChecked--;
                etVo.IsEnabled = false;
            }

            if (numChecked == 3)
            {
                if (chkVs == false)
                {

                    cbVs.IsEnabled = false;
                    etVs.IsEnabled = false;
                }
                if (chkR1 == false)
                {
                    cbR1.IsEnabled = false;
                    etR1.IsEnabled = false;
                }
                if (chkR2 == false)
                {
                    cbR2.IsEnabled = false;
                    etR2.IsEnabled = false;
                }
                if (chkVo == false)
                {
                    cbVo.IsEnabled = false;
                    etVo.IsEnabled = false;
                }
            }
            else
            {
                cbVs.IsEnabled = true;
                cbR1.IsEnabled = true;
                cbR2.IsEnabled = true;
                cbVo.IsEnabled = true;
            }
        }

        private void etVs_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (checkBoxVsource.IsChecked == true)
            {
                if (!Regex.IsMatch(sender.Text, "^\\d*?\\d*$") && sender.Text != "")
                {
                    int pos = sender.SelectionStart - 1;
                    sender.Text = sender.Text.Remove(pos, 1);
                    sender.SelectionStart = pos;
                }
            }
        }

        private void etR1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (checkBoxR1.IsChecked == true)
            {
                if (!Regex.IsMatch(sender.Text, "^\\d*?\\d*$") && sender.Text != "")
                {
                    int pos = sender.SelectionStart - 1;
                    sender.Text = sender.Text.Remove(pos, 1);
                    sender.SelectionStart = pos;
                }
            }
        }

        private void etR2_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (checkBoxR2.IsChecked == true)
            {
                if (!Regex.IsMatch(sender.Text, "^\\d*?\\d*$") && sender.Text != "")
                {
                    int pos = sender.SelectionStart - 1;
                    sender.Text = sender.Text.Remove(pos, 1);
                    sender.SelectionStart = pos;
                }
            }
        }

        private void etVo_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (checkBoxVout.IsChecked == true)
            {
                if (!Regex.IsMatch(sender.Text, "^\\d*?\\d*$") && sender.Text != "")
                {
                    int pos = sender.SelectionStart - 1;
                    sender.Text = sender.Text.Remove(pos, 1);
                    sender.SelectionStart = pos;
                }
            }
        }

        private void ClearUnchecked() {

            if (checkBoxVsource.IsChecked == false)
                {
                    etVs.Text = "";
                }
                if (checkBoxR1.IsChecked == false)
                {
                    etR1.Text = "";
                }
                if (checkBoxR2.IsChecked == false)
                {
                    etR2.Text = "";
                }
                if (checkBoxVout.IsChecked == false)
                {
                    etVo.Text = "";
                }
        }
    }
}
