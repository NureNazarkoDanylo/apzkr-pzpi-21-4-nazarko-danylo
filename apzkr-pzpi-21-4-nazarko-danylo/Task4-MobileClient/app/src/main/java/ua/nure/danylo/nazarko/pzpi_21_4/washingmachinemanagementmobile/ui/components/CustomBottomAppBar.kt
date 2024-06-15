package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.components

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Home
import androidx.compose.material.icons.filled.Search
import androidx.compose.material.icons.filled.Settings
import androidx.compose.material3.BottomAppBar
import androidx.compose.material3.FilledIconButton
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.ApplicationScreen

@Composable
fun CustomBottomAppBar(
    currentScreen: ApplicationScreen,
    navigateToDiscoveryScreen: () -> Unit,
    navigateToHomeScreen: () -> Unit,
    navigateToSettingsScreen: () -> Unit,
    modifier: Modifier = Modifier,
) {
    BottomAppBar(
        actions = {
            Row(
                modifier = Modifier
                    .fillMaxWidth(),
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.SpaceEvenly,
            ) {
                if (currentScreen == ApplicationScreen.WashingMachineDiscovery) {
                    FilledIconButton(onClick = {}) {
                        Icon(
                            Icons.Filled.Search,
                            contentDescription = "Discovery screen button",
                        )
                    }
                } else {
                    IconButton(onClick = navigateToDiscoveryScreen) {
                        Icon(
                            Icons.Filled.Search,
                            contentDescription = "Discovery screen button",
                        )
                    }
                }

                if (currentScreen == ApplicationScreen.WashingMachineList) {
                    FilledIconButton(onClick = {}) {
                        Icon(
                            Icons.Filled.Home,
                            contentDescription = "Home screen button",
                        )
                    }
                } else {
                    IconButton(onClick = navigateToHomeScreen) {
                        Icon(
                            Icons.Filled.Home,
                            contentDescription = "Home screen button",
                        )
                    }
                }

                if (currentScreen == ApplicationScreen.Login) {
                    FilledIconButton(onClick = {}) {
                        Icon(
                            Icons.Filled.Settings,
                            contentDescription = "Settings screen button",
                        )
                    }
                } else {
                    IconButton(onClick = navigateToSettingsScreen) {
                        Icon(
                            Icons.Filled.Settings,
                            contentDescription = "Settings screen button",
                        )
                    }
                }
            }
        }
    )
}
