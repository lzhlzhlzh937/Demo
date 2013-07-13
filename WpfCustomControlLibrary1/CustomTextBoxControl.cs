using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;


namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1;assembly=WpfCustomControlLibrary1"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl2/>
    ///
    /// </summary>
    public class CustomTextBoxControl : Control
    {

        protected Timer Timer;
        protected String InputText;
        protected bool IsTyping = false;

        static CustomTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBoxControl), new FrameworkPropertyMetadata(typeof(CustomTextBoxControl)));
        }

        public CustomTextBoxControl()
        {
            //this.Timer = new Timer(100);
            //this.Timer.Stop();
            //this.Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }

        //protected virtual void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    Timer.Stop();
        //    IsTyping = false;
        //    Dispatcher.BeginInvoke((Action)(() =>
        //    {
        //        Text = Text + this.InputText;
        //        InputText = String.Empty;
        //    }), null);

        //}


        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(CustomTextBoxControl), new UIPropertyMetadata(String.Empty));

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.Focus();
        }
        bool shiftPressed = false;
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            //http://stackoverflow.com/questions/8310777/convert-keydown-keys-to-one-string-c-sharp
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftPressed = true;
            }
            else if (e.Key.Equals(Key.Back) && Text.Length != 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }
            else
            {
                if (shiftPressed == false && e.Key >= Key.D0 && e.Key <= Key.D9)
                {
                    // Number keys pressed so need to so special processing
                    // also check if shift pressed

                    Text += e.Key.ToString()[1].ToString();

                }
                else
                {
                    if (shiftPressed == false)
                    {
                        Text += e.Key.ToString().ToLower();
                    }
                    else
                    {
                        Text += e.Key.ToString();
                    }
                }

            }
            base.OnPreviewKeyDown(e);

        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key.Equals(Key.LeftShift) || e.Key.Equals(Key.RightShift))
            {
                shiftPressed = false;
            }
            base.OnKeyUp(e);
        }

    }
}
