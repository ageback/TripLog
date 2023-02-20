using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;


namespace TripLog.Services
{
    public class XamarinFormsNavService : INavService
    {
        readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        public INavigation XamarinFormsNav { get; set; }
        public bool CanGoBack => XamarinFormsNav.NavigationStack != null && XamarinFormsNav.NavigationStack.Any();

        public event PropertyChangedEventHandler CanGoBackChanged;
        public void RegisterViewMapping(Type viewModel, Type view) => _map.Add(viewModel, view);

        public void ClearBackStack()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2) return;
            for (var i = 0; i < XamarinFormsNav.NavigationStack.Count - 1; i++)
            {
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
            }
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await XamarinFormsNav.PopAsync(true);
                OnCanGoBackChanged();
            }
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));
            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel)
            {
                ((BaseViewModel)XamarinFormsNav.NavigationStack.Last().BindingContext).Init();
            }
        }


        public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));
            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel<TParameter>)
            {
                ((BaseViewModel<TParameter>)XamarinFormsNav.NavigationStack.Last().BindingContext).Init(parameter);
            }
        }

        public void NavigateToUri(Uri uri)
        {
            if (uri == null) throw new ArgumentException("Invalid URL");
            Device.OpenUri(uri);
        }

        public void RemoveLastView()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2) return;
            var lastView = XamarinFormsNav.NavigationStack[XamarinFormsNav.NavigationStack.Count - 2];

            XamarinFormsNav.RemovePage(lastView);
        }

        private async Task NavigateToView(Type viewModelType)
        {
            if (!_map.TryGetValue(viewModelType, out Type viewType))
            {
                throw new ArgumentException($"No view found in view mapping for {viewModelType.FullName}.");
            }

            var constructor = viewType.GetTypeInfo().DeclaredConstructors
                .FirstOrDefault(dc => !dc.GetParameters().Any());
            var view = constructor?.Invoke(null) as Page;
            var vm = ((App)Application.Current).Kernel.GetService(viewModelType);
            view.BindingContext = vm;
            await XamarinFormsNav.PushAsync(view, true);
        }

        void OnCanGoBackChanged() => CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
    }
}