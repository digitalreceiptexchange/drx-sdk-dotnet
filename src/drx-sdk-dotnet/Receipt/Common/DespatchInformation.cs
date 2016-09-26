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

namespace Net.Dreceiptx.Receipt.Common
{
    public class DespatchInformation
    {
        //@SerializedName("estimatedDeliveryDateTime")
        private DateTime _deliveryDate;
        //@SerializedName("despatchDateTime")
        private DateTime _despatchDate;
        //@SerializedName("deliveryInstructions")

        public DespatchInformation()
        {
        }

        public DespatchInformation(DateTime deliveryDate)
        {
            _deliveryDate = deliveryDate;
        }

        public DespatchInformation(DateTime deliveryDate, string instructions)
        {
            _deliveryDate = deliveryDate;
            DeliveryInstructions = instructions;
        }

        public DespatchInformation(DateTime deliveryDate, DateTime despatchDate, string instructions)
        {
            _deliveryDate = deliveryDate;
            _despatchDate = despatchDate;
            DeliveryInstructions = instructions;
        }

        public DateTime DeliveryDate { get; set; }

        public DateTime DespatchDate { get; set; }

        public string DeliveryInstructions { get; set; }

        //TODO: Remove this
        public bool gsonValidator()
        {
            return DeliveryInstructions != null || _despatchDate != null || _deliveryDate != null;

        }
    }
}