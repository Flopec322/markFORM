using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MarkSoftWPF
{
    class ButtonWithParent : Button
    {
        public CriterionForm criterionParent;
        public GradationWithButton gradationParent;
        public ButtonWithParent(CriterionForm criterionParent)
        {
            this.Style = (Style)Application.Current.Resources["Buttons"];
            this.criterionParent = criterionParent;
        }
        public ButtonWithParent(CriterionForm criterionParent, GradationWithButton gradationParent)
        {
            this.Style = (Style)Application.Current.Resources["Buttons"];
            this.criterionParent = criterionParent;
            this.gradationParent = gradationParent;
        }
    }
}
