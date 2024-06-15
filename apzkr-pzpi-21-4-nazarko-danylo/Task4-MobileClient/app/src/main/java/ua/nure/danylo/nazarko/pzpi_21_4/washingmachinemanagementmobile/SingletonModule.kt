package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile

import android.content.Context
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.preferencesDataStore
import com.jakewharton.retrofit2.converter.kotlinx.serialization.asConverterFactory
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import kotlinx.serialization.json.Json
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network.ApiService
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.utils.OAuthAuthenticator
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.utils.UserStore
import javax.inject.Singleton

val Context.dataStore: DataStore<Preferences> by preferencesDataStore(name = "data_store")

@Module
@InstallIn(SingletonComponent::class)
class SingletonModule {

    @Singleton
    @Provides
    fun provideUserStore(@ApplicationContext context: Context): UserStore = UserStore(context)

    @Singleton
    @Provides
    fun provideOkHttpClient(
        authAuthenticator: OAuthAuthenticator,
    ): OkHttpClient {
//        val loggingInterceptor = HttpLoggingInterceptor()
//        loggingInterceptor.level = HttpLoggingInterceptor.Level.BODY

        return OkHttpClient.Builder()
            .authenticator(authAuthenticator)
            .build()
    }

    @Singleton
    @Provides
    fun provideAuthAuthenticator(
        userStore: UserStore,
        retrofit: Retrofit.Builder
    ): OAuthAuthenticator =
        OAuthAuthenticator(
            userStore,
            retrofit
                .build()
                .create(ApiService::class.java)
        )

    private val json = Json {
        ignoreUnknownKeys = true
    }

    @Singleton
    @Provides
    fun provideRetrofitBuilder(): Retrofit.Builder =
        Retrofit.Builder()
            .baseUrl("http://10.0.2.2:5000")
            .addConverterFactory(json.asConverterFactory("application/json".toMediaType()))

    @Singleton
    @Provides
    fun provideApiService(
        retrofit: Retrofit.Builder,
        okHttpClient: OkHttpClient
    ): ApiService =
        retrofit
            .client(okHttpClient)
            .build()
            .create(ApiService::class.java)
}