/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


namespace Net.Dreceiptx.Client
{
    public class UriParameters {
        private Map<string, string> _uriParameters = new HashMap<>();

        public void add( string name, string value){
            _uriParameters.put(name, value);
        }

        public string get(string name){
            return _uriParameters.get(name);
        }

        public string get(string name, string defaultValue)
        {
            if(_uriParameters.containsKey(name)){
                return _uriParameters.get(name);
            }
            return defaultValue;
        }

        public bool has(string name){
            return _uriParameters.containsKey(name);
        }
    
        public Set<Map.Entry<string, string>> getEntrySet(){
            return _uriParameters.entrySet();
        }
    }
}