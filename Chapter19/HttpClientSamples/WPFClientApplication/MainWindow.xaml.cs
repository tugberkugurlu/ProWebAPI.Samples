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

        const string baseAPIAddress = "http://localhost:11421/api/cars";

        public MainWindow() {

            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) {

            await bindListBoxData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {

            var postWindow = new PostWindow();

            postWindow.Closed += postWindow_Closed;

            postWindow.ShowDialog();
        }

        async void postWindow_Closed(object sender, EventArgs e) {

            await bindListBoxData();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e) {

            var car = (Car)listBox1.SelectedValue;

            if (car != null)
                await deleteCar(car);
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (!btnDelete.IsEnabled)
                btnDelete.IsEnabled = true;

            if (!btnPut.IsEnabled)
                btnPut.IsEnabled = true;
        }

        //private helpers

        private async Task<IEnumerable<Car>> getCars() {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                var response = await httpClient.GetAsync(baseAPIAddress);
                var content = await response.Content.ReadAsAsync<IEnumerable<Car>>();

                return content;
            }
        }

        private async Task deleteCar(Car car) {

            using (HttpClient httpClient = new HttpClient()) {

                var response = await httpClient.DeleteAsync(
                    string.Format("{0}/{1}", baseAPIAddress, car.Id)
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

            var cars = await getCars();
            listBox1.ItemsSource = cars;
        }
    }
}
