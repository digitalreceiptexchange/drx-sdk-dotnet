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
using System.Collections.Generic;
using Net.Dreceiptx.Users;
using DigitalReceipt = Net.Dreceiptx.Receipt.Serialization.Json.DigitalReceipt;

namespace Net.Dreceiptx.Client
{
    public interface IExchangeClient
    {
        /// <summary>
        /// Searches for a user matching the given UserIdentifierType and identifier
        /// </summary>
        /// <param name="identifierType">The UserIdentifierType to be searched</param>
        /// <param name="identifier">The identifier of the identifierType</param>
        /// <returns>A User will be returned if one can be found matching the given identifierType and
        /// identifier. If no User can be found matching then null will be returned</returns>
        User SearchUser(UserIdentifierType identifierType, string identifier); 

        /// <summary>
        /// Searches for a list of of users matching the given identifierType.
        /// </summary>
        /// <param name="identifierType">The UserIdentifierType of the users being searched</param>
        /// <param name="userIdentifiers">The list of user identifiers to be searched</param>
        /// <returns>A List of User instances will be returned for each found in the exchange</returns>
        List<User> SearchUsers(UserIdentifierType identifierType, List<string> userIdentifiers);
    
        /// <summary>
        /// Sends the receipt to the exchange. 
        /// </summary>
        /// <param name="receipt">The receipt to be sent to the exchange</param>
        /// <returns>The receipt identifier will be returned</returns>
        string SendReceipt(DigitalReceipt receipt);


        /// <summary>
        /// Registers the give NewUser instance in the exchange.
        /// </summary>
        /// <param name="newUser">The user to be created</param>
        /// <returns>The returned NewUserRegistrationExchangeResponse message indicates success or failure.
        /// You must inspect the object to determine if your request was successful or not.
        /// </returns>
        NewUserRegistrationExchangeResponse RegisterNewUser(NewUser newUser);

        /**
         * @param List<NewUser>
         * @return Dictionary<string, NewUserRegistrationResult>
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        NewUserRegistrationExchangeResponse RegisterNewUser(List<NewUser> newUsers);
    }
}