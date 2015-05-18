using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace CGG
{
    class ArgsPanel
    {
        /**add to given StackPanel n TextBox, and return list of this TextBox
         */
        public static List<TextBox> CreateTextBoxs(
            StackPanel panel, int n)
        {
            var list = new List<TextBox>();
            list.Clear();
            panel.Children.Clear();
            for (var i = 0; i < n; i++)
            {
                var a = new TextBox
                {
                    Margin = new Thickness(35, 10, 0, 0),
                    Height = 23,
                    Width = 63,
                    TextWrapping = TextWrapping.Wrap
                };
                list.Add(a);
                panel.Children.Add(a);
            }
            return list;
        }
    }
}
