using System;
using Aggregator.ServiceClients.Client;

namespace Aggregator.ServiceClients.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ITemperatureHistorian : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Add data to the device history
        /// </summary>
        /// <remarks>
        /// Adds a data point from an IoT device. Once saved, calculates the running average of the existing data, saves it idempotentently and returns it.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">Device Id</param>
        /// <param name="datapointId">Each data point needs to have a unique ID</param>
        /// <param name="timestamp">Timestamp when received from the device.</param>
        /// <param name="value">Value registered by the device.</param>
        /// <returns>float?</returns>
        float? AddDeviceData (string deviceId, string datapointId, DateTime? timestamp, float? value);

        /// <summary>
        /// Add data to the device history
        /// </summary>
        /// <remarks>
        /// Adds a data point from an IoT device. Once saved, calculates the running average of the existing data, saves it idempotentently and returns it.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">Device Id</param>
        /// <param name="datapointId">Each data point needs to have a unique ID</param>
        /// <param name="timestamp">Timestamp when received from the device.</param>
        /// <param name="value">Value registered by the device.</param>
        /// <returns>ApiResponse of float?</returns>
        ApiResponse<float?> AddDeviceDataWithHttpInfo (string deviceId, string datapointId, DateTime? timestamp, float? value);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Add data to the device history
        /// </summary>
        /// <remarks>
        /// Adds a data point from an IoT device. Once saved, calculates the running average of the existing data, saves it idempotentently and returns it.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">Device Id</param>
        /// <param name="datapointId">Each data point needs to have a unique ID</param>
        /// <param name="timestamp">Timestamp when received from the device.</param>
        /// <param name="value">Value registered by the device.</param>
        /// <returns>Task of float?</returns>
        System.Threading.Tasks.Task<float?> AddDeviceDataAsync (string deviceId, string datapointId, DateTime? timestamp, float? value);

        /// <summary>
        /// Add data to the device history
        /// </summary>
        /// <remarks>
        /// Adds a data point from an IoT device. Once saved, calculates the running average of the existing data, saves it idempotentently and returns it.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">Device Id</param>
        /// <param name="datapointId">Each data point needs to have a unique ID</param>
        /// <param name="timestamp">Timestamp when received from the device.</param>
        /// <param name="value">Value registered by the device.</param>
        /// <returns>Task of ApiResponse (float?)</returns>
        System.Threading.Tasks.Task<ApiResponse<float?>> AddDeviceDataAsyncWithHttpInfo (string deviceId, string datapointId, DateTime? timestamp, float? value);
        #endregion Asynchronous Operations
    }
}