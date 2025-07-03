using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class JsonFileReaderWriter
    {
        public static T Read<T>(Stream stream)
        {
            var item = JsonSerializer.Deserialize<T>(stream);
            if (item == null)
            {
                throw new Exception("Invalid json");
            }
            return item;
        }

        public static void Write<T>(T item, Stream stream)
        {
            JsonSerializer.Serialize<T>(stream, item);
        }
    }
}
