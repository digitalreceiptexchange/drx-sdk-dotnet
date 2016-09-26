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
using Net.Dreceiptx.Receipt;
using Net.Dreceiptx.Receipt.Merchant;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Client
{
    public interface IExchangeClient {

        /**
         * @param identifierType
         * @param identifier
         * @return The matching users will be returned if it is found otherwise null
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        User SearchUser(UserIdentifierType identifierType, string identifier); 

        /**
         * @param identifierType
         * @param userIdentifiers
         * @return The matching users will be returned if it is found otherwise null
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        Users.Users SearchUsers(UserIdentifierType identifierType, List<string> userIdentifiers);
    
        /**
         * @param receipt
         * @return newReceiptGUID
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        string SendReceipt(DigitalReceiptGenerator receipt);

    
        /**
         * @param newUser
         * @return newUserGUID
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        NewUserRegistrationResult RegisterNewUser(NewUser newUser);
    
        /**
         * @param List<NewUser>
         * @return Dictionary<string, NewUserRegistrationResult>
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        Dictionary<string, NewUserRegistrationResult> RegisterNewUser(List<NewUser> newUsers);
    
        /**
         * @param receiptId
         * @return
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        DigitalReceipt LookupReceipt(string receiptId);

        /**
         * @param receiptId
         * @return
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        bool DownloadReceiptPDF(string receiptId);


        /**
         * @param Id
         * @return
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        Merchant LookupMerchant(string Id);

    }
}