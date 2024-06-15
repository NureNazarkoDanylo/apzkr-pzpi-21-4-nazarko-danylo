package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.utils

import android.content.Context
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.TokensModel
import javax.inject.Inject

class UserStore @Inject constructor(
    private val context: Context
) {
    companion object {
        private val Context.dataStore: DataStore<Preferences> by preferencesDataStore("userTokens")
        private val ACCESS_TOKEN_KEY = stringPreferencesKey("accessToken")
        private val REFRESH_TOKEN_KEY = stringPreferencesKey("refreshToken")
    }

    val getTokens: Flow<TokensModel> = context.dataStore.data.map { preferences ->
        TokensModel(
            preferences[ACCESS_TOKEN_KEY] ?: "",
            preferences[REFRESH_TOKEN_KEY] ?: ""
        )
    }

    suspend fun saveTokens(tokens: TokensModel) {
        context.dataStore.edit { preferences ->
            preferences[ACCESS_TOKEN_KEY] = tokens.accessToken
            preferences[REFRESH_TOKEN_KEY] = tokens.refreshToken
        }
    }
}
