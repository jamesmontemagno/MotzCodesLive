using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLTest.Database;
using System.Collections.ObjectModel;
using System.Linq;

namespace SQLTest
{
    public partial class DatabasePage : ContentPage
    {

        ToDoDatabase database;
        public ObservableCollection<ToDoItem> Items { get; set; }
        public DatabasePage()
        {
            InitializeComponent();
            database = new ToDoDatabase();

            Items = new ObservableCollection<ToDoItem>();

            ItemList.ItemsSource = Items;

            SaveItemButton.Clicked += (sender, e) => 
                {
                    if(string.IsNullOrWhiteSpace(Item.Text))
                        return;

                    database.SaveItem(new ToDoItem{
                        Name = Item.Text
                    });

                    RefreshList();
                    Item.Text = string.Empty;
                };

            RefreshList();
        }



        private void RefreshList()
        {
            Items.Clear();

            var items = (from i in database.GetItems<ToDoItem>()
                        orderby i.Created 
                                  select i);

            foreach (var item in items)
                Items.Add(item);
            

        }
    }
}

