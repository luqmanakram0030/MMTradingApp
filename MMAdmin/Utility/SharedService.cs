﻿using MMAdmin.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Utility
{
    public class SharedService : ISharedService
    {
        private Dictionary<string, object> DTODict { get; set; } = new Dictionary<string, object>();
        public void Add<T>(string key, T value) where T : class
        {
            if (DTODict.ContainsKey(key))
            {
                DTODict[key] = value;
            }
            else
            {
                DTODict.Add(key, value);
            }
        }
        public T GetValue<T>(string key) where T : class
        {
            if (DTODict.ContainsKey(key))
            {
                return DTODict[key] as T;
            }
            return null;
        }
    }
}
