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

using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.LineItem.Travel
{
    public enum GroundTransportType
    {
        [DrxEnumExtendedInformation("GTP0000", "Transportation")] DEFAULT,
        [DrxEnumExtendedInformation("GTP0001", "Taxi")] TAXI,
        [DrxEnumExtendedInformation("GTP0002", "Train")] TRAIN,
        [DrxEnumExtendedInformation("GTP0003", "Bus")] BUS,
        [DrxEnumExtendedInformation("GTP0004", "Ride Sharing")] RIDE_SHARING,
        [DrxEnumExtendedInformation("GTP0005", "Car Sharing")] CAR_POOLING,
        [DrxEnumExtendedInformation("GTP0006", "Car Rental")] CAR_RENTAL,
        [DrxEnumExtendedInformation("GTP0007", "Private Car Rental")] PRIVATE_CAR_RENTAL
    }
}
