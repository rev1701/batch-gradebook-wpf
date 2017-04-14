using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BatchGbViewer
{
   /// <summary>
   /// This partial class is designed to handle everything involved with the GradeBook Tab
   /// </summary>
   public partial class MainWindow : Window
   {

      private static HttpClient users = new HttpClient();
      private static HttpClient exams = new HttpClient();

      /// <summary>
      /// This method will handle the events that occure when the user changes the listbox selection
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void GB_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         List<string> x = new List<string>() { };
         //listBatchName.Items.Add(x);
      }


      /// <summary>
      /// The purpose of this method is to get all batches and their content as a List Object.
      /// 
      /// </summary>
      /// <returns></returns>
      private async Task<List<string>> GetBatchList()
      {
         HttpResponseMessage response = await batchClient.GetAsync("./api/Batches");
         response.EnsureSuccessStatusCode(); // throw an error code
         var batch = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
         List<string> batches = new List<string>();

         if (batch != null)
         {
            foreach (Batch b in batch)
            {
               batches.Add(b.Name);
            }
         }

         return batches;
      }


      /// <summary>
      /// This method will handle how the drop down filter is populated
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void GB_listBox_Loaded(object sender, RoutedEventArgs e)
      {
         var comboBox = sender as ComboBox;
         comboBox.ItemsSource = await GetBatchList();
         comboBox.SelectedIndex = 0;
      }

      /// <summary>
      /// This method is called by the Grade Book DataGrid (GB_DataGrid_View) and initializes
      /// the tabular data that the user will see upon opening the tab
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void GB_DataGrid_View_Loaded(object sender, RoutedEventArgs e)
      {
         var gridView = sender as DataGrid;
         gridView.ItemsSource = await GetInitialGridView();
      }

      /// <summary>
      /// This method is designed to interlace several API calls and generate the
      /// initial DataGrid view based on the results
      /// </summary>
      private async Task<List<Batch>> GetInitialGridView()
      {
         // The following gathers all the batches into a list object
         HttpResponseMessage batchResponse = await batchClient.GetAsync("./api/Batches");
         batchResponse.EnsureSuccessStatusCode(); // throw an error code if connection fails
         var batch = await batchResponse.Content.ReadAsAsync<IEnumerable<Batch>>();
         List<Batch> batches = new List<Batch>();

         // this if statment will generate the batch list
         if (batch != null)
         {
            foreach (Batch b in batch)
            {
               batches.Add(b);
            }
         }

         // The following gathers all the users into a list object
         HttpResponseMessage userResponse = await usersClient.GetAsync("./api/Users");
         userResponse.EnsureSuccessStatusCode(); //throw an error if connection fails
         var user = await userResponse.Content.ReadAsAsync<IEnumerable<User>>();
         List<User> users = new List<User>();

         // this if statment will generate the user list
         if (user != null)
         {
            foreach (User u in user)
            {
               users.Add(u);
            }
         }

         // The following gathers all the exams into a list object
         HttpResponseMessage examResponse = await examClient.GetAsync("./api/exam");
         return batches;
      }

      /// <summary>
      /// This method is designed to alter the DataGrid view based on desired filters
      /// </summary>
      private void FilterGridViewResults()
      {

      }



      /// <summary>
      /// This method handles the generations of columns for the DataGrid view
      /// </summary>
      private void GenerateGridViewColumns()
      {

      }

      /// <summary>
      /// This method is a test method designed to learn how to consume RESTful services within XAML
      /// 
      /// API for Assessments: http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/Help/Api/GET-api-ExamAssessments-GetExamAssessments
      /// API for Batches: http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/Help/Api/GET-api-Batches-GetBatches
      /// API for Users: http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/Help/Api/GET-api-Users-GetUsers
      /// </summary>
      /// <param name="uri"></param>
      private void GetRESTData(string uri)
      {
        
      }
   }
}
