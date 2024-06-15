package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.IntrinsicSize
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.Button
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.hilt.navigation.compose.hiltViewModel
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.LoginViewModel

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun LoginScreenPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false,
    ) {
        LoginScreen()
    }
}

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun LoginScreenPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true,
    ) {
        LoginScreen()
    }
}

@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@Composable
fun LoginScreen(
    viewModel: LoginViewModel = hiltViewModel(),
    onSuccessfulSubmitCallback: () -> Unit = {}
) {
    val uiState by viewModel.uiState.collectAsState()

    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        LoginForm(
            modifier = Modifier
                .fillMaxSize(),
            emailValue = viewModel.email,
            onEmailValueChange = { viewModel.changeEmail(it) },
            passwordValue = viewModel.password,
            onPasswordValueChange = { viewModel.changePassword(it) },
            onSubmit = { viewModel.login(onSuccessfulSubmitCallback) },
            submitNetworkState = uiState.submitNetworkState
        )
    }
}

@Composable
fun LoginForm(
    emailValue: String = "",
    onEmailValueChange: (String) -> Unit = {},
    passwordValue: String = "",
    onPasswordValueChange: (String) -> Unit = {},
    onSubmit: () -> Unit,
    submitNetworkState: NetworkState,
    modifier: Modifier = Modifier,
) {
    Column(
        modifier = modifier,
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center,
    ) {
        Column(
            modifier = Modifier
                .width(intrinsicSize = IntrinsicSize.Max),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text(
                modifier = Modifier
                    .padding(bottom = 16.dp),
                text = "Login",
                fontWeight = FontWeight.Bold,
                fontSize = 24.sp
            )
            TextField(
                modifier = Modifier
                    .padding(bottom = 8.dp),
                value = emailValue,
                onValueChange = onEmailValueChange,
                label = {
                    Text(
                        text = "Email"
                    )
                }
            )

            TextField(
                modifier = Modifier
                    .padding(bottom = 16.dp),
                value = passwordValue,
                onValueChange = onPasswordValueChange,
                label = {
                    Text(
                        text = "Password"
                    )
                }
            )
            Button(
                modifier = Modifier
                    .fillMaxWidth(),
                onClick = onSubmit
            ) {
                when (submitNetworkState) {
                    is NetworkState.Loading -> Text(text = "Loading...")
                    is NetworkState.Error -> Text(text = submitNetworkState.errorMessage)
                    is NetworkState.Success -> Text(text = "Success!")
                    else -> Text(text = "Login")
                }
            }
        }
    }
}
