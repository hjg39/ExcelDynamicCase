using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDynamicCase
{
    interface ILevel<T> : ILevel
    {
        void NextLevel(T input);
    }

    public interface ILevel
    {
        string LevelName { get; }

        int BaseDeadline { get; }
    }
}
