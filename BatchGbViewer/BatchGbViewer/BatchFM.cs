using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BatchGbViewer
{
    public partial class MainWindow : Window
    {
        // create the client for connecting to the api
        HttpClient client = new HttpClient();
                

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listTrainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            List<string> x = new List<string>() {  };
            //listBatchName.Items.Add(x);

        }
        private async void listTrainer_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> datat = new List<string>();
            datat.Add("Fred");
            datat.Add("Joe");
            datat.Add("Ryan");
            datat.Add("Emily");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = await getUser();

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void listTechnology_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> datae = new List<string>();
            datae.Add("Java");
            datae.Add("Dot Net");
            datae.Add("Pega");
            datae.Add("Sdet");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = datae;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void listTechnology_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBatches_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> datab = new List<string>();
            datab.Add("1701Net");
            datab.Add("1702Java");
            datab.Add("1703Sdet");
            datab.Add("1702Pega");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = datab;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        public async Task<List<Batch>> getBatch()
        {
            client.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("./api/batches");
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var batches = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
            List<Batch> batch = new List<Batch>();
                     
            if(batches != null)
            {               
                foreach(Batch b in batches.ToList())
                {
                    
                    batch.Add(b);
                }
            }
            return batch;                                               
        }

        public async Task<List<User>> getUser()
        {
            client.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("./api/users");
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            List<User> user = new List<User>();
           
           // working on getting the trainers into a list 

            if (users != null)
            {
                foreach (User u in users.ToList())
                {                   
                        user.Add(u);                                       
                }
            }
            return user;
        }

    }

    public class Batch
    {
        public string ID { get; set; }
        public string BatchName { get; set; }
        public string Technology { get; set; }
        public DateTime StartDateFrom { get; set; }
        public DateTime StartDateTo { get; set; }
        public string Trainer { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserType { get; set; }
        
    }

    public class Exam
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Technology { get; set; }
        public double Grade { get; set; }
    }
}
