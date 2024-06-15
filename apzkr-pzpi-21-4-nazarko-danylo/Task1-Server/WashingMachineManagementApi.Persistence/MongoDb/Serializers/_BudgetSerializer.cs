// using MongoDB.Bson;
// using MongoDB.Bson.IO;
// using MongoDB.Bson.Serialization;
// using MongoDB.Bson.Serialization.Serializers;
// using WashingMachineManagementApi.Domain.Entities;
// using WashingMachineManagementApi.Domain.Enums;
//
// namespace WashingMachineManagementApi.Persistence.Serializers;
//
// public class BudgetSerializer : SerializerBase<Budget>, IBsonDocumentSerializer
// {
//     private const string IdDeserializedFieldName = "Id";
//     private const string IdSerializedFieldName = "_id";
//
//     private const string AmountDeserializedFieldName = "Amount";
//     private const string AmountSerializedFieldName = "amount";
//
//     private const string CurrencyDeserializedFieldName = "Currency";
//     private const string CurrencySerializedFieldName = "currency";
//
//     private const string CategoryDeserializedFieldName = "Category";
//     private const string CategorySerializedFieldName = "category";
//
//     private const string FromTimeDeserializedFieldName = "FromTime";
//     private const string FromTimeSerializedFieldName = "fromTime";
//
//     private const string ToTimeDeserializedFieldName = "ToTime";
//     private const string ToTimeSerializedFieldName = "toTime";
//
//     private const string UserIdDeserializedFieldName = "UserId";
//     private const string UserIdSerializedFieldName = "userId";
//
//     public override Budget Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
//     {
//         context.Reader.ReadStartDocument();
//
//         var entity = new Budget();
//
//         while (context.Reader.ReadBsonType() != BsonType.EndOfDocument)
//         {
//             var fieldName = context.Reader.ReadName();
//
//             switch (fieldName)
//             {
//                 case IdSerializedFieldName:
//                     entity.Id = context.Reader.ReadString();
//                     break;
//                 case AmountSerializedFieldName:
//                     entity.Amount = context.Reader.ReadDouble();
//                     break;
//                 case CurrencySerializedFieldName:
//                     entity.Currency = Currency.FromName(context.Reader.ReadString())!;
//                     break;
//                 case CategorySerializedFieldName:
//                     entity.Category = Category.FromName(context.Reader.ReadString())!;
//                     break;
//                 case FromTimeSerializedFieldName:
//                     entity.FromTime = DateTimeOffset.FromUnixTimeMilliseconds(context.Reader.ReadDateTime());
//                     break;
//                 case ToTimeSerializedFieldName:
//                     entity.ToTime = DateTimeOffset.FromUnixTimeMilliseconds(context.Reader.ReadDateTime());
//                     break;
//                 case UserIdSerializedFieldName:
//                     entity.UserId = context.Reader.ReadString();
//                     break;
//             }
//         }
//
//         context.Reader.ReadEndDocument();
//
//         return entity;
//     }
//
//     public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Budget value)
//     {
//         context.Writer.WriteStartDocument();
//
//         context.Writer.WriteString(IdSerializedFieldName, value.Id);
//         context.Writer.WriteDouble(AmountSerializedFieldName, value.Amount);
//         context.Writer.WriteString(CurrencySerializedFieldName, value.Currency.Name);
//         context.Writer.WriteString(CategorySerializedFieldName, value.Category.Name);
//         context.Writer.WriteDateTime(FromTimeSerializedFieldName, value.FromTime.ToUnixTimeMilliseconds());
//         context.Writer.WriteDateTime(ToTimeSerializedFieldName, value.ToTime.ToUnixTimeMilliseconds());
//         context.Writer.WriteString(UserIdSerializedFieldName, value.UserId);
//
//         context.Writer.WriteEndDocument();
//     }
//
//     public bool TryGetMemberSerializationInfo(string memberName, out BsonSerializationInfo serializationInfo)
//     {
//         switch (memberName)
//         {
//             case IdDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(IdSerializedFieldName, new StringSerializer(), typeof(string));
//                 return true;
//             case AmountDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(AmountSerializedFieldName, new DoubleSerializer(), typeof(double));
//                 return true;
//             case CurrencyDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(CurrencySerializedFieldName, new StringSerializer(), typeof(string));
//                 return true;
//             case CategoryDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(CategorySerializedFieldName, new StringSerializer(), typeof(string));
//                 return true;
//             case FromTimeDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(FromTimeSerializedFieldName, new DateTimeSerializer(), typeof(DateTimeOffset));
//                 return true;
//             case ToTimeDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(ToTimeSerializedFieldName, new DateTimeSerializer(), typeof(DateTimeOffset));
//                 return true;
//             case UserIdDeserializedFieldName:
//                 serializationInfo = new BsonSerializationInfo(UserIdSerializedFieldName, new StringSerializer(), typeof(string));
//                 return true;
//             default:
//                 serializationInfo = null!;
//                 return false;
//         }
//     }
// }
