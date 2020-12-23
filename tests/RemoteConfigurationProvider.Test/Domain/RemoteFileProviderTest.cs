using System;
using System.Collections.Generic;

namespace RemoteConfigurationProvider.Test.Domain
{
    public abstract class RemoteFileProviderTest
    {
        protected IDictionary<string, (Type type, object resultExpected)> ResultExpected() =>
            new Dictionary<string, (Type type, object resultExpected)>
            {
                {"RESULT:REMOTE_APPSETTINGS", (typeof(string), "Private")},
                {"RESULT:COMPLEX:ITEM1", (typeof(int), 1)},
                {"RESULT:COMPLEX:ITEM2", (typeof(string), "ITEM2")},
                {"RESULT:COMPLEX:ITEM3", (typeof(int[]), new[] {1, 2, 3})},
                {"RESULT:COMPLEX:ITEM4", (typeof(string[]), new[] {"a", "b", "c"})},
                {"RESULT:NUMBER", (typeof(int), 123546)},
                {"RESULT:BOOL", (typeof(bool), true)},
                {"RESULT:DYNAMIC_VALUE", (typeof(string), "Valor alterado dinamicamente !!!!")}
            };
    }
}