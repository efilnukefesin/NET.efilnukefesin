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

        private ICollection<IGridItem<T>> items;
        public ISizeF Size { get; private set; }

        #endregion Properties

        #region Construction

        public Grid(int Width, int Height)
        {
            this.Size = new SizeF(Width, Height);
            this.initialize();
        }

        public Grid(ISizeF size)
        {
            this.Size = size;
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

            if (this.isPositionOccupied(x, y))
            {
                result = this.items.Where(i => i.X.Equals(x) && i.Y.Equals(y)).FirstOrDefault().Data;
            }

            return result;
        }
        #endregion getItem

        #region addOrUpdate
        private void addOrUpdate(int x, int y, T value)
        {
            if (!this.isPositionOccupied(x, y))
            {
                this.items.Add(new GridItem<T>(x, y, value));
            }
            else
            {
                this.setValue(x, y, value);
            }
        }
        #endregion addOrUpdate

        #region isPositionOccupied
        private bool isPositionOccupied(int x, int y)
        {
            return this.items.Any(i => i.X.Equals(x) && i.Y.Equals(y));
        }
        #endregion isPositionOccupied

        #region setValue
        private void setValue(int x, int y, T value)
        {
            this.items.Where(i => i.X.Equals(x) && i.Y.Equals(y)).FirstOrDefault().Data = value;
        }
        #endregion setValue

        #region Fill
        public void Fill(T value)
        {
            for (int x = 0; x < this.Size.Width; x++)
            {
                for (int y = 0; y < this.Size.Height; y++)
                {
                    this[x, y] = value;
                }
            }
        }
        #endregion Fill

        #region Clear
        public void Clear()
        {
            this.items.Clear();
        }
        #endregion Clear

        #region CopyFrom
        public void CopyFrom(IGrid<T> Source)
        {
            this.Clear();
            this.Size = Source.Size;

            for (int x = 0; x < this.Size.Width; x++)
            {
                for (int y = 0; y < this.Size.Height; y++)
                {
                    this[x, y] = Source[x, y];
                }
            }
        }
        #endregion CopyFrom

        #region Equals
        public override bool Equals(object obj)
        {
            bool result = true;

            if (obj is Grid<T>)
            {
                Grid<T> castObj = (Grid<T>)obj;

                if (this.Size.ToString().Equals(castObj.Size.ToString()))
                {
                    if (this.items.Count == castObj.items.Count)
                    {
                        this.items.Cast<IGridItem<T>>().SequenceEqual<IGridItem<T>>(castObj.items.Cast<IGridItem<T>>());
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
        #endregion Equals

        #endregion Methods

        #region Events

        #endregion Events
    }
}
