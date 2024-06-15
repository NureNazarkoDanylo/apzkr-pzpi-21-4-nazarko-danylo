package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models

import kotlinx.serialization.Serializable
import kotlinx.serialization.json.JsonNames

@Serializable
class WashingMachineOperationStatus (
    val state: WashingMachineState,
    val waterTemperatureCelcius: Int,
    @JsonNames("motorSpeedRPM")
    val motorSpeedRpm: Int,
    val isLidClosed: Boolean,
    val loadWeightKg: Float
)