using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum UserEmailAction
    {
        [EnumMember(Value = "put")]
        Put,
        [EnumMember(Value = "delete")]
        Delete
    }
}
