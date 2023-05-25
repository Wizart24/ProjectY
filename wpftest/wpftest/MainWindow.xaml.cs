using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
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
using wpftest.Models;

namespace wpftest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private HttpClient _httpClient;
		public List<Picture> Pictures { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			// Initialize the HttpClient with the base URL
			_httpClient = new HttpClient()
			{
				BaseAddress = new Uri("https://localhost:7085/")
			};
		}

		private async void btn_Login_Click(object sender, RoutedEventArgs e)
		{
			var token = "";

			var user = new User()
			{
				Username = txt_Name.Text,
				Password = txt_Password.Text
			};

			var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", user);

			if (response.IsSuccessStatusCode)
			{
				var wpfServiceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
				if (wpfServiceResponse != null && wpfServiceResponse.Success)
				{
					token = wpfServiceResponse.Data!;
					StoreToken(token); // Store the token securely
				}
			}
			else
			{
				// Handle login failure
			}

			token = GetStoredToken(); // Retrieve the token from storage

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				response = await _httpClient.GetAsync("api/Picture");

				if (response.IsSuccessStatusCode)
				{
					var responseJson = await response.Content.ReadAsStringAsync();
					var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

					if (responseObject!["data"] is JArray dataArray)
					{
						var pictures = dataArray.ToObject<List<Picture>>();
						dtg_Pictures.ItemsSource = pictures;
					}
				}
			}
			else
			{
				// Handle case when token is not available or expired
			}
		}

		private string GetStoredToken()
		{
			// Retrieve the token from your storage mechanism
			// Return the token or an empty string if not available
			string token = "";

			using (var reader = new StreamReader("log.txt"))
			{
				token = reader.ReadLine()!;
			}

			return token;
		}

		private void StoreToken(string token)
		{
			// Store the token securely in your storage mechanism
			using (var writer = new StreamWriter("log.txt", true))
			{
				writer.WriteLine(token);
			}
		}
	}
}
