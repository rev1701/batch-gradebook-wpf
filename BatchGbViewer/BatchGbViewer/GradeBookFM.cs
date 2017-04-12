using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

      }

      /// <summary>
      /// This method is designed to pre-render the DataGrid view
      /// </summary>
      private void GetInitialGridView()
      {

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
         using (var httpClient = new HttpClient())
         {
            try
            {

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
         }
      }
   }
}
