using System;
using System.Collections.Generic;

namespace Aggregator.Services
{
    public class Store : IStore
    {
        private readonly Dictionary<string, float> internalStore;

        public Store()
        {
            this.internalStore = new Dictionary<string, float>();
        }
        public void Add(string key, float value)
        {
            this.internalStore.Add(key, value);
        }

        public float Get(string key)
        {
            if(!this.internalStore.ContainsKey(key))
            {
                throw new InvalidOperationException($"No record for key {key}");
            }

            return this.internalStore[key];
        }

        public bool Exists(string key)
        {
            return this.internalStore.ContainsKey(key);
        }

        public IDictionary<string, float> GetAll()
        {
            return this.internalStore;
        }

        public void Update(string key, float value)
        {
            if (this.Exists(key))
            {
                this.internalStore[key] = value;
            }
        }
    }
}