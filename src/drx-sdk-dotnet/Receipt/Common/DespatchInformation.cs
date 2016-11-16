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
using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.Common
{
    [DataContract]
    public class DespatchInformation
    {
        public DespatchInformation()
        {
        }

        public DespatchInformation(DateTime deliveryDate)
            : this(deliveryDate, null)
        {
        }

        public DespatchInformation(DateTime deliveryDate, string instructions) :
            this(deliveryDate, instructions, DateTime.MinValue)
        {
        }

        public DespatchInformation(DateTime deliveryDate, string instructions, DateTime despatchDate)
        {
            DeliveryDate = deliveryDate;
            DespatchDateTime = despatchDate;
            DeliveryInstructions = instructions;
        }

        [DataMember(Name = "EstimatedDeliveryDateTime")]
        public DateTime DeliveryDate { get; set; }

        [DataMember]
        public DateTime DespatchDateTime { get; set; }

        [DataMember]
        public string DeliveryInstructions { get; set; }

        //TODO: Remove this
        public bool gsonValidator()
        {
            return DeliveryInstructions != null || DespatchDateTime != null || DeliveryDate != null;

        }
    }
}