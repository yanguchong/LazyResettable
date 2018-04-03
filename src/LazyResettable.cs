using System;

namespace LazyResettable
{
    /// <summary>
    /// This class assumes that it will be long lived.
    /// 
    /// For instance, being used inside an IOC container where 
    /// the class being instantiated is leveraging container
    /// controlled lifetime or as a static field.
    /// </summary>
    public sealed class LazyResettable<T> : IResettable
    {
        /// <summary>
        /// A template for retrieving data as needed
        /// </summary>
        private readonly Func<T> _getData;

        /// <summary>
        /// When content is expired and then refreshed, the
        /// amount of time until the next expiration
        /// </summary>
        private readonly double _expirationInMinutes;

        /// <summary>
        /// When the next expiration will happend
        /// </summary>
        private DateTime _expiration;

        /// <summary>
        /// The actual data being stored in memory
        /// </summary>
        private T _data;

        /// <summary>
        /// Just some debug info to 
        /// keep track of how many times the data has
        /// been refreshed.
        /// </summary>
        private int _refreshCount;

        private readonly object _locker = new object();


        public LazyResettable(Func<T> getData, double expirationInMinutes)
        {
            _getData = getData;
            _expirationInMinutes = expirationInMinutes;
        }

        public LazyResettable()
        {
        }

        public void Reset()
        {
            /*
             * prevent any other thread from gaining a lock
             * until the expiration has been reset
             */
            lock (_locker)
            {
                _expiration = DateTime.MinValue;
            }
        }

        /// <summary>
        /// This method is called to retrieve data
        /// </summary>
        public T GetValue()
        {
            //if data exists and is not expired, just return data
            if (DataIsOk())
            {
                return _data;
            }

            lock (_locker)
            {
                /*
                 * check one more time inside the lock, guaranteed that only one thread can be in this section of code
                 * 
                 * If a previous thread came in and refreshed the data, then any thread that was waiting for the lock
                 * should not go beyond this point
                 */
                if (DataIsOk())
                {
                    return _data;
                }

                //get data
                _data = _getData();

                //set expiration
                _expiration = DateTime.Now.AddMinutes(_expirationInMinutes);

                _refreshCount++;

#if DEBUG
                Console.WriteLine("Refresh Count: " + _refreshCount);
#endif

                //return data
                return _data;
            }
        }


        /// <summary>
        /// Checks to see if data exists and is not expired
        /// </summary>
        /// <returns></returns>
        private bool DataIsOk()
        {
            return _data != null && DateTime.Now <= _expiration;
        }
    }
}
