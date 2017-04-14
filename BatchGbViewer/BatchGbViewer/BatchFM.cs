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
      private void btnSearch_Click(object sender, RoutedEventArgs e)
      {

      }

      private void listTrainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {

         List<string> x = new List<string>() { };
         //listBatchName.Items.Add(x);

      }


      private async void listTrainer_Loaded(object sender, RoutedEventArgs e)
      {
         // ... Get the ComboBox reference.
         var comboBox = sender as ComboBox;

         // ... Assign the ItemsSource to the List.
         comboBox.ItemsSource = await getTrainers();

         // ... Make the first item selected.
         comboBox.SelectedIndex = 0;
      }

      public async Task<List<string>> getTrainers()
      {
         initializeClients();
         HttpResponseMessage response = await usersClient.GetAsync("./api/users");
         response.EnsureSuccessStatusCode(); // Throw on error code.
         var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
         List<string> user = new List<string>();

         // working on getting the trainers into a list 

         if (users != null)
         {
            foreach (User u in users.ToList())
            {
               if (u.UserType == "3" && !string.IsNullOrEmpty(u.FName) && !string.IsNullOrEmpty(u.LName))
               {
                  user.Add(u.FName + " " + u.LName);
               }
            }
         }
         return user;
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

      private async void listBatches_Loaded(object sender, RoutedEventArgs e)
      {
         // ... Get the ComboBox reference.
         var comboBox = sender as ComboBox;

         // ... Assign the ItemsSource to the List.
         comboBox.ItemsSource = await getBatches();

         // ... Make the first item selected.
         comboBox.SelectedIndex = 0;
      }

      public async Task<List<string>> getBatches()
      {
         HttpResponseMessage response = await batchClient.GetAsync("./api/batches");
         response.EnsureSuccessStatusCode(); // Throw on error code. 
         var batches = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
         List<string> batch = new List<string>();

         if (batches != null)
         {
            foreach (Batch b in batches.ToList())
            {

               batch.Add(b.BatchName);
            }
         }
         return batch;
      }

      public async Task<List<string>> getUser()
      {
         HttpResponseMessage response = await usersClient.GetAsync("./api/users");
         response.EnsureSuccessStatusCode(); // Throw on error code. 
         var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
         List<string> user = new List<string>();

         // working on getting the trainers into a list 

         if (users != null)
         {
            foreach (User u in users.ToList())
            {
               user.Add(u.FName + " " + u.LName);
            }
         }
         return user;
      }
   }
}