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

namespace Net.Dreceiptx.Receipt.Common.Measurements
{
    [DataContract]
    public class TradeItemMeasurements
    {
        public TradeItemMeasurements()
        { }

        public TradeItemMeasurements(double netContent, MeasurementType measurementType)
        {
            NetContent = new Measurement(netContent, measurementType);
        }

        public TradeItemMeasurements(double height, double width, double depth, MeasurementType measurementType)
        {
            Height = new Measurement(height, measurementType);
            Width = new Measurement(width, measurementType);
            Depth = new Measurement(depth, measurementType);
        }


        /// <summary>
        /// Gets and sets the Depth
        /// </summary>
        [DataMember]
        public Measurement Depth { get; set; }

        /// <summary>
        /// Gets and sets the Height
        /// </summary>
        [DataMember]
        public Measurement Height { get; set; }

        /// <summary>
        /// Gets and sets the Width
        /// </summary>
        [DataMember]
        public Measurement Width { get; set; }

        /// <summary>
        /// Gets and sets the Diameter
        /// </summary>
        [DataMember]
        public Measurement Diameter { get; set; }

        /// <summary>
        /// Gets and sets the NetContent.
        /// </summary>
        [DataMember]
        public Measurement NetContent { get; set; }

        public void SetMeasurements(double height, double width, double depth, MeasurementType measurementType)
        {
            Height = Set(height, measurementType, Height);
            Width = Set(width, measurementType, Width);
            Depth = Set(depth, measurementType, Depth);
        }

        private Measurement Set(double value, MeasurementType measurementType, Measurement measurement)
        {
            if (measurement == null)
            {
                measurement = new Measurement();
            }
            measurement.Value = value;
            measurement.MeasurementType = measurementType;
            return measurement;
        }
    }
}
