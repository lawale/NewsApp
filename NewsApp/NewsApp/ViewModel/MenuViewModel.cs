using NewsApp.Extensions;
using NewsApp.Helpers;
using NewsApp.Model;
using NewsApp.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static NewsApp.Helpers.Categories;

namespace NewsApp.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {

        private readonly IServices services;
        private NewsCategory selectedNewsCategory;
        private readonly List<NewsCategory> _categories;
        private readonly List<NewsViewModel> _newsCategories;
        public ICommand LoadCategory { get; private set; }
        public List<NewsCategory> Categories { get { return _categories; } }

        public NewsCategory SelectedNewsCategory
        {
            get => selectedNewsCategory;
            set { selectedNewsCategory = value; }
        }
        
        public MenuViewModel(IServices service)
        {
            _newsCategories = new List<NewsViewModel>();
            services = service;
            _categories = new List<NewsCategory>
            {
                new NewsCategory { CategoryName = topstories.ToUpper(), CategoryImage = null},
                new NewsCategory { CategoryName = business.ToUpper(), CategoryImage = null},
                new NewsCategory { CategoryName = entertainment.ToUpper(), CategoryImage = null},
                new NewsCategory { CategoryName = health.ToUpper(), CategoryImage = null},
                new NewsCategory { CategoryName = science.ToUpper(), CategoryImage = null},
                new NewsCategory { CategoryName = sports.ToUpper(), CategoryImage = null},
                new NewsCategory { CategoryName = technology.ToUpper(), CategoryImage = null}
            };

            foreach (var category in _categories)
            {
                _newsCategories.Add(new NewsViewModel(category, services));
            }
            
            LoadCategory = new Command(LoadPage);
        }

        private void LoadPage()
        {
            if (selectedNewsCategory == null)
                return;
            var PageViewModel = _newsCategories.Find(x => x.category == selectedNewsCategory);
            services.SetIsPresented(false);
            services.SetDetailPage(new ArticlesPage { BindingContext = PageViewModel });
            SetValue(ref selectedNewsCategory, null);
            OnPropertyChanged(nameof(SelectedNewsCategory));
        }
    }
}
