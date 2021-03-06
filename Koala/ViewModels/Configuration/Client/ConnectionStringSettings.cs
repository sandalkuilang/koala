﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Configuration.Client
{
    public class ConnectionStringSettings : BaseConfigurationSection
    {
        [ConfigurationProperty("connectionString")]
        [ConfigurationCollection(typeof(ConnectionStringCollection))]
        public ConnectionStringCollection Items
        {
            get
            {
                return ((ConnectionStringCollection)(base["connectionString"]));
            }
            set
            {
                base["connectionString"] = value;
            }
        }
         
        public ConnectionStringElement DefaultConnection
        {
            get
            { 
                foreach(ConnectionStringElement collection in this.Items)
                {
                    if (collection.IsDefault)
                    {
                        return collection; 
                    }
                }
                return null;
            } 
        }
    }
}
