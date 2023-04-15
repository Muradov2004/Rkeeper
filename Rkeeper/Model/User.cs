using Newtonsoft.Json;
using Rkeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.Model;

class User
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public User(string? username, string? password)
    {
        Username = username;
        Password = password;
    }

}

class Users
{
    public ObservableCollection<User> users { get; set; } = new();
    public Users()
    {
        JsonToUser();
    }

    public void AddUser(User user)
    {
        users.Add(user);
        UsersToJson();
    }

    public void UsersToJson()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\Users.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void JsonToUser()
    {

        users.Clear();
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\Users.json";
        string UserJson = File.ReadAllText(path);
        if (JsonConvert.DeserializeObject<ObservableCollection<User>>(UserJson) != null)
            foreach (var item in JsonConvert.DeserializeObject<ObservableCollection<User>>(UserJson))
                users.Add(item);

    }
}
