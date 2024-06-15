package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.serializers

//class PaginatedListSerializer<T>(private val dataSerializer: KSerializer<T>) : KSerializer<PaginatedList<T>> {
//    override val descriptor: SerialDescriptor = buildClassSerialDescriptor("PaginatedList") {
//        element<List<T>>("items")
//        element<Int>("pageNumber")
//        element<Int>("totalPages")
//        element<Int>("totalCount")
//        element<Boolean>("hasPreviousPage")
//        element<Boolean>("hasNextPage")
//    }
//
//    override fun serialize(encoder: Encoder, value: PaginatedList<T>) {
//        // Implement serialization if necessary
//    }
//
//    override fun deserialize(decoder: Decoder): PaginatedList<T> {
//        require(decoder is JsonDecoder)
//        val jsonObject = decoder.decodeJsonElement().jsonObject
//
//        val items = jsonObject["items"]?.let {
//            decoder.json.decodeFromJsonElement(ListSerializer(dataSerializer), it)
//        } ?: emptyList()
//
//        val pageNumber = jsonObject["pageNumber"]?.jsonPrimitive?.int ?: 0
//        val totalPages = jsonObject["totalPages"]?.jsonPrimitive?.int ?: 0
//        val totalCount = jsonObject["totalCount"]?.jsonPrimitive?.int ?: 0
//        val hasPreviousPage = jsonObject["hasPreviousPage"]?.jsonPrimitive?.boolean ?: false
//        val hasNextPage = jsonObject["hasNextPage"]?.jsonPrimitive?.boolean ?: false
//
//        return PaginatedList(
//            items = items,
//            pageNumber = pageNumber,
//            totalPages = totalPages,
//            totalCount = totalCount,
//            hasPreviousPage = hasPreviousPage,
//            hasNextPage = hasNextPage
//        )
//    }
//}
