using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarkSoftWPF{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int countNeg = 0;
        public int countPos = 0;
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public MainWindow()
        {
            InitializeComponent();

         
            titleBorder.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
            mainMenuBorder.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
          
        }

        public void Button_Click_Menu_1(object sender, RoutedEventArgs e)
        {
            if (workSpaceTab.Visibility != Visibility.Visible)
            {
                workSpaceTab.Visibility = Visibility.Visible;
                createForm(true);
                createForm(true);
                createForm(false);
                createForm(false);
            }
            workSpaceTab.IsSelected = IsActive;
        }

        private void Button_Click_Menu_2(object sender, RoutedEventArgs e)
        {
            if (catalogTab.Visibility != Visibility.Visible)
            {
                catalogTab.Visibility = Visibility.Visible;
            }
            catalogTab.IsSelected = IsActive;

            catalog();
        }

        private void catalog()
        {
            //Чистка каталога
            while (markGroupList.Children.Count > 0)
            {
                markGroupList.Children.RemoveAt(markGroupList.Children.Count - 1);
            }
            //Оценочные группы в каталоге
            string path = Environment.CurrentDirectory + "..\\..\\..\\Оценочные группы";
            DirectoryInfo dir = new DirectoryInfo(@path);

            foreach (var item in dir.GetFiles())
            {
                Button markGroup = new Button();
                markGroup.Style = (Style)Application.Current.Resources["Catalog"];
                markGroup.Content = item.Name.Replace(".txt", "");
                markGroupList.Children.Add(markGroup);

            }
            Console.ReadLine();
        }

        private void Button_Click_Menu_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Minimized(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        List<CriterionForm> criterionList = new List<CriterionForm>();
        
        private void createForm(bool positive)
        {
            criterionList.Add(new CriterionForm());
            int criterionCount = criterionList.Count;
            criterionList[criterionCount - 1].btnAddForm.Name = "btn_" + criterionCount;
            criterionList[criterionCount - 1].btnAddGrad.Name = "btn_" + criterionCount;
            criterionList[criterionCount - 1].btnCloseForm.Name = "btn_" + criterionCount;
            criterionList[criterionCount - 1].btnAddForm.Click += Button_Click_Add_Critrrian;
            criterionList[criterionCount - 1].btnAddGrad.Click += Button_Click_Add_Grad;
            criterionList[criterionCount - 1].btnCloseForm.Click += Button_Click_Remove_Form;
            if (positive)
            {
                addCritPanel.Children.Add(criterionList[criterionList.Count - 1].border);
                countPos++;
            }
            else
            {
                addCritPanelNeg.Children.Add(criterionList[criterionList.Count - 1].border);
                criterionList[criterionList.Count - 1].positive = false;
                countNeg++;
            }
        }

        private void Button_Click_Add_Critrrian(object sender, RoutedEventArgs e)
        {
            CriterionForm parent = (sender as ButtonWithParent).criterionParent;
            createForm(parent.positive);
        }

        private void Button_Click_Add_Grad(object sender, RoutedEventArgs e)
        {
            CriterionForm parent = (sender as ButtonWithParent).criterionParent;
            parent.gradationsList.Add(new GradationWithButton(parent));
            parent.gradationsList[parent.gradationsList.Count - 1].removeGrad.Click += Button_Click_Remove_Grad;
            parent.betweenSP.Children.Add(parent.gradationsList[parent.gradationsList.Count - 1].line);
        }

        private void Button_Click_Remove_Form(object sender, RoutedEventArgs e)
        {
            CriterionForm parent = (sender as ButtonWithParent).criterionParent;
            if (parent.positive)
                if (countPos > 1)
                {
                    addCritPanel.Children.Remove(parent.border);
                    criterionList.Remove(parent);
                    countPos--;
                }
            if (!parent.positive)
                if (countNeg > 1)
                {
                    addCritPanelNeg.Children.Remove(parent.border);
                    criterionList.Remove(parent);
                    countNeg--;
                }
        }
        private void Button_Click_Remove_Grad(object sender, RoutedEventArgs e)
        {
            CriterionForm parent = (sender as ButtonWithParent).criterionParent;
            GradationWithButton gradParent = (sender as ButtonWithParent).gradationParent;
            parent.betweenSP.Children.Remove(parent.gradationsList[parent.gradationsList.IndexOf(gradParent)].line);
            parent.gradationsList.Remove(gradParent);
        }

        
        private void Button_Click_Screen_mod_change(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                
                this.WindowState = WindowState.Maximized;
            }

            else
            {
                
                this.WindowState = WindowState.Normal;
            }
                
        }
        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {
            //Проверка на пустые поля
            if(markGroup.Text == "")
            {
                MessageBox.Show("Оценочная группа не заполнена", "Не все поля заполнены",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            for (int i = 0; i < criterionList.Count; i++)
            {
                if (criterionList[i].criterionTB.Text == "")
                {
                    MessageBox.Show("Заполните все поля или удалите лишние критерии", "Не все названия критериев заполнены",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                for (int j = 0; j < criterionList[i].gradationsList.Count; j++)
                {
                    if (criterionList[i].gradationsList[j].tb.Text == "")
                    {
                        MessageBox.Show("Заполните все поля или удалите лишние градации (Минимум 2 в одном критерии)", "Не все градации заполнены",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

            }
          
            //Вывод критериев и градаций в файл *markGroup*.txt
            string path = Environment.CurrentDirectory + "..\\..\\..\\Оценочные группы\\";
            path += markGroup.Text + ".txt";
            try
            {
                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(file);
                    for (int i = 0; i < criterionList.Count; i++)
                    {
                        writer.Write("Критерий:" + criterionList[i].criterionTB.Text + "\n");
                        writer.Write("Положительный:" + criterionList[i].positive + "\n");
                        for (int j = 0; j < criterionList[i].gradationsList.Count; j++)
                        {
                            writer.Write("Градация:" + criterionList[i].gradationsList[j].tb.Text + "\n");
                        }
                        writer.Write("\n");
                    }
                    writer.Close();
                }
                if (catalogTab.Visibility == Visibility.Visible)
                {
                    catalog();
                }
                MessageBox.Show("Оценочная группа '"+ markGroup.Text + "' сохранена в каталог", "Сохранено",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.IO.IOException)
            {
                //MessageBox.Show("Требуется ввести имя", "Ошибка при вводе имени",
                //MessageBoxButton.OK, MessageBoxImage.Error);
            }

      

            //try
            //{
            //    using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            //    {
            //        StreamReader reader = new StreamReader(file);
            //        string line;
            //        int i = 0;
            //        while ((line = reader.ReadLine()) != null)
            //        {
            //            if (line == "")
            //                i++;

            //        }
            //        reader.Close();
            //    }
            //}
            //catch (System.IO.IOException)
            //{
            //    //MessageBox.Show("Требуется ввести имя", "Ошибка при вводе имени",
            //    //MessageBoxButton.OK, MessageBoxImage.Error);
            //}

        }
    }
}
