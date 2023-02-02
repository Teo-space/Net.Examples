


/// <summary>
/// Рассматриваем object[,] как таблицу object[y,x]
/// где object[0,x] это заголовки
/// </summary>
public static class ExtensionsTable
{
    /// <summary>
    /// Высота Таблицы
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static int YLength(this object[,] data) => data.GetLength(0);


    /// <summary>
    /// Ширина таблицы
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static int XLength(this object[,] data) => data.GetLength(1);

    /// <summary>
    /// Сводка по таблице
    /// </summary>
    /// <param name="data"></param>
    /// <param name="rows"></param>
    /// <returns></returns>
    public static string Head(this object[,] data, int rows = 5)
    {
        StringBuilder builder = new();

        int YLength = data.YLength();
        int XLength = data.XLength();

        builder.Append("Head: ");
        for (int x = 0; x < XLength; x++)
        {
            builder.Append(data[0, x]);
            builder.Append("    ");
        }
        builder.Append("\n");

        builder.Append("\nBody:\n");
        for (int y = 1; y < YLength && y < rows; y++)//строка таблицы. сдвиг 1 (пропустили тело)
        {
            for (int x = 0; x < XLength; x++)//ячейка в строке
            {
                builder.Append($" ({data[0, x]}:{y}:{x}.v:{data[y, x]})  ");
            }
            builder.Append("\n");
        }

        return builder.ToString();
    }


    /// <summary>
    /// Срез столбцов по их индексам
    /// </summary>
    /// <param name="data"></param>
    /// <param name="indexes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static object[,] Slice(this object[,] data, params int[] indexes)
    {
        int YLength = data.YLength();
        int XLength = data.XLength();
        int ResultXLength = indexes.Length;

        //Валидация
        for (int i = 0; i < indexes.Length; i++)
        {
            if (indexes[i] > XLength)
            {
                throw new ArgumentException("index in index > data XLength/ индекс для срезов больше чем ширина таблицы");
            }
        }

        object[,] result = new object[YLength, ResultXLength];

        //копируем шапку
        for (int r = 0; r < ResultXLength; r++)//индекс среза
        {
            result[0, r] = data[0, indexes[r]];
        }
        //копируем данные
        for (int y = 1; y < YLength; y++)//строка таблицы. сдвиг 1 (пропустили тело)
        {
            for (int x = 0; x < XLength; x++)//ячейка в строке
            {
                for (int r = 0; r < ResultXLength; r++)//индекс среза
                {
                    if (indexes[r] == x)//если индекс среза = текущая ячейка
                    {
                        result[y, r] = data[y, x];
                        break;
                    }
                }
            }
        }
        return result;
    }


    /// <summary>
    /// срез столбцов по значениям в шапке
    /// </summary>
    /// <param name="data"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static object[,] Slice(this object[,] data, params object[] keys)
    {
        int XLength = data.XLength();

        int[] indexes = new int[keys.Length];

        for (int k = 0; k < keys.Length; k++)
        {
            for (int x = 0; x < XLength; x++)
            {
                if (data[0, x] == keys[k])
                {
                    indexes[k] = x;
                    break;
                }
            }
        }
        return Slice(data, indexes);
    }


    public static DataTable ToTable(this object[,] data)
    {
        int YLength = data.YLength();
        int XLength = data.XLength();

        DataTable dataTable = new DataTable();
        //Копирование заголовков
        for (int x = 0; x < XLength; x++)
        {
            dataTable.Columns.Add(data[0, x].ToString());
        }

        for (int y = 1; y < YLength; y++)//отступ 1 (заголовки)
        {
            var row = dataTable.NewRow();

            for (int x = 0; x < XLength; x++)
            {
                //row[data[0,x].ToString()] = data[y,x];
                row[dataTable.Columns[x]] = data[y, x];
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }


}

