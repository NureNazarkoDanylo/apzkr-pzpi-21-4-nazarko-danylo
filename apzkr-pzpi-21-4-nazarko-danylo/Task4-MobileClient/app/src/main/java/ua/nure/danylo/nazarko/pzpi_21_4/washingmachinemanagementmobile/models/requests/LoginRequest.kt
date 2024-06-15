package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.requests

import kotlinx.serialization.Serializable

@Serializable
data class LoginRequest(
    val email: String,
    val password: String
)