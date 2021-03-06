using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Historian.Services
{
    public interface IStore
    {
        void Add(string key, float value);
        
        float Get(string key);
        
        bool Exists(string key);
        
        IDictionary<string, float> GetAll();
        
        void Update(string key, float value);
    }
}