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
using System.Windows.Shapes;
using Newtonsoft.Json;
using WPFClientApplication.Model;

namespace WPFClientApplication {

    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window {

        public PostWindow() {

            DataContext = new Car();
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) {

            var car = (Car)DataContext;

            using (HttpClient httpClient = new HttpClient()) {

                var jsonContent = new StringContent(
                    await JsonConvert.SerializeObjectAsync(car)
                );
                jsonContent.Headers.ContentType.MediaType = "application/json";

                var response = await httpClient.PostAsync(
                    Constants.BASE_API_ADDRESS, jsonContent
                );

                try {

                    response.EnsureSuccessStatusCode();

                } catch (Exception ex) {

                    MessageBox.Show(
                        string.Format(
                            "The request was not successful. Message: {0}", ex.Message
                        )
                    );

                } finally {

                    this.Close();
                }
            }

        }
    }
}
