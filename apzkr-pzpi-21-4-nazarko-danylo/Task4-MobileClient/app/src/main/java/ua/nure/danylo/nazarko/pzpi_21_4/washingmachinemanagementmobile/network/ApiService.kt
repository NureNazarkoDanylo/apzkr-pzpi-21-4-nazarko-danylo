package ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.network

import okhttp3.ResponseBody
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import retrofit2.http.Streaming
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.PaginatedList
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.TokensModel
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.WashingMachine
import ua.nure.danylo.nazarko.pzpi_21_4.washingmachinemanagementmobile.models.requests.LoginRequest


interface ApiService {

    @POST("washingMachines")
    suspend fun createWashingMachine(@Body washingMachine: WashingMachine)

    @GET("washingMachines")
    suspend fun getWashingMachines(): PaginatedList<WashingMachine>

    @GET("washingMachines/{id}")
    suspend fun getWashingMachine(@Path("id") id: String): WashingMachine

    @PUT("washingMachines")
    suspend fun updateWashingMachine(@Body washingMachine: WashingMachine)

    @DELETE("washingMachines/{id}")
    suspend fun deleteWashingMachine(@Path("id") id: String)


    @GET("washingMachines/discover")
    @Streaming
    suspend fun discoverWashingMachines(): ResponseBody

    @GET("washingMachines/{id}/streamStatus")
    @Streaming
    suspend fun streamWashingMachineOperationStatus(@Path("id") id: String): ResponseBody


    @POST("identity/login")
    suspend fun login(@Body request: LoginRequest): TokensModel

    @POST("identity/renewAccessToken")
    suspend fun renewAccessToken(@Body refreshToken: String,): TokensModel

    @POST("identity/revokeRefreshToken")
    suspend fun revokeRefreshToken(@Body refreshToken: String,)
}