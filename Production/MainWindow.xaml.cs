using Production.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Production
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                App.DB = new Model.Entities();
                MessageBox.Show("успешн подкл","попытка подкл к бд",MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("не удалось подкл", "попытка подкл к бд", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Material> materials = App.DB.Material.ToList();
           
            foreach (Model.Material mat in materials)
            {
                double amountNeeded;
                List<Model.MaterialProduct> matprods = App.DB.MaterialProduct.Where(mp=>mp.MaterialId==mat.MaterialId).ToList();
                amountNeeded = matprods.Sum(mp => mp.MaterialQuantityNeeded);
                mat.AmountNeeded = Math.Round(amountNeeded, 2);  
            }
            lbMaterials.ItemsSource = materials;
        }
        private void lbMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Material mat=lbMaterials.SelectedItem as Model.Material; 
            View.EditMaterialWindow editMaterialWindow = new View.EditMaterialWindow(1,mat);
            this.Hide();
            editMaterialWindow.ShowDialog();
            this.Show();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
