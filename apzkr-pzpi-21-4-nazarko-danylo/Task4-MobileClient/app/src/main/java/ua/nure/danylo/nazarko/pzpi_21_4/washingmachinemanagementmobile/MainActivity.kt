package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile

import android.annotation.SuppressLint
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.WindowInsets
import androidx.compose.foundation.layout.asPaddingValues
import androidx.compose.foundation.layout.calculateEndPadding
import androidx.compose.foundation.layout.calculateStartPadding
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.safeDrawing
import androidx.compose.foundation.layout.statusBarsPadding
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Surface
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalLayoutDirection
import androidx.navigation.NavHostController
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.currentBackStackEntryAsState
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument
import dagger.hilt.android.AndroidEntryPoint
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.ApplicationScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.components.CustomBottomAppBar
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.components.CustomTopAppBar
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens.LoginScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens.WashingMachineCreateScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens.WashingMachineDetailsScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens.WashingMachineDiscoveryScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens.WashingMachineEditScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.screens.WashingMachineListScreen
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.ui.theme.WashingMachineManagementMobileTheme

@AndroidEntryPoint
class MainActivity : ComponentActivity() {
    @SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            WashingMachineManagementMobileTheme {
                val layoutDirection = LocalLayoutDirection.current
                val navController: NavHostController = rememberNavController()

                // Get current back stack entry
                val backStackEntry by navController.currentBackStackEntryAsState()
                // Get the name of the current screen
                val currentScreen = ApplicationScreen.valueOf(
                    backStackEntry?.destination?.route?.split('/')?.get(0) ?: ApplicationScreen.WashingMachineList.name
                )

                Surface(
                    modifier = Modifier
                        .fillMaxSize(),
                ) {
                    Scaffold(
                        modifier = Modifier
                            .fillMaxSize()
                            .statusBarsPadding()
                            .padding(
                                start = WindowInsets.safeDrawing
                                    .asPaddingValues()
                                    .calculateStartPadding(layoutDirection),
                                end = WindowInsets.safeDrawing
                                    .asPaddingValues()
                                    .calculateEndPadding(layoutDirection)
                            ),
                        topBar = {
                            CustomTopAppBar(
                                currentScreen = currentScreen,
                                navigateUp = { navController.navigateUp() },
                                canNavigateBack =
                                    navController.previousBackStackEntry != null &&
                                    currentScreen != ApplicationScreen.Login &&
                                    currentScreen != ApplicationScreen.WashingMachineDiscovery

                            )
                        },
                        bottomBar = {
                            CustomBottomAppBar(
                                currentScreen = currentScreen,
                                navigateToDiscoveryScreen = {
                                    navController.navigate(ApplicationScreen.WashingMachineDiscovery.name) {
                                        popUpTo(navController.graph.startDestinationId)
                                        launchSingleTop = true
                                    }
                                },
                                navigateToHomeScreen = {
                                    navController.navigate(ApplicationScreen.WashingMachineList.name) {
                                        popUpTo(navController.graph.startDestinationId)
                                        launchSingleTop = true
                                    }
                                },
                                navigateToSettingsScreen = {
                                    navController.navigate(ApplicationScreen.Login.name){
                                        popUpTo(navController.graph.startDestinationId)
                                        launchSingleTop = true
                                    }
                                },
                            )
                        },
                    ) { innerPadding ->
                        NavHost(
                            navController = navController,
                            startDestination = ApplicationScreen.WashingMachineList.name,
                            modifier = Modifier
                                .padding(innerPadding),
                        ) {
                            composable(route = ApplicationScreen.Login.name) {
                                LoginScreen(
                                    onSuccessfulSubmitCallback = {
                                        navController.navigate(ApplicationScreen.WashingMachineList.name){
                                            popUpTo(navController.graph.startDestinationId)
                                            launchSingleTop = true
                                        }
                                    }
                                )
                            }
                            composable(route = ApplicationScreen.WashingMachineDiscovery.name) {
                                WashingMachineDiscoveryScreen(
                                    onCreateButtonClick =  fun () = navController.navigate(ApplicationScreen.WashingMachineCreate.name)
                                )
                            }
                            composable(route = ApplicationScreen.WashingMachineList.name) {
                                WashingMachineListScreen(
                                    onCreateButtonClick = fun () = navController.navigate(ApplicationScreen.WashingMachineCreate.name),
                                    onEditButtonClick = fun (washingMachineId: String) = navController.navigate("${ApplicationScreen.WashingMachineEdit.name}/${washingMachineId}"),
                                    onDetailsButtonClick = fun (washingMachineId: String) = navController.navigate("${ApplicationScreen.WashingMachineDetails.name}/${washingMachineId}")
                                )
                            }
                            composable(route = ApplicationScreen.WashingMachineCreate.name) {
                                WashingMachineCreateScreen(
                                    onSuccessfulCreateCallback = {
                                        navController.navigate(ApplicationScreen.WashingMachineList.name){
                                            popUpTo(navController.graph.startDestinationId)
                                            launchSingleTop = true
                                        }
                                    }
                                )
                            }
                            composable(
                                route = "${ApplicationScreen.WashingMachineEdit.name}/{washingMachineId}",
                                arguments = listOf(navArgument("washingMachineId") { type = NavType.StringType })
                            ) {backStackEntry ->
                                val washingMachineId = backStackEntry.arguments?.getString("washingMachineId") ?: ""
                                WashingMachineEditScreen(
                                    washingMachineId = washingMachineId,
                                    onSuccessfulDelete = {
                                        navController.navigate(ApplicationScreen.WashingMachineList.name){
                                            popUpTo(navController.graph.startDestinationId)
                                            launchSingleTop = true
                                        }
                                    }
                                )
                            }
                            composable(
                                route = "${ApplicationScreen.WashingMachineDetails.name}/{washingMachineId}",
                                arguments = listOf(navArgument("washingMachineId") { type = NavType.StringType })
                            ) {backStackEntry ->
                                val washingMachineId = backStackEntry.arguments?.getString("washingMachineId") ?: ""
                                WashingMachineDetailsScreen(washingMachineId = washingMachineId)
                            }
                        }
                    }
                }
            }
        }
    }
}