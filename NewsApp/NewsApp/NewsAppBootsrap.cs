﻿using NewsApp.Core;
using NewsApp.Core.Factories;
using NewsApp.Core.Services;
using NewsApp.Core.ViewModel;
using NewsApp.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp
{
    public class NewsAppBootsrap : Bootstrapper
    {
        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<MenuViewModel, MenuView>();
            viewFactory.Register<ArticlesViewModel, ArticlesView>();
        }
    }
}
