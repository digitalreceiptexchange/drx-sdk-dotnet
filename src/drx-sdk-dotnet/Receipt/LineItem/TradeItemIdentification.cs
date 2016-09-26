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

package net.dreceiptx.receipt.lineitem;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

public class TradeItemIdentification {
    private Map<String, String> _additionalTradeItemIdentification = new HashMap<String, String>();
    
    public void add( String code, String value){
        _additionalTradeItemIdentification.put(code, value);
    }
    
    public String get(String code){
        return this.get(code, null);
    }
    
    public String get(String code, String defaultValue){
        if(_additionalTradeItemIdentification.containsKey(code)){
            return _additionalTradeItemIdentification.get(code);
        }else{
            return defaultValue;
        }
    }

    public boolean has(String code){
        return this._additionalTradeItemIdentification.containsKey(code);
    }
    
    public Set<Map.Entry<String, String>> getEntrySet(){
        return _additionalTradeItemIdentification.entrySet();
    }
}
