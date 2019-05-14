/*
 * IoT Aggregator API
 *
 * Sample API for aggregating data from multiple IoT devices and returning stored running averages.
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Aggregator.Models;
using Aggregator.ServiceClients.Api;
using Aggregator.Services;
using IO.Swagger.Models;
using Polly;
using Swashbuckle.Swagger.Annotations;

namespace Aggregator.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    public sealed class DefaultApiController : Controller
    {
        private readonly IStore _store;
        private readonly ILogger _logger;
        private readonly ITemperatureHistorian _historian;
            
        public DefaultApiController(IStore store, ILogger<DefaultApiController> logger, ITemperatureHistorian historian)
        {
            this._store = store;
            this._logger = logger;
            this._historian = historian;
        }
        /// <summary>
        /// Add data generated from a device to the aggregator
        /// </summary>
        /// <remarks>Adds a data point from an IoT device. The aggregator selects the historian service, posts data to it, and receives the running average. Then updates its store for the history of running averages by device id and type.</remarks>
        /// <param name="deviceType">Device type</param>
        /// <param name="deviceId">Device ID</param>
        /// <param name="dataPointId">Each data point needs to have a unique ID</param>
        /// <param name="value">Value registered by the device.</param>
        /// <response code="201">Data added successfully.</response>
        /// <response code="401">Invalid input parameter.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpPost]
        [Route("/v1/deviceData/{deviceType}/{deviceId}")]
        //[ValidateModelState]
        [SwaggerOperation("AddDeviceData")]
        [SwaggerResponse(statusCode: 401, type: typeof(Error), description: "Invalid input parameter.")]
        [SwaggerResponse(statusCode: 500, type: typeof(Error), description: "An unexpected error occurred.")]
        public IActionResult AddDeviceData([FromRoute][Required]string deviceType, [FromRoute][Required][RegularExpression("/^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/")][StringLength(36, MinimumLength=36)]string deviceId, [FromQuery][Required()][RegularExpression("/^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/")][StringLength(36, MinimumLength=36)]string dataPointId, [FromQuery][Required()]float? value)
        {
            if (!deviceType.Equals("TEMP"))
            {
                this._logger.LogError($"Device Type {deviceType} is not supported. ");
                return BadRequest($"Unsupported device type {deviceType}");
            }

            float? averageValue = default(float?);

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetry(5, retryAttemp =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)));

            averageValue = retryPolicy.Execute(() =>
                this._historian.AddDeviceData(deviceId, dataPointId, DateTimeOffset.UtcNow.DateTime, value));

            if (!averageValue.HasValue)
            {
                var message = $"Cannot calculate the average";
                this._logger.LogError(message);
                return BadRequest(message);
            }

            var key = $"{deviceType}; {deviceId}";

            if (this._store.Exists(key))
            {
                this._logger.LogInformation($"Updating {key} with {averageValue.Value}");
                this._store.Update(key, averageValue.Value);
            }
            else
            {
                this._logger.LogInformation($"Added {key} with {averageValue.Value}");
                this._store.Add(key, averageValue.Value);
            }

            return Ok(averageValue.Value);
        }

        /// <summary>
        /// Get the running averages of a device type given a date range.
        /// </summary>
        /// <remarks>Returns the running average of a device type given a date range, averaged by the minute.</remarks>
        /// <param name="deviceType">Device type</param>
        /// <param name="fromTime">Start of the date range.</param>
        /// <param name="toTime">End of the date range.</param>
        /// <response code="200">Running averages per minute</response>
        /// <response code="400">Invalid input parameter.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet]
        [Route("/v1/averageByDeviceType/{deviceType}")]
        //[ValidateModelState]
        [SwaggerOperation("AverageByDeviceTypeDeviceTypeGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(DeviceDataPoints), description: "Running averages per minute")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "Invalid input parameter.")]
        [SwaggerResponse(statusCode: 500, type: typeof(Error), description: "An unexpected error occurred.")]
        public IActionResult AverageByDeviceTypeDeviceTypeGet([FromRoute][Required]string deviceType, [FromQuery][Required()]DateTime? fromTime, [FromQuery][Required()]DateTime? toTime)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(DeviceDataPoints));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(500, default(Error));

            string exampleJson = null;
            exampleJson = "\"\"";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<DeviceDataPoints>(exampleJson)
            : default(DeviceDataPoints);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
