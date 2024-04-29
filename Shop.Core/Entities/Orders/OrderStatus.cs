using System.Runtime.Serialization;

namespace Shop.Core.Entities.Orders
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pending,

        [EnumMember(Value = "Pago recibido")]
        PaymentReceived,

        [EnumMember(Value = "Pago fallido")]
        PaymentFailed,
    }
}