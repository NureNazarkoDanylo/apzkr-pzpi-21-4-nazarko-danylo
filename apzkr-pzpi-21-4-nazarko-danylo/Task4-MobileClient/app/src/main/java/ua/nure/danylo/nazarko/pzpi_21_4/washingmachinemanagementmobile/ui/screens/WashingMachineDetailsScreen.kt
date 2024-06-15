package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens

import android.util.Log
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.IntrinsicSize
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.Divider
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
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
import kotlinx.coroutines.flow.StateFlow
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.WashingMachineDetailsCardUiState
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.WashingMachineDetailsViewModel


@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun WashingMachineDetailsScreenPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false,
    ) {
        WashingMachineDetailsScreen()
    }
}

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun WashingMachineDetailsScreenPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true,
    ) {
        WashingMachineDetailsScreen()
    }
}

@Composable
fun WashingMachineDetailsScreen(
    viewModel: WashingMachineDetailsViewModel = hiltViewModel(),
    washingMachineId: String = "",
) {
    val uiState by viewModel.uiState.collectAsState()

//    viewModel.getWashingMachine(washingMachineId)
//    viewModel.streamOperationStatus(washingMachineId)
    viewModel.getData(washingMachineId)

    when (uiState.washingMachineNetworkState) {
        is NetworkState.Loading -> NetworkLoadingScreen()
        is NetworkState.Error -> NetworkErrorScreen((uiState.washingMachineNetworkState as NetworkState.Error).errorMessage)
        else -> {
            Log.d("MyLog", "Screen redrawn")
            Surface(
                modifier = Modifier
                    .fillMaxSize()
            ) {
                Column(
                    modifier = Modifier.padding(16.dp),
                    horizontalAlignment = Alignment.CenterHorizontally,
                    verticalArrangement = Arrangement.Center
                ) {
                    DetailsCard(
                        washingMachine = uiState.washingMachine!!,
                        viewModel.cardUuiState
                    )
                }
            }
        }
    }
}

@Composable
fun DetailsCard(
    washingMachine: WashingMachine,
    cardUiState: StateFlow<WashingMachineDetailsCardUiState>,
    modifier: Modifier = Modifier
) {
    val uiState by cardUiState.collectAsState()

    Text(
        modifier = Modifier
            .padding(bottom = 16.dp),
        text = "Details",
        fontWeight = FontWeight.Bold,
        fontSize = 24.sp
    )

    Card(
        elevation = CardDefaults.cardElevation(defaultElevation = 8.dp)
    ) {
        Column(
            modifier = Modifier
                .padding(16.dp)
                .width(IntrinsicSize.Max),
        ) {
            Text(text = "Id: ${washingMachine.id}")
            Text(text = "Name: ${washingMachine.name}")
            Text(text = "Manufacturer: ${washingMachine.manufacturer}")
            Text(text = "Serial Number: ${washingMachine.serialNumber}")
            Text(text = "Description: ${washingMachine.description}")

            Divider(
                modifier = Modifier.padding(vertical = 8.dp)
            )

            when(uiState.washingMachineOperationStatusSseStreamNetworkState) {
                is NetworkState.Loading -> Text(text = "Loading...")
                is NetworkState.Error -> {
                    Text(text = "Can not report data from washing machine sensors. It might be offline")
//                    Text(text = operationStatusNetworkState.errorMessage)
                }
                else -> {
                    Text(text = "State: ${uiState.washingMachineOperationStatus!!.state.name}")
                    Text(text = "Water Temperature (Celcius): ${uiState.washingMachineOperationStatus!!.waterTemperatureCelcius}")
                    Text(text = "Motor Speed (RPM): ${uiState.washingMachineOperationStatus!!.motorSpeedRpm}")
                    Text(text = "Is Lid Closed: ${uiState.washingMachineOperationStatus!!.isLidClosed}")
                    Text(text = "Load Weight (Kg): ${uiState.washingMachineOperationStatus!!.loadWeightKg}")
                }
            }
        }
    }
}
