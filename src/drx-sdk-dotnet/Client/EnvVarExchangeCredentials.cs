#region copyright
// Copyright 2016 Digital Receipt Exchange Limited
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
using System;
using Net.Dreceiptx.Client.Exceptions;

namespace Net.Dreceiptx.Client
{
    /// <summary>
    /// The EnvVarExchangeCredentials is a simply mechanism to create 
    /// ExchangeCredentials from Environment Variables.
    /// </summary>
    public class EnvVarExchangeCredentials : IExchangeCredentials
    {
        /// <summary>
        /// Creates instance using the credentials from environment variables.The following
        /// environment varaibles MUST be supplied otherwise the ExchangeClientException will
        /// be thrown:
        /// <ul>
        ///     <li>DRX_REQUESTER_ID</li>
        ///     <li>DRX_API_KEY</li>
        ///     <li>DRX_API_SECRET</li>
        /// </ul>
        /// </summary>
        public EnvVarExchangeCredentials()
        {
            RequesterId = GetEnvVarOrThrow("DRX_REQUESTER_ID");
            APIKey = GetEnvVarOrThrow("DRX_API_KEY");
            APISecret = GetEnvVarOrThrow("DRX_API_SECRET");
        }

        private string GetEnvVarOrThrow(String envVar) 
        {
            String value = Environment.GetEnvironmentVariable(envVar);
            if(string.IsNullOrWhiteSpace(value))
            { 
                throw new ExchangeClientException($"Environment variable {envVar} can not be null or empty");
            }
            return value;
        }

        public string RequesterId { get; }
        public string APIKey { get; }
        public string APISecret { get; }
    }
}
