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
namespace Net.Dreceiptx.Receipt.Ecom
{
    public class AVP
    {
        public AVP(string name, string value)
        {
            AttributeName = name;
            Value = value;
        }

        //@SerializedName("attributeName")
        public string AttributeName { get; set; }

        //@SerializedName("qualifierCodeList")
        public string CodeList { get; set; } = null;

        //@SerializedName("qualifierCodeListVersion")
        public string CodeListVersion { get; set; } = null;

        //@SerializedName("qualifierCodeName")
        public string CodeName { get; set; } = null;

        //@SerializedName("value")
        public string Value { get; set; } = null;
    }

}