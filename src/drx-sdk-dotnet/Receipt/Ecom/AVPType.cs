package net.dreceiptx.receipt.ecom;

public enum AVPType {
    DRIVER_NAME("DRIVER_NAME"),
    PASSENGER_NAME("PASSENGER_NAME"),
    TRIP_DISTANCE("TRIP_DISTANCE"),
    VEHICLE_IDENTIFIER("VEHICLE_IDENTIFIER");

    private String code;

    AVPType(String code)
    {
        this.code = code;
    }

    public String Code(){
        return this.code;
    }

}
