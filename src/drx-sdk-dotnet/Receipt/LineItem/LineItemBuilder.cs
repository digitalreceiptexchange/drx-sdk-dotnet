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

import net.dreceiptx.receipt.allowanceCharge.ReceiptAllowanceCharge;
import net.dreceiptx.receipt.tax.Tax;
import java.util.Date;

public class LineItemBuilder {
    private StandardLineItem _lineItem;

    public LineItemBuilder(String brand, String name, String description, int quantity, double price) {
        _lineItem = new StandardLineItem(brand,name, description, quantity, price);
    }
    
    public LineItemBuilder(TransactionalTradeItemType type, String code, int quantity, double price) {
        _lineItem = new StandardLineItem(type,code, quantity, price);
    }
    
    public LineItemBuilder addTax(Tax tax) {
        _lineItem.addTax(tax);
        return this;
    }
    
    public LineItemBuilder addGeneralDiscount(double amount, String description) {
        _lineItem.addReceiptAllowanceCharges(ReceiptAllowanceCharge.GeneralDiscount(amount, description));
        return this;
    }

    public LineItemBuilder addGeneralDiscount(double amount, String description, Tax tax) {
        _lineItem.addReceiptAllowanceCharges(ReceiptAllowanceCharge.GeneralDiscount(amount, description, tax));
        return this;
    }
    
    public LineItemBuilder addPackagingFee(double amount, String description) {
        _lineItem.addReceiptAllowanceCharges(ReceiptAllowanceCharge.PackagingFee(amount, description));
        return this;
    }

    public LineItemBuilder addPackagingFee(double amount, String description, Tax tax) {
        _lineItem.addReceiptAllowanceCharges(ReceiptAllowanceCharge.PackagingFee(amount, description, tax));
        return this;
    }
    
    public LineItemBuilder setSerialNumber(String serialNumber) {
        _lineItem.setSerialNumber(serialNumber);
        return this;
    }
    
    public LineItemBuilder setBatchNumber(String batchNumber) {
        _lineItem.setBatchNumber(batchNumber);
        return this;
    }
    
    public LineItemBuilder setBillingCostCentre(String billingCostCentre) {
        _lineItem.setBillingCostCentre(billingCostCentre);
        return this;
    }
    
    public LineItemBuilder setDespatchDate(Date despatchDate){
        _lineItem.setDespatchDate(despatchDate);
        return this;
    }
    
    public LineItemBuilder setDeliveryDate(Date deliveryDate){
        _lineItem.setDeliveryDate(deliveryDate);
        return this;
    }
    
    public LineItemBuilder setDeliveryInstructions(String deliveryInstructions){
        _lineItem.setDeliveryInstructions(deliveryInstructions);
        return this;
    }
    
    public LineItem create(){
        return _lineItem;
    }
}
