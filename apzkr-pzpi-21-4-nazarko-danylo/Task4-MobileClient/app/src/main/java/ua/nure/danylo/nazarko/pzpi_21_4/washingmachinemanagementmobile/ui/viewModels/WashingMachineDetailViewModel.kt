package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels

import android.util.Log
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.serialization.json.Json
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachineOperationStatus
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import javax.inject.Inject

data class WashingMachineDetailsUiState(
    val washingMachineNetworkState: NetworkState,
    val washingMachine: WashingMachine?
)

data class WashingMachineDetailsCardUiState(
    val washingMachineOperationStatusSseStreamNetworkState: NetworkState,
    val washingMachineOperationStatus: WashingMachineOperationStatus?
)

@HiltViewModel
class WashingMachineDetailsViewModel @Inject constructor(
    private val apiService: ApiService
): ViewModel() {
    private val _uiState = MutableStateFlow(
        WashingMachineDetailsUiState(
            washingMachineNetworkState = NetworkState.Loading,
            washingMachine = null
        )
    )

    val uiState: StateFlow<WashingMachineDetailsUiState> = _uiState.asStateFlow()

    private val _cardUiState = MutableStateFlow(
        WashingMachineDetailsCardUiState(
            washingMachineOperationStatusSseStreamNetworkState = NetworkState.Loading,
            washingMachineOperationStatus = null
        )
    )

    val cardUuiState: StateFlow<WashingMachineDetailsCardUiState> = _cardUiState.asStateFlow()

    fun getData(id: String) {
        viewModelScope.launch(Dispatchers.IO) {
            if (_uiState.value.washingMachine == null) {
                try {
                    val result = apiService.getWashingMachine(id)
                    _uiState.update { currentState ->
                        currentState.copy(
                            washingMachineNetworkState = NetworkState.Success,
                            washingMachine = result
                        )
                    }
                } catch (e: Exception) {
                    _uiState.update { currentState ->
                        currentState.copy(
                            washingMachineNetworkState = NetworkState.Error(e.toString())
                        )
                    }
                }
            }


            try {
                val response = apiService.streamWashingMachineOperationStatus(id)

                _cardUiState.update { currentState ->
                    currentState.copy(
                        washingMachineOperationStatusSseStreamNetworkState = NetworkState.Success
                    )
                }

                val source = response?.byteStream()?.bufferedReader()
                source?.use { reader ->
                    var line: String?
                    while (reader.readLine().also { line = it } != null) {
                        if (line != null && line!!.isNotBlank() && line!!.startsWith("data:")) {
                            val newOperationStatus = Json { ignoreUnknownKeys = true }
                                .decodeFromString<WashingMachineOperationStatus>(line!!.substring(5).trim())
                            _cardUiState.update { currentState ->
                                currentState.copy(
                                    washingMachineOperationStatus = newOperationStatus
                                )
                            }
                        }
                    }
                }
            } catch (e: Exception) {
                Log.d("MyLog", e.toString())
                _cardUiState.update { currentState ->
                    currentState.copy(
                        washingMachineOperationStatusSseStreamNetworkState = NetworkState.Error(e.toString())
                    )
                }
            }
        }
    }

//    fun getWashingMachine(id: String) {
//        viewModelScope.launch {
//            try {
//                val result = apiService.getWashingMachine(id)
//                _uiState.update { currentState ->
//                    currentState.copy(
//                        washingMachineNetworkState = NetworkState.Success,
//                        washingMachine = result
//                    )
//                }
//            } catch (e: Exception) {
//                _uiState.update { currentState ->
//                    currentState.copy(
//                        washingMachineNetworkState = NetworkState.Error(e.toString())
//                    )
//                }
//            }
//        }
//    }
//
//    fun streamOperationStatus(id: String) {
//        viewModelScope.launch(Dispatchers.IO) {
//            try {
//                val response = apiService.streamWashingMachineOperationStatus(id)
//
//                _uiState.update { currentState ->
//                    currentState.copy(
//                        washingMachineOperationStatusSseStreamNetworkState = NetworkState.Success
//                    )
//                }
//
//                val source = response?.byteStream()?.bufferedReader()
//                source?.use { reader ->
//                    var line: String?
//                    while (reader.readLine().also { line = it } != null) {
//                        if (line != null && line!!.isNotBlank() && line!!.startsWith("data:")) {
//                            val newOperationStatus = Json { ignoreUnknownKeys = true }
//                                .decodeFromString<WashingMachineOperationStatus>(line!!.substring(5).trim())
//                            _uiState.update { currentState ->
//                                currentState.copy(
//                                    washingMachineOperationStatus = newOperationStatus
//                                )
//                            }
//                        }
//                    }
//                }
//            } catch (e: Exception) {
//                Log.d("MyLog", _uiState.value.washingMachine!!.id)
//                Log.d("MyLog", id)
//                Log.d("MyLog", e.toString())
//                _uiState.update { currentState ->
//                    currentState.copy(
//                        washingMachineOperationStatusSseStreamNetworkState = NetworkState.Error(e.toString())
//                    )
//                }
//            }
//        }
//    }
}
