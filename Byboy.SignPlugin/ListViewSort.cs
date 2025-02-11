using System.Collections;

namespace Byboy.SignPlugin
{
    public class ListViewSort : IComparer
    {
        private int col;
        private bool descK;
        private SortType sortType;
        public ListViewSort()
        {
            col = 0;
        }

        public ListViewSort(int column,object Desc,SortType sortType = SortType.String)
        {
            descK = (bool)Desc;
            col = column; //当前列,0,1,2...,参数由ListView控件的ColumnClick事件传递 
            this.sortType = sortType;
        }
        /// <summary>
        /// 比较两个对象并返回一个值指示一个对象小于、等于还是大于另一个对象。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x,object y)
        {
            int tempInt = 0;
            var s1 = ((ListViewItem)x).SubItems[col].Text;
            var s2 = ((ListViewItem)y).SubItems[col].Text;
            //if(s1 == null && s2 == null)
            //    return 0;
            //else if(s1 == null)
            //    return -1;
            //else if(s2 == null)
            //    return 1;

            switch (sortType) {
                case SortType.String:
                    tempInt = String.Compare(s1,s2);
                    break;
                case SortType.Value:
                    double d1, d2;
                    double.TryParse(s1,out d1);
                    double.TryParse(s2,out d2);
                    unchecked {
                        tempInt = (int)(d1 - d2);
                    }
                    break;
                case SortType.DateTime:
                    DateTime t1, t2;
                    if (DateTime.TryParse(s1,out t1) && DateTime.TryParse(s2,out t2))
                        tempInt = DateTime.Compare(t1,t2);
                    else
                        tempInt = String.Compare(s1,s2);
                    break;
                default:
                    break;
            }
            if (descK) return -tempInt;
            else return tempInt;
        }
    }

    public enum SortType
    {
        /// <summary>
        /// 字母排序
        /// </summary>
        String,
        /// <summary>
        /// 数字排序
        /// </summary>
        Value,
        /// <summary>
        /// 日期排序
        /// </summary>
        DateTime
    }
}
