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

package net.dreceiptx.receipt.settlement;

public class PaymentAuthorisation {
    private String _paymentProvider;
    private String _authorisationNumber;
    private String _authorisationCode;

    public PaymentAuthorisation(String paymentProvider, String authorisationNumber,
                                String authorisationCode)
    {
        _paymentProvider = paymentProvider;
        _authorisationNumber = authorisationNumber;
        _authorisationCode = authorisationCode;
    }

    public String getPaymentProvider() {
        return _paymentProvider;
    }

    public void setPaymentProvider(String paymentProvider) {
        _paymentProvider = paymentProvider;
    }

    public String getAuthorisationNumber() {
        return _authorisationNumber;
    }

    public void setAuthorisationNumber(String authorisationNumber) {
        _authorisationNumber = authorisationNumber;
    }

    public String getAuthorisationCode() {
        return _authorisationCode;
    }

    public void setAuthorisationCode(String authorisationCode) {
        _authorisationCode = authorisationCode;
    }
}
