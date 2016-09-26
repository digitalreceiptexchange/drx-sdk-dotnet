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
package net.dreceiptx.receipt.lineitem;

import java.util.Dictionary;
import java.util.Dictionary;
import java.util.Set;

public class TradeItemIdentification {
    private Dictionary<string, string> _additionalTradeItemIdentification = new Dictionary<string, string>();
    
    public void add( string code, string value){
        _additionalTradeItemIdentification.put(code, value);
    }
    
    public string get(string code){
        return this.get(code, null);
    }
    
    public string get(string code, string defaultValue){
        if(_additionalTradeItemIdentification.containsKey(code)){
            return _additionalTradeItemIdentification.get(code);
        }else{
            return defaultValue;
        }
    }

    public bool has(string code){
        return this._additionalTradeItemIdentification.containsKey(code);
    }
    
    public Set<Dictionary.Entry<string, string>> getEntrySet(){
        return _additionalTradeItemIdentification.entrySet();
    }
}
