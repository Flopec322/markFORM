using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MarkSoftWPF
{
    class GradationWithButton : Gradation
    {
        public ButtonWithParent removeGrad;
        public GradationWithButton()
        {

        }
        public GradationWithButton(CriterionForm parent)
        {
            removeGrad = new ButtonWithParent(parent, this);
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(@"img/esc2.png", UriKind.RelativeOrAbsolute));
            img.Width = 15;
            removeGrad.Content = img;
            removeGrad.Margin = new Thickness(10, 0, 0, 0);
            line.Children.Add(this.removeGrad);
        }
    }
}
