using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BlockChain
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockChain bc = new BlockChain();
            List<BlockChain.Entry> entries = new List<BlockChain.Entry>();

            for (int i = 0; i < 10; ++i)
            {
                BlockChain.Entry e = new BlockChain.Entry($"Test{i}");
                entries.Add(e);
                bc.AddEntry(e);
            }

            string t = JsonConvert.SerializeObject(bc, Formatting.Indented);
            Trace.WriteLine(t);

            Tuple<bool, BlockChain.Entry> is_valid = bc.IsValid();
            Trace.WriteLine($"Is valid: {is_valid}");

            // Now tweak one of the entries and see if it's still valid.
            entries[5].Data = "Test55";

            is_valid = bc.IsValid();
            Trace.WriteLine($"Is valid: {is_valid}");
        }
    }
}
