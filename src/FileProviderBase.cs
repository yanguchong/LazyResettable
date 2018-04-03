using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LazyResettable
{
    abstract internal class FileProviderBase
    {
        public virtual List<Data1> GetData1()
        {
            using (var reader = File.OpenText("data1.json"))
            {
                try
                {
                    var temp = reader.ReadToEnd();

                    var data1 = JsonConvert.DeserializeObject<List<Data1>>(temp);

                    return data1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
#if DEBUG
                    Debugger.Break();
#endif
                    throw;
                }
            }           
        }

        public virtual List<Data2> GetData2()
        {
            using (var reader = File.OpenText("data1.json"))
            {
                try
                {
                    var temp = reader.ReadToEnd();

                    var data2 = JsonConvert.DeserializeObject<List<Data2>>(temp);

                    return data2;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

#if DEBUG
                    Debugger.Break();
#endif


                    throw;
                }
            }
        }

    }
}
