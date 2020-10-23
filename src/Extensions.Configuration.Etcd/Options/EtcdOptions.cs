using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Configuration.Etcd.Options
{
   public class EtcdOptions
    {
        public string Address { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Path { get; set; }

        public bool ReloadOnChange { get; set; }
    }
}
