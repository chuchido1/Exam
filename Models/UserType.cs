using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        Admin = 1,
        PrivilegedUser = 2,
        NormalUser = 3
    }
}
