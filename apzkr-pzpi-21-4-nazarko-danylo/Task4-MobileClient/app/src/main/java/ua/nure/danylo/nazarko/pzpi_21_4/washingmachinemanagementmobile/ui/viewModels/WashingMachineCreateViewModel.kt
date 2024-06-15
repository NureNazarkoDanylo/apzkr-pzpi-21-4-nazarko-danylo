package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels

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
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.data.CrossScreenDataStore
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import javax.inject.Inject

data class WashingMachineCreateUiState(
    val submitNetworkState: NetworkState
)

@HiltViewModel
class WashingMachineCreateViewModel @Inject constructor(
    private val apiService: ApiService
): ViewModel() {

    private val _uiState = MutableStateFlow(
        WashingMachineCreateUiState(
            submitNetworkState = NetworkState.Idle
        )
    )

    val uiState: StateFlow<WashingMachineCreateUiState> = _uiState.asStateFlow()

    var washingMachine: WashingMachine by mutableStateOf(
        CrossScreenDataStore.washingMachine ?:
        WashingMachine("","","","")
    )
        private set

    init {
        CrossScreenDataStore.washingMachine = null
    }

    fun createWashingMachine(
        onSuccessfulCreateCallback: () -> Unit
    ) {
        viewModelScope.launch {
            try {
                _uiState.update { currentState ->
                    currentState.copy(
                        submitNetworkState = NetworkState.Loading
                    )
                }

                apiService.createWashingMachine(washingMachine)

                _uiState.update { currentState ->
                    currentState.copy(
                        submitNetworkState = NetworkState.Success
                    )
                }

//                delay(3000)
//                onSuccessfulCreateCallback()
            } catch (e: Exception) {
                _uiState.update { currentState ->
                    currentState.copy(
                        submitNetworkState = NetworkState.Error(e.toString())
                    )
                }
            }

            delay(3000)

            _uiState.update { currentState ->
                currentState.copy(
                    submitNetworkState = NetworkState.Idle
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