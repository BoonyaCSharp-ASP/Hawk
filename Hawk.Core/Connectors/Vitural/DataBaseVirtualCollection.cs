﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hawk.Core.Utils.Plugins;

namespace Hawk.Core.Connectors.Vitural
{
    public class DataBaseVirtualCollection : IItemsProvider<IDictionarySerializable>
    {
        private IDataBaseConnector connector;
        private string tableName;
        public DataBaseVirtualCollection(IDataBaseConnector db, string mtableName)
        {
            connector = db;
            tableName = mtableName;
        }

        private int? count = null;
        public int FetchCount()
        {
            if (count == null)
            {
                var table = connector.RefreshTableNames().FirstOrDefault(d => d.Name == tableName);
                if (table != null) count= table.Size;
                return count.Value;
            }
            else
            {
                return  count.Value;
            }
            return 0;
        }

        public string Name 
        {
            get { return "数据库虚拟化"; }
        }

        public IList<IDictionarySerializable> FetchRange(int startIndex, int count)
        {
            return connector.GetEntityList<IDictionarySerializable>(tableName,  count, startIndex);
        }

        public event EventHandler AlreadyGetSize;
    }
}