﻿using System;
using System.Linq;
using Newtonsoft.Json;
using SessionPersistence;

namespace SessionInteractor.Adapters.Input.Implementation
{
    public class SessionPersister : ISessionPersister
    {
        private readonly ISessionStore _store;

        public SessionPersister(ISessionStore store)
        {
            _store = store;
        }

        public void SetValue<T>(T value, Guid id)
        {
            var valueFound = _store.Store.FirstOrDefault(s => s.Id == id);
            if (valueFound == null)
            {
                _store.Store.Add(new SessionValue
                {
                    Id = id,
                    Value = JsonConvert.SerializeObject(value),
                    TypeOfValue = value.GetType().Name
                });
            }
            else
            {
                valueFound.Value = JsonConvert.SerializeObject(value);
            }
        }
    }
}