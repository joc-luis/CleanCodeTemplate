using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace CleanCodeTemplate.Business.Modules.TypeHandlers;

public class JsonListTypeHandler<TValue> : SqlMapper.TypeHandler<IEnumerable<TValue>>
{
    public override void SetValue(IDbDataParameter parameter, IEnumerable<TValue> value)
    {
        parameter.Value = JsonConvert.SerializeObject(value);

        parameter.DbType = DbType.String;
    }

    public override IEnumerable<TValue> Parse(object value)
    {
        return JsonConvert.DeserializeObject<IEnumerable<TValue>>(value.ToString());
    }
}