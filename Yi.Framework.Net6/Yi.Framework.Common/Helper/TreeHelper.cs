using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;

namespace Yi.Framework.Common.Helper
{
    public static class TreeHelper
    {
        public static IList<T> SetTree<T>(IList<T> list, Action<T> action = null)
        {
            if (list != null && list.Count > 0)
            {
                IList<T> result = new List<T>();
                long pid = list.Min(m => (m as ITreeModel<T>).parentId);
                IList<T> t = list.Where(m => (m as ITreeModel<T>).parentId == pid).ToList();
                foreach (T model in t)
                {
                    if (action != null)
                    {
                        action(model);
                    }
                    result.Add(model);
                    var item = (model as ITreeModel<T>);
                    IList<T> children = list.Where(m => (m as ITreeModel<T>).parentId == item.id).ToList();
                    if (children.Count > 0)
                    {
                        SetTreeChildren(list, children, model, action);
                    }
                }
                return result.OrderBy(m => (m as ITreeModel<T>).sort).ToList();
            }
            return null;
        }
        private static void SetTreeChildren<T>(IList<T> list, IList<T> children, T model, Action<T> action = null)
        {
            var mm = (model as ITreeModel<T>);
            mm.children = new List<T>();
            foreach (T item in children)
            {
                if (action != null)
                {
                    action(item);
                }
                mm.children.Add(item);
                var _item = (item as ITreeModel<T>);
                IList<T> _children = list.Where(m => (m as ITreeModel<T>).parentId == _item.id).ToList();
                if (_children.Count > 0)
                {
                    SetTreeChildren(list, _children, item, action);
                }
            }
            mm.children = mm.children.OrderBy(m => (m as ITreeModel<T>).sort).ToList();
        }
    }
}
