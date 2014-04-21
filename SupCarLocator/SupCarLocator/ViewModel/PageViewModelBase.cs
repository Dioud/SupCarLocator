using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace SupCarLocator.ViewModel
{
    class PageViewModelBase : BaseViewModel
    {
        protected NavigationContext NavigationContext;
        protected NavigationService NavigationService;

        public virtual void OnNavigatedTo(NavigationContext navigationContext, NavigationService navigationService)
        {
            NavigationContext = navigationContext;
            NavigationService = navigationService;
        }
        
    }
}
