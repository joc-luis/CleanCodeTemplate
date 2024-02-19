using System.Data;
using System.Text;
using Dapper;

namespace CleanCodeTemplate.Business.Modules.TypeHandlers;

public class ByteArrayTypeHandler : SqlMapper.TypeHandler<IEnumerable<byte>>
{
    public override void SetValue(IDbDataParameter parameter, IEnumerable<byte> value)
    {
        parameter.DbType = DbType.String;

        parameter.Value = Encoding.ASCII.GetString(value.ToArray());
    }

    public override IEnumerable<byte> Parse(object value)
    {
        return Encoding.ASCII.GetBytes(value.ToString());
    }
}