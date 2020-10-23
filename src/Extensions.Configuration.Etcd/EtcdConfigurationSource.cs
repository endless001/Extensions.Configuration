using Extensions.Configuration.Etcd.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Configuration.Etcd
{
    public class EtcdConfigurationSource : IConfigurationSource
    {
        public EtcdOptions  EtcdOptions { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            
        }
    }
}
