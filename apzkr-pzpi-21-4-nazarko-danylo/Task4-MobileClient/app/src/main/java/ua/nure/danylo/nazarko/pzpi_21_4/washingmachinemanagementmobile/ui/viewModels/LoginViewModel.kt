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
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.requests.LoginRequest
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.utils.UserStore
import javax.inject.Inject

data class LoginUiState(
    val submitNetworkState: NetworkState
)

@HiltViewModel
class LoginViewModel @Inject constructor(
    private val apiService: ApiService,
    private val userStore: UserStore
): ViewModel() {

    private val _uiState = MutableStateFlow(
        LoginUiState(
            submitNetworkState = NetworkState.Idle
        )
    )

    val uiState: StateFlow<LoginUiState> = _uiState.asStateFlow()

    var email by mutableStateOf("")
        private set

    var password by mutableStateOf("")
        private set

    fun login(
        onSuccessfulCallback: () -> Unit
    ) {
        viewModelScope.launch {
            try {
                _uiState.update { currentState ->
                    currentState.copy(
                        submitNetworkState = NetworkState.Loading
                    )
                }

                val result = apiService.login(LoginRequest(email, password))

                userStore.saveTokens(result)

                _uiState.update { currentState ->
                    currentState.copy(
                        submitNetworkState = NetworkState.Success
                    )
                }

                onSuccessfulCallback()
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

    fun changeEmail(value: String) {
        email = value
    }

    fun changePassword(value: String) {
        password = value
    }
}
