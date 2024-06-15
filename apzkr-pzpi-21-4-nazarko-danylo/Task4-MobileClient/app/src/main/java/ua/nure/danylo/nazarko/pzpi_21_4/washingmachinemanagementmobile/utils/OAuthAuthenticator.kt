package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.utils

import android.util.Log
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.runBlocking
import okhttp3.Authenticator
import okhttp3.Request
import okhttp3.Response
import okhttp3.Route
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.TokensModel
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import javax.inject.Inject

class OAuthAuthenticator @Inject constructor(
    private val userStore: UserStore,
    private val apiService: ApiService
) : Authenticator {
    override fun authenticate(route: Route?, response: Response): Request? {
        Log.d("MyLog", "Auth header: ${if (response.request.header("Authorization") == null) "null" else if (response.request.header("Authorization").equals("")) "empty" else response.request.header("Authorization")}")
        if (response.request.header("Authorization") == null ||
            response.request.header("Authorization")!!.isEmpty()) {
            val credentials = runBlocking { getTokensModel() }

            // NOTE: this can be improved by checking the expiration date locally instead of
            // sending a request to the API which will result in a 401.

            // Adding the access token to the request.
            return response.request.newBuilder()
                .header("Authorization", "Bearer ${credentials.accessToken}")
                .build()
        }

        if (response.code == 401) {
            // The access token is expired. Refresh the credentials.
            synchronized(this) {
                // Make sure only one coroutine refreshes the token at a time.
                return runBlocking {
                    try {
                        val newTokensModel = refreshTokensModel()

                        // Update the access token in your storage.
                        updateTokensModel(newTokensModel)

                        return@runBlocking response.request.newBuilder()
                            .header("Authorization", "Bearer ${newTokensModel.accessToken}")
                            .build()
                    } catch (e: Exception) {
                        // The refresh process failed. Give up on retrying the request.
                        return@runBlocking null
                    }
                }
            }
        }

        // Use the authenticated original request.
        return response.request
    }

    private suspend fun getTokensModel(): TokensModel {
        val tokens = userStore.getTokens.first()
        Log.d("MyLog", "get Access Token: ${tokens.accessToken}")
        return tokens
    }

    private suspend fun updateTokensModel(tokens: TokensModel) {
        Log.d("MyLog", "update Access Token: ${tokens.accessToken}")
        userStore.saveTokens(tokens)
    }

    private suspend fun refreshTokensModel(): TokensModel {
        val tokens = userStore.getTokens.first()
        Log.d("MyLog", "refresh Access Token: ${tokens.accessToken}")
        val newTokens = apiService.renewAccessToken(tokens.refreshToken)
        return newTokens
    }
}
