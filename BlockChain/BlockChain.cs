using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain
{
    public class BlockChain
    {
        public class Entry
        {
            public string Next { get; set; }
            public string Timestamp { get; private set; }
            public string Data { get; set; }

            public Entry(string data)
            {
                Data = data;
                Timestamp = DateTimeOffset.Now.ToString("o");
            }

            public string CalculateHash()
            {
                byte[] bytes = Encoding.UTF8.GetBytes(ToString());
                SHA256Managed sha256 = new SHA256Managed();
                byte[] hash = sha256.ComputeHash(bytes);

                return BitConverter.ToString(hash).Replace("-", "");
            }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
        }

        public Dictionary<string, Entry> Entries { get; private set; }
        public string Head { get; private set; }

        public BlockChain()
        {
            Entries = new Dictionary<string, Entry>();
            Head = "0";
        }

        public void AddEntry(Entry entry)
        {
            entry.Next = Head;
            Head = entry.CalculateHash();
            Entries[Head] = entry;
        }

        public Tuple<bool, Entry> IsValid()
        {
            bool is_valid = true;
            Entry bad_entry = null;
            string existing_hash = Head;

            while (is_valid)
            {
                if (existing_hash == "0")
                {
                    break;
                }
                else
                {
                    is_valid = Entries.TryGetValue(existing_hash, out Entry entry);

                    if(is_valid)
                    {
                        string calculated_hash = entry.CalculateHash();
                        is_valid = calculated_hash == existing_hash;

                        if (is_valid)
                            existing_hash = entry.Next;
                        else
                            bad_entry = entry;
                    }
                }
            }

            return Tuple.Create(is_valid, bad_entry);
        }
    }
}
