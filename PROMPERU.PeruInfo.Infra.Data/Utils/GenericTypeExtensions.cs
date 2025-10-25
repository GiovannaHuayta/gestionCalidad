using System;
using System.Data;
using System.Data.SqlClient;

namespace PROMPERU.PeruInfo.Infra.Data.Utils
{
    public static class GenericTypeExtensions
    {
        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return null;
            int col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ? (DateTime?)null : (DateTime?)reader.GetDateTime(col);
        }

        public static DateTime GetDateTime(this SqlDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return DateTime.MinValue;
            int col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ? DateTime.MinValue : reader.GetDateTime(col);
        }

        public static string GetNullableString(this SqlDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return null;
            int col = reader.GetOrdinal(name);
            return !reader.IsDBNull(col) ? reader.GetString(col) : string.Empty;
        }

        public static int GetNullableInt(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return 0;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) ? reader.GetInt32(colIndex) : 0;
        }

        public static Guid GetGuid(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return Guid.Empty;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) ? reader.GetGuid(colIndex) : Guid.Empty;
        }

        public static Guid? GetNullableGuid(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return null;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) ? reader.GetGuid(colIndex) : (Guid?)null;
        }

        public static Guid GetDefaultGuid(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return Guid.Empty;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) ? reader.GetGuid(colIndex) : Guid.Empty;
        }

        public static bool GetDefaultBoolean(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return false;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) && reader.GetBoolean(colIndex);
        }

        public static bool? GetNullableBoolean(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return null;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) ? reader.GetBoolean(colIndex) : (bool?)null;
        }

        public static decimal GetDefaultDecimal(this SqlDataReader reader, string colName)
        {
            if (!ColumnExists(reader, colName)) return 0;
            int colIndex = reader.GetOrdinal(colName);
            return !reader.IsDBNull(colIndex) ? reader.GetDecimal(colIndex) : 0;
        }

        #region Private methods

        private static bool ColumnExists(IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        #endregion
    }
}