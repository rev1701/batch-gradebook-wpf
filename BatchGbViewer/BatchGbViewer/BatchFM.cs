using BatchGbViewer.Models;
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
      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnSearch_Click(object sender, RoutedEventArgs e)
      {

      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void listTrainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {

         List<string> x = new List<string>() { };
         //listBatchName.Items.Add(x);

      }

      /// <summary>
      /// This method is designed to populate the Trainer Dropdown (ComboBox) with a list of trainers
      /// pulled from the database. Trainers is one of the filters for narrowing Batch Tabular Results
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void listTrainer_Loaded(object sender, RoutedEventArgs e)
      {
         // ... Get the ComboBox reference.
         var comboBox = sender as ComboBox;

         // ... Assign the ItemsSource to the List.
         comboBox.ItemsSource = await getTrainers();

         // ... Make the first item selected.
         comboBox.SelectedIndex = 0;
      }

      /// <summary>
      /// This method assists ListTrainer_Loaded by getting the list of Trainers and returning it as
      /// a list of strings
      /// </summary>
      /// <returns></returns>
      public async Task<List<string>> getTrainers()
      {
         HttpResponseMessage response = await usersClient.GetAsync("./api/users");
         response.EnsureSuccessStatusCode(); // Throw on error code if HttpClient fails to connect.
         var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
         List<string> user = new List<string>();

         // working on getting the trainers into a list 

         if (users != null)
         {
            foreach (User u in users.ToList())
            {
               // This if statement is to ensure only users who are Trainers are collected 
               // and only valid Trainers (must have first and last name) are accepted into the list
               if (u.UserType == 3 && !string.IsNullOrEmpty(u.fname) && !string.IsNullOrEmpty(u.lname))
               {
                  user.Add(u.fname + " " + u.lname);
               }
            }
         }
         return user;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void listTechnology_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {

      }

      /// <summary>
      /// This method is designed to populate the Technology Dropdown (ComboBox) with a list of Technologies
      /// pulled from the database. Technologies is one of the filters for narrowing Batch Tabular Results
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void listTechnology_Loaded(object sender, RoutedEventArgs e)
      {
         // ... Get the ComboBox reference.
         var comboBox = sender as ComboBox;

         // ... Assign the ItemsSource to the List.
         comboBox.ItemsSource = await getTechnologies();

         // ... Make the first item selected.
         comboBox.SelectedIndex = 0;
      }

      /// <summary>
      /// This method assists listTechnology_Loaded by getting the list of Technologies and returning it as
      /// a list of strings
      /// </summary>
      /// <returns></returns>
      private async Task<List<string>> getTechnologies()
      {
         HttpResponseMessage response = await techClient.GetAsync("./api/Batches");
         response.EnsureSuccessStatusCode(); // Throw on error code.
         var tech = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
         List<string> teches = new List<string>();

         // working on getting the trainers into a list 

         if (tech != null)
         {
            foreach (Batch b in tech.ToList())
            {
               teches.Add(b.Name);
            }
         }
         return teches;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void listBatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {

      }

      /// <summary>
      /// This method is designed to populate the Batches Dropdown (ComboBox) with a list of Batches
      /// pulled from the database. Batches is one of the filters for narrowing Batch Tabular Results
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void listBatches_Loaded(object sender, RoutedEventArgs e)
      {
         // ... Get the ComboBox reference.
         var comboBox = sender as ComboBox;

         // ... Assign the ItemsSource to the List.
         comboBox.ItemsSource = await getBatches();

         // ... Make the first item selected.
         comboBox.SelectedIndex = 0;
      }

      /// <summary>
      /// This method assists listBatches_Loaded by getting the list of Batches and returning it as
      /// a list of strings
      /// </summary>
      /// <returns></returns>
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

               batch.Add(b.Name);
            }
         }
         return batch;
      }
   }
}