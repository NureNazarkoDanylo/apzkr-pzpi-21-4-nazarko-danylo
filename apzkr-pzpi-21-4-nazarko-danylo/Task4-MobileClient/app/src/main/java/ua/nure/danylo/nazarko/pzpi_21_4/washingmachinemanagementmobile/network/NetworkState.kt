package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network

sealed interface NetworkState {
    data object Idle: NetworkState
    data object Loading: NetworkState
    data class Error(val errorMessage: String): NetworkState
    data object Success : NetworkState
}
