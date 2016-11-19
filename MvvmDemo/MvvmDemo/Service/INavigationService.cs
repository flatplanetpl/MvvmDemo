using System;
using Xamarin.Forms;

namespace MvvmDemo.Service
{
    public interface INavigationService
    {
        void Go<T>();

        void Back();
    }

    public class NavigationService : INavigationService
    {
        INavigation Navigation => Application.Current.MainPage.Navigation;

        public void Go<T>()
        {
            var page = GetView<T>();
            Navigation.PushAsync(page);
        }

        public void Back()
        {
            Navigation.PopAsync(true);
        }

        private static ContentPage GetView<T>()
        {
            Type viewmodelType = typeof(T);
            var className = viewmodelType.Name;
            var viewmodelStartIndex = viewmodelType.Namespace.IndexOf(".ViewModel", StringComparison.Ordinal);
            var namespaceBeforeViewPart = viewmodelType.Namespace.Substring(0, viewmodelStartIndex);
            var viewClassName = className.Replace("ViewModel", "");
            var viewFullClassName = $"{namespaceBeforeViewPart}.View.{viewClassName}";
            var type = Type.GetType(viewFullClassName);
            try
            {
                var page = Activator.CreateInstance(type) as ContentPage;
                return page;
            }
            catch
            {
                throw;
            }
        }
    }
}