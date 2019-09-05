namespace OpenInput.Touch
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// NOTE: Pages with more on touch events
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd353242(v=vs.85).aspx
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd940543(v=vs.85).aspx
    /// http://social.technet.microsoft.com/wiki/contents/articles/6460.simulating-touch-input-in-windows-8-using-touch-injection-api.aspx?PageIndex=2

    /// <summary>
    /// Provides methods and properties for accessing state information for the 
    /// touch screen of a touch-enabled device.
    /// </summary>
    public struct TouchCollection : IList<TouchLocation>, IEnumerable<TouchLocation>
    {
        private readonly TouchLocation[] collection;

        /// <summary>
        /// Initializes a new instance of <see cref="TouchCollection"/>.
        /// </summary>
        public TouchCollection(TouchLocation[] collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        /// <inheritdoc />
        public TouchLocation this[int index]
        {
            get => this.collection[index];
            set => throw new NotSupportedException();
        }

        /// <inheritdoc />
        public int Count => this.collection.Length;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        #region IList
        /// <inheritdoc />
        public bool Contains(TouchLocation item)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                if (collection[i] == item)
                    return true;
            }
            return false;
        }

        /// <inheritdoc />
        public void CopyTo(TouchLocation[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public int IndexOf(TouchLocation item)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                if (collection[i] == item)
                    return i;
            }
            return -1;
        }

        void ICollection<TouchLocation>.Add(TouchLocation item)
        {
            throw new NotSupportedException();
        }

        void ICollection<TouchLocation>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<TouchLocation>.Remove(TouchLocation item)
        {
            throw new NotSupportedException();
        }

        void IList<TouchLocation>.Insert(int index, TouchLocation item)
        {
            throw new NotSupportedException();
        }

        void IList<TouchLocation>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IEnumerable
        /// <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <inheritdoc />
        IEnumerator<TouchLocation> IEnumerable<TouchLocation>.GetEnumerator()
        {
            return new Enumerator(this);
        }
        #endregion

        class Enumerator : IEnumerator<TouchLocation>
        {
            public TouchLocation Current => collection[position];
            object IEnumerator.Current => Current;

            private readonly TouchCollection collection;
            private int position;

            public Enumerator(TouchCollection collection)
            {
                this.collection = collection;
                this.position = -1;
            }

            public bool MoveNext()
            {
                this.position++;
                return (this.position < this.collection.Count);
            }

            public void Reset()
            {
                this.position = -1;
            }

            public void Dispose()
            {

            }
        }
    }
}
