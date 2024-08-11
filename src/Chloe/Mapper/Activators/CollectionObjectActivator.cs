﻿using Chloe.Query;
using Chloe.Reflection;
using System.Collections.ObjectModel;
using System.Data;

namespace Chloe.Mapper.Activators
{
    public class CollectionObjectActivator : ObjectActivatorBase, IObjectActivator
    {
        Type _collectionType;
        InstanceCreator _activator;

        static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, InstanceCreator> ActivatorCache = new System.Collections.Concurrent.ConcurrentDictionary<Type, InstanceCreator>();

        static InstanceCreator GetActivator(Type collectionType)
        {
            InstanceCreator activator = ActivatorCache.GetOrAdd(collectionType, type =>
            {
                var typeDefinition = type.GetGenericTypeDefinition();
                Type implTypeDefinition = null;
                if (typeDefinition.IsAssignableFrom(typeof(List<>)))
                {
                    implTypeDefinition = typeof(List<>);
                }
                else if (typeDefinition.IsAssignableFrom(typeof(Collection<>)))
                {
                    implTypeDefinition = typeof(Collection<>);
                }
                else
                {
                    throw new NotSupportedException($"Not supported collection type '{type.Name}'");
                }

                return InstanceCreatorContainer.Get(implTypeDefinition.MakeGenericType(type.GetGenericArguments()[0]).GetDefaultConstructor());
            });

            return activator;
        }

        CollectionObjectActivator()
        {

        }

        public CollectionObjectActivator(Type collectionType)
        {
            this._collectionType = collectionType;
            this._activator = GetActivator(collectionType);
        }

        public override async ObjectResultTask CreateInstance(QueryContext queryContext, IDataReader reader, bool @async)
        {
            return this._activator();
        }

        public override IObjectActivator Clone()
        {
            CollectionObjectActivator collectionObjectActivator = new CollectionObjectActivator();
            collectionObjectActivator._collectionType = this._collectionType;
            collectionObjectActivator._activator = this._activator;

            return collectionObjectActivator;
        }
    }
}
