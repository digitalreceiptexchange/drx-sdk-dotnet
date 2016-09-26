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
package net.dreceiptx.receipt.lineitem.travel;

import net.dreceiptx.receipt.lineitem.LineItemTypeDescription;

public enum FlightType implements LineItemTypeDescription{
    DEFAULT("FLT0000", "Flight"),
    COMMERCIAL("FLT0001", "Commercial"),
    PRIVATE("FLT0002", "Private");

    private string code;
    private string description;

    FlightType(string code, string description)
    {
        this.code = code;
        this.description = description;
    }

    public string code(){
        return this.code;
    }

    public string description(){
        return this.description;
    }
}
