using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Grid;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace NET.efilnukefesin.Implementations.Grid
{
    public class Grid<T> : IGrid<T>
    {
        #region Properties

        private ISize size;
        private ICollection<IGridItem<T>> items;

        #endregion Properties

        #region Construction

        public Grid(int Width, int Height)
        {
            this.size = new Size(Width, Height);
            this.initialize();
        }

        public Grid(ISize size)
        {
            this.size = size;
            this.initialize();
        }

        #endregion Construction

        #region Indexer

        public T this[int x, int y]    // Indexer declaration  
        {
            get
            {
                return this.getItem(x, y);
            }

            set
            {
                this.addOrUpdate(x, y, value);
            }
        }
        #endregion Indexer

        #region Methods

        #region initialize
        private void initialize()
        {
            this.items = new List<IGridItem<T>>();
        }
        #endregion initialize

        #region getItem
        private T getItem(int x, int y)
        {
            T result = default(T);

            if (this.items.Any(i => i.X.Equals(x) && i.Y.Equals(y)))
            {
                result = this.items.Where(i => i.X.Equals(x) && i.Y.Equals(y)).FirstOrDefault().Data;
            }

            return result;
        }
        #endregion getItem

        #region addOrUpdate
        private void addOrUpdate(int x, int y, T value)
        {
            T temp = this.getItem(x, y);
            if (temp == null)
            {
                this.items.Add(new GridItem<T>(x, y, value));
            }
            else
            {
                this.setValue(x, y, value);
            }
        }
        #endregion addOrUpdate

        #region setValue
        private void setValue(int x, int y, T value)
        {
            this.items.Where(i => i.X.Equals(x) && i.Y.Equals(y)).FirstOrDefault().Data = value;
        }
        #endregion setValue

        #endregion Methods

        #region Events

        #endregion Events
    }
}
