
using Azure.Storage.Queues;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliEngine.OliDataAccess.Functions
{
    public class AzureStorage
    {
        QueueClient doFischenQueue;

        public AzureStorage(string connectionString)
        {
            doFischenQueue = new QueueClient(connectionString, "do-fischen");
            doFischenQueue.CreateIfNotExists();
        }

        /// <summary>
        /// Send an Azure Storage Queue message with CodeGuid and AnglerGuid
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="codeGuid"></param>
        /// <param name="anglerGuid"></param>
        public void QueueDoFischen(string msg, Guid codeGuid, Guid anglerGuid)
        {
            var doFischen = new DoFischen()
            {
                Comment = msg,
                MachineName = Environment.MachineName,
                CodeGuid = codeGuid,
                AnglerGuid = anglerGuid
            };
            doFischenQueue.SendMessage(doFischen.ToJson());
        }
    }
    public class DoFischen
    {
        public string Comment { get; set; }
        public string MachineName { get; set; }
        public Guid CodeGuid { get; set; }
        public Guid AnglerGuid { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

