public static class FullTextSearchHelper
{
    public static string GetIdentifierSql(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("The name must not be null or empty.");
        }
        return "\"" + name.Replace("\"", "") + "\"";
    }

    public static string GetTsVectorComputedColumnSql(string config, IEnumerable<string> columns)
    {
        var filteredColumns = columns?.Where(item => !string.IsNullOrEmpty(item.ToString()));
        if (filteredColumns == null || !filteredColumns.Any())
        {
            return "";
        }
        return "to_tsvector('" + config.Replace("\'", "") + "', " + string.Join(" || ' ' || ", filteredColumns.Select(item => "coalesce(" + GetIdentifierSql(item.ToString()) + ", '')")) + ")";
    }
}