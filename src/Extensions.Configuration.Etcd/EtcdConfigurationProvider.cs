using Extensions.Configuration.Etcd.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Extensions.Configuration.Etcd
{
   public class EtcdConfigurationProvider: ConfigurationProvider
    {

        private readonly string _path;

        private readonly bool _reloadOnChange;
        
        private readonly EtcdClient _etcdClient;

        public EtcdConfigurationProvider(EtcdOptions options)
        {
            _etcdClient = new EtcdClient(options.Address,options.Username, Password = options.Password); 
            _path = options.Path;
            _reloadOnChange = options.ReloadOnChange;
        }

        public override void Load()
        {
         

        }

        private void LoadData()
        {
            string result = _etcdClient.GetNodeValueAsync(_path).ConfigureAwait(false).GetAwaiter().GetResult();
            if (string.IsNullOrEmpty(result))
            {
                return;
            }
            Data = ConvertData(result);


        }
        private IDictionary<string,string> ConvertData(string result)
        {
            byte[] array = Encoding.UTF8.GetBytes(result);
            MemoryStream stream = new MemoryStream(array);
            return JsonConfigurationFileParser.Parse(stream);

        }

        private void ReloadData()
        {
            WatchRequest request = new WatchRequest()
            {
                CreateRequest = new WatchCreateRequest()
                {
                    //需要转换一个格式,因为etcd v3版本的接口都包含在grpc的定义中
                    Key = ByteString.CopyFromUtf8()
                }
            };
            _etcdClient.Watch(request, rsp =>
            {
                if (rsp.Events.Any())
                {
                    var @event = rsp.Events[0];
                    //需要转换一个格式,因为etcd v3版本的接口都包含在grpc的定义中
                    Data = ConvertData(@event.Kv.Value.ToStringUtf8());
                    //需要调用ConfigurationProvider的OnReload方法触发ConfigurationReloadToken通知
                    //这样才能对使用Configuration的类发送数据变更通知
                    //比如IOptionsMonitor就是通过ConfigurationReloadToken通知变更数据的
                    OnReload();
                }
            });

        }
    }
}
