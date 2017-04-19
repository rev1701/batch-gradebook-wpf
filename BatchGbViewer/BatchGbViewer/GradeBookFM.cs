using BatchGbViewer.Models;
using Newtonsoft.Json;
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
      /// THe purpose of this method is to gather only the Batch Names and return them as a list
      /// </summary>
      /// <returns></returns>
      private async Task<List<string>> GetBatchList()
      {
         HttpResponseMessage response = await client.GetAsync("api/Batches");
         response.EnsureSuccessStatusCode(); // throw an error code if the connection is unsuccessful
         var batch = await response.Content.ReadAsAsync<IEnumerable<Batch>>(); // populate a temp variable with the Ansynchronous Data
         List<string> batches = new List<string>(); // initialize the Batch Name list

         if (batch != null)
         {
            foreach (Batch b in batch)
            {
               batches.Add(b.Name); // store only the Batch Name in the list
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
      private void GB_DataGrid_View_Loaded(object sender, RoutedEventArgs e)
      {
         var gridView = sender as DataGrid;
         gridView.ItemsSource = GetInitialGridView2();
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private List<Grade> GetInitialGridView2()
      {
         List<UserGradeBook> gradebooks = new List<UserGradeBook>(); // initialize gradebook list
         List<Grade> grades = new List<Grade>(); // initialize grade list

         HttpResponseMessage response = client.GetAsync("api/Users/GetAllUserGradebooks").Result;
         if (response.IsSuccessStatusCode) // confirm successful HTTP Request
         {
            var obj = response.Content.ReadAsStringAsync().Result; 
            gradebooks = JsonConvert.DeserializeObject<List<UserGradeBook>>(obj);
            Batch batch = new Batch();

            foreach (var user in gradebooks)
            {
               foreach (var gradebook in user.gradebook)
               {
                  var grade = new Grade(); // create a temp object to act as a mapping helper to pass data between objects

                  // populate the temp object with the appropriate components
                  grade.fname = user.user.fname;
                  grade.lname = user.user.lname;
                  grade.email = user.user.email;
                  grade.batchName = user.Batches[user.gradebook.IndexOf(gradebook)]; // assigns the string associated with the presented Index to grade.batchName
                  grade.examID = gradebook.ExamSetting.ExamSettingsID;
                  grade.technology = null;
                  grade.Score = gradebook.Score;

                  // add the temp object to the List Object
                  grades.Add(grade);
               }
            }
         }

         return grades; // return list object
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
   }
}
