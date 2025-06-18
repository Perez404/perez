using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
using System.Windows.Shapes;

namespace Production.View
{
    /// <summary>
    /// Логика взаимодействия для EditMaterialWindow.xaml
    /// </summary>
    public partial class EditMaterialWindow : Window
    {
        int mode;
        Model.Material mat;
        //public EditMaterialWindow()
        //{
        //    InitializeComponent();
        //}
        public EditMaterialWindow(int mode, Model.Material mat )
        {
            InitializeComponent();
            this.mode = mode;
            this.mat = mat;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.MaterialType> matTypes = App.DB.MaterialType.ToList();
            cbType.ItemsSource = matTypes;
            cbType.SelectedValuePath = "MaterialTypeId";
            cbType.DisplayMemberPath = "MaterialTypeName";

            if (mode==1)
            {
                tbTitle.Text = "редактирование материала";
                btnFix.Content = "обновить материал";
                cbType.SelectedValue=mat.MaterialTypeId;
                tbName.Text = mat.MaterialName;
                tbUnitPrice.Text=mat.MaterialUnitPrice.ToString();
                tbQuantityInPack.Text=mat.MaterialQuantityInPack.ToString();
                tbQuantityInStock.Text=mat.MaterialQuanityInStock.ToString();
                tbMinQuantity.Text=mat.MaterialMinQuantity.ToString();
            }
            else
            {
                tbTitle.Text = "добавление материала";
                btnFix.Content = "добавить материал";
            }
        }
        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            mat.MaterialTypeId = (int)cbType.SelectedValue;
            mat.MaterialName=tbName.Text;
            
            mat.MaterialUnitPrice=double.Parse(tbUnitPrice.Text);
            tbQuantityInPack.Text = mat.MaterialQuantityInPack.ToString();
            tbQuantityInStock.Text = mat.MaterialQuanityInStock.ToString();
            tbMinQuantity.Text = mat.MaterialMinQuantity.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
