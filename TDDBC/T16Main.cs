﻿using System.Collections.Generic;
using System.Linq;

namespace TDDBC
{
    public class T16Main
    {
        private Dictionary<string,string> _set = new Dictionary<string,string>();

        public void Put(string key,string value)
        {
            _set.Add(key, value);

        }

        public string Get(string key)
        {
            return _set[key];
        }

        public string Dump()
        {
            string result = "";
            _set.ToList().ForEach(x => { result += x.Value + "\n"; });
            return result;
        }
    }
}
