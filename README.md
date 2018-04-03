# LazyResettable
Simple class to enable in lazy memory caching of consistently requested data.  Has two simple features, cache timeout
and forced expiration.

## Tools
* Visual Studio 2017

##Start
* Clone this repository https://github.com/yanguchong/LazyResettable.git
* Open LazyResettable.sln
* Build, make sure that nuget package restore is enabled
* Run

## Overview
*FileProviderBase* is basically your data getter class.  This is where raw data is pulled in from a data source.
I chose to pull from files to make this solution self contained, but a database would be a prime example of what an
external data source is.

*CachedFileProvider* inherits from *FileProviderBase* and overrides the get methods to provide it's own functionality.
We are enhancing the raw get functions of *FileProviderBase* with caching functionality provided by *CachedFileProvider*.

The way we accomplish this is by leveraging *LazyResettable*.


## How to Implement Into Your Own Projects
Let's assume that we have a data class *FileProviderBase*.  We want to give our data class caching functionality so
that we lighten the load on our data source.  While there are other options such as Redis and Memcache, this is a simple
way to extend the capabilities of your web server with very little work.


First we create a class called *CachedFileProvider* which inherits from *FileProviderBase*, we also create an interface
called *IFileProvider* for our public methods that we would like to expose.
```
internal sealed class CachedFileProvider : FileProviderBase, IFileProvider, IResettable
```

Next we initialize *LazyResettable* inside the constructor.  *FileProviderBase* provides the raw methods to retrieve data.

*LazyResettable takes two parameters in the constructor*

1. `Func<T>` where T is the type of data returned from the raw getters in *FileProviderBase*
2. When you would like to expire data in minutes

```
private LazyResettable<List<Data1>> _data1;
private LazyResettable<List<Data2>> _data2;


public CachedFileProvider()
{
    _data1 = new LazyResettable<List<Data1>>(base.GetData1, 1);
    _data2 = new LazyResettable<List<Data2>>(base.GetData2, 1);
}
```

We then override the *FileProviderBase* implementations 

```
public override List<Data1> GetData1()
{
    return _data1.GetValue();
}

public override List<Data2> GetData2()
{
    return _data2.GetValue();
}
```

Finally we implement *IResettable*

```
public void Reset()
{
    _data1.Reset();
    _data2.Reset();
}
```

####Important:
This is meant to be used in either a static class as a static variable or an IOC container with container
lifetime (container only creates one instance of an object and serves the same object).



