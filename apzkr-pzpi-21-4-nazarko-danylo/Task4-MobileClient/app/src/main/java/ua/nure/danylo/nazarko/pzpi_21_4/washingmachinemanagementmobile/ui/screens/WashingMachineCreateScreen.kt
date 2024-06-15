package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens

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
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.WashingMachineCreateViewModel

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun CreateScreenPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false,
    ) {
        WashingMachineCreateScreen()
    }
}

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun CreateScreenPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true,
    ) {
        WashingMachineCreateScreen()
    }
}

@Composable
fun WashingMachineCreateScreen(
    viewModel: WashingMachineCreateViewModel = hiltViewModel(),
    onSuccessfulCreateCallback: () -> Unit = {}
) {
    val uiState by viewModel.uiState.collectAsState()

    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        WashingMachineCreateForm(
            modifier = Modifier
                .fillMaxSize(),
            id = viewModel.washingMachine.id,
            onIdChange = { viewModel.setWashingMachineId(it) },
            name = viewModel.washingMachine.name,
            onNameChange = { viewModel.setWashingMachineName(it) },
            manufacturer = viewModel.washingMachine.manufacturer,
            onManufacturerChange = { viewModel.setWashingMachineManufacturer(it) },
            serialNumber = viewModel.washingMachine.serialNumber,
            onSerialNumberChange = { viewModel.setWashingMachineSerialNumber(it) },
            description = viewModel.washingMachine.description ?: "",
            onDescriptionChange = { viewModel.setWashingMachineDescription(it) },
            onSubmit = { viewModel.createWashingMachine(onSuccessfulCreateCallback) },
            submitNetworkState = uiState.submitNetworkState
        )
    }
}

@Composable
fun WashingMachineCreateForm(
    id: String,
    onIdChange: (String) -> Unit,
    name: String,
    onNameChange: (String) -> Unit,
    manufacturer: String,
    onManufacturerChange: (String) -> Unit,
    serialNumber: String,
    onSerialNumberChange: (String) -> Unit,
    description: String,
    onDescriptionChange: (String) -> Unit,
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
                .width(intrinsicSize = IntrinsicSize.Max)
                .padding(64.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
        ) {
            Text(
                modifier = Modifier
                    .padding(bottom = 16.dp),
                text = "Create",
                fontWeight = FontWeight.Bold,
                fontSize = 24.sp
            )

            TextField(
                modifier = Modifier
                    .padding(bottom = 8.dp)
                    .fillMaxWidth(),
                value = id,
                onValueChange = onIdChange,
                label = {
                    Text(
                        text = "Id"
                    )
                },
            )

            TextField(
                modifier = Modifier
                    .padding(bottom = 8.dp)
                    .fillMaxWidth(),
                value = name,
                onValueChange = onNameChange,
                label = {
                    Text(
                        text = "Name"
                    )
                },
            )

            TextField(
                modifier = Modifier
                    .padding(bottom = 8.dp)
                    .fillMaxWidth(),
                value = manufacturer,
                onValueChange = onManufacturerChange,
                label = {
                    Text(
                        text = "Manufacturer"
                    )
                },
            )

            TextField(
                modifier = Modifier
                    .padding(bottom = 8.dp)
                    .fillMaxWidth(),
                value = serialNumber,
                onValueChange = onSerialNumberChange,
                label = {
                    Text(
                        text = "Serial Number"
                    )
                },
            )

            TextField(
                modifier = Modifier
                    .padding(bottom = 8.dp)
                    .fillMaxWidth(),
                value = description,
                onValueChange = onDescriptionChange,
                label = {
                    Text(
                        text = "Description"
                    )
                },
            )

            Button(
                modifier = Modifier
                    .padding(bottom = 8.dp)
                    .fillMaxWidth(),
                onClick = onSubmit,
            ) {
                when (submitNetworkState) {
                    is NetworkState.Loading -> Text(text = "Loading...")
                    is NetworkState.Error -> Text(text = "Error: ${submitNetworkState.errorMessage}")
                    is NetworkState.Success -> Text(text = "Success!")
                    else -> Text(text = "Create")
                }
            }
        }
    }
}
