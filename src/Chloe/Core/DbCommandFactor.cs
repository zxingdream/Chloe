﻿using Chloe.Mapper;

namespace Chloe.Core
{
    class DbCommandFactor
    {
        public DbCommandFactor(DbContext dbContext, IObjectActivator objectActivator, string commandText, DbParam[] parameters)
        {
            this.DbContext = dbContext;
            this.ObjectActivator = objectActivator;
            this.CommandText = commandText;
            this.Parameters = parameters;
        }
        public DbContext DbContext { get; set; }
        public IObjectActivator ObjectActivator { get; set; }
        public string CommandText { get; set; }
        public DbParam[] Parameters { get; set; }
    }
}
