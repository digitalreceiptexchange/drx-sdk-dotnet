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

namespace Net.Dreceiptx.Client.Exceptions
{ 
    public class ExchangeClientException : Exception
    {
        public ExchangeClientException() 
        {
        }

        public ExchangeClientException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ExchangeClientException(int errorCode, string message, Exception innerException) 
            : base (message, innerException)
        {
            ErrorCode = errorCode;
        }

        public ExchangeClientException(Error error) : this(error.ErrorCode, error.Message)
        {
            ErrorCode = error.ErrorCode;
        }

        public ExchangeClientException(Error error, Exception innerException) :this(error.ErrorCode, error.Message, innerException)
        {
        }

        public ExchangeClientException(string message) : base(message)
        {
        }

        public int ErrorCode { get; }
    }
}