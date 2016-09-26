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

package net.dreceiptx.receipt.lineitem.travel;

import net.dreceiptx.receipt.lineitem.LineItemTypeDescription;

public enum AccommodationType implements LineItemTypeDescription{
    DEFAULT("ACC0000", "Accommodation"),
    HOTEL("ACC0001", "Hotel"),
    HOSTEL("ACC0002", "Hostel"),
    MOTEL("ACC0003", "Motel"),
    BED_AND_BREAKFAST("ACC0004", "Bed and Breakfast"),
    RENTAL("ACC0005", "Rental"),
    PRIVATE_RENTAL("ACC0006", "Private Rental");

    private String code;
    private String description;

    AccommodationType(String code, String description)
    {
        this.code = code;
        this.description = description;
    }

    public String code(){
        return this.code;
    }

    public String description(){
        return this.description;
    }
}
