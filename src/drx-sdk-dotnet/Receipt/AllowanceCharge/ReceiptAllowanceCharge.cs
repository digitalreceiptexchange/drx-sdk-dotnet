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
package net.dreceiptx.receipt.allowanceCharge;

import net.dreceiptx.receipt.tax.Tax;
import net.dreceiptx.receipt.tax.TaxCode;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.List;

public class ReceiptAllowanceCharge {
    private transient int Id;
    @SerializedName("allowanceOrChargeType") private AllowanceOrChargeType _allowanceOrChargeType;
    @SerializedName("allowanceChargeType") private AllowanceChargeType _allowanceChargeType;
    @SerializedName("settlementType") private SettlementType _settlementType;
    @SerializedName("baseAmount") private double _amount;
    @SerializedName("allowanceChargeDescription") private string _description;
    @SerializedName("leviedDutyFeeTax") private List<Tax> _taxes  = new ArrayList<Tax>();;
    
    public double getId() {
        return Id;
    }
    
    public SettlementType getType() {
        return _settlementType;
    }
    
    public double getSubTotal() {
        return _amount;
    }

    public string getDescription() {
        return _description;
    }
    
    public double getNetTotal() {
        return _amount;
    }
    
    public double getTotal() {
        double total = _amount;
        total = total + this.getTaxesTotal();
        return total;
    }
    
    public bool hasTaxes() {
        return !_taxes.isEmpty();
    }

    public double getTaxesTotal() {
        Double totalTaxes = 0.0;
        for (Tax tax : _taxes) {
            totalTaxes =+ tax.getTaxTotal();
        }
        return totalTaxes;
    }
    
    public double getTaxesTotal(TaxCode taxCode) {
        Double totalTaxes = 0.0;
        for (Tax tax : _taxes) {
            if(tax.getTaxCode().equals(taxCode)){
                totalTaxes =+ tax.getTaxTotal();
            }
        }
        return totalTaxes;
    }
    
    public List<Tax> getTaxes() {
        return _taxes;
    }
    
    public bool isCharge(){
        return (_allowanceOrChargeType == AllowanceOrChargeType.CHARGE);
    }
    
    public bool isAllowance(){
        return (_allowanceOrChargeType == AllowanceOrChargeType.ALLOWANCE);
    }
    
    public static ReceiptAllowanceCharge Tip(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.CHARGE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
        receiptAllowanceCharge._settlementType = SettlementType.TIP;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge Tip(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.Tip(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge DeliveryFee(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.CHARGE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
        receiptAllowanceCharge._settlementType = SettlementType.DeliveryFee;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge DeliveryFee(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.DeliveryFee(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }

    public static ReceiptAllowanceCharge FreightFee(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.CHARGE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
        receiptAllowanceCharge._settlementType = SettlementType.FreightFee;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge FreightFee(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.FreightFee(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge PackagingFee(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.CHARGE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
        receiptAllowanceCharge._settlementType = SettlementType.PackagingFee;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge PackagingFee(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.PackagingFee(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge ProcessingFee(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.CHARGE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
        receiptAllowanceCharge._settlementType = SettlementType.PackagingFee;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge ProcessingFee(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.ProcessingFee(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge BookingFee(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.CHARGE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
        receiptAllowanceCharge._settlementType = SettlementType.BookingFee;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge BookingFee(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.BookingFee(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge GeneralDiscount(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.ALLOWANCE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CREDIT_CUSTOMER_ACCOUNT;
        receiptAllowanceCharge._settlementType = SettlementType.GeneralDiscount;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge GeneralDiscount(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.GeneralDiscount(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge MultiBuyDiscount(double amount, string description){
        ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
        receiptAllowanceCharge._amount = amount;
        receiptAllowanceCharge._description = description;
        receiptAllowanceCharge._allowanceOrChargeType = AllowanceOrChargeType.ALLOWANCE;
        receiptAllowanceCharge._allowanceChargeType = AllowanceChargeType.CREDIT_CUSTOMER_ACCOUNT;
        receiptAllowanceCharge._settlementType = SettlementType.MultiBuyDiscount;
        return receiptAllowanceCharge;
    }
    
    public static ReceiptAllowanceCharge MultiBuyDiscount(double amount, string description, Tax tax){
        ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.MultiBuyDiscount(amount, description);
        receiptAllowanceCharge._taxes.add(tax);
        return receiptAllowanceCharge;
    }
}
