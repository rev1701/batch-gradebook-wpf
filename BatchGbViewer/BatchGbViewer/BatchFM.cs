﻿using BatchGbViewer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BatchGbViewer
{
    public partial class MainWindow : Window
    {
        #region Batch

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
            //Batch x = new Batch() { Name = "Select Batch" };
            //batch.Add(x.Name);

            //batch.Add("Select Batch"); // create the default selection for the list box
            //batch.Add("No Batch"); // create an option to include those who do not have a batch

            if (batches != null)
            {
                foreach (Batch b in batches.ToList())
                {                   
                    batch.Add(b.Name);
                }
            }
            return batch;
        }

        /// <summary>
        /// set the value of batch name to the one selected for searches
        /// </summary>
        /// <returns></returns>
        public async Task<List<Batch>> setBatches()
        {
            HttpResponseMessage response = await batchClient.GetAsync("./api/batches");
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var batches = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
            List<Batch> batch = new List<Batch>();

            if (batches != null)
            {
                foreach (Batch b in batches.ToList())
                {
                    batch.Add(b);
                }
            }
            return batch;
        }

        #endregion

        #region trainers

        /// <summary>
        /// This method assists ListTrainer_Loaded by getting the list of Trainers and returning it as
        /// a list of strings
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> getTrainers()
        {
            HttpResponseMessage response = await batchClient.GetAsync("./api/users");
            response.EnsureSuccessStatusCode(); // Throw on error code if HttpClient fails to connect.
            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            List<string> user = new List<string>();
            //User x = new User() { ID = 0, FName = "Select", LName = "Trainer", UserType = "3" };

            //user.Add(x.FName + " " + x.LName);
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
        /// set the value of the selected trainer as a filter for searches
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> setTrainers()
        {
            HttpResponseMessage response = await batchClient.GetAsync("./api/users");
            response.EnsureSuccessStatusCode(); // Throw on error code if HttpClient fails to connect.
            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            List<User> user = new List<User>();

            // working on getting the trainers into a list 

            if (users != null)
            {
                foreach (User u in users.ToList())
                {
                    // This if statement is to ensure only users who are Trainers are collected 
                    // and only valid Trainers (must have first and last name) are accepted into the list
                    if (u.UserType == 3 && !string.IsNullOrEmpty(u.fname) && !string.IsNullOrEmpty(u.lname))
                    {
                        user.Add(u);
                    }
                }
            }
            return user;
        }
        #endregion

        #region technology

        /// <summary>
        /// This method assists listTechnology_Loaded by getting the list of Technologies and returning it as
        /// a list of strings
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> getTechnologies()
        {
            HttpResponseMessage response = await batchClient.GetAsync("./api/Batches");
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var tech = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
            List<string> techs = new List<string>();

            // working on getting the trainers into a list 
            //Batch x = new Batch() { BatchID = "Select Technology" };
            //techs.Add(x.BatchID);
            if (tech != null)
            {
                foreach (Batch b in tech.ToList())
                {
                    var c = new Batch();
                    c.BatchID = b.BatchID;
                    if (c.BatchID == "WeTheBest")
                    {
                        c.Technology = ".NET";
                    }
                    else if (c.BatchID == "LetItBurn")
                    {
                        c.Technology = "Java";
                    }
                    else
                    {
                        c.Technology = "SDET";
                    }

                    techs.Add(c.Technology);
                }
            }
            return techs;
        }

        /// <summary>
        /// set the value of technology to the one selected for searches
        /// </summary>
        /// <returns></returns>
        private async Task<List<Batch>> setTechnologies()
        {
            HttpResponseMessage response = await batchClient.GetAsync("./api/Batches");
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var tech = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
            List<Batch> techs = new List<Batch>();

            // working on getting the trainers into a list 

            if (tech != null)
            {
                foreach (Batch b in tech.ToList())
                {
                    var c = new Batch();
                    c.BatchID = b.BatchID;
                    if (c.BatchID == "WeTheBest")
                    {
                        c.Technology = ".NET";
                    }
                    else if (c.BatchID == "LetItBurn")
                    {
                        c.Technology = "Java";
                    }
                    else
                    {
                        c.Technology = "SDET";
                    }

                    techs.Add(c);
                }
            }
            return techs;
        }

        #endregion

        #region start up view
        /// <summary>
        /// Load batch data prior to filtering.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Batchlistview_Loaded(object sender, RoutedEventArgs e)
        {
            // retrieve the batch info from the api
            HttpResponseMessage response = batchClient.GetAsync("api/Batches").Result;
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var batches = await response.Content.ReadAsAsync<IEnumerable<Batch>>();
            // retrieve the user info from the api
            HttpResponseMessage response2 = batchClient.GetAsync("api/Users").Result;
            response2.EnsureSuccessStatusCode(); // Throw on error code. 
            var users = await response2.Content.ReadAsAsync<IEnumerable<User>>();
            // list for the batch view model
            List<BatchVM> batch = new List<BatchVM>();
            // ensure that neither list is empty
            if (batches != null && users != null)
            {                // create the roster list
                List<Roster> r = new List<Roster>();
                // map the batch and trainer info
                foreach (Batch b in batches.ToList())
                {                    // temporay batch vm for the population of the page
                    var x = new BatchVM();
                    // sift through the users for only trainers
                    foreach (var t in users)
                    {                        // get the roster of users associated with a batch
                        for (int i = 0; i < batches.Count(); i++)
                        {                            // grab only the trainers
                            r = b.Rosters.Where(pp => pp.User.UserType == 3).ToList();
                        }                        // go through the roster and map the trainers to their batches
                        foreach (var s in r)
                        {// for now the technology is being associated with the batch id which is a string
                            x.Technology = b.BatchID;
                            if (x.Technology == "WeTheBest")
                            {
                                x.Technology = ".NET";
                            }
                            else if (x.Technology == "LetItBurn")
                            {
                                x.Technology = "Java";
                            }
                            else
                            {
                                x.Technology = "SDET";
                            }
                            x.Name = b.Name;
                            x.FromDate = b.StartDate;
                            x.ToDate = b.StartDate.Value.AddDays(69);
                            x.Trainer = s.User.fname + " " + s.User.lname;
                        }
                    }
                    batch.Add(x);
                }
            }
            Batchlistview.ItemsSource = batch.ToList();
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
        #endregion

        #region search
        /// <summary>
        /// the button to use the selections for search filtering
        /// in this we map the models to a viewmodel which is used to display
        /// the results in the listview
        /// STILL NEED TO GET THIS WORKING CORRECTLY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // retrieve the batch info from the api
            HttpResponseMessage response = batchClient.GetAsync("api/Batches").Result;
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var batches = await response.Content.ReadAsAsync<IEnumerable<Batch>>();

            // retrieve the user infor from the api
            HttpResponseMessage response2 = batchClient.GetAsync("api/Users").Result;
            response2.EnsureSuccessStatusCode(); // Throw on error code. 
            var users = await response2.Content.ReadAsAsync<IEnumerable<User>>();

            List<BatchVM> batch = new List<BatchVM>();

            //pretty self explanitory 
            BatchMapper m = new BatchMapper();

            List<BatchVM> x = new List<BatchVM>();


            try
            {
                //check for valid inputs
                if ((listBatches.SelectedItem.ToString() != null) || (listTrainer.SelectedItem.ToString() != null) || (FromDatePicker != null && ToDatePicker != null) || (listTechnology.SelectedItem.ToString() != null))
                {
                    //create our empty lists
                    List<Batch> bat = new List<Batch>();
                    List<User> us = new List<User>();
                    //assign values of inputs to variables
                    string b = listBatches.SelectedItem.ToString();
                    string tr = listTrainer.SelectedItem.ToString();
                    string t = listTechnology.SelectedItem.ToString();
                    DateTime fd = FromDate;
                    DateTime td = ToDate;
                    //get data from the database
                    bat = await setBatches();
                    us = await setTrainers();

                    //here there to be some better logic
                    {
                        // currently only pulling one thing out and putting it into the listview
                        // and it only matches date if the specified date is in the database,
                        // does not do a range of dates and for the batch, trainer and tech
                        // the listview only shows what was selected in the drop downs
                        // I have not found a good way to have a default value such as
                        // "Select a type" in the combo boxes without getting null errors
                        // the only thing happening is getting first or default based on 
                        // the value of the combo boxes when the button is pressed.
                        // need to return a list


                        var c = bat.Where(n => n.Name == b).FirstOrDefault();
                        var k = us.Where(z => z.fname + " " + z.lname == tr).FirstOrDefault();

                        x.Add(m.MapToBatch(c, k));
                        Batchlistview.ItemsSource = x;
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("ran into a null");
            }

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


        #endregion

        #region date

        /// <summary>
        /// the start of the date range
        /// </summary>
        private DateTime _fromDate;
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;

            }
        }

        /// <summary>
        /// the end of the date range
        /// </summary>
        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;

            }
        }

        /// <summary>
        /// To Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
                ToDate = DateTime.Parse(date.Value.ToShortDateString());
            }
        }


        ///// <summary>
        ///// From Date
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void FromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
                FromDate = DateTime.Parse(date.Value.ToShortDateString());
            }
        }
        #endregion
    }
    #region converter
    /// <summary>
    /// People are saying to use a converter to set a default option to combo boxes
    /// so far I haven't gotten it to work. lolololol
    /// </summary>
    public class ComboBoxNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return -1;
            return value;
        }
    }
    #endregion
}