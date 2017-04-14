﻿using System;
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
      private static HttpClient usersClient = new HttpClient();
      private static HttpClient techClient = new HttpClient();

      private void initializeClients()
      {
         batchClient.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/");
         batchClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         usersClient.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/");
         usersClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         techClient.BaseAddress = new Uri("http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/");
         techClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      }


      public MainWindow()
      {

         initializeClients();
         InitializeComponent();
      }
   }
}