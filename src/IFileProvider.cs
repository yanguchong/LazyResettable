using System.Collections.Generic;

namespace LazyResettable
{
    internal interface IFileProvider : IResettable
    {
        List<Data1> GetData1();
        List<Data2> GetData2();
    }
}
