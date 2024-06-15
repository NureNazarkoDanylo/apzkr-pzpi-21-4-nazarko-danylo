package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models

import kotlinx.serialization.Serializable

@Serializable
data class PaginatedList<T> (
    val items: ArrayList<T>,
    val pageNumber: Int,
    val totalPages: Int,
    val totalCount: Int,
    val hasPreviousPage: Boolean,
    val hasNextPage: Boolean
)