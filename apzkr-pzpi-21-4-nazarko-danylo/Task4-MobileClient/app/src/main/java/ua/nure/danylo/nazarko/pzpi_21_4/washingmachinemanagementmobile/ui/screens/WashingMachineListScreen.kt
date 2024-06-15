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
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.hilt.navigation.compose.hiltViewModel
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.WashingMachineListUiState
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.viewModels.WashingMachineListViewModel

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun WashingMachineListScreenPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false,
    ) {
//        WashingMachineListScreen()
    }
}

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun WashingMachineListScreenPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true,
    ) {
//        WashingMachineListScreen()
    }
}

@Composable
fun WashingMachineListScreen(
    viewModel: WashingMachineListViewModel = hiltViewModel(),
    onCreateButtonClick: () -> Unit,
    onDetailsButtonClick: (String) -> Unit,
    onEditButtonClick: (String) -> Unit,
    modifier: Modifier = Modifier,
) {
    viewModel.getWashingMachines()

    when (viewModel.uiState) {
        is WashingMachineListUiState.Loading -> NetworkLoadingScreen()
        is WashingMachineListUiState.Error -> NetworkErrorScreen((viewModel.uiState as WashingMachineListUiState.Error).errorMessage)
        else ->
            Surface(
                modifier = Modifier
                    .fillMaxSize()
            ) {
                Column {
                    Button(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(8.dp),
                        onClick = onCreateButtonClick
                    ) {
                        Text(text = "Create new")
                    }
                    LazyColumn(
                        modifier = Modifier
                            .fillMaxHeight(),
                        contentPadding = PaddingValues(start = 8.dp, top = 8.dp, end = 8.dp),
                    ) {
                        items(
                            (viewModel.uiState as WashingMachineListUiState.Success).data.items,
//                            Datasource.washingMachines,
                            key = { item -> item.id }
                        ) { washingMachine ->
                            WashingMachineCard(
                                modifier = Modifier
                                    .fillMaxWidth()
                                    .padding(bottom = 8.dp),
                                washingMachine = washingMachine,
                                onDetailsButtonClick = onDetailsButtonClick,
                                onEditButtonClick = onEditButtonClick
                            )
                        }
                    }
                }
            }
    }
}

@Composable
fun WashingMachineCard(
    washingMachine: WashingMachine,
    onDetailsButtonClick: (String) -> Unit,
    onEditButtonClick: (String) -> Unit,
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
            //verticalAlignment = Alignment.CenterVertically,
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
                    onClick = { onDetailsButtonClick("${washingMachine.id}") },
                ) {
                    Text(
                        text = "Details"
                    )
                }

                Button(
                    modifier = Modifier
                        .fillMaxWidth(),
                    onClick = { onEditButtonClick("${washingMachine.id}") },
                ) {
                    Text(
                        text = "Edit"
                    )
                }
            }
        }
    }
}