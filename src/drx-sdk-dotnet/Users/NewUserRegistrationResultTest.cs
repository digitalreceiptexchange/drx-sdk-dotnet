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

namespace Net.Dreceiptx.Users
{
    public class NewUserRegistrationResultTest
    {
        private bool _success;
        private int _code;
        private string _message;
        private string _userGUID;

        public string UserGUID 
        {
            get
            {
                return _userGUID;
            }
            set
            {
                _success = true;
                _code = 201;
                _userGUID = value;
            }
        }


        public void SetException(int code, string message)
        {
            _success = false;
            _code = code;
            _message = message;
        }

        public bool Success => _success;

        public int Code => _code;

        public string Message => _message;
    }

}