package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.PaginatedList
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import javax.inject.Inject

sealed interface WashingMachineListUiState {
    object Loading: WashingMachineListUiState
    data class Error(val errorMessage: String): WashingMachineListUiState
    data class Success(val data: PaginatedList<WashingMachine>): WashingMachineListUiState
}

@HiltViewModel
class WashingMachineListViewModel @Inject constructor(
    private val apiService: ApiService
): ViewModel() {
    var uiState: WashingMachineListUiState by mutableStateOf(WashingMachineListUiState.Loading)
        private set

    fun getWashingMachines() {
        viewModelScope.launch {
            try {
                val result = apiService.getWashingMachines()
                uiState = WashingMachineListUiState.Success(result)
            } catch (e: Exception) {
                uiState = WashingMachineListUiState.Error(e.toString())
            }
        }
    }
}