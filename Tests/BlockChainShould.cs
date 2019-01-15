using com.alfiejohnson.blockchain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Tests
{
    public class BlockChainShould
    {
        [Fact]
        public void BuildAnEntryOK()
        {
            BlockChain bc = new BlockChain();
            Assert.Equal("0", bc.Head);
            Assert.Empty(bc.Entries);

            BlockChain.Entry e = new BlockChain.Entry("Test");
            bc.AddEntry(e);

            Assert.NotEqual("0", bc.Head);
            Assert.Single(bc.Entries);

            string t = JsonConvert.SerializeObject(bc);
            Trace.WriteLine(t);
        }

        [Fact]
        public void AddMultipleEntriesOK()
        {
            BlockChain bc = new BlockChain();

            int count = 10;
            for(int i = 0; i < count; ++i)
            {
                BlockChain.Entry e = new BlockChain.Entry(Guid.NewGuid().ToString());
                bc.AddEntry(e);
            }

            Assert.NotEqual("0", bc.Head);
            Assert.Equal(count, bc.Entries.Count);
            Tuple<bool, BlockChain.Entry> valid = bc.IsValid();
            Assert.True(valid.Item1);
            Assert.Null(valid.Item2);
        }

        [Fact]
        public void DetectTampering()
        {
            BlockChain bc = new BlockChain();
            List<BlockChain.Entry> entries = new List<BlockChain.Entry>();

            int count = 10;
            for (int i = 0; i < count; ++i)
            {
                BlockChain.Entry e = new BlockChain.Entry(Guid.NewGuid().ToString());
                entries.Add(e);
                bc.AddEntry(e);
            }

            Tuple<bool, BlockChain.Entry> valid = bc.IsValid();
            Assert.True(valid.Item1);
            Assert.Null(valid.Item2);

            entries[4].Data = Guid.NewGuid().ToString();

            valid = bc.IsValid();
            Assert.False(valid.Item1);
            Assert.NotNull(valid.Item2);
        }
    }
}
