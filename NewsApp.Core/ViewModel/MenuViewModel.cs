using NewsApp.Core.Extensions;
using NewsApp.Core.Helpers;
using NewsApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static NewsApp.Core.Helpers.Categories;

namespace NewsApp.Core.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        
        private readonly IServices services = DependencyService.Get<IServices>();
        private NewsCategory selectedNewsCategory;
        private List<NewsCategory> _categories;
        private List<NewsViewModel> _newsCategories;
        public ICommand LoadCategory { get; private set; }
        public List<NewsCategory> Categories
        {
            get => _categories;
            set => SetValue(ref _categories, value);
        }

        public NewsCategory SelectedNewsCategory
        {
            get => selectedNewsCategory;
            set => SetValue(ref selectedNewsCategory, value);
        }
        
        public MenuViewModel()
        {
            _newsCategories = new List<NewsViewModel>();
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
                _newsCategories.Add(new NewsViewModel(category));
            }
            
            LoadCategory = new Command(LoadPage);
        }

        private void LoadPage()
        {
            if (selectedNewsCategory == null)
                return;
            //var PageViewModel = _newsCategories.Find(x => x.Title == selectedNewsCategory.CategoryName);
            //services.SetIsPresented(false);
            //services.SetDetailPage(new ArticlesPage { BindingContext = PageViewModel });
            //SelectedNewsCategory = null;
        }
    }
}
