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
         List<Grade> grades = new List<Grade>(); // initialize list

         if (!string.IsNullOrEmpty(fname) || !string.IsNullOrEmpty(lname)) // confirm that either first name or last name contain data
         {
            char[] searchByFirst = fname.ToLower().ToArray(); // append fname to array and remove case sensitivity
            char[] searchByLast = lname.ToLower().ToArray(); // append lname to array and remove case sensitivity

            foreach (var grade in gb)
            {
               char[] testAgainstFirst = grade.fname.ToLower().ToArray(); // append grade.fname to array and remove case sensitivity
               char[] testAgainstLast = grade.lname.ToLower().ToArray(); // append grade.lname to array and remove case sensitivity
               int f = 0; // initialize f -- used for first name comparison
               int l = 0; // initialize l -- used for last name comparison

               if (searchByFirst != null && searchByLast == null) // search by first name only
               {
                  f = compareNames(searchByFirst, testAgainstFirst);
                  if (f == fname.Length)
                  {
                     grades.Add(grade); // add match to results
                  }
               }
               else if (searchByFirst != null && searchByLast != null) // search by first and last
               {
                  f = compareNames(searchByFirst, testAgainstFirst);
                  l = compareNames(searchByLast, testAgainstLast);

                  if (f == fname.Length && l == lname.Length)
                  {
                     grades.Add(grade); // add match to results
                  }
               }
               else if (searchByFirst == null && searchByLast != null) // search by last name only
               {
                  l = compareNames(searchByLast, testAgainstLast);

                  if (l == lname.Length)
                  {
                     grades.Add(grade); // add match to results
                  }
               }
            }
         }
         else
         {
            return gb;
         }

         return grades;
      }

      /// <summary>
      /// This is a factory method designed specifically for testing the characters in two given names
      /// which are put inside of an array so that each letter can be tested individually.
      /// This is designed with partial search in mind
      /// 
      /// Later implementation will be to test an accidental discovery that might lead to only comparing the strings
      /// without the need of an array
      /// </summary>
      /// <param name="search"></param>
      /// <param name="against"></param>
      /// <returns></returns>
      private int compareNames(char[] search, char[] against)
      {
         int i = 0; // initialize comparison variable

         while (i < search.Length)
         {
            if (search[i] == against[i])
            {
               i++;
            }
            else
            {
               break;
            }
         }
         return i;
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
