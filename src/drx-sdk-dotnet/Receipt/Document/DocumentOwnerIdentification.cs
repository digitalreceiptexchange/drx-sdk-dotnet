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
namespace Net.Dreceiptx.Receipt.Document
{
    public class DocumentOwnerIdentification
    {
        //@SerializedName("authority")
        /// <summary>
        /// Gets and sets the Authority agency of the identification key.
        /// The "Authority" attribute, will be either set to GS1 or dRx
        /// </summary>
        public string Authority { get; set; }
        
        //@SerializedName("value")
        public string Value { get; set; }
    }
}