using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Mvvm.Assets
{
    internal class DummyNavigationPresenter : BaseObject, INavigationPresenter
    {
        public bool Present(string ViewUri, object DataContext)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void RegisterPresenter(object Presenter)
        {
            //throw new NotImplementedException();
        }

        protected override void dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
