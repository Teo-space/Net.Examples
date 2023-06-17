using ConsoleTables;
//using DataTablePrettyPrinter;
using System.Reactive.Linq;

public static class ExtensionsConsoleTable
{
    //public static string ToPrettyPrintedString(this DataTable dataTable) => DataTablePrettyPrinter.DataTableExtensions.ToPrettyPrintedString(dataTable);



    public static ConsoleTable ToConsoleTable<T>(this IEnumerable<T> collection) =>
        ConsoleTable
        .From(collection)
        .Configure(o => o.NumberAlignment = Alignment.Right)
        ;

    public static ConsoleTable ToConsoleTable(this DataTable dataTable)
    {
        var table = new ConsoleTable();
        //print($"Columns: {dataTable.Columns.Count}");

        foreach (var column in dataTable.Columns.OfType<DataColumn>())
        {
            table.Columns.Add(column.ColumnName);
        }
        foreach (var row in dataTable.Rows.OfType<DataRow>())
        {
            var values = dataTable.Columns.OfType<DataColumn>().Select(column => row[column]).ToArray();
            //print($"values: {values.Count()}");
            table.AddRow(values);
        }
        return table;
    }

}