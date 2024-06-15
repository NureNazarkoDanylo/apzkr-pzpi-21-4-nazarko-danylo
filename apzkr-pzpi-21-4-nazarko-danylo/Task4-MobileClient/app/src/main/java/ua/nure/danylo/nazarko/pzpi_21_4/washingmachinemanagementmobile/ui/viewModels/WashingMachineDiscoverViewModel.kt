package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels

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
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import javax.inject.Inject

data class WashingMachineDiscoveryUiState(
    val sseStreamState: NetworkState,
    val washingMachines: List<WashingMachine>,
)

@HiltViewModel
class WashingMachineDiscoverViewModel @Inject constructor(
    private val apiService: ApiService
): ViewModel() {

    private val _uiState = MutableStateFlow(
        WashingMachineDiscoveryUiState(
            sseStreamState = NetworkState.Loading,
            listOf()
        )
    )

    val uiState: StateFlow<WashingMachineDiscoveryUiState> = _uiState.asStateFlow()

    init {
        startDiscovery()
    }

    private fun startDiscovery() {
        viewModelScope.launch(Dispatchers.IO) {
            try {
                val response = apiService.discoverWashingMachines()

                _uiState.update { currentState ->
                    currentState.copy(
                        sseStreamState = NetworkState.Success
                    )
                }

                val source = response?.byteStream()?.bufferedReader()
                source?.use { reader ->
                    var line: String?
                    while (reader.readLine().also { line = it } != null) {
                        if (line != null && line!!.isNotBlank() && line!!.startsWith("data:")) {
                            val newWashingMachine = Json { ignoreUnknownKeys = true }
                                .decodeFromString<WashingMachine>(line!!.substring(5).trim())
                            _uiState.update { currentState ->
                                currentState.copy(
                                    washingMachines = currentState.washingMachines + newWashingMachine
                                )
                            }
                        }
                    }
                }

//                Log.d("MyLog", response?.byteStream()?.bufferedReader()?.readLine() ?: "no data")
//                    val input = response?.byteStream()?.bufferedReader() ?: throw Exception()
//                    try {
//                        while (isActive) {
//                            val line = input.readLine()
// ?: continue
//
//                            Log.d("MyLog", line)
////                            if (line.startsWith("data:")) {
////                                Log.d("MyLog", line)
////                                try {
////                                    val washingMachine =
////                                        Json.decodeFromString<WashingMachine>(line.substring(5).trim())
////                                    uiState.add(washingMachine)
////                                } catch (e: Exception) {
////                                    e.printStackTrace()
////                                    Log.d("MyLog", e.toString())
////                                    Log.d("MyLog", e.stackTraceToString())
////                                }
////                            }
//                        }
//                    } catch (e: IOException) {
//                        Log.d("MyLog", e.toString())
//                        Log.d("MyLog", e.stackTraceToString())
//                        throw Exception(e)
//                    } finally {
//                        input.close()
//                    }
////                uiState = WashingMachineDetailsUiState.Success(result)
            } catch (e: Exception) {
                _uiState.update { currentState ->
                    currentState.copy(
                        sseStreamState = NetworkState.Error(e.toString())
                    )
                }
            }
        }
    }
}
