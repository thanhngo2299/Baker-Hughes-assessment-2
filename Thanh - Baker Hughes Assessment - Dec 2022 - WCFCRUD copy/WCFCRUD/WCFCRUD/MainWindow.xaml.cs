using CrudService;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WCFCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceClient client = new ServiceClient();
        public MainWindow()
        {
            InitializeComponent();
          
            
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var allData = await client.GetAllUserAsync();

            dataGrid.ItemsSource = (System.Collections.IEnumerable)allData;
        }

        
        private async void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var item = ((DataGridRow)e.Row).Item as UserData;
            var editedTextbox = e.EditingElement as TextBox;
            await client.UpdateAsync(item.Id, editedTextbox.Text);
        }

        private async void DeleteClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var data = btn.DataContext as UserData;
            await client.DeleteAsync(data.VIN);
            var allData = await client.GetAllUserAsync();

            dataGrid.ItemsSource = (System.Collections.IEnumerable)allData;
        }

        private async void AddClick(object sender, RoutedEventArgs e)
        {
            await client.InsertAsync(textBox_Input_VIN.Text, 
						comboBox_Input_Vehicle_Maker.selectedIndex,
						textBox_vehicle_year.Text,
						textBox_vehicle_model.Text,
						textBox_inspection_date.Text,
						textBox_inspector_name.Text,
						textBox_inspection_location.Text,
						comboBox_Input_inspection_result.selectedIndex,
						textBox_notes.Text);

            var allData = await client.GetAllUserAsync();

            dataGrid.ItemsSource = (System.Collections.IEnumerable)allData;
            popup.IsOpen = false;
        }

        private void InsertRecord(object sender, RoutedEventArgs e)
        {
            popup.IsOpen= true;
        }
    }
}





