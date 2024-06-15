package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models

import kotlinx.serialization.Serializable

@Serializable
data class WashingMachine(
    val id: String,
    val name: String,
    val manufacturer: String,
    val serialNumber: String,
    val description: String? = null
)