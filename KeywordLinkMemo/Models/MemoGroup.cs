using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordLinkMemo.Models
{
    public class MemoGroup
    {
        public string Name { get; set; }

        public List<MemoGroup> MemoNames { get; set; }

        public MemoGroup(string name)
        {
            Name = name;
            MemoNames = new List<MemoGroup>();
        }
    }
}
