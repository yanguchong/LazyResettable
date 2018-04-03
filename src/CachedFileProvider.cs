using System.Collections.Generic;

namespace LazyResettable
{
    internal sealed class CachedFileProvider : FileProviderBase, IFileProvider
    {
        private LazyResettable<List<Data1>> _data1;
        private LazyResettable<List<Data2>> _data2;

        public CachedFileProvider()
        {
            _data1 = new LazyResettable<List<Data1>>(base.GetData1, 1);
            _data2 = new LazyResettable<List<Data2>>(base.GetData2, 1);
        }

        public override List<Data1> GetData1()
        {
            return _data1.GetValue();
        }

        public override List<Data2> GetData2()
        {
            return _data2.GetValue();
        }

        public void Reset()
        {
            _data1.Reset();
            _data2.Reset();
        }
    }
}
