using System;
using MVVMFramework.ViewModel;

namespace MVVMFramework.Droid
{
    public static class App
    {
        static ViewModelLocator locator;
        public static ViewModelLocator Locator
        {
            get { return locator ?? (locator = new ViewModelLocator()); }
        }
    }
}

