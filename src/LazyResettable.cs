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
        private readonly Func<T> _getData;
        private readonly double _expirationInMinutes;
        private DateTime _expiration;
        private T _data;
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
            lock (_locker)
            {
                _expiration = DateTime.MinValue;
            }
        }

        public T GetValue()
        {
            //if data exists and is not expired, just return data
            if (DataIsOk())
            {
                return _data;
            }

            lock (_locker)
            {
                //check one more time
                if (DataIsOk())
                {
                    return _data;
                }

                //get data
                _data = _getData();

                //set expiration
                _expiration = DateTime.Now.AddMinutes(_expirationInMinutes);

                _refreshCount++;

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
