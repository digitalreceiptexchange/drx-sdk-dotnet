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

package net.dreceiptx.receipt.common;

import com.google.gson.annotations.SerializedName;
import java.util.Date;

public class DespatchInformation {
    @SerializedName("estimatedDeliveryDateTime") private Date _deliveryDate;
    @SerializedName("despatchDateTime") private Date _despatchDate;
    @SerializedName("deliveryInstructions") private String _instructions;

    public DespatchInformation(){
    }
    
    public DespatchInformation(Date deliveryDate){
        _deliveryDate = deliveryDate;
    }
    
    public DespatchInformation(Date deliveryDate, String instructions){
        _deliveryDate = deliveryDate;
        _instructions = instructions;
    }
    
    public DespatchInformation(Date deliveryDate, Date despatchDate, String instructions){
        _deliveryDate = deliveryDate;
        _despatchDate = despatchDate;
        _instructions = instructions;
    }

    public Date getDeliveryDate() {
        return _deliveryDate;
    }
    
    public void setDeliveryDate(Date deliveryDate) {
         _deliveryDate = deliveryDate;
    }
    
    public Date getDespatchDate() {
        return _despatchDate;
    }
    
    public void setDespatchDate(Date despatchDate) {
         _despatchDate = despatchDate;
    }

    public String getDeliveryInstructions() {
        return _instructions;
    }
    
    public void setInstructions(String instructions) {
         _instructions = instructions;
    }
    
    public boolean gsonValidator(){
        return _instructions != null || _despatchDate != null || _deliveryDate != null;

    }
}
