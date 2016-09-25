/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
        User SearchUser(UserIdentifierType identifierType, string identifier) throws ExchangeClientException, UnsupportedEncodingException;

        /**
         * @param identifierType
         * @param userIdentifiers
         * @return The matching users will be returned if it is found otherwise null
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        Users SearchUsers(UserIdentifierType identifierType, ArrayList<string> userIdentifiers) throws ExchangeClientException;
    
        /**
         * @param receipt
         * @return newReceiptGUID
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        string SendReceipt(DigitalReceiptGenerator receipt) throws ExchangeClientException;

    
        /**
         * @param newUser
         * @return newUserGUID
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        NewUserRegistrationResult RegisterNewUser(NewUser newUser) throws ExchangeClientException;
    
        /**
         * @param List<NewUser>
         * @return Map<string, NewUserRegistrationResult>
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        Map<string, NewUserRegistrationResult> RegisterNewUser(List<NewUser> newUsers) throws ExchangeClientException;
    
        /**
         * @param receiptId
         * @return
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        DigitalReceipt LookupReceipt(string receiptId) throws ExchangeClientException;

        /**
         * @param receiptId
         * @return
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        boolean DownloadReceiptPDF(string receiptId) throws ExchangeClientException;


        /**
         * @param Id
         * @return
         * @throws ExchangeClientException will be thrown if problem calling the service.
         */
        Merchant LookupMerchant(string Id) throws ExchangeClientException;

    }
}