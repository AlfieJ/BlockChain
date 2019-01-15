using Newtonsoft.Json;
using System.Diagnostics;
using Xunit;

namespace Tests
{
    public class BlockChainShould
    {
        [Fact]
        public void BuildAnEntryOK()
        {
            BlockChain.BlockChain bc = new BlockChain.BlockChain();
            Assert.Equal("0", bc.Head);
            Assert.Empty(bc.Entries);

            BlockChain.BlockChain.Entry e = new BlockChain.BlockChain.Entry("Test");
            bc.AddEntry(e);

            Assert.NotEqual("0", bc.Head);
            Assert.Single(bc.Entries);

            string t = JsonConvert.SerializeObject(bc);
            Trace.WriteLine(t);
        }
    }
}
