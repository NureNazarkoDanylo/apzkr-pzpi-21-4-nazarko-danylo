package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models

import kotlinx.serialization.Serializable

@Serializable
data class TokensModel(
    val accessToken: String,
    val refreshToken: String
)