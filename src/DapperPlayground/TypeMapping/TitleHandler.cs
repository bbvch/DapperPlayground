namespace DapperPlayground.TypeMapping
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dapper;

    /// <remarks>
    /// Unfortunately, EnumType Handlers are not supported by Dapper. See:
    /// https://github.com/DapperLib/Dapper/issues/259
    /// </remarks>
    public class TitleHandler : SqlMapper.TypeHandler<Title>
    {
        private static readonly Dictionary<Title, string> TitleMap = new Dictionary<Title, string>
        {
            {Title.Miss, "Miss"},
            {Title.Misses, "Mrs."},
            {Title.Mizz, "Ms."},
            {Title.Mister, "Mr."},
            {Title.Doctor, "Dr."}
        };

        public override void SetValue(IDbDataParameter parameter, Title keyValue)
        {
            parameter.DbType = DbType.AnsiString;
            parameter.Value = TitleMap[keyValue];
        }

        public override Title Parse(object value)
        {
            return TitleMap
                .First(kvp => kvp.Value == (string)value)
                .Key;
        }
    }
}