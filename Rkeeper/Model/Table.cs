using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Rkeeper.Model;

enum TableShape
{
    Rectangle,
    Circle,
    Square
}

class Table
{

    public string? Name { get; set; }
    public int NumberOfChairs { get; set; }
    public TableShape ShapeOfTable { get; set; }
    public ObservableCollection<Food> OrderedFood { get; set; } = new();
    public Table(string? name, int numberOfChairs, TableShape shapeOfTable)
    {
        Name = name;
        NumberOfChairs = numberOfChairs;
        ShapeOfTable = shapeOfTable;
    }
}

class TableCollection
{
    public ObservableCollection<Table> Tables { get; set; } = new();

    public TableCollection()
    {
        JsonToTables();
    }

    public void AddTable(Table table)
    {
        Tables.Add(table);
        TablesToJson();
    }

    public void TablesToJson()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\Tables.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        string json = JsonConvert.SerializeObject(Tables, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void JsonToTables()
    {

        Tables.Clear();
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\Tables.json";
        string TableJson = File.ReadAllText(path);
        if (JsonConvert.DeserializeObject<ObservableCollection<Table>>(TableJson) != null)
            foreach (var item in JsonConvert.DeserializeObject<ObservableCollection<Table>>(TableJson))
                Tables.Add(item);

    }
}