package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.IntrinsicSize
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.hilt.navigation.compose.hiltViewModel
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.data.CrossScreenDataStore
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.NetworkState
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.WashingMachineDiscoverViewModel

@Preview(
    showBackground = true
)
@Composable
fun WashingMachineDiscoveryScreenPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false
    ) {
        WashingMachineDiscoveryScreen()
    }
}

@Preview(
    showBackground = true
)
@Composable
fun WashingMachineDiscoveryScreenPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true
    ) {
        WashingMachineDiscoveryScreen()
    }
}

@Composable
fun WashingMachineDiscoveryScreen(
    viewModel: WashingMachineDiscoverViewModel = hiltViewModel(),
    onCreateButtonClick: () -> Unit = {}
) {
    val uiState by viewModel.uiState.collectAsState()

    when(uiState.sseStreamState) {
        is NetworkState.Loading -> NetworkLoadingScreen()
        is NetworkState.Error -> NetworkErrorScreen((uiState.sseStreamState as NetworkState.Error).errorMessage)
        else ->
            Surface(
                modifier = Modifier
                    .fillMaxSize(),
            ) {
                LazyColumn(
                    modifier = Modifier
                        .fillMaxHeight(),
                    contentPadding = PaddingValues(start = 8.dp, top = 8.dp, end = 8.dp),
                ) {
                    items(
                        uiState.washingMachines,
                        key = { item -> item.id }
                    ) { washingMachine ->
                        WashingMachineCard(
                            modifier = Modifier
                                .fillMaxWidth()
                                .padding(bottom = 8.dp),
                            washingMachine = washingMachine,
                            onCreateButtonClick = onCreateButtonClick,
                        )
                    }
                }
            }
    }
}

@Composable
fun WashingMachineCard(
    washingMachine: WashingMachine,
    onCreateButtonClick: () -> Unit = {},
    modifier: Modifier = Modifier,
) {
    Card(
        modifier = modifier,
        elevation = CardDefaults.cardElevation(defaultElevation = 8.dp),
    ) {
        Row(
            modifier = Modifier
                .fillMaxWidth()
                .padding(8.dp),
            verticalAlignment = Alignment.CenterVertically,
            horizontalArrangement = Arrangement.SpaceBetween,
        ) {
            Column(
                modifier = Modifier
                    .weight(4F),
                horizontalAlignment = Alignment.Start,
            ) {
                Text(
                    modifier = Modifier.padding(bottom = 8.dp),
                    text = "Name: ${washingMachine.name}",
                    fontSize = 16.sp,
                    maxLines = 1,
                    overflow = TextOverflow.Ellipsis,
                )

                Text(
                    modifier = Modifier.padding(bottom = 8.dp),
                    text = "Manufacturer: ${washingMachine.manufacturer}",
                    fontSize = 16.sp,
                    maxLines = 1,
                    overflow = TextOverflow.Ellipsis,
                )

                Text(
                    text = "Serial Number: ${washingMachine.serialNumber}",
                    fontSize = 16.sp,
                    maxLines = 1,
                    overflow = TextOverflow.Ellipsis,
                )
            }

            Column(
                modifier = Modifier
                    .weight(2F)
                    .width(intrinsicSize = IntrinsicSize.Max)
                    .padding(start = 8.dp),
                horizontalAlignment = Alignment.End,
                verticalArrangement = Arrangement.SpaceAround,
            ) {
                Button(
                    modifier = Modifier
                        .fillMaxWidth(),
                    onClick = { CrossScreenDataStore.washingMachine = washingMachine; onCreateButtonClick() },
                ) {
                    Text(
                        text = "Create"
                    )
                }
            }
        }
    }
}
