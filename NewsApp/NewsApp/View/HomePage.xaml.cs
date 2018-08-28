using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NewsApp.Model;
using NewsApp.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using NewsApp.ViewModel;

namespace NewsApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : MasterDetailPage
	{
		public HomePage ()
		{
			InitializeComponent ();
        }
    }
}