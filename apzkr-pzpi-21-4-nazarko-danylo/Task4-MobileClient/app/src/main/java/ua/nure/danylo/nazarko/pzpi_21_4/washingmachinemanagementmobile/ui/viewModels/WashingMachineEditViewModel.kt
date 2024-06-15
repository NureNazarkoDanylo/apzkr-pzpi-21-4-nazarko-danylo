package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels

import android.util.Log
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import javax.inject.Inject

data class WashingMachineEditUiState(
    val washingMachineNetworkState: NetworkState,
    val editSubmitNetworkState: NetworkState,
    val deleteSubmitNetworkState: NetworkState
)

@HiltViewModel
class WashingMachineEditViewModel @Inject constructor(
    private val apiService: ApiService
): ViewModel() {

    private val _uiState = MutableStateFlow(
        WashingMachineEditUiState(
            washingMachineNetworkState = NetworkState.Loading,
            editSubmitNetworkState = NetworkState.Idle,
            deleteSubmitNetworkState = NetworkState.Idle
        )
    )

    val uiState: StateFlow<WashingMachineEditUiState> = _uiState.asStateFlow()

    var washingMachine: WashingMachine by mutableStateOf(WashingMachine("","","",""))
        private set

    fun getWashingMachine(id: String) {
        viewModelScope.launch {
            try {
                val result = apiService.getWashingMachine(id)

                _uiState.update { currentState ->
                    currentState.copy(
                        washingMachineNetworkState = NetworkState.Success
                    )
                }

                washingMachine = result
            } catch (e: Exception) {
                _uiState.update { currentState ->
                    currentState.copy(
                        washingMachineNetworkState = NetworkState.Error(e.toString())
                    )
                }
            }
        }
    }

    fun updateWashingMachine() {
        viewModelScope.launch {
            try {
                _uiState.update { currentState ->
                    currentState.copy(
                        editSubmitNetworkState = NetworkState.Loading
                    )
                }

                apiService.updateWashingMachine(washingMachine)

                _uiState.update { currentState ->
                    currentState.copy(
                        editSubmitNetworkState = NetworkState.Success
                    )
                }
            } catch (e: Exception) {
                _uiState.update { currentState ->
                    currentState.copy(
                        editSubmitNetworkState = NetworkState.Error(e.toString())
                    )
                }
            }

            delay(3000)

            _uiState.update { currentState ->
                currentState.copy(
                    editSubmitNetworkState = NetworkState.Idle
                )
            }
        }
    }

    fun deleteWashingMachine(onSuccessfulDelete: () -> Unit) {
        viewModelScope.launch {
            try {
                Log.d("MyLog", "Starting deleting")
                _uiState.update { currentState ->
                    currentState.copy(
                        deleteSubmitNetworkState = NetworkState.Loading
                    )
                }

                apiService.deleteWashingMachine(washingMachine.id)

                onSuccessfulDelete()
            } catch (e: Exception) {
                _uiState.update { currentState ->
                    currentState.copy(
                        deleteSubmitNetworkState = NetworkState.Error(e.toString())
                    )
                }
                Log.d("MyLog", e.toString())
            }

            delay(3000)

            _uiState.update { currentState ->
                currentState.copy(
                    deleteSubmitNetworkState = NetworkState.Idle
                )
            }
        }
    }

    fun setWashingMachineId(value: String) {
        washingMachine = washingMachine.copy(
            id = value
        )
    }

    fun setWashingMachineName(value: String) {
        washingMachine = washingMachine.copy(
            name = value
        )
    }

    fun setWashingMachineManufacturer(value: String) {
        washingMachine = washingMachine.copy(
            manufacturer = value
        )
    }

    fun setWashingMachineSerialNumber(value: String) {
        washingMachine = washingMachine.copy(
            serialNumber = value
        )
    }

    fun setWashingMachineDescription(value: String) {
        washingMachine = washingMachine.copy(
            description =  value
        )
    }
}
