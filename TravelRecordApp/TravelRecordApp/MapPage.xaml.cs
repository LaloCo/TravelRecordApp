using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
                if(status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
                    {
                        await DisplayAlert("Need permission", "We will have to access your location for this", "Ok");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if(status == PermissionStatus.Granted)
                {
                    locationsMap.IsShowingUser = true;
                    var locator = CrossGeolocator.Current;
                    locator.PositionChanged += Locator_PositionChanged;
                    await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);

                    var position = await locator.GetPositionAsync();

                    var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
                    locationsMap.MoveToRegion(span);

                    var posts = await Post.Read();
                    DisplayInMap(posts);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("No permission", "You didn't grant permission for us to access your device's lcoation", "Ok");
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> posts)
        {
            List<Xamarin.Forms.Maps.Pin> pins = new List<Xamarin.Forms.Maps.Pin>();
            foreach (var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                { }
                catch (Exception ex)
                { }
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}