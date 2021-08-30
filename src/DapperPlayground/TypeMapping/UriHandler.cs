namespace DapperPlayground.TypeMapping
{
    using System;
    using System.Data;

    using Dapper;

    public class UriHandler : SqlMapper.TypeHandler<Uri>
    {
        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            parameter.DbType = DbType.AnsiString;
            parameter.Value = value.ToString();
        }

        public override Uri Parse(object value)
        {
            return new Uri((string)value);
        }
    }
}