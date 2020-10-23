using Extensions.Configuration.Etcd.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Configuration.Etcd.Extensions
{
   public static class EtcdConfigurationExtensions
    {
        public  static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder,string serverAddress,string path)
        {
            return AddEtcd();
        }

        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder,Action<EtcdOptions> options)
        {
            EtcdOptions etcd = new EtcdOptions();
            options.Invoke(etcd);

            
        }
    }
}
