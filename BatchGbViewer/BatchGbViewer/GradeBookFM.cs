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
      }

      /// <summary>
      /// THe purpose of this method is to gather only the Batch Names and return them as a list
      /// </summary>
      /// <returns></returns>
      private async Task<List<string>> GetBatchList()
      {
         HttpResponseMessage response = await batchClient.GetAsync("api/Batches");
         response.EnsureSuccessStatusCode(); // throw an error code if the connection is unsuccessful
         var batch = await response.Content.ReadAsAsync<IEnumerable<Batch>>(); // populate a temp variable with the Ansynchronous Data
         List<string> batches = new List<string>(); // initialize the Batch Name list

         batches.Add("Select Batch"); // create the default selection for the list box
         batches.Add("No Batch"); // create an option to include those who do not have a batch

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
         gridView.ItemsSource = GetInitialGridView(); // uses the GetInitialGridView method to collect and populate the Gradebook List
      }
      
      /// <summary>
      /// This method is used to gather the list and generate the initial Grid View for the Gradebook Tab
      /// </summary>
      /// <returns></returns>
      private List<Grade> GetInitialGridView()
      {
         List<UserGradeBook> gradebooks = new List<UserGradeBook>(); // initialize gradebook list
         List<Grade> grades = new List<Grade>(); // initialize grade list

         HttpResponseMessage response = batchClient.GetAsync("api/Users/GetAllUserGradebooks").Result;
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
                  grade.examName = gradebook.ExamSetting.ExamTemplateID; // get name assigned to exam
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
      /// This method is designed to filter the Gradebook results based on the user's search Criteria
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void FilterGradeBook_Click(object sender, RoutedEventArgs e)
      {
         // Declare all list objects
         List<Grade> gb = new List<Grade>();
         List<Grade> grades = new List<Grade>();

         gb = GetInitialGridView(); // initialize gb (gradebook) list

         if (!string.IsNullOrEmpty(AssociateFN.Text) || !string.IsNullOrEmpty(AssociateLN.Text)) // if either fname or lname have data, then filter by Name
         {
            grades = FilterGradeBookByName(gb, AssociateFN.Text, AssociateLN.Text); // populate list based on the filtered First/Last Name

            if (GB_BatchList.SelectedIndex != 0) // if Batch is also not the default index ("Select Batch"), then filter the new list by batch as well
            {
               grades = FilterGradeBookByBatch(grades, GB_BatchList.Text);
            }

            GB_DataGrid_View.ItemsSource = grades; // populate the grid view with the new results
         }
         // If no name (first/last) is specified, but a batch is, then filter by batch only
         else if (string.IsNullOrEmpty(AssociateFN.Text) && string.IsNullOrEmpty(AssociateLN.Text) && GB_BatchList.SelectedIndex != 0) 
         {
            grades = FilterGradeBookByBatch(gb, GB_BatchList.Text);
            GB_DataGrid_View.ItemsSource = grades; // populate the grid view with the new results
         }
         else // If the search button is clicked, but no name (first/last) or batch is specified, then return the Initial Grid View
         {
            GB_DataGrid_View.ItemsSource = gb; // else restore the initial grid view
         }
      }

      /// <summary>
      /// This method is designed to alter the DataGrid view based on desired filters
      /// </summary>
      /// <param name="gb"></param>
      /// <param name="fname"></param>
      /// <param name="lname"></param>
      /// <returns></returns>
      private List<Grade> FilterGradeBookByName(List<Grade> gb, string fname, string lname)
      {
         List<Grade> grades = new List<Grade>();

         if (!string.IsNullOrEmpty(fname) && string.IsNullOrEmpty(lname)) // if first name has data, but last name is null/empty
         {
            foreach (var grade in gb)
            {
               if (grade.fname == fname)
               {
                  grades.Add(grade);
               }
            }
         }
         else if (string.IsNullOrEmpty(fname) && !string.IsNullOrEmpty(lname)) // if first name is null/empty, but last name has data
         {
            foreach (var grade in gb)
            {
               if (grade.lname == lname)
               {
                  grades.Add(grade);
               }
            }
         }
         else if (!string.IsNullOrEmpty(fname) && !string.IsNullOrEmpty(lname)) // if both first name and last name have data
         {
            foreach (var grade in gb)
            {
               if (grade.fname == fname && grade.lname == lname)
               {
                  grades.Add(grade);
               }
            }
         }
         else
         {
            return gb; // if both first name and last name are empty/null return the original list
         }

         return grades; // return the newly filtered list
      }

      /// <summary>
      /// This method will filter the search based on the batch name chosen by the user
      /// </summary>
      /// <param name="gb"></param>
      /// <param name="batch"></param>
      /// <returns></returns>
      private List<Grade> FilterGradeBookByBatch(List<Grade> gb, string batch)
      {
         // Initialize the new list object
         List<Grade> grades = new List<Grade>();

         foreach (var grade in gb)
         {
            if (grade.batchName == batch)
            {
               grades.Add(grade);
            }
         }

         return grades; // return the newly filtered list
      }


      /// <summary>
      /// This method handles the generations of columns for the DataGrid view
      /// </summary>
      private void GenerateGridViewColumns()
      {
      }
   }
}
