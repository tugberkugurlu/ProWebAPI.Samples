using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using WPFClientApplication.Model;

namespace WPFClientApplication {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            
            InitializeComponent();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (!btnDelete.IsEnabled)
                btnDelete.IsEnabled = true;

            if (!btnPut.IsEnabled)
                btnPut.IsEnabled = true;
        }

        private async void btnGet_Click(object sender, RoutedEventArgs e) {

            await bindListBoxData();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e) {

            var car = (Car)listBox1.SelectedValue;
            if (car != null) { 
                var msgResult = MessageBox.Show("Do you really would like to delete the record?", "Are you sure?", MessageBoxButton.YesNo);
                if (msgResult == MessageBoxResult.Yes) { 
                    await deleteCar(car);
                }
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e) {

            var postWindow = new PostWindow();
            postWindow.Closed += postWindow_Closed;
            postWindow.ShowDialog();
        }
        async void postWindow_Closed(object sender, EventArgs e) {

            await bindListBoxData();
        }

        private void btnPut_Click(object sender, RoutedEventArgs e) {

            var car = (Car)listBox1.SelectedValue;
            if (car != null) {

                var putWindow = new PutWindow();
                putWindow.Closed += putWindow_Closed;
                putWindow.DataContext = car;
                putWindow.ShowDialog();
            }
        }
        async void putWindow_Closed(object sender, EventArgs e) {

            await bindListBoxData();
        }

        //private helpers

        private async Task<IEnumerable<Car>> getCars() {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                var response = await httpClient.GetAsync(Constants.BASE_API_ADDRESS);

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsAsync<IEnumerable<Car>>();
                return content;
            }
        }

        private async Task deleteCar(Car car) {

            using (HttpClient httpClient = new HttpClient()) {

                var response = await httpClient.DeleteAsync(
                    string.Format("{0}/{1}", Constants.BASE_API_ADDRESS, car.Id)
                );

                try {

                    response.EnsureSuccessStatusCode();
                    await bindListBoxData();

                } catch (Exception ex) {

                    MessageBox.Show(
                        string.Format(
                            "The request was not successful. Message: {0}", ex.Message
                        )
                    );
                }
            }
        }

        private async Task bindListBoxData() {

            try {

                var cars = await getCars();
                listBox1.ItemsSource = cars;

            } catch (Exception ex) {

                MessageBox.Show(
                    string.Format(
                        "The request was not successful. Message: {0}", ex.Message
                    )
                );
            }
        }
    }
}