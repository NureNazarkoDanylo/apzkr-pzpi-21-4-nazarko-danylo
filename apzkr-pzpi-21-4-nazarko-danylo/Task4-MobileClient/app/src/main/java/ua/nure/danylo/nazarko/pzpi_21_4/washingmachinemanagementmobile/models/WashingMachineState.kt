package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
enum class WashingMachineState(val id: Int) {
    @SerialName(0.toString())
    Idle(0),
    @SerialName(1.toString())
    Prewash(1),
    @SerialName(2.toString())
    Washing(2),
    @SerialName(3.toString())
    Rinsing(3),
    @SerialName(4.toString())
    Spinning(4)
}