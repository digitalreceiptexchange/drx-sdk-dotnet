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

package net.dreceiptx.receipt.validation;

import java.util.ArrayList;
import java.util.List;

public class ReceiptValidation {
    private final List<String> _errors = new ArrayList<String>();
    private boolean _isValid;

    public ReceiptValidation() {
        _isValid = true;
    }

    public boolean isValid() {
        return _isValid;
    }

    public List<String> getErrors() {
        return _errors;
    }

    public void AddError(String error) {
        _isValid = false;
        _errors.add(error);
    }
}
