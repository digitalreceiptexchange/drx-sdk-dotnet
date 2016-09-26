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

public enum GroundTransportType implements LineItemTypeDescription {
    DEFAULT("GTP0000", "Transportation"),
    TAXI("GTP0001", "Taxi"),
    TRAIN("GTP0002", "Train"),
    BUS("GTP0003", "Bus"),
    RIDE_SHARING("GTP0004", "Ride Sharing"),
    CAR_POOLING("GTP0005", "Car Sharing"),
    CAR_RENTAL("GTP0006", "Car Rental"),
    PRIVATE_CAR_RENTAL("GTP0007", "Private Car Rental");

    private String code;
    private String description;

    GroundTransportType(String code, String description)
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
