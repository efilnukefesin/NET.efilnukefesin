using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NET.efilnukefesin.Extensions.Wpf.AttachedProperties.ListView
{
    internal class ListViewSelectedItemsBinder
    {
        #region Properties

        private System.Windows.Controls.ListView listView;
        private IList collection;

        #endregion Properties

        #region Construction

        public ListViewSelectedItemsBinder(System.Windows.Controls.ListView listView, IList collection)
        {
            this.listView = listView;
            this.collection = collection;

            this.listView.SelectedItems.Clear();

            foreach (var item in this.collection)
            {
                this.listView.SelectedItems.Add(item);
            }
        }
        #endregion Construction

        #region Methods

        #region Bind
        public void Bind()
        {
            this.listView.SelectionChanged += this.listView_SelectionChanged;

            if (this.collection is INotifyCollectionChanged)
            {
                var observable = (INotifyCollectionChanged)collection;
                observable.CollectionChanged += this.collection_CollectionChanged;
            }
        }
        #endregion Bind

        #region UnBind
        public void UnBind()
        {
            if (this.listView != null)
            {
                this.listView.SelectionChanged -= listView_SelectionChanged;
            }

            if (this.collection != null && this.collection is INotifyCollectionChanged)
            {
                var observable = (INotifyCollectionChanged)this.collection;
                observable.CollectionChanged -= collection_CollectionChanged;
            }
        }
        #endregion UnBind

        #region collection_CollectionChanged
        private void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems ?? new object[0])
            {
                if (!this.listView.SelectedItems.Contains(item))
                {
                    this.listView.SelectedItems.Add(item);
                }
            }
            foreach (var item in e.OldItems ?? new object[0])
            {
                this.listView.SelectedItems.Remove(item);
            }
        }
        #endregion collection_CollectionChanged

        #region listView_SelectionChanged
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems ?? new object[0])
            {
                if (!this.collection.Contains(item))
                {
                    this.collection.Add(item);
                }
            }

            foreach (var item in e.RemovedItems ?? new object[0])
            {
                this.collection.Remove(item);
            }
        }
        #endregion listView_SelectionChanged

        #endregion Methods
    }
}
