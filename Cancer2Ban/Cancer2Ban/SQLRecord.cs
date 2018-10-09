using System.Collections.Generic;
using System.Linq;

namespace Cancer2Ban
{
    public class SQLRecord
    {
        private Dictionary<int, Dictionary<string, string>> _struct = null;

        public SQLRecord(Dictionary<int, Dictionary<string, string>> raw)
        {
            _struct = raw;
        }

        public Dictionary<int, Dictionary<string, string>> GetKeyPairs()
        {
            return _struct;
        }
        public List<string> GetColumns()
        {
            List<string> ret = new List<string>();
            foreach (string column in _struct[0].Keys)
            {
                ret.Add(column);
            }
            return ret;
        }
        public string GetValue(int index, string column)
        {
            return _struct[index][column];
        }
        public string GetKey(int index)
        {
            return _struct[index].Keys.First();
        }

        public int NumRows()
        {
            return _struct.Keys.Count();
        }
    }
}