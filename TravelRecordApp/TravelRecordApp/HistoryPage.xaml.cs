﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
        HistoryVM viewModel;
        public HistoryPage()
        {
            InitializeComponent();

            viewModel = new HistoryVM();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.UpdatePosts();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((MenuItem)sender).CommandParameter;
            viewModel.DeletePost(post);

            viewModel.UpdatePosts();
        }

        private async void postListView_Refreshing(object sender, EventArgs e)
        {
            await viewModel.UpdatePosts();
            postListView.IsRefreshing = false;
        }
    }
}