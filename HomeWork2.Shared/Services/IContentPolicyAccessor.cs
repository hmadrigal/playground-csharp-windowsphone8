using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork2.Services
{
    public interface IContentPolicyAccessor
    {
        bool IsValid<T>(T state = default(T));
        string GetFileKey<T>(T state = default(T));
    }    
}
