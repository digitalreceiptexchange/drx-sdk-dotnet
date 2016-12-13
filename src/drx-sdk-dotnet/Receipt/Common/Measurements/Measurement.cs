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

using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.Common.Measurements
{
    [DataContract]
    public class Measurement
    {
        public Measurement()
        { }

        public Measurement(double value, MeasurementType measurementType)
        {
            Value = value;
            MeasurementType = measurementType;
        }

        /// <summary>
        /// Gets and sets the MeasurementType
        /// </summary>
        [DataMember(Name = "MeasurementUnitCode")]
        public MeasurementType MeasurementType { get; set; }

        /// <summary>
        /// Gets and sets the Value of the measurement
        /// </summary>
        [DataMember]
        public double Value { get; set; }

        public override string ToString()
        {
            return $"{Value} {MeasurementType.Description()}";
        }
    }
}
