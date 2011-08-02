using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDBC
{
    public class T16Main
    {
        private Dictionary<string, string> _set = new Dictionary<string, string>();

        public void Put(string key, string value)
        {
            if (_set.ContainsKey(key))
            {
                _set[key] = value;
            }
            else
            {
                _set.Add(key, value);
            }
        }

        public string Get(string key)
        {
            if (!_set.ContainsKey(key)) return null;
            return _set[key];
        }

        public string Dump()
        {
            string result = "";
            _set.ToList().ForEach(x => { result += x.Value + "\n"; });
            return result;
        }

        public void Delete(string key)
        {
            _set.Remove(key);
        }

        public void Put(List<string[]> list)
        {
            var backup = new Dictionary<string, string>();
            _set.ToList().ForEach(x => backup.Add(x.Key, x.Value));

            try
            {
                list.ForEach(x => Put(x[0], x[1]));
            }
            catch (ArgumentNullException ex)
            {
                _set = backup;
                throw;
            }
        }
    }
}
