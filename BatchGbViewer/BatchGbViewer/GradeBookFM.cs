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
         gridView.ItemsSource = await GetInitialGridView2();
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private async Task<List<UserGradeBook>> GetInitialGridView2()
      {
         HttpClient client = new HttpClient();

      }




      /// <summary>
      /// This method is designed to interlace several API calls and generate the
      /// initial DataGrid view based on the results
      /// </summary>
      private async Task<List<Grade>> GetInitialGridView()
      {
         // Creates the GradeBook List variable
         List<Grade> grades = new List<Grade>();

         // Creates the initial HTTP Request to the API call to collect the Users
         HttpResponseMessage userResponse = await usersClient.GetAsync("./api/Users/");
         
         if (userResponse.IsSuccessStatusCode) // confirms that the connection was successful
         {
            try
            {
               // Establishes a variable holder for the initial content that is being pooled
               var user = await userResponse.Content.ReadAsAsync<IEnumerable<User>>();

               if (user != null) // Confirms that the pool is not empty
               {
                  foreach (User u in user)
                  {
                     // Makes a 2nd Http Request utilizing the emails of the collected Users to get each user's Gradebook
                     HttpResponseMessage gradeResponse = await usersClient.GetAsync("./api/Users/GetUserGradebook?email=" + u.email);
                     if (gradeResponse.IsSuccessStatusCode) // Confirms that the connection was successful
                     {
                        try
                        {
                           // Establishes a variable holder for the Grades that we are collecting
                           var grade = await gradeResponse.Content.ReadAsAsync<IEnumerable<Grade>>();
                           foreach (Grade g in grade)
                           {
                              grades.Add(g); // stores all of the new content into the established list variable
                           }
                        }
                        catch (Exception e) // Display error if the try fails to generate and store the list properly
                        {
                           Console.WriteLine(e);
                        }
                     }
                     else // display an error if the connection to the User/Gradebook API fails
                     {
                        Console.WriteLine("Failed to establish a connection with GetUserGradebook");
                     }
                  }
               }
               else // display an error if User Pool is Null
               {
                  Console.WriteLine("The users pool is empty. No Data to display");
               }
            }
            catch (Exception e) // Display an error if it fails to complete any of the above tasks successfully
            {
               Console.WriteLine(e);
            }
         }
         else // display an error if the connection to the User API fails
         {
            Console.WriteLine("Failed to establish a connection to API/Users");
         }

         return grades; // return the results
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
