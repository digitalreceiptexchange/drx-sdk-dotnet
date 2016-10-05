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

using System.Collections.Generic;

namespace Net.Dreceiptx.Receipt.Validation
{
    public class ReceiptValidation
    {
        private readonly List<string> _errors = new List<string>();
        private bool _isValid;

        public ReceiptValidation()
        {
            _isValid = true;
        }

        public bool IsValid => _isValid;

        public List<string> Errors => _errors;

        public void AddError(string error)
        {
            _isValid = false;
            _errors.Add(error);
        }
    }
}