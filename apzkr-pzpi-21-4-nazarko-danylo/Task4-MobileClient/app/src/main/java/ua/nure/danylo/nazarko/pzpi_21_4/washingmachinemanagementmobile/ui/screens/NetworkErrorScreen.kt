package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun NetworkErrorScreenPreviewLightTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = false,
    ) {
        NetworkErrorScreen()
    }
}

@Preview(
    showBackground = true,
    showSystemUi = true,
)
@Composable
fun NetworkErrorScreenPreviewDarkTheme() {
    WashingMachineManagementMobileTheme(
        dynamicColor = false,
        darkTheme = true,
    ) {
        NetworkErrorScreen()
    }
}

@Composable
fun NetworkErrorScreen(
    errorMessage: String = ""
) {
    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(16.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            Text(
                modifier = Modifier
                    .padding(bottom = 16.dp),
                text = "Error loading network data",
                fontWeight = FontWeight.Bold,
                fontSize = 24.sp
            )

            Text(text = errorMessage)
        }
    }
}