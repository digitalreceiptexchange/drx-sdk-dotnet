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
import net.dreceiptx.receipt.common.DespatchInformation;
import net.dreceiptx.receipt.common.LocationInformation;
import net.dreceiptx.receipt.ecom.AVP;
import net.dreceiptx.receipt.ecom.AVPList;
import net.dreceiptx.receipt.tax.Tax;
import net.dreceiptx.receipt.tax.TaxCode;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public abstract class LineItem {
    protected transient int _lineItemId;
    protected transient int _quantity;
    protected transient double _price;
    protected transient boolean _creditLineIndicator = false;
    protected transient final List<Tax> _taxes = new ArrayList<Tax>();
    protected transient final List<ReceiptAllowanceCharge> _receiptAllowanceCharges = new ArrayList<ReceiptAllowanceCharge>();
    protected transient DespatchInformation _despatchInformation = new DespatchInformation();
    protected transient TradeItemIdentification _tradeItemIdentification = new TradeItemIdentification();
    protected transient TradeItemDescriptionInformation _tradeItemDescriptionInformation = null;
    protected transient AVPList _AVPList = new AVPList();
    protected transient TransactionalTradeItemType _transactionalTradeItemType = null;
    protected transient String _transactionalTradeItemCode = null;
    protected transient String _serialNumber = null;
    protected transient String _batchNumber = null;
    protected transient String _billingCostCentre = null;
    protected transient LocationInformation _origin = new LocationInformation();
    protected transient LocationInformation _destination = new LocationInformation();
    
    public static final String LineItemTypeIdentifier = "DRX_LINEITEM_TYPE";

    protected LineItem(){

    }

    public LineItem(String brand, String name, String description, int quantity, double price) {
        _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
        _tradeItemDescriptionInformation = new TradeItemDescriptionInformation(brand,name,description);
        _quantity = quantity;
        _price = price;
    }

    public LineItem(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) {
        _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
        _tradeItemDescriptionInformation = tradeItemDescriptionInformation;
        _quantity = quantity;
        _price = price;
    }

    public LineItem(TransactionalTradeItemType transactionalTradeItemType, String transactionalTradeItemCode, int quantity, double price) {
        _transactionalTradeItemType = transactionalTradeItemType;
        _transactionalTradeItemCode = transactionalTradeItemCode;
        _quantity = quantity;
        _price = price;
    }

    public String getBrandName() {
        return _tradeItemDescriptionInformation.getBrandName();
    }

    public String getName() {
        return _tradeItemDescriptionInformation.getDescriptionShort();
    }

    public String getDescription() {
        return _tradeItemDescriptionInformation.getTradeItemDescription();
    }

    public void setLineItemId(Integer lineItemId) {
        _lineItemId = lineItemId;
    }

    public Integer getLineItemId() {
        return _lineItemId;
    }

    public void setReturnOrExchange() {
        _creditLineIndicator = true;
    }

    public boolean isReturnOrExchange(){
        return _creditLineIndicator;
    }

    public void addReceiptAllowanceCharges(ReceiptAllowanceCharge receiptAllowanceCharge) {
        _receiptAllowanceCharges.add(receiptAllowanceCharge);
    }

    public void setTradeItemDescriptionInformation(String brand, String name, String description){
        if(_tradeItemDescriptionInformation != null){
            _tradeItemDescriptionInformation.setBrandName(brand);
            _tradeItemDescriptionInformation.setDescriptionShort(name);
            _tradeItemDescriptionInformation.setTradeItemDescription(description);
        }else{
            _tradeItemDescriptionInformation = new TradeItemDescriptionInformation(brand,name,description);
        } 
    }

    public void setTradeItemDescriptionInformation(TradeItemDescriptionInformation tradeItemDescriptionInformation){
        _tradeItemDescriptionInformation = tradeItemDescriptionInformation;
    }

    protected void setTradeItemGroupIdentificationCode(String code){
        if(_tradeItemDescriptionInformation != null){
            _tradeItemDescriptionInformation.setTradeItemGroupIdentificationCode(code);
        }
    }

    protected <T extends Enum<T> & LineItemTypeDescription> LineItemTypeDescription getLineItemType(Class<T> lineItemTypeDescription, LineItemTypeDescription defaultValue){
        if(_tradeItemDescriptionInformation != null){
            for (T lineItemTypeDescriptionEnum : lineItemTypeDescription.getEnumConstants()) {
                if(lineItemTypeDescriptionEnum.code().equals(_tradeItemDescriptionInformation.getTradeItemGroupIdentificationCode())){
                    return lineItemTypeDescriptionEnum;
                }
            }
        }

        return defaultValue;
    }

    public TradeItemDescriptionInformation getTradeItemDescriptionInformation(){
        return _tradeItemDescriptionInformation;
    }

    public void setTransactionalTradeItemType(TransactionalTradeItemType transactionalTradeItemType, String transactionalTradeItemCode){
        _transactionalTradeItemType = transactionalTradeItemType;
        _transactionalTradeItemCode = transactionalTradeItemCode;
    }

    public TransactionalTradeItemType getTransactionalTradeItemType(){
        return _transactionalTradeItemType;
    }

    public String getTransactionalTradeItemCode(){
        return _transactionalTradeItemCode;
    }

    public void setTradeItemIdentification(TradeItemIdentification tradeItemIdentification){
        _tradeItemIdentification = tradeItemIdentification;
    }

    public TradeItemIdentification getTradeItemIdentification(){
        return _tradeItemIdentification;
    }

    public void addTradeItemIdentification(String code, String value){
        _tradeItemIdentification.add(code, value);
    }

    public boolean hasTradeItemIdentificationValue(String code){
        return _tradeItemIdentification.has(code);
    }

    public String getTradeItemIdentificationValue(String code){
        if(_tradeItemIdentification.has(code)){
            return _tradeItemIdentification.get(code);
        }

        return null;
    }

    public AVPList getEcomAVPList(){
        return this._AVPList;
    }

    public void addEcomAVP(AVP avp){
        this._AVPList.add(avp);
    }

    public void setSerialNumber(String serialNumber){
        _serialNumber = serialNumber;
    }

    public String getSerialNumber(){
        return _serialNumber;
    }

    public void setBatchNumber(String batchNumber){
        _batchNumber = batchNumber;
    }

    public String getBatchNumber(){
        return _batchNumber;
    }

    public void setBillingCostCentre(String billingCostCentre){
        _billingCostCentre = billingCostCentre;
    }

    public String getBillingCostCentre(){
        return _billingCostCentre;
    }

    public void setDespatchDate(Date despatchDate){
        _despatchInformation.setDespatchDate(despatchDate);
    }

    public Date getDespatchDate(){
        return _despatchInformation.getDespatchDate();
    }

    public void setDeliveryDate(Date deliveryDate){
        _despatchInformation.setDeliveryDate(deliveryDate);
    }

    public Date getDeliveryDate(){
        return _despatchInformation.getDeliveryDate();
    }

    public void setDeliveryInstructions(String deliveryInstructions){
        _despatchInformation.setInstructions(deliveryInstructions);
    }

    public String getDeliveryInstructions(){
        return _despatchInformation.getDeliveryInstructions();
    }

    public void setDespatchInformation(DespatchInformation despatchInformation){
        _despatchInformation = despatchInformation;
    }

    public DespatchInformation getDespatchInformation(){
        return _despatchInformation;
    }

    public void setOriginInformation(LocationInformation originInformation){
        _origin = originInformation;
    }

    public LocationInformation getOriginInformation(){
        return _origin;
    }

    public void setDestinationInformation(LocationInformation destinationInformation){
        _destination = destinationInformation;
    }

    public LocationInformation getDestinationInformation(){
        return _destination;
    }

    public double getSubTotal() {
        double total = this._price*this._quantity;
        return total;
    }
    
    public double getNetTotal() {
        double total = this._price*this._quantity;
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _receiptAllowanceCharges) {
            total += receiptAllowanceCharge.getNetTotal();
        }
        return total;
    }
    
    public double getTotal() {
        double total = this._price*this._quantity;
        for (Tax tax : _taxes) {
            total += tax.getTaxTotal();
        }
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _receiptAllowanceCharges) {
            total += receiptAllowanceCharge.getTotal();
        }
        return total;
    }

    public double getTaxesTotal() {
        double total = 0;
        for (Tax tax : _taxes) {
            total += tax.getTaxTotal();
        }
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _receiptAllowanceCharges) {
            total += receiptAllowanceCharge.getTaxesTotal();
        }
        return total;
    }
    
    public double getTaxesTotal(TaxCode taxCode) {
        double total = 0;
        for (Tax tax : _taxes) {
            if(tax.getTaxCode().equals(taxCode)){
                total += tax.getTaxTotal();
            }
        }
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _receiptAllowanceCharges) {
            total += receiptAllowanceCharge.getTaxesTotal(taxCode);
        }
        return total;
    }

    public double getAllowancesTotal() {
        double total = 0;
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _receiptAllowanceCharges) {
            total += receiptAllowanceCharge.getNetTotal();
        }
        return total;
    }
    
    public List<ReceiptAllowanceCharge>  getReceiptAllowanceCharges() {
        return _receiptAllowanceCharges;
    }

    public void addTax(Tax tax) {
        _taxes.add(tax);
    }
    
    public List<Tax> getTaxes() {
        return _taxes;
    }

    public long getQuantity() {
        return _quantity;
    }

    public void setQuantity(int quantity) {
        _quantity = quantity;
    }

    public double getPrice() {
        return _price;
    }

    public void setPrice(double price) {
        _price = price;
    }
    
    public boolean hasTaxes(){
        return !_taxes.isEmpty();
    }
}