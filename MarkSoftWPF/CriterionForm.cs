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
    class CriterionForm
    {
        public Border border = new Border();
        public TextBox criterionTB = new TextBox();
        public ButtonWithParent btnAddForm;
        public ButtonWithParent btnAddGrad;
        public ButtonWithParent btnCloseForm;
        public StackPanel betweenSP = new StackPanel();
        public bool positive = true;
        public List<GradationWithButton> gradationsList = new List<GradationWithButton>();
        public CriterionForm()
        {
            
            btnAddForm = new ButtonWithParent(this);
            btnAddGrad = new ButtonWithParent(this);
            btnCloseForm = new ButtonWithParent(this);
            border.Style = (Style)Application.Current.Resources["addFormBorderStyle"];
            StackPanel mainSP = new StackPanel();
            StackPanel line = new StackPanel();
            line.Orientation = Orientation.Horizontal;
            Label label = new Label();
            label.Content = "Критерий:";
            label.Style = (Style)Application.Current.Resources["addFormLabelStyle"];
            line.Children.Add(label);

        
            line.Children.Add(criterionTB);

            Image img = new Image();
            img.Source = new BitmapImage(new Uri(@"img/icon1.png", UriKind.RelativeOrAbsolute));
            img.Width = 15;
            btnAddForm.Content = img;
            btnAddForm.Margin = new Thickness(10, 0, 0, 0);

            
            line.Children.Add(btnAddForm);
            mainSP.Children.Add(line);
            mainSP.Children.Add(betweenSP);

            gradationsList.Add(new GradationWithButton());
            gradationsList.Add(new GradationWithButton());

            StackPanel line2 = gradationsList[0].line;
            StackPanel line1 = gradationsList[1].line;

            Image img2 = new Image();
            img2.Source = new BitmapImage(new Uri(@"img/icon1.png", UriKind.RelativeOrAbsolute));
            img2.Width = 15;
            btnAddGrad.Content = img2;
            btnAddGrad.Margin = new Thickness(10, 0, 0, 0);
            line2.Children.Add(btnAddGrad);
            mainSP.Children.Add(line1);
            mainSP.Children.Add(line2);
           


            StackPanel line3 = new StackPanel();

            Image img3 = new Image();
            img3.Height = 15;
            line3.Margin = new Thickness(15);
            img3.Source = new BitmapImage(new Uri(@"img/esc2.png", UriKind.RelativeOrAbsolute));
            line3.Children.Add(btnCloseForm);
            btnCloseForm.VerticalAlignment = VerticalAlignment.Bottom;
            btnCloseForm.Content = img3;
            mainSP.Children.Add(line3);

            this.border.Child = mainSP;

        }
    }
}
