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

package net.dreceiptx.receipt.ecom;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

public class AVPList {
    private Map<String, AVP> _avpList = new HashMap<String, AVP>();

    public void add( String code, AVP avp){
        _avpList.put(code, avp);
    }

    public void add(AVP avp){
        _avpList.put(avp.getAttributeName(), avp);
    }

    public void add( String code, String value){
        _avpList.put(code, new AVP(code, value));
    }
    
    public AVP get(String code){
        return this.get(code, null);
    }
    
    public AVP get(String code, AVP defaultValue){
        if(_avpList.containsKey(code)){
            return _avpList.get(code);
        }else{
            return defaultValue;
        }
    }

    public String getValue(String code){
        return this.getValue(code, null);
    }

    public String getValue(String code, String defaultValue){
        if(_avpList.containsKey(code)){
            return _avpList.get(code).getValue();
        }else{
            return defaultValue;
        }
    }

    public boolean has(String code){
        return this._avpList.containsKey(code);
    }
    
    public Set<Map.Entry<String, AVP>> getEntrySet(){
        return _avpList.entrySet();
    }
}
