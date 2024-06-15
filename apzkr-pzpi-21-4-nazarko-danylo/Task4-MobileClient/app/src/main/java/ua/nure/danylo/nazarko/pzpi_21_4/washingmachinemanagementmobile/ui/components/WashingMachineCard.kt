package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.components

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.IntrinsicSize
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.data.Datasource
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme

@Preview(
    showBackground = true
)
@Composable
fun WashingMachineCardPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false
    ) {
        WashingMachineCard(washingMachine = Datasource.washingMachines.first())
    }
}

@Preview(
    showBackground = true
)
@Composable
fun WashingMachineCardPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true
    ) {
        WashingMachineCard(washingMachine = Datasource.washingMachines.first())
    }
}

@Composable
fun WashingMachineCard(
    washingMachine: WashingMachine,
    onDetailsButtonClick: (String) -> Unit = {},
    onEditButtonClick: (String) -> Unit = {},
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
