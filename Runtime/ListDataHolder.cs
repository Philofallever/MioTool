using System;
using System.Collections.Generic;
using Debug = System.Diagnostics.Debug;

namespace MioTool.List
{
    public sealed class ListDataHolder<T>
    {
        private IList<T> _list;

        public event Action<int> InsertItem;
        public event Action<int> RemoveItem;
        public event Action<int> UpdateItem;

        public ListDataHolder()
        {
            _list = new List<T>();
        }

        public ListDataHolder(IList<T> list)
        {
            _list = list;
        }

        public T this[int index] => _list[index];

        #region necessary

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            Debug.Assert(InsertItem != null, "InsertItem != null");
            InsertItem?.Invoke(index);
        }

        public void Remove(int index)
        {
            RemoveItem?.Invoke(index);
            _list.RemoveAt(index);
        }

        public void Update(int index, T item)
        {
            _list[index] = item;
            UpdateItem?.Invoke(index);
        }

        #endregion

        private void AssetNull(T data)
        {
            Debug.Assert(data != null, "data cann't be null!");
        }

    }
}