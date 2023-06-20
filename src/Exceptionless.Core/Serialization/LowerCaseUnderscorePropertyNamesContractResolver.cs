﻿using System.Reflection;
using Exceptionless.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exceptionless.Core.Serialization;

public class LowerCaseUnderscorePropertyNamesContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        var shouldSerialize = property.ShouldSerialize;
        property.ShouldSerialize = obj => (shouldSerialize == null || shouldSerialize(obj)) && !property.IsValueEmptyCollection(obj);
        return property;
    }

    protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
    {
        var contract = base.CreateDictionaryContract(objectType);
        contract.DictionaryKeyResolver = propertyName => propertyName;
        return contract;
    }

    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLowerUnderscoredWords();
    }
}
