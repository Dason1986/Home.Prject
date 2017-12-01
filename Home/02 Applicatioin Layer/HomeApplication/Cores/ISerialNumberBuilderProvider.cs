using Library;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeApplication.Interceptors
{
    public interface ISerialNumberBuilderProvider
    {
        int Count { get; }
        string[] GetBuilderCodes();
        ISerialNumberBuilder GetBuilder(string name);

        string Dequeue(string name);

        string Dequeue(string name, string serialNumberFormat);
    }

  
    public abstract class SerialNumberBuilderProvider : ISerialNumberBuilderProvider
    {
        protected SerialNumberBuilderProvider()
        {
           
        }

        public abstract void Initialize();

        public abstract SerialNumberBuilder GetBuilder(string name);

        ISerialNumberBuilder ISerialNumberBuilderProvider.GetBuilder(string name)
        {
            return GetBuilder(name);
        }
        public abstract int Count { get; }
        public abstract string[] GetBuilderCodes();
        public abstract string Dequeue(string name);

        public abstract string Dequeue(string name, string serialNumberFormat);
    }


}