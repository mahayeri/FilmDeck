using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;

namespace Api.Shared.Domain.Contracts;
public class SnakeCaseElementNameConvention : ConventionBase, IMemberMapConvention
{
    public void Apply(BsonMemberMap memberMap)
    {
        // Skip properties with [BsonElement] attributes
        if (memberMap.MemberInfo.GetCustomAttributes(typeof(BsonElementAttribute), false).Any())
        {
            return; // Do nothing if [BsonElement] is present
        }

        // Apply snake_case to other properties
        string snakeCaseName = ConvertToSnakeCase(memberMap.MemberName);
        memberMap.SetElementName(snakeCaseName);
    }

    private string ConvertToSnakeCase(string input)
    {
        return string.Concat(input.Select((c, i) =>
            i > 0 && char.IsUpper(c) ? "_" + c.ToString().ToLower() : c.ToString().ToLower()));
    }
}
