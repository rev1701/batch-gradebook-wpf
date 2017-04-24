using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

namespace BatchGbViewer
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      // create the client for connecting to the api
      private static HttpClient batchClient = new HttpClient();
      private static HttpClient examClient = new HttpClient();

      /// <summary>
      /// The purpose of this class is to initialize the Base Address for the HttpClients the application will
      /// be working with.
      /// </summary>
      private void initializeClients()
      {
         batchClient.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/");
         batchClient.DefaultRequestHeaders.Accept.Clear();
         batchClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         examClient.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/ExamAssessmentWebAPI/");
         examClient.DefaultRequestHeaders.Accept.Clear();
         examClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      }

      /// <summary>
      /// Main function of the entire application, this will run first.
      /// </summary>
      public MainWindow()
      {
         initializeClients(); // initialze clients
         InitializeComponent(); //begin application
      }
   }
}