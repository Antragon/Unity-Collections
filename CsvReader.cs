namespace Collections {
    using System.Data;
    using System.Linq;

    public static class CsvReader {
        public static DataTable ReadAsTable(string text, char rowSeparator, char columnSeparator) {
            var rows = text.Split(rowSeparator);
            var headers = rows[0].Split(columnSeparator);

            var table = new DataTable();
            foreach (var header in headers) {
                table.Columns.Add(header);
            }

            foreach (var row in rows.Skip(1)) {
                var strings = row.Split(columnSeparator);
                if (strings.All(string.IsNullOrWhiteSpace)) continue;
                // ReSharper disable once CoVariantArrayConversion
                table.Rows.Add(strings);
            }

            return table;
        }
    }
}